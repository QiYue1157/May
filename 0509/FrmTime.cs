using System;
using System.Drawing;
using System.Windows.Forms;
using static _0509.Daytime;

namespace _0509
{
    public partial class FrmTime : Form
    {
        public FrmTime()
        {

            InitializeComponent();
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            var temp = dateTimePicker1.Value - DateTime.Now;
            if (temp.Ticks > 0)
            {
                // labTime.ForeColor = Color.Green;
                if (labTime.Width > (this.Width - 5))
                {
                    label1.Visible = true;
                    label1.Font = labTime.Font;
                    labTime.Text = "距离选择时间还有 ";
                    label1.Text = temp.Days + " 天 " +
                    temp.Hours + " 小时 " + temp.Minutes + " 分钟 ";
                }
                else
                {
                    labTime.Text = "距离选择时间还有 " + temp.Days + " 天 " +
                        temp.Hours + " 小时 " + temp.Minutes + " 分钟 ";
                    label1.Visible = false;
                }
            }
            else
            {
                //labTime.ForeColor = Color.Gray;
                if (labTime.Width > (this.Width - 5))
                {
                    label1.Visible = true;
                    label1.Font = labTime.Font;
                    labTime.Text = "距离选择时间已过去: ";
                    label1.Text =  Math.Abs(temp.Days) + " 天 " +
                    temp.Hours + " 小时 " + temp.Minutes + " 分钟 ";
                }
                else
                {
                    labTime.Text = "距离选择时间已过去 " + Math.Abs(temp.Days) + " 天 " +
                        temp.Hours + " 小时 " + temp.Minutes + " 分钟 ";
                    label1.Visible = false;
                }
            }
        }

        private void FrmTime_Load(object sender, EventArgs e)
        {
            // DateTime nettime = Daytime.GetTime().ToLocalTime();   //这个就是同步后准确的时间，DateTime类型的。
            // label2.Text = "北京时间:" + nettime.ToString();
            // label2.Text = "当前时间:" + DateTime.Now.ToString();
            this.Text = "倒计时" + Application.ProductVersion;
            toolStripStatusLabel1.Text = "当前时间:" + DateTime.Now.ToString("F");
            label1.Visible = false;
            dateTimePicker1.Text = "2020年6月17日 8:00:00";
            dateTimePicker1.Value = new DateTime(2020, 06, 17, 8, 0, 0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var temp = dateTimePicker1.Value - DateTime.Now;
            //label2.Text = "当前时间:" + DateTime.Now.ToString();
            toolStripStatusLabel1.Text = "当前时间:" + DateTime.Now.ToString("F");
            if (temp.Ticks > 0)
            {
                // labTime.ForeColor = Color.Green;
                if (labTime.Width > (this.Width - 5))
                {
                    label1.Visible = true;
                    label1.Font = labTime.Font;
                    labTime.Text = "距离选择时间还有 ";
                    label1.Text = Math.Abs(temp.Days) + " 天 " +
                    Math.Abs(temp.Hours) + " 小时 " +
                    Math.Abs(temp.Minutes) + " 分钟 " +
                    Math.Abs(temp.Seconds) + " 秒 ";
                }
                else
                {
                    labTime.Text = "距离选择时间还有 " + Math.Abs(temp.Days) + " 天 " +
                    Math.Abs(temp.Hours) + " 小时 " +
                    Math.Abs(temp.Minutes) + " 分钟 " +
                    Math.Abs(temp.Seconds) + " 秒 ";
                    label1.Visible = false;
                }
            }
            else
            {
                if (labTime.Width > (this.Width - 5))
                {
                    label1.Visible = true;
                    label1.Font = labTime.Font;
                    labTime.Text = "距离选择时间已过去 ";
                    label1.Text = Math.Abs(temp.Days) + " 天 " +
                        Math.Abs(temp.Hours) + " 小时 " +
                        Math.Abs(temp.Minutes) + " 分钟 " +
                        Math.Abs(temp.Seconds) + " 秒 ";
                }
                else
                {
                    // labTime.ForeColor = Color.Gray;
                    labTime.Text = "距离选择时间已过去 " + Math.Abs(temp.Days) + " 天 " +
                    Math.Abs(temp.Hours) + " 小时 " +
                    Math.Abs(temp.Minutes) + " 分钟 " +
                    Math.Abs(temp.Seconds) + " 秒 ";
                    label1.Visible = false;
                }
            }
        }

        private void FrmTime_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.notifyIcon1.Dispose();
            Application.Exit();
            //Environment.Exit(0);
        }

        private void FrmTime_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.ApplicationExitCall)
            {
                Environment.Exit(0);
            }
            else if (e.CloseReason == CloseReason.TaskManagerClosing)
            {
                notifyIcon1.Dispose();
            }
            else
            {
                e.Cancel = true;
                WindowState = FormWindowState.Minimized;
                //Visible = false;
                Hide();
                notifyIcon1.Visible = true;
                notifyIcon1.Text = "倒计时版本:\r\n" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            notifyIcon1.Visible = true;
            // Visible = true;
            Show();
            Activate();
            WindowState = FormWindowState.Normal;
        }

        private void 显示主窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //notifyIcon1.Visible = false;
            //Visible = true;
            Show();
            Activate();
            WindowState = FormWindowState.Normal;
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                notifyIcon1.Visible = true;
                // Visible = true;
                Show();
                Activate();
                WindowState = FormWindowState.Normal;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (toolStripMenuItem1.Text == "置顶窗口")
            {
                TopMost = true;
                toolStripMenuItem1.Text = "取消置顶窗口";
            }
            else
            {
                TopMost = false;
                toolStripMenuItem1.Text = "置顶窗口";
            }
        }

        private void 开机自启ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (开机自启ToolStripMenuItem.Text == "开机自启")
            {
                SysSet sysSet = new SysSet();
                sysSet.SetMeAutoStart(true);
                开机自启ToolStripMenuItem.Text = "取消自启";
            }
            else
            {
                SysSet sysSet = new SysSet();
                sysSet.SetMeAutoStart(false);
                开机自启ToolStripMenuItem.Text = "开机自启";
            }
        }

        private void 修改字体颜色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                labTime.ForeColor = colorDialog1.Color;
                label1.ForeColor = labTime.ForeColor;
                colorDialog1.Dispose();
            }
        }

        private void 修改背景ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "图像文件|*.jpg";
            openFileDialog1.Title = "选择JPG背景图片";
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string PicPath = openFileDialog1.FileName;
                this.BackgroundImage = Image.FromFile(PicPath);
                openFileDialog1.Dispose();
            }
        }

        private void 修改字体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //字体长度超过边框  则换行 未作修改
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                labTime.Font = fontDialog1.Font;
                label1.Font = labTime.Font;
                //label1.Text = "字体宽度" + labTime.Width.ToString() + "窗体宽度" + Width.ToString();
                fontDialog1.Dispose();
            }
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImageLayout = ImageLayout.None;
        }

        private void tileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImageLayout = ImageLayout.Tile;
        }

        private void centerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImageLayout = ImageLayout.Center;
        }

        private void stretchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackgroundImageLayout = ImageLayout.Zoom;
        }
    }
}
