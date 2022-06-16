using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace _0530
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ////清除默认的series
            //chart1.Series.Clear();
            ////new 一个叫做【Strength】的系列
            //Series Strength = new Series("力量");
            ////设置chart的类型，这里为柱状图
            //Strength.ChartType = SeriesChartType.Column;
            ////给系列上的点进行赋值，分别对应横坐标和纵坐标的值
            //Strength.Points.AddXY("A", "90");
            //Strength.Points.AddXY("B", "88");
            //Strength.Points.AddXY("C", "60");
            //Strength.Points.AddXY("D", "93");
            //Strength.Points.AddXY("E", "79");
            //Strength.Points.AddXY("F", "85");
            ////把series添加到chart上
            //chart1.Series.Add(Strength);
            chart1.Series.Clear();
            Series Strength = new Series("力量");
            Series Speed = new Series("速度");

            Strength.ChartType = SeriesChartType.Spline;
            Strength.IsValueShownAsLabel = true;
            Strength.Color = System.Drawing.Color.Cyan;

            Speed.ChartType = SeriesChartType.Spline;
            Speed.IsValueShownAsLabel = true;

            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 0.5;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            //chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
            chart1.ChartAreas[0].AxisX.IsMarginVisible = true;
            chart1.ChartAreas[0].AxisX.Title = "英雄";
            chart1.ChartAreas[0].AxisX.TitleForeColor = System.Drawing.Color.Crimson;

            chart1.ChartAreas[0].AxisY.Title = "属性";
            chart1.ChartAreas[0].AxisY.TitleForeColor = System.Drawing.Color.Crimson;
            chart1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Horizontal;



            Strength.LegendText = "力气";
            Strength.Points.AddXY("A", "90");
            Strength.Points.AddXY("B", "88");
            Strength.Points.AddXY("C", "60");
            Strength.Points.AddXY("D", "93");
            Strength.Points.AddXY("E", "79");
            Strength.Points.AddXY("F", "86");

            Speed.Points.AddXY("A", "120");
            Speed.Points.AddXY("B", "133");
            Speed.Points.AddXY("C", "100");
            Speed.Points.AddXY("D", "98");
            Speed.Points.AddXY("E", "126");
            Speed.Points.AddXY("F", "89");

            //把series添加到chart上
            chart1.Series.Add(Speed);
            chart1.Series.Add(Strength);
        }
    }
}
