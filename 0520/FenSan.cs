using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;

namespace _0520
{
    public class FenSan : Control
    {
        public FenSan()
        {
            CheckForIllegalCrossThreadCalls = false;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            t1 = new Thread(() =>
            {
                while (true)
                {
                    p++;
                    if (p > 360)
                    {
                        p = 0;
                    }
                    Refresh();
                    if (p % 360 == 0)
                    {
                        if (Isrun)
                        {
                            r = rand.Next(205);
                            Thread.Sleep(4);
                            g = rand.Next(205);
                            Thread.Sleep(4);
                            b = rand.Next(205);
                        }
                    }

                    Thread.Sleep(Inid);
                }
            })
            {
                IsBackground = true
            };
            t1.Start();
        }
        public bool Isrun { get; set; } = false;
        public int Inid = 5;
        public Color color = Color.DarkBlue;
        Random rand = new Random();
        int r = 00, g = 0xbf, b = 0xff;

        private Color TColor1 = Color.CornflowerBlue;
        [Browsable(true), Category("控件的重绘设置"), Description("修改控件颜色")] //在“属性”窗口中显示DataStyle属性
        public Color Color1
        {
            get { return TColor1; }
            set
            {
                TColor1 = value;
                this.Invalidate();
            }
        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
           //color = Color.FromArgb(r, g, b);
           // Color1 = Color.FromArgb(r, g, b);
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Width = Height;
        }
        Thread t1;
        int p = 0;
        // Bitmap bit = new Bitmap(@"C:\Users\Administrator\Desktop\1.png");
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // color = Color.FromArgb(r, g, b);
            //Color1 = Color.FromArgb(r, g, b);
            int w = Width / 2;
            int h = Height / 2;

            Matrix matrix = e.Graphics.Transform;
            matrix.RotateAt(p, new PointF(w, h));
            e.Graphics.Transform = matrix;
            matrix.Dispose();
            // e.Graphics.RotateTransform(p, MatrixOrder.Prepend);
            //e.Graphics.TranslateClip(w,h);
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            //e.Graphics.DrawImage(bit, ClientRectangle);
            //e.Graphics.FillPie(new SolidBrush(color), new Rectangle(w / 2, 0, w, h), 90, 180);
            //e.Graphics.FillPie(new SolidBrush(color), new Rectangle(w - 1, h / 2, w, h), 180, 180);
            //e.Graphics.FillPie(new SolidBrush(color), new Rectangle(w / 2, h - 1, w, h), 270, 180);
            //e.Graphics.FillPie(new SolidBrush(color), new Rectangle(0, h / 2, w, h), 360, 180);
            e.Graphics.FillPie(new SolidBrush(Color1), new Rectangle(w / 2, 0, w, h), 90, 180);
            e.Graphics.FillPie(new SolidBrush(Color1), new Rectangle(w - 1, h / 2, w, h), 180, 180);
            e.Graphics.FillPie(new SolidBrush(Color1), new Rectangle(w / 2, h - 1, w, h), 270, 180);
            e.Graphics.FillPie(new SolidBrush(Color1), new Rectangle(0, h / 2, w, h), 360, 180);
            e.Graphics.TranslateTransform(-w, -h);
        }
    }

}
