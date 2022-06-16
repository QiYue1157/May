using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _0529
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Graphics g = this.CreateGraphics();
            //Rectangle rect = new Rectangle(10, 10, 50, 50);//定义矩形,参数为起点横纵坐标以及其长和宽
            //                                               //用渐变色填充
            //rect.Location = new Point(10, 20);
            //LinearGradientBrush b3 = new LinearGradientBrush(rect, Color.Yellow, Color.Black, LinearGradientMode.Horizontal);
            //g.FillRectangle(b3, rect);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g1 = this.CreateGraphics();
            Rectangle rect = new Rectangle(e.ClipRectangle.X, 
                e.ClipRectangle.Y, 
                e.ClipRectangle.Width-1, 
                e.ClipRectangle.Height-1);//定义矩形,参数为起点横纵坐标以及其长和宽
            Rectangle rect2 = new Rectangle(e.ClipRectangle.X+3,
                e.ClipRectangle.Y+3,
                e.ClipRectangle.Width - 4,
                e.ClipRectangle.Height - 4);//定义矩形,参数为起点横纵坐标以及其长和宽
            Graphics g = e.Graphics; //创建画板,这里的画板是由Form提供的.  
            Pen p = new Pen(Color.Red, 3);//定义了一个蓝色,宽度为的画笔
            g.DrawRectangle(p, rect);//在画板上画矩形,起始坐标为(10,10),宽为,高为
            //Brush brush = new Brush();
            //rect.Location = new Point(0, 0);
           // e.Graphics.DrawLine(brush, rect);
            LinearGradientBrush b3 = new LinearGradientBrush(rect, Color.Yellow, Color.AntiqueWhite, LinearGradientMode.Horizontal);//用渐变色填充
            g1.FillRectangle(b3, rect2);

            //Pen p = new Pen(Color.Blue, 5);//设置笔的粗细为,颜色为蓝色
            //Graphics g = this.CreateGraphics();
            ////画虚线
            //p.DashStyle = DashStyle.Dot;//定义虚线的样式为点
            //g.DrawLine(p, 10, 10, 200, 10);
        }
    }
}
