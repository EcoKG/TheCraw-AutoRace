using System;
using System.Runtime.InteropServices;

namespace Eco_Lib.WinApi
{
    public class WinApis
    {
        [DllImport("User32", EntryPoint = "FindWindow")] internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)] internal static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);
        [DllImport("user32.dll")] static extern int SendMessage(IntPtr hWnd, uint msg, int wParam, int lParam);
        [DllImport("user32")] public static extern bool GetAsyncKeyState(Int32 vKey);
        [DllImportAttribute("user32.dll")] public static extern bool ReleaseCapture();


        private static WinApis instance = null;
        public static WinApis GetInstance()
        {
            return instance == null ? instance = new WinApis() : instance;
        }

        private WinApis()
        {

        }

        public IntPtr WinWindowFinder(string ipClassName, string IpWindowName)
        {
            return FindWindow(ipClassName, ipClassName);
        }
        public int WinBitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop)
        {
            return BitBlt(hDC, x, y, nWidth, nHeight, hSrcDC, xSrc, ySrc, dwRop);
        }
        public bool WinGetAsyncKey(Int32 vkey)
        {
            return GetAsyncKeyState(vkey);
        }
        public bool WinReleaseCapture()
        {
            return ReleaseCapture();
        }
        public int WinSendMessage(IntPtr hWnd, uint msg, int wParam, int lParam)
        {
            return SendMessage(hWnd, msg, wParam, lParam);
        }

    }
}
