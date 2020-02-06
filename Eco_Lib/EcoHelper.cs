using Eco_Lib.OpenCvLib;
using Eco_Lib.TextHelperLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eco_Lib
{
    public class EcoHelper
    {
        private TextHelper _textHelper = null;
        private ImageSearch _imageSearch = null;
        private PixelGetColor _pixelGetColor = null;

        private static EcoHelper instance = null;
        private EcoHelper()
        {
            _textHelper = TextHelper.GetInstance();
            _imageSearch = ImageSearch.GetInstanc();
            _pixelGetColor = PixelGetColor.GetInstance();
        }
        public static EcoHelper GetInstance()
        {
            return instance == null ? new EcoHelper() : instance;
        }

        public string ImgSearch(int x1,int y1,int x2,int y2,string path,float error,string winname = "")
        {
            return _imageSearch.ImgSearch(x1, y1, x2, y2, path,error, winname);
        }
        public bool ImgSearch2(int x1,int y1,int x2,int y2,string path,int error,string winname = "")
        {
            return _imageSearch.ImgSearch2(x1, y1, x2, y2, path, error, winname);
        }
        public string GetColor(int x,int y)
        {
            return _pixelGetColor.GetColorAt(x,y);
        }
        public void SetCtlText(Control control,params string[] text)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in text)
            {
                sb.Append(s);
            }
            _textHelper.SetControlText(control, sb.ToString());
        }
        public string ObjToString(object value)
        {
            return _textHelper.ObjToString(value);
        }
        public int ObjToInt(object value)
        {
            return _textHelper.ObjToInt(value);
        }






    }
}
