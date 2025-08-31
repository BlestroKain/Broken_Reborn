using System;
using System.Runtime.InteropServices;

namespace Intersect.Client.MonoGame.NativeInterop
{
    public static partial class Sdl2
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private unsafe delegate int SDL_GetDisplayDPI_d(int displayIndex, float* ddpi, float* hdpi, float* vdpi);

        private static readonly SDL_GetDisplayDPI_d? SDL_GetDisplayDPI_f =
            Loader.Functions.LoadFunction<SDL_GetDisplayDPI_d>(nameof(SDL_GetDisplayDPI_f));

        /// <summary>
        /// Intenta obtener el DPI del display. Devuelve false si no está disponible.
        /// </summary>
        public static unsafe bool TryGetDisplayDpi(int displayIndex, out float ddpi, out float hdpi, out float vdpi)
        {
            ddpi = hdpi = vdpi = 0f;
            if (SDL_GetDisplayDPI_f == null)
            {
                return false;
            }

            fixed (float* ddpiPtr = &ddpi)
            fixed (float* hdpiPtr = &hdpi)
            fixed (float* vdpiPtr = &vdpi)
            {
                var rc = SDL_GetDisplayDPI_f(displayIndex, ddpiPtr, hdpiPtr, vdpiPtr);
                if (rc != 0)
                {
                    ddpi = hdpi = vdpi = 0f;
                    return false;
                }
            }

            return ddpi > 0 || (hdpi > 0 && vdpi > 0);
        }

        /// <summary>
        /// Devuelve un DPI razonable. Si no se puede consultar, 96f.
        /// </summary>
        public static float GetDisplayDpi(int displayIndex = 0)
        {
            if (TryGetDisplayDpi(displayIndex, out var ddpi, out var hdpi, out var vdpi))
            {
                // Preferimos ddpi si viene consistente, si no promedio de (hdpi, vdpi).
                var dpi = ddpi > 0 ? ddpi : (hdpi + vdpi) / 2f;
                // Clamp para evitar valores absurdos por drivers raros
                if (dpi < 48f) dpi = 48f;
                if (dpi > 480f) dpi = 480f;
                return dpi;
            }

            return 96f;
        }
    }
}
