using Eco_Lib.WinApi;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Eco_Lib.PixelHelper
{
    public class ImageSearch
    {
        private static ImageSearch instance = null;
        private ImageSearch()
        {

        }
        public static ImageSearch GetInstanc()
        {
            return instance ?? (instance = new ImageSearch());
        }
        public object[] ImgSearch2(int x1, int y1, int x2, int y2, string path, int error, string winanme = "")
        {
            int srcstride, tapstride;
            bool sucess = false;
            IntPtr hdc, ghdc, c;
            BitmapData srcData, tapData;
            byte[] srcrgb;
            byte[] taprgb;
            object[] result = new object[3];
            c = winanme == "" ? IntPtr.Zero : WinApis.FindWindow(null, winanme);

            int w = x1 == x2 ? x2 - 1 : x2 - x1;
            int h = y1 == y2 ? y2 - 1 : y2 - y1;

            Bitmap srcBitmap = new Bitmap(w, h);
            Bitmap tap = new Bitmap(path);

            int tbw = tap.Width;
            int tbh = tap.Height;

            Rectangle rec1 = new Rectangle(0, 0, w, h);
            Rectangle rec2 = new Rectangle(0, 0, tap.Width, tap.Height);
            try
            {
                using (Graphics g = Graphics.FromImage(srcBitmap))
                {
                    using (Graphics gdata = Graphics.FromHwnd(c))
                    {
                        hdc = g.GetHdc();
                        ghdc = gdata.GetHdc();
                        WinApis.BitBlt(hdc, 0, 0, w, h, ghdc, x1, y1, (int)CopyPixelOperation.SourceCopy);
                        g.ReleaseHdc(hdc);
                        gdata.ReleaseHdc(ghdc);

                    }
                }

                srcData = srcBitmap.LockBits(rec1, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                tapData = tap.LockBits(rec2, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

                srcstride = srcData.Stride;
                tapstride = tapData.Stride;

                srcrgb = new byte[srcstride * srcBitmap.Height];
                taprgb = new byte[tapstride * tap.Height];

                Marshal.Copy(srcData.Scan0, srcrgb, 0, srcstride * srcBitmap.Height);
                Marshal.Copy(tapData.Scan0, taprgb, 0, tapstride * tap.Height);

                srcBitmap.UnlockBits(srcData);
                tap.UnlockBits(tapData);

                srcBitmap.Dispose();
                tap.Dispose();

                for (int sh = 0; sh < h - tbh - 1; sh++)
                {
                    for (int sw = 0; sw < w - tbw - 1; sw++)
                    {
                        if ((Math.Abs(srcrgb[srcstride * sh + sw * 3] - taprgb[0]) < error) && (Math.Abs(srcrgb[srcstride * sh + sw * 3 + 1] - taprgb[1]) < error) && (Math.Abs(srcrgb[srcstride * sh + sw * 3 + 2] - taprgb[2]) < error))
                        {
                            for (int th = 0; th < tbh - 1; th++)
                            {
                                for (int tw = 0; tw < tbw - 1; tw++)
                                {
                                    if ((Math.Abs(srcrgb[srcstride * (sh + th) + (sw + tw) * 3] - taprgb[tapstride * th + tw * 3]) < error) && (Math.Abs(srcrgb[srcstride * (sh + th) + (sw + tw) * 3 + 1] - taprgb[tapstride * th + tw * 3 + 1]) < error) && (Math.Abs(srcrgb[srcstride * (sh + th) + (sw + tw) * 3 + 2] - taprgb[tapstride * th + tw * 3 + 2]) < error))
                                    {
                                        sucess = true;
                                    }
                                    else
                                    {
                                        sucess = false;
                                        break;
                                    }
                                }
                                if (!sucess)
                                    break;
                            }
                            if (sucess)
                            {
                                result = new object[] {sucess,sw,sh };
                                return result;
                            }
                        }
                    }
                }
                return new object[] { false, 0, 0 };
            }
            catch
            {
                return new object[] { "error", 0, 0 };
            }
            finally
            {

            }
        }
    }
}