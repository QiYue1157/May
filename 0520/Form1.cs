using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0520
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           // label1.Text += this.PointToScreen(label1.Location).ToString();//Control.MousePosition;
            //label2.Text += MousePosition.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "窗口位置:" + this.Location.X + "," + Location.Y;
            label2.Text = "鼠标位置:" + MousePosition.X + "," + MousePosition.Y;
            int x = Location.X + this.Width;
            label4.Text = "X:" + x.ToString();
            int y = Location.Y + Height;
            label5.Text = "Y:" + y.ToString();
            if ((MousePosition.X > Location.X && MousePosition.X < x)
                && (MousePosition.Y > Location.Y && MousePosition.Y < y))
            {
                label3.Text = "鼠标进入窗体";
            }
            else
            {
                label3.Text = "鼠标离开窗体";
            }
        }
    }
}
