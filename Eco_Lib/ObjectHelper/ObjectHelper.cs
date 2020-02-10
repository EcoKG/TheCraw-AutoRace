using System.Text;
using System.Windows.Forms;

namespace Eco_Lib.ObjHelper
{
    public class ObjectHelper
    {
        private static ObjectHelper instance = null;

        private ObjectHelper()
        {

        }
        public static ObjectHelper GetInstance()
        {
            return instance == null ? instance = new ObjectHelper() : instance;
        }
        public void ControlSetText(Control control, params object[] value)
        {
            StringBuilder stringbuider = new StringBuilder();
            foreach (object text in value)
            {
                stringbuider.Append(text.ToString());
            }
            control.Text = stringbuider.ToString();
        }
        public string ObjectToString(object value)
        {
            return value.ToString();
        }
        public int StringToInteger(string text)
        {
            int result = 0;
            if (int.TryParse(text, out result))
            {
                return result;
            }
            return 0;
        }
        public string CreateString(params object[] value)
        {
            StringBuilder stringbuider = new StringBuilder();
            foreach (object text in value)
            {
                stringbuider.Append(text.ToString());
            }
            return stringbuider.ToString();
        }

    }
}
