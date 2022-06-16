using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace _0528
{
    public partial class FrmRecord : Form
    {
        public FrmRecord()
        {
            InitializeComponent();
        }
        // 连接字符串
        private string connStr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Desktop\MyMBD.mdb";//此处为前面复制出来的代码

        //声明
        private OleDbConnection conn = null;
        private OleDbDataAdapter adapter = null;
        private DataTable dt = null;
        private void FrmRecord_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Thread.Sleep(2000);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 连接数据库，需要传递连接字符串
            conn = new OleDbConnection(connStr);
            // 打开数据库连接
            conn.Open();

            // "Select * from 表1"为SQL语句，意思是从数据库中选择叫做“表1”的表，“conn”为连接
            adapter = new OleDbDataAdapter("Select * from 表1", conn);
            // CommandBuilder对应的是数据适配器，需要传递参数
            var cmd = new OleDbCommandBuilder(adapter);

            // 在内存中创建一个DataTable，用来存放、修改数据库表
            dt = new DataTable();
            // 通过适配器把表的数据填充到内存dt
            adapter.Fill(dt);
          // dt.

            // 把数据显示到界面
            dataGridView1.DataSource = dt.DefaultView;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // 按下按钮后，把内存中修改的部分传递给适配器，再通过适配器传递给数据库
                adapter.Update(dt);
                // 清除内存中存放的表数据
                dt.Clear();
                // 重新读取已经改变过的表数据
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sql = textBox1.Text;
            OleDbCommand comm = new OleDbCommand(sql, conn);
            comm.ExecuteNonQuery();
        }

        private void FrmRecord_Load(object sender, EventArgs e)
        {

            textBox1.Text = "insert into 表1(工作时间,工作机位,摄像头编号,IP地址)values(2009/12/31,2,3,4)";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            dataGridView1.DataSource = dt.DefaultView;
        }
    }
}
