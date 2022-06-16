using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _0505
{
    public partial class FrmFather : Form
    {
        public FrmFather()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmChild frmChild = new FrmChild();
            frmChild.Owner = this;
            frmChild.Show();
            this.Hide();
        }
        public static bool IsMDI = false;
        private void button2_Click(object sender, EventArgs e)
        {
            this.IsMdiContainer = true;
            //FrmChild frmChild = new FrmChild();
            FrmChild frmChild = new FrmChild(this, button1, button2, button3);
            frmChild.MdiParent = this;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            IsMDI = true;
            frmChild.Show();
        }

        private void FrmFather_Load(object sender, EventArgs e)
        {

            //if (IsMDI == false)
            //{
            //    button1.Visible = true;
            //    button2.Visible = true;
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
           // FrmChild frmChild = new FrmChild(button3);

            this.IsMdiContainer = true;
            FrmChild frmChild = new FrmChild(button1,button2,button3);
            frmChild.MdiParent = this;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            IsMDI = true;
            frmChild.Show();
        }
    }
}
