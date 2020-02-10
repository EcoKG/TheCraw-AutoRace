using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LibAutoHotkeyScriptManager
{
    public class UICloseButton : Button
    {
        public UICloseButton()
        {
            BackColor = Color.Gray;
            this.Text = "";
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            pevent.Graphics.DrawImage(AutoRace.Properties.Resources.icons8_close_window_100, new Rectangle(0, 0, this.Height, this.Height));
            pevent.Graphics.FillRectangle(new SolidBrush(BackColor), 0, 0, this.Width, this.Height);
            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
            TextRenderer.DrawText(pevent.Graphics, Text, Font, new Point(Width + 3, Height / 2), ForeColor, flags);
        }

    }
}
