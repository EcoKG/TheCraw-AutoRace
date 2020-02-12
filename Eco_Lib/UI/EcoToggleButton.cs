using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eco_Skin
{
    public class EcoToggleButton : CheckBox
    {
        private int oldWidth;
        private int tbw = 30;
        public EcoToggleButton()
        {
            BackColor = Color.Gray;
            oldWidth = Size.Width;
            MouseEnter += (sender, e) =>
            {
                if (oldWidth < Size.Width)
                {
                    ToggleBoxWidth -= 1;
                    oldWidth = Size.Width;
                    Invalidate();
                }
                else if(oldWidth > Size.Width)
                {
                    ToggleBoxWidth += 1;
                    oldWidth = Size.Width;
                    Invalidate();
                }
            };
        }
        [Category("EcoSkin"), DisplayName("ToggleBoxWidth")]
        public int ToggleBoxWidth
        {
            get => tbw;
            set
            {
                tbw = value;
                Invalidate();
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.AutoSize = false;
            Color formbackcolor = FindForm().BackColor;
            Color CheckBoxColor = this.BackColor;
            Color Checkbuttocolor = Checked ? Color.GreenYellow : CheckBoxColor;

            RectangleF WidthRect = new RectangleF((this.Width - 4) - (this.Height), 0, this.Height - 2, this.Height - 2);
            RectangleF ZeroRect = new RectangleF(WidthRect.X - tbw, 0, this.Height-2, this.Height-2);
            
            RectangleF onoffrect1 = Checked ? new RectangleF(WidthRect.X + 4, WidthRect.Y + 4, WidthRect.Height - 8, WidthRect.Height - 8) : new RectangleF(ZeroRect.X + 4, ZeroRect.Y + 4, ZeroRect.Height - 8, ZeroRect.Height - 8);
            

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(formbackcolor);

            g.FillRectangle(new SolidBrush(Checkbuttocolor), ZeroRect.X + Height / 2, ZeroRect.Y, WidthRect.X - ZeroRect.X,Height - 2);

            //g.FillRectangle(new SolidBrush(formbackcolor), 0, 0, this.Height/2, this.Height);
            //g.FillRectangle(new SolidBrush(formbackcolor), (this.Width) - (this.Height) / 2, 0, this.Height/2, this.Height);
            
            g.FillEllipse(new SolidBrush(Checkbuttocolor), ZeroRect);
            g.FillEllipse(new SolidBrush(Checkbuttocolor), WidthRect);

            g.FillEllipse(new SolidBrush(Color.White), onoffrect1);

            TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter;
            TextRenderer.DrawText(g, Text, Font, new Point(5, Height / 2 + 2), ForeColor, flags);

        }
    }
}
