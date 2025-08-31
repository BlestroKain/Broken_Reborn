using System.Runtime.InteropServices;

namespace Intersect.Client.MonoGame.NativeInterop;

public partial class Sdl2
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private unsafe delegate int SDL_GetDisplayDPI_d(int displayIndex, float* ddpi, float* hdpi, float* vdpi);

    private static SDL_GetDisplayDPI_d? SDL_GetDisplayDPI_f =
        Loader.Functions.LoadFunction<SDL_GetDisplayDPI_d>(nameof(SDL_GetDisplayDPI_f));

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
            return SDL_GetDisplayDPI_f(displayIndex, ddpiPtr, hdpiPtr, vdpiPtr) == 0;
        }
    }

    public static float GetDisplayDpi(int displayIndex = 0)
    {
        return TryGetDisplayDpi(displayIndex, out var ddpi, out var hdpi, out var vdpi)
            ? (ddpi > 0 ? ddpi : (hdpi + vdpi) / 2f)
            : 96f;
    }
}
