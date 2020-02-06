using OpenCvSharp;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Eco_Lib.OpenCvLib
{
    public class ImageSearch
    {
        [DllImport("User32", EntryPoint = "FindWindow")]
        internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        internal static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        internal static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);
        private static ImageSearch instance = null;
        private ImageSearch()
        {

        }
        public static ImageSearch GetInstanc()
        {
            return instance == null ? instance = new ImageSearch() : instance;
        }
        public string ImgSearch(int x1, int y1, int x2, int y2, string path, float error, string winanme = "")
        {
            Bitmap srcBitmap = null, tap = null, source = null;
            IntPtr hdc, ghdc, c;
            int w, h;
            Graphics g = null, gdata = null;
            Mat ScreenMat, FindMat, result;
            try
            {

                c = winanme == "" ? IntPtr.Zero : FindWindow(null, winanme);
                w = x1 == x2 ? 1 : x2 - x1;
                h = y1 == y2 ? 1 : y2 - y1;
                srcBitmap = new Bitmap(w, h);
                tap = new Bitmap(path);
                using (g = Graphics.FromImage(srcBitmap))
                {
                    using (gdata = Graphics.FromHwnd(c))
                    {
                        hdc = g.GetHdc();
                        ghdc = gdata.GetHdc();
                        BitBlt(hdc, 0, 0, w, h, ghdc, x1, y1, (int)CopyPixelOperation.SourceCopy);
                        source = srcBitmap.Clone(new Rectangle(0, 0, w, h), tap.PixelFormat);
                    }
                }
                using (ScreenMat = OpenCvSharp.Extensions.BitmapConverter.ToMat(source))
                using (FindMat = OpenCvSharp.Extensions.BitmapConverter.ToMat(tap))
                using (result = new Mat())
                {
                    double minval, maxval = 0;
                    OpenCvSharp.Point minloc, maxloc;
                    Cv2.MatchTemplate(ScreenMat, FindMat, result, TemplateMatchModes.CCoeffNormed);
                    Cv2.MinMaxLoc(result, out minval, out maxval, out minloc, out maxloc);
                    if (maxval >= error)
                    {
                        return "0";
                    }
                    else
                    {
                        MessageBox.Show(maxval.ToString() + "오차값");
                        return "1";
                    }
                }
            }
            catch (Exception e)
            {
                //source.Dispose();
                //srcBitmap.Dispose();
                //tap.Dispose();
                return e.Message == "_img.size().height <= _templ.size().height && _img.size().width <= _templ.size().width" ? "지정한 화면보다 이미지가 더 큽니다" : e.Message;
            }
            finally
            {
                srcBitmap.Dispose();
                tap.Dispose();
                source.Dispose();
                g.Dispose();
                gdata.Dispose();
                GC.Collect();
            }
        }

















        public bool ImgSearch2(int x1, int y1, int x2, int y2, string path, int error, string winanme = "")
        {
            Bitmap srcBitmap = null, tap = null, source = null;
            IntPtr hdc, ghdc, c;
            int w, h;
            Graphics g = null, gdata = null;
            bool sucess = false;
            Color fscolor;
            Color ftcolor;
            Color scolor;
            Color tcolor;
            try
            {
                //StreamWriter Sw = new StreamWriter(DateTime.Now.ToString(@"yyyy-MM-dd") + "[LOG].txt", true);
                c = winanme == "" ? IntPtr.Zero : FindWindow(null, winanme);
                w = x1 == x2 ? 1 : x2 - x1 - 1;
                h = y1 == y2 ? 1 : y2 - y1 - 1;
                srcBitmap = new Bitmap(w, h);
                tap = new Bitmap(path);
                using (g = Graphics.FromImage(srcBitmap))
                {
                    using (gdata = Graphics.FromHwnd(c))
                    {
                        hdc = g.GetHdc();
                        ghdc = gdata.GetHdc();
                        BitBlt(hdc, 0, 0, w, h, ghdc, x1, y1, (int)CopyPixelOperation.SourceCopy);
                        g.ReleaseHdc();
                        gdata.ReleaseHdc();
                        source = srcBitmap.Clone(new Rectangle(0, 0, w, h), tap.PixelFormat);
                        ftcolor = tap.GetPixel(0, 0);
                    }
                }
                for (int sh = 0; (source.Height - 1) - (tap.Height - 1) >= sh; sh++)
                {
                    for (int sw = 0; (source.Width - 1) - (tap.Width - 1) >= sw; sw++)
                    {
                        fscolor = source.GetPixel(sw, sh);
                        if ((ftcolor.R - error <= fscolor.R && fscolor.R <= ftcolor.R + error) &&
                            (ftcolor.G - error <= fscolor.G && fscolor.G <= ftcolor.G + error) &&
                            (ftcolor.B - error <= fscolor.B && fscolor.B <= ftcolor.B + error))
                        {
                            for (int th = 0; tap.Height - 1 >= th; th++)
                            {
                                for (int tw = 0; tap.Width - 1 >= tw; tw++)
                                {
                                    scolor = source.GetPixel(sw + tw, sh + th);
                                    tcolor = tap.GetPixel(tw, th);
                                    if ((scolor.R - error <= tcolor.R && tcolor.R <= scolor.R + error) &&
                                        (scolor.G - error <= tcolor.G && tcolor.G <= scolor.G + error) &&
                                        (scolor.B - error <= tcolor.B && tcolor.B <= scolor.B + error))
                                    {
                                        sucess = true;
                                        if (tap.Height - 1 == th && tw == tap.Width - 1)
                                        {
                                            return sucess;
                                        }
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
                        }
                    }
                }
                return sucess;
            }
            finally
            {
                srcBitmap.Dispose();
                tap.Dispose();
                source.Dispose();
                g.Dispose();
                gdata.Dispose();
                GC.Collect();
            }
        }
    }
}