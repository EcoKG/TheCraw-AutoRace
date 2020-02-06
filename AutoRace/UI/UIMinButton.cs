
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibAutoHotkeyScriptManager.UI
{
    public class UIMinButton : Button
    {
        public UIMinButton()
        {
            BackColor = Color.White;
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            pevent.Graphics.Clear(this.BackColor);
            pevent.Graphics.DrawImage(AutoRace.Properties.Resources.icons8_close_window_100, new Rectangle(0, 0, this.Height, this.Height));
            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
            TextRenderer.DrawText(pevent.Graphics, Text, Font, new Point(Width + 3, Height / 2), ForeColor, flags);
        }

    }
}
