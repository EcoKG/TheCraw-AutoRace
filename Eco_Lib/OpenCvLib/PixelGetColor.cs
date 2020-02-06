using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;

namespace Eco_Lib.OpenCvLib
{
    public class PixelGetColor
    {
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);
        private static PixelGetColor instance = null;
        private PixelGetColor()
        {

        }
        public static PixelGetColor GetInstance()
        {
            return instance == null ? new PixelGetColor() : instance;
        }
        public String GetColorAt(int pointx, int pointy)
        {
            Bitmap screenPixel = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
            System.Drawing.Point location = new System.Drawing.Point(pointx, pointy);
            using (Graphics gdest = Graphics.FromImage(screenPixel))
            {
                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
                {
                    BitBlt(gdest.GetHdc(), 0, 0, 1, 1, gsrc.GetHdc(), location.X, location.Y, (int)CopyPixelOperation.SourceCopy);
                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }
            }
            Color c = screenPixel.GetPixel(0, 0);
            screenPixel.Dispose();
            GC.Collect();
            return "0x" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }
    }
}
