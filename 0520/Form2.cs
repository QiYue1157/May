using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0520
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        int OldX;
        int OldY;
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }
        // int fx = 
        static Point Point = new Point(20, 35);
        static Size size = new Size(120, 120);
        //static Rectangle Rectangle = new Rectangle(Point, size);
        private void Form2_Load(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;//隐藏关闭按钮
            //this.BackColor = Color.FromArgb(85, 24, 1);
            //TransparencyKey = BackColor;
        }
        static int i = 0;
        bool Up = true;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (i < 80 && Up == true)
            {
                i++;
                if (i == 80)
                {
                    Up = false;
                }
            } 
            else if(Up==false&&i>0)
            {
                i--;
                if (i==0)
                {
                    Up = true;
                }
            }
            size = new Size((20 + i), (20 + i));
            fenSan1.Size = size;
            int fx = this.Width / 2 - fenSan1.Width / 2;
            int fy = this.Height / 2 - fenSan1.Height / 2;
            fenSan1.Left = fx;
            fenSan1.Top = fy;
        }
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            //if (pictureBox1.Visible == false)
            {
              //  pictureBox1.Visible = true;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button==MouseButtons.Left)
            {
                OldX = e.Location.X;
                OldY = e.Location.Y;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left += e.Location.X - OldX;
                Top += e.Location.Y - OldY;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            int x = Location.X + this.Width;
            //label4.Text = "X:" + x.ToString();
            int y = Location.Y + Height;
            // label5.Text = "Y:" + y.ToString();
            if ((MousePosition.X > Location.X && MousePosition.X < x)
                && (MousePosition.Y > Location.Y && MousePosition.Y < y))
            {
                pictureBox1.Visible = true;
            }
            else
            {
                pictureBox1.Visible = false;
            }
        }
    }
}
