using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Intersect.Framework.Core.Collections
{
    /// <summary>
    /// Grid binaria compacta (1 bit por celda). Incluye helpers para:
    /// - Set/Get/Unset/Toggle
    /// - Limpieza total/por rectángulo
    /// - Operaciones OR/AND/XOR entre grids compatibles
    /// - Conteo de bits encendidos
    /// - Enumeración de celdas encendidas
    /// - Detección de "dirty runs" (RLE por filas) contra otra BitGrid
    /// Mantiene compatibilidad con Data (byte[]).
    /// </summary>
    public sealed class BitGrid
    {
        private readonly int _width;
        private readonly int _height;
        private readonly byte[] _data;

        /// <summary>
        /// Ancho del grid (en celdas).
        /// </summary>
        public int Width => _width;

        /// <summary>
        /// Alto del grid (en celdas).
        /// </summary>
        public int Height => _height;

        /// <summary>
        /// Buffer subyacente (compacto) que almacena los bits.
        /// </summary>
        public byte[] Data => _data;

        /// <summary>
        /// Tamaño esperado del buffer para (width*height) bits.
        /// </summary>
        public static int GetExpectedSize(int width, int height) => ((width * height) + 7) / 8;

        /// <summary>
        /// Crea una BitGrid. Si se pasa <paramref name="data"/>, su longitud debe coincidir con el tamaño esperado.
        /// </summary>
        public BitGrid(int width, int height, byte[]? data = null)
        {
            if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width));
            if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height));

            _width = width;
            _height = height;

            var size = GetExpectedSize(width, height);
            if (data != null)
            {
                if (data.Length != size)
                    throw new ArgumentException($"Data length {data.Length} does not match expected size {size}.", nameof(data));

                _data = data;
            }
            else
            {
                _data = new byte[size];
            }
        }

        /// <summary>
        /// Clona profundamente esta BitGrid (incluye una copia del buffer).
        /// </summary>
        public BitGrid Clone()
        {
            var copy = new byte[_data.Length];
            Buffer.BlockCopy(_data, 0, copy, 0, _data.Length);
            return new BitGrid(_width, _height, copy);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool InBounds(int x, int y) => (uint)x < (uint)_width && (uint)y < (uint)_height;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int LinearIndex(int x, int y) => y * _width + x;

        /// <summary>
        /// Enciende el bit (x,y). Fuera de rango: no hace nada.
        /// </summary>
        public void Set(int x, int y)
        {
            if (!InBounds(x, y)) return;

            var index = LinearIndex(x, y);
            _data[index >> 3] |= (byte)(1 << (index & 7));
        }

        /// <summary>
        /// Apaga el bit (x,y). Fuera de rango: no hace nada.
        /// </summary>
        public void Unset(int x, int y)
        {
            if (!InBounds(x, y)) return;

            var index = LinearIndex(x, y);
            _data[index >> 3] &= (byte)~(1 << (index & 7));
        }

        /// <summary>
        /// Cambia el estado del bit (x,y). Fuera de rango: no hace nada.
        /// </summary>
        public void Toggle(int x, int y)
        {
            if (!InBounds(x, y)) return;

            var index = LinearIndex(x, y);
            _data[index >> 3] ^= (byte)(1 << (index & 7));
        }

        /// <summary>
        /// Lee el bit (x,y). Fuera de rango: false.
        /// </summary>
        public bool Get(int x, int y)
        {
            if (!InBounds(x, y)) return false;

            var index = LinearIndex(x, y);
            return (_data[index >> 3] & (1 << (index & 7))) != 0;
        }

        /// <summary>
        /// Pone todos los bits a 0 o 1.
        /// </summary>
        public void SetAll(bool value)
        {
            Array.Fill(_data, (byte)(value ? 0xFF : 0x00));
            // Si el total de bits no es múltiplo de 8 y value=true, recorta el sobrante para no dejar bits "fuera de grid" en 1.
            if (value)
            {
                int totalBits = _width * _height;
                int remainder = totalBits & 7;
                if (remainder != 0)
                {
                    int lastIndex = _data.Length - 1;
                    int validMask = (1 << remainder) - 1; // ej: remainder=3 => 0b00000111
                    _data[lastIndex] &= (byte)validMask;
                }
            }
        }

        /// <summary>
        /// Limpia un rectángulo (apaga) dentro de los límites. Coords inclusivas.
        /// </summary>
        public void ClearRect(int x, int y, int w, int h)
        {
            if (w <= 0 || h <= 0) return;
            int x0 = Math.Max(0, x);
            int y0 = Math.Max(0, y);
            int x1 = Math.Min(_width - 1, x + w - 1);
            int y1 = Math.Min(_height - 1, y + h - 1);
            if (x0 > x1 || y0 > y1) return;

            for (int yy = y0; yy <= y1; yy++)
            {
                for (int xx = x0; xx <= x1; xx++)
                {
                    Unset(xx, yy);
                }
            }
        }

        /// <summary>
        /// Enciende un rectángulo dentro de los límites. Coords inclusivas.
        /// </summary>
        public void SetRect(int x, int y, int w, int h)
        {
            if (w <= 0 || h <= 0) return;
            int x0 = Math.Max(0, x);
            int y0 = Math.Max(0, y);
            int x1 = Math.Min(_width - 1, x + w - 1);
            int y1 = Math.Min(_height - 1, y + h - 1);
            if (x0 > x1 || y0 > y1) return;

            for (int yy = y0; yy <= y1; yy++)
            {
                for (int xx = x0; xx <= x1; xx++)
                {
                    Set(xx, yy);
                }
            }
        }

        /// <summary>
        /// OR-in-place con otra BitGrid del mismo tamaño.
        /// </summary>
        public void OrWith(BitGrid other)
        {
            EnsureSameShape(other);
            var bufA = _data;
            var bufB = other._data;
            for (int i = 0; i < bufA.Length; i++) bufA[i] |= bufB[i];
        }

        /// <summary>
        /// AND-in-place con otra BitGrid del mismo tamaño.
        /// </summary>
        public void AndWith(BitGrid other)
        {
            EnsureSameShape(other);
            var bufA = _data;
            var bufB = other._data;
            for (int i = 0; i < bufA.Length; i++) bufA[i] &= bufB[i];
        }

        /// <summary>
        /// XOR-in-place con otra BitGrid del mismo tamaño.
        /// </summary>
        public void XorWith(BitGrid other)
        {
            EnsureSameShape(other);
            var bufA = _data;
            var bufB = other._data;
            for (int i = 0; i < bufA.Length; i++) bufA[i] ^= bufB[i];
        }

        /// <summary>
        /// Devuelve el conteo de bits encendidos. (Popcount).
        /// </summary>
        public int CountSetBits()
        {
            int count = 0;
            for (int i = 0; i < _data.Length; i++)
            {
                count += PopCount(_data[i]);
            }
            // No hace falta recortar: bits "extra" fuera del grid solo pueden estar encendidos si los seteaste manualmente;
            // SetAll(true) ya los recorta.
            return count;
        }

        /// <summary>
        /// Itera todas las celdas encendidas devolviendo (x,y).
        /// </summary>
        public IEnumerable<(int x, int y)> EnumerateSetBits()
        {
            int total = _width * _height;
            for (int idx = 0; idx < total; idx++)
            {
                if ((_data[idx >> 3] & (1 << (idx & 7))) != 0)
                {
                    int y = idx / _width;
                    int x = idx - y * _width;
                    yield return (x, y);
                }
            }
        }

        /// <summary>
        /// Estructura de un "run" de celdas encendidas en una misma fila.
        /// </summary>
        public readonly struct Run
        {
            public readonly int Y;
            public readonly int X;      // inicio (inclusive)
            public readonly int Length; // longitud del run (>=1)

            public Run(int y, int x, int length)
            {
                Y = y;
                X = x;
                Length = length;
            }

            public override string ToString() => $"(Y={Y}, X={X}, Len={Length})";
        }

        /// <summary>
        /// Extrae los runs (RLE) de celdas encendidas, por filas. Útil para enviar "diffs".
        /// </summary>
        public List<Run> ExtractRuns()
        {
            var runs = new List<Run>();
            for (int y = 0; y < _height; y++)
            {
                int x = 0;
                while (x < _width)
                {
                    // Busca inicio de run (1)
                    while (x < _width && !Get(x, y)) x++;
                    if (x >= _width) break;

                    int start = x;
                    // Avanza hasta fin de run
                    while (x < _width && Get(x, y)) x++;
                    int len = x - start;
                    runs.Add(new Run(y, start, len));
                }
            }
            return runs;
        }

        /// <summary>
        /// Aplica runs encendidos (usa Additive/OR). Ignora runs fuera de rango.
        /// </summary>
        public void ApplyRuns(IEnumerable<Run> runs)
        {
            foreach (var run in runs)
            {
                if ((uint)run.Y >= (uint)_height || run.Length <= 0) continue;

                int x0 = Math.Max(0, run.X);
                int x1 = Math.Min(_width - 1, run.X + run.Length - 1);
                if (x0 > x1) continue;

                for (int x = x0; x <= x1; x++) Set(x, run.Y);
            }
        }

        /// <summary>
        /// Calcula los "dirty runs" entre esta grid y otra (XOR) y devuelve los runs encendidos del resultado.
        /// Sirve para mandar solo las diferencias.
        /// </summary>
        public List<Run> ExtractDirtyRuns(BitGrid other)
        {
            EnsureSameShape(other);
            // XOR de buffers para hallar diferencias:
            var tmp = new byte[_data.Length];
            for (int i = 0; i < tmp.Length; i++) tmp[i] = (byte)(_data[i] ^ other._data[i]);

            var runs = new List<Run>();
            // Recorremos por filas usando tmp como fuente
            for (int y = 0; y < _height; y++)
            {
                int rowStart = y * _width;
                int x = 0;
                while (x < _width)
                {
                    // Busca inicio de run en XOR (1)
                    while (x < _width && !GetFromBuffer(tmp, rowStart + x)) x++;
                    if (x >= _width) break;

                    int start = x;
                    while (x < _width && GetFromBuffer(tmp, rowStart + x)) x++;
                    int len = x - start;
                    runs.Add(new Run(y, start, len));
                }
            }
            return runs;
        }

        /// <summary>
        /// Copia el contenido de otra grid (mismo tamaño) sobre esta (overwrite).
        /// </summary>
        public void CopyFrom(BitGrid other)
        {
            EnsureSameShape(other);
            Buffer.BlockCopy(other._data, 0, _data, 0, _data.Length);
        }

        /// <summary>
        /// Compara igualdad estructural (mismo tamaño y mismo contenido).
        /// </summary>
        public bool ContentEquals(BitGrid other)
        {
            if (other is null) return false;
            if (_width != other._width || _height != other._height) return false;
            if (_data.Length != other._data.Length) return false;

            for (int i = 0; i < _data.Length; i++)
            {
                if (_data[i] != other._data[i]) return false;
            }
            return true;
        }

        private void EnsureSameShape(BitGrid other)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));
            if (_width != other._width || _height != other._height)
                throw new ArgumentException("BitGrid shapes differ.", nameof(other));
            if (_data.Length != other._data.Length)
                throw new ArgumentException("BitGrid buffer sizes differ.", nameof(other));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool GetFromBuffer(byte[] buffer, int linearIndex)
        {
            return (buffer[linearIndex >> 3] & (1 << (linearIndex & 7))) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int PopCount(byte b)
        {
            // Kernighan’s trick on byte
            int c = 0;
            while (b != 0)
            {
                b &= (byte)(b - 1);
                c++;
            }
            return c;
        }
    }
}
