#nullable enable
using System;
using System.Runtime.InteropServices;

namespace AutoRace.Infrastructure.Win32;

/// <summary>
/// Win32 interop declarations used by AutoRace infrastructure.
/// </summary>
public static class Win32Api
{
    /// <summary>
    /// Finds a window by class name and window title.
    /// </summary>
    [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern IntPtr FindWindow(string? className, string? windowName);

    /// <summary>
    /// Finds a window by its title only.
    /// </summary>
    public static IntPtr FindWindowByTitle(string? title) => FindWindow(null, title);

    /// <summary>
    /// Performs a bit-block transfer of the color data from a source device context to a destination device context.
    /// </summary>
    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
    public static extern int BitBlt(
        IntPtr hdc,
        int x,
        int y,
        int nWidth,
        int nHeight,
        IntPtr hdcSrc,
        int xSrc,
        int ySrc,
        int dwRop);

    /// <summary>
    /// Sends the specified message to a window or windows.
    /// </summary>
    [DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

    /// <summary>
    /// Determines whether a key is up or down at the time the function is called.
    /// </summary>
    [DllImport("user32.dll")]
    public static extern bool GetAsyncKeyState(int vKey);

    /// <summary>
    /// Releases the mouse capture from a window in the current thread.
    /// </summary>
    [DllImport("user32.dll")]
    public static extern bool ReleaseCapture();
}

/// <summary>
/// Raster operation codes for BitBlt.
/// </summary>
public enum RasterOperation : int
{
    /// <summary>
    /// Copies the source rectangle directly to the destination rectangle.
    /// </summary>
    SRCCOPY = 0x00CC0020,
}
