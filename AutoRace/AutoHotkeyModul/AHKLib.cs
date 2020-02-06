using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sharpAHK;
namespace AutoRace.AutoHotkeyModul
{
    public class AHKCMD
    {
        private static _AHK ahk ;
        private static AHKCMD instance = null;
        private AHKCMD()
        {
            ahk = new _AHK();
        }
        public static AHKCMD GetInstanc()
        {
            return instance == null ? instance = new AHKCMD() : instance;
        }
        public void SendkeyDown(string sendkey)
        {
            ahk.Send(@"{" + sendkey + " Down}");
        }
        public void SendKeyUP(string sendkey)
        {
            ahk.Send(@"{" + sendkey + " Up}");
        }
        public void Sleep(int mtime)
        {
            ahk.Sleep(mtime);
        }
        public void msgbox(string msg)
        {
            ahk.ToolTip(msg);
        }
    }
}
