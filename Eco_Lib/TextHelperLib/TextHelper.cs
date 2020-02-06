using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eco_Lib.TextHelperLib
{
    public class TextHelper
    {
        private static TextHelper instance = null;
        private TextHelper()
        {
            
        }
        public static TextHelper GetInstance()
        {
            return instance == null ? new TextHelper() : instance;
        }
        public void SetControlText(Control control,string text)
        {
            control.Text = text;
        }
        public string ObjToString(object value)
        {
            return value.ToString();
        }
        public int ObjToInt(object value)
        {
            int result = 0;
            if (int.TryParse(value.GetType().ToString(),out result))
            {
                return result;
            }
            else
            {
                return 0;
            }
        }
    }
}
