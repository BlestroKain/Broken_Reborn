using System;

namespace Intersect.Framework.Core.Collections
{
    public class BitGrid
    {
        private readonly int _width;
        private readonly int _height;
        private readonly byte[] _data;

        public BitGrid(int width, int height, byte[]? data = null)
        {
            _width = width;
            _height = height;
            var size = (width * height + 7) / 8;
            _data = data ?? new byte[size];
        }

        public byte[] Data => _data;

        public void Set(int x, int y)
        {
            if (x < 0 || y < 0 || x >= _width || y >= _height)
            {
                return;
            }

            var index = y * _width + x;
            var byteIndex = index / 8;
            var bitIndex = index % 8;
            _data[byteIndex] |= (byte)(1 << bitIndex);
        }

        public bool Get(int x, int y)
        {
            if (x < 0 || y < 0 || x >= _width || y >= _height)
            {
                return false;
            }

            var index = y * _width + x;
            var byteIndex = index / 8;
            var bitIndex = index % 8;
            return (_data[byteIndex] & (1 << bitIndex)) != 0;
        }
    }
}
