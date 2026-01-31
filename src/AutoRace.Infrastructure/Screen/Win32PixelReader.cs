using System;
using AutoRace.Core.Screen;
using AutoRace.Infrastructure.Win32;

namespace AutoRace.Infrastructure.Screen;

public sealed class Win32PixelReader : IPixelReader
{
    public Win32PixelReader()
    {
        if (!OperatingSystem.IsWindows())
        {
            throw new PlatformNotSupportedException("Win32 pixel reading is only supported on Windows.");
        }
    }

    public Rgb24 GetColorAt(int x, int y)
    {
        var hdc = Win32Api.GetDC(IntPtr.Zero);
        if (hdc == IntPtr.Zero)
        {
            throw new InvalidOperationException("GetDC returned a null handle.");
        }

        try
        {
            var colorRef = Win32Api.GetPixel(hdc, x, y);
            if (colorRef == 0xFFFFFFFF)
            {
                throw new InvalidOperationException("GetPixel failed.");
            }

            var r = (byte)(colorRef & 0xFF);
            var g = (byte)((colorRef >> 8) & 0xFF);
            var b = (byte)((colorRef >> 16) & 0xFF);
            return new Rgb24(r, g, b);
        }
        finally
        {
            _ = Win32Api.ReleaseDC(IntPtr.Zero, hdc);
        }
    }
}
