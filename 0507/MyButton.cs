using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;

namespace _0507
{
    public class MyButton : Button
    {
        Rectangle r;
        private Brush _myBrush = null;
        private Color _color1 = System.Drawing.Color.FromArgb(255, 255, 192);
        private Color _color2 = System.Drawing.Color.FromArgb(0, 0, 192);
        private Color color3;
        private Color color4;


        [Category("设置"), Description("渐变开始颜色")]
        public Color color1
        {
            get { return _color1; }
            set { _color1 = value; }
        }

        [Category("设置"), Description("渐变结束颜色")]
        public Color color2
        {
            get { return _color2; }
            set { _color2 = value; }
        }
        public void ButtoonNew()
        {
            r = new Rectangle(0, 0, 150, 80);
            _myBrush = new LinearGradientBrush(r, color1, color2, LinearGradientMode.Vertical);
        }
        public Brush MyBrush
        {
            get { return _myBrush; }
            set { _myBrush = value; }
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            r = new Rectangle(0, 0, this.Width, this.Height);
            MyBrush = new LinearGradientBrush(r, color1, color2, LinearGradientMode.Vertical);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            r = new Rectangle(0, 0, this.Width, this.Height);
            color1 = color3;
            color2 = color4;
            MyBrush = new LinearGradientBrush(r, color1, color2, LinearGradientMode.Vertical);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            color3 = this.color1;
            color4 = this.color2;
            //color1 = System.Drawing.Color.FromArgb(255, 255, 136);
            //color2 = Color.FromArgb(0, 0, 192);
            r = new Rectangle(0, 0, this.Width, this.Height);
            MyBrush = new LinearGradientBrush(r, color4, color3, LinearGradientMode.Vertical);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            Graphics g = pevent.Graphics;
            g.FillRectangle(MyBrush, this.ClientRectangle);
            StringFormat strF = new StringFormat();
            strF.Alignment = StringAlignment.Center;
            strF.LineAlignment = StringAlignment.Center;
            g.DrawString(this.Text, this.Font, new SolidBrush(Color.Black), this.ClientRectangle, strF);
        }
    }
}

