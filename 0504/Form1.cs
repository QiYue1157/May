using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _0504
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*
            DataTable dt = new DataTable();  //新建一个datatable用于保存读入的数据            
            dt.Columns.Add(new DataColumn("点号", typeof(string)));   //给datatable添加五列
            //dt.Columns.Add(new DataColumn("类型", typeof(string)));
            dt.Columns.Add(new DataColumn("控制点X", typeof(string)));
            dt.Columns.Add(new DataColumn("控制点Y", typeof(string)));
            dt.Columns.Add(new DataColumn("控制点Z", typeof(string)));
            */
           // dataGridView1.RowHeadersVisible = false;
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex < 0 && e.RowIndex >= 0) // 绘制 自动序号
            {
                e.Paint(e.ClipBounds, DataGridViewPaintParts.All);
                Rectangle vRect = e.CellBounds;
                vRect.Inflate(-2, 2);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.CellStyle.Font, vRect, e.CellStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
                e.Handled = true;
            }

            // ----- 其它样式设置 -------
            if (e.RowIndex % 2 == 0)
            { // 行序号为双数（含0）时 
                e.CellStyle.BackColor = Color.White;
            }
            else
            {
                e.CellStyle.BackColor = Color.Honeydew;
            }
            e.CellStyle.SelectionBackColor = Color.Gray; // 选中单元格时，背景色
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //单位格内数据对齐方式
        }
    }
}
