using Eco_Lib.ObjHelper;
using Eco_Lib.PixelHelper;
using Eco_Lib.WinApi;
using System;
using System.Windows.Forms;
using static InputKeyLib;

namespace Eco_Lib
{
    public class EcoLib
    {
        private readonly PixelGetColor _PixelGetColor = null;
        private readonly ImageSearch _Imagesearch = null;
        private readonly ObjectHelper _ObjectHelper = null;
        private readonly InputKeyLib _InputKeyLib = null;
        private readonly WinApis _WinApi = null;
        private static EcoLib Instance = null;
        private EcoLib()
        {
            _PixelGetColor = PixelGetColor.GetInstance();
            _Imagesearch = ImageSearch.GetInstanc();
            _ObjectHelper = ObjectHelper.GetInstance();
            _InputKeyLib = InputKeyLib.GetInstance();
            _WinApi = WinApis.GetInstance();
        }
        public static EcoLib GetInstance()
        {
            return Instance ?? (Instance = new EcoLib());
        }
        #region "ObjectHelper"
        public void CtlSetTxt(Control control, params object[] value)
        {
            _ObjectHelper.ControlSetText(control, value);
        }
        public int StrToInt(string text)
        {
            return _ObjectHelper.StringToInteger(text);
        }
        public string ObjToStr(object value)
        {
            return _ObjectHelper.ObjectToString(value);
        }
        public string CreateString(params object[] value)
        {
            return _ObjectHelper.CreateString(value);
        }
        #endregion

        #region "PixelHelper"
        /*public string CvImgSearch(int x1,int y1,int x2,int y2,string path,float error,string winname = "")
        {
            return _Imagesearch.ImgSearch(x1, y1, x2, y2, path, error, winname);
        }*/
        public bool ImgSearch(int x1, int y1, int x2, int y2, string path, int error, string winname = "")
        {
            return _Imagesearch.ImgSearch2(x1, y1, x2, y2, path, error, winname);
        }
        public string GetColor(int x, int y)
        {
            return _PixelGetColor.GetColorAt(x, y);
        }
        #endregion

        #region "WinApi"
        public IntPtr FindWindow(string ipClassName, string IpWindowName)
        {
            return _WinApi.WinWindowFinder(ipClassName, IpWindowName);
        }
        public int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop)
        {
            return _WinApi.WinBitBlt(hDC, x, y, nWidth, nHeight, hSrcDC, xSrc, ySrc, dwRop);
        }
        public bool GetAsyncKeyState(Int32 vkey)
        {
            return _WinApi.WinGetAsyncKey(vkey);
        }
        public bool ReleaseCapture()
        {
            return _WinApi.WinReleaseCapture();
        }
        public int SendMessage(IntPtr hWnd, uint msg, int wParam, int lParam)
        {
            return _WinApi.WinSendMessage(hWnd, msg, wParam, lParam);
        }
        #endregion

        #region "Keyboard"
        public void Send(ScanCodeShort a)
        {
            _InputKeyLib.Send(a);
        }
        public void SendDown(ScanCodeShort a, KEYEVENTF keyType)
        {
            if (keyType == KEYEVENTF.SCANCODE)
                _InputKeyLib.VSendDown(a);
            else if (keyType == KEYEVENTF.EXTENDEDKEY)
                _InputKeyLib.SendDown(a);
        }
        public void SendUp(ScanCodeShort a, KEYEVENTF keyType)
        {
            if (keyType == KEYEVENTF.SCANCODE)
                _InputKeyLib.VSendUp(a);
            else if (keyType == KEYEVENTF.EXTENDEDKEY)
                _InputKeyLib.SendUp(a);
        }

        #endregion

        #region "Delay"
        public DateTime Delay(int ms)
        {
            DateTime thismoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, ms);
            DateTime afterWards = thismoment.Add(duration);
            while(afterWards >= thismoment)
            {
                Application.DoEvents();
                thismoment = DateTime.Now;
            }
            return DateTime.Now;
        }
        #endregion
    }
}
