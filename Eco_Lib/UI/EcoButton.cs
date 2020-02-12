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
    public class EcoButton : Button
    {

        private bool _isHovering;
        private bool _isClicked;
        public EcoButton()
        {
            BackColor = Color.DimGray;
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
            MouseDown += (sender, e) =>
            {
                _isClicked = true;
                Invalidate();
            };
            MouseUp += (sender, e) =>
            {
                _isClicked = false;
                Invalidate();
            };
        }
        private int roundsize1 = 30;
        [Category("EcoSkin"), DisplayName("RoundSize1")]
        public int RoundSize1
        {
            get => roundsize1;
            set
            {
                roundsize1 = value;
                Invalidate();
            }
        }
        private int roundsize2 = 20;
        [Category("EcoSkin"), DisplayName("RoundSize2")]
        public int RoundSize2
        {
            get => roundsize2;
            set
            {
                roundsize2 = value;
                Invalidate();
            }
        }
        private int roundX = 10;
        [Category("EcoSkin"), DisplayName("Round XPos")]
        public int RoundX
        {
            get => roundX;
            set
            {
                roundX = value;
                Invalidate();
            }
        }
        private int edgeR;
        [Category("EcoSkin"), DisplayName("EdgeRound")]
        public int EdgeRound
        {
            get => edgeR;
            set
            {
                edgeR = value;
                Invalidate();
            }
        }
        private int textoffset;
        [Category("EcoSkin"), DisplayName("Text PosX OffSet")]
        public int TextOffset
        {
            get => textoffset;
            set
            {
                textoffset = value;
                Invalidate();
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Color formbackcolor = FindForm().BackColor;
            Color backcolor = _isHovering ? Color.FromArgb(this.BackColor.R ^ 30, this.BackColor.G ^ 30, this.BackColor.B ^ 30) : this.BackColor;
            if(_isClicked)
            {
                backcolor = Color.FromArgb(this.BackColor.R ^ 50, this.BackColor.G ^ 50, this.BackColor.B ^ 50);
            }
            Color btncolor = Color.FromArgb(backcolor.R ^ 150, backcolor.G ^ 150, backcolor.B ^ 150);

            RectangleF buttonrect = new RectangleF(0, 0, this.Width, this.Height);

            RectangleF edg1 = new RectangleF(6, 6, edgeR, edgeR);
            RectangleF edg2 = new RectangleF(6, this.Height - edgeR - 6, edgeR, edgeR);
            RectangleF edg3 = new RectangleF(this.Width - edgeR - 6, 6, edgeR, edgeR);
            RectangleF edg4 = new RectangleF(this.Width - edgeR - 6, this.Height - edgeR - 6, edgeR, edgeR);

            RectangleF outcyl = new RectangleF(3 + RoundX, this.Height / 2 - roundsize1 / 2, roundsize1, roundsize1);
            RectangleF incyl = new RectangleF(outcyl.X + (outcyl.Width - roundsize2) / 2, outcyl.Y + (outcyl.Height - roundsize2) / 2, roundsize2, roundsize2);

            Graphics g = e.Graphics;
            Pen p = new Pen(backcolor);
            
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(formbackcolor);

            g.FillEllipse(new SolidBrush(backcolor), edg1);
            g.FillEllipse(new SolidBrush(backcolor), edg2);
            g.FillEllipse(new SolidBrush(backcolor), edg3);
            g.FillEllipse(new SolidBrush(backcolor), edg4);

            g.FillRectangle(new SolidBrush(backcolor), new RectangleF(edg1.X + edgeR / 2, edg1.Y, edg3.X - edg1.X, edg2.Y - edg1.Y + edgeR + 0.1f));
            g.FillRectangle(new SolidBrush(backcolor), new RectangleF(edg1.X, edg1.Y + edgeR / 2, edg3.X - edg1.X + edgeR + 0.1f, edg2.Y - edg1.Y));


            g.FillEllipse(new SolidBrush(btncolor), outcyl);
            g.FillEllipse(new SolidBrush(backcolor), incyl);


            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
            TextRenderer.DrawText(g, Text, Font, new Point(Width + textoffset, Height / 2 + 2), ForeColor, flags);
        }
    }
}
