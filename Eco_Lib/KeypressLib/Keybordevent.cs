using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eco_Lib.KeypressLib
{
    public class Keybordevent
    {
        [DllImport("user32.dll")]
        internal static extern void keybd_event(uint vk, uint scan, uint flags, uint extraInfo);
        [DllImport("user32.dll")]
        internal static extern uint MapVirtualKey(int wCode, int wMapType);
        private static Keybordevent instance = null;
        private Keybordevent()
        {

        }
        public static Keybordevent GetInstance()
        {
            return instance == null ? new Keybordevent() : instance;
        }
        public void SendkeyDown(uint key)
        {
            keybd_event(key, 0, 0, 0);
        }
        public void SendKeyUP(uint key)
        {
            keybd_event(key, 0, 0x0002, 0);
        }

    }
}
