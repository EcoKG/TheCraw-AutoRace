using Eco_Lib.WinApi;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Eco_Lib.PixelHelper
{
    public class PixelGetColor
    {
        private static PixelGetColor instance = null;
        private PixelGetColor()
        {

        }
        public static PixelGetColor GetInstance()
        {
            return instance ?? (instance = new PixelGetColor());
        }
        public String GetColorAt(int pointx, int pointy)
        {
            Rectangle rec1;
            int srcstride;
            IntPtr hdc, ghdc;
            BitmapData srcData;
            byte[] srcrgb;
            Bitmap srcBitmap = new Bitmap(1, 1);
            rec1 = new Rectangle(0, 0, 1, 1);
            using (Graphics g = Graphics.FromImage(srcBitmap))
            {
                using (Graphics gdata = Graphics.FromHwnd(IntPtr.Zero))
                {
                    hdc = g.GetHdc();
                    ghdc = gdata.GetHdc();
                    WinApis.BitBlt(hdc, 0, 0, 1, 1, ghdc, pointx, pointy, (int)CopyPixelOperation.SourceCopy);
                    g.ReleaseHdc(hdc);
                    gdata.ReleaseHdc(ghdc);

                }
            }
            srcData = srcBitmap.LockBits(rec1, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            srcstride = srcData.Stride;
            srcrgb = new byte[srcstride * srcBitmap.Height];
            Marshal.Copy(srcData.Scan0, srcrgb, 0, srcstride * srcBitmap.Height);
            srcBitmap.UnlockBits(srcData);
            srcBitmap.Dispose();
            return "0x" + srcrgb[2].ToString("X2") + srcrgb[1].ToString("X2") + srcrgb[0].ToString("X2");
        }
    }
}
