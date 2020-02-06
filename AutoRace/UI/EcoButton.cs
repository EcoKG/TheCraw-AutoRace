using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace EcoLib.UI
{
    public partial class EcoButton : Button
    {
        private bool _isHovering;
        public EcoButton()
        {
            BackColor = Color.White;
            DoubleBuffered = true;
            MouseEnter += (sender, e) =>
            {
                _isHovering = true;
                Invalidate();
            };
            MouseLeave += (sender, e) =>
            {
                _isHovering = false;
                Invalidate();
            };
            Invalidate();
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            Color ab = this.BackColor;
            if (this.Enabled == false)
            {

                pevent.Graphics.Clear(Color.FromArgb(47, 49, 45));
            }
            else
            {
                pevent.Graphics.Clear(_isHovering ? Color.FromArgb(ab.R - 20, ab.G - 20, ab.B - 20) : this.BackColor);
            }
            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
            TextRenderer.DrawText(pevent.Graphics, Text, Font, new Point(Width + 3, Height / 2), ForeColor, flags);
        }
    }
}
