using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static _0528.MyDat;

namespace _0528
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //MyDat myDat = new MyDat();

        string ConfigFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.dat");
        private void Form1_Load(object sender, EventArgs e)
        {
            // MyDat.WriteDat();
            if (ConfigFilePath==null)
            {
                MessageBox.Show("配置初始化失败");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //写入
            Hashtable para = new Hashtable();
            para.Add("ZH", textBox1.Text);
            para.Add("MM", textBox2.Text);
            
            if (MyDat.EncryptObject(para, ConfigFilePath))
            {
                textBox1.Clear();
                textBox2.Clear();
                MessageBox.Show("TEST SUCCEED");
            }
            else
            {
                MessageBox.Show("TEST FAILED");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            //读取
            Hashtable para = new Hashtable();
            object obj = MyDat.DecryptObject(ConfigFilePath);
            para = obj as Hashtable;
            string ZH = para["ZH"].ToString();
            string MM = para["MM"].ToString();
            textBox1.Text = ZH;
            textBox2.Text = MM;
        }
    }
}
