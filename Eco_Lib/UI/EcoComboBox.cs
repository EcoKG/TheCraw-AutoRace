using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eco_Skin
{
    public class EcoComboBox : ComboBox
    {
        public EcoComboBox()
        {
            SetStyle(ControlStyles.UserPaint, true);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.SetStyle(ControlStyles.UserPaint,true);
            Graphics g = e.Graphics;
            g.Clear(Color.Red);
        }
    }
}
