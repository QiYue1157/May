using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace _0506
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string FilePath = null;
        Stream Savefilestream = null;
        StreamReader Openfilestream = null;
        MemoryStream memoryStream = new MemoryStream();
        FontFamily FontFamily = null;//用于记录字体类型;
        float FontSize = 9F;//用于记录字体大小;
        Color FontColor = Color.Black;//用于记录字体颜色;


        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
                //检索数据格式相关联的数据
                //MessageBox.Show(((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString());
                String[] str_Drop = (String[])e.Data.GetData(DataFormats.FileDrop, true);
                FilePath = str_Drop[0];
                OpenFile(FilePath);
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
            //e.Effect = DragDropEffects.Copy;//设置拖放操作中目标放置类型为复制
            //String[] str_Drop = (String[])e.Data.GetData(DataFormats.FileDrop, true);//检索数据格式相关联的数据
            //string path = str_Drop[0];
            //MessageBox.Show(path);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // textBoxOut.Text = "dashdda";
            // richTextBox1.Text = "12344654";
            //richTextBox1.Select(0, 2);
            //richTextBox1.SelectionColor = Color.Red;
            //richTextBox1.Select(0, 0);
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
            btnReplace.Enabled = false;
            FontSize = richTextBox1.Font.Size;
            FontFamily = richTextBox1.Font.FontFamily;
            FontColor = richTextBox1.ForeColor;
            this.Focus();
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAbout frmAbout = new FrmAbout();
            frmAbout.ShowDialog();

        }

        private void OpenFile(string path)
        {
            textBox2.Text = path;
            Openfilestream = new StreamReader(path, Encoding.Default);//File.Open(FilePath, FileMode.Open, FileAccess.ReadWrite);
            richTextBox1.Clear();
            try
            {
                //richTextBox1.LoadFile(FilePath);
                richTextBox1.Text = Openfilestream.ReadToEnd();
                btnReplace.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                Openfilestream.Close();
            }

        }
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            btnOpenFile.Enabled = false;
            openFileDialog1.Title = "请选择文本文件";
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog1.Filter = "文本文件|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FilePath = openFileDialog1.FileName;
                btnOpenFile.Enabled = true;
                textBox2.Text = FilePath;
                Openfilestream = new StreamReader(FilePath, Encoding.Default);//File.Open(FilePath, FileMode.Open, FileAccess.ReadWrite);
                richTextBox1.Clear();
                try
                {
                    //richTextBox1.LoadFile(FilePath);
                    richTextBox1.Text = Openfilestream.ReadToEnd();
                    btnReplace.Enabled = true;
                    this.Text = FilePath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    Openfilestream.Close();
                }
            }
            else
            {
                MessageBox.Show("未选择文件");
                btnOpenFile.Enabled = true;
            }
        }

        private void 保存SToolStripButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.CreatePrompt = false;//文件不存在将自动创建文件 true:询问是否创建
            saveFileDialog1.OverwritePrompt = true;//覆盖文件提示
            saveFileDialog1.Title = "选择文件路径";
            saveFileDialog1.FileName = "新建文本文件";
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Filter = "文本文件|*.txt";
            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            DialogResult dialogResult = saveFileDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    Savefilestream = saveFileDialog1.OpenFile();
                    richTextBox1.SaveFile(memoryStream, RichTextBoxStreamType.PlainText);
                    memoryStream.WriteByte(13);
                    memoryStream.Position = 0;
                    memoryStream.WriteTo(Savefilestream);
                    this.Text = FilePath;
                }
                catch (Exception ex)
                {
                    textBoxOut.Text += ex.Message;
                    //te
                    //throw;
                }
                finally
                {
                    Savefilestream.Close();
                }
            }

        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            保存SToolStripButton_Click(sender, e);
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnOpenFile_Click(sender, e);
        }

        private void 新建NToolStripButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();//清空文本
            richTextBox1.Focus();//获得焦点
        }

        private void 打开OToolStripButton_Click(object sender, EventArgs e)
        {
            btnOpenFile_Click(sender, e);
        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this, "替换字符选择");
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length > 0)
            {
                btnReplace.Enabled = true;
            }
            else
                btnReplace.Enabled = false;
            this.Text = FilePath+"*";
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();//清空文本
            richTextBox1.Focus();//获得焦点
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length > 0)
            {
                Clipboard.SetText(richTextBox1.SelectedText);
            }
        }
        /// <summary>
        /// 设置复制按钮是否可用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length > 0)
            {
                复制ToolStripMenuItem.Enabled = true;
                复制CToolStripButton.Enabled = true;
            }
            else
            {
                复制ToolStripMenuItem.Enabled = false;
                复制CToolStripButton.Enabled = false;
            }
        }

        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void 编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText() == true)
            {
                粘贴ToolStripMenuItem.Enabled = true;
                粘贴PToolStripButton.Enabled = true;
            }
            else
            {
                粘贴ToolStripMenuItem.Enabled = false;
                粘贴PToolStripButton.Enabled = false;
            }
        }

        private void 编辑ToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText() == true)
            {
                粘贴ToolStripMenuItem.Enabled = true;
                粘贴PToolStripButton.Enabled = true;
            }
            else
            {
                粘贴ToolStripMenuItem.Enabled = false;
                粘贴PToolStripButton.Enabled = false;
            }
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste(DataFormats.GetFormat(DataFormats.UnicodeText));//粘贴UnicodeText字符
        }

        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length > 0)
            {
                Clipboard.SetData(DataFormats.UnicodeText, richTextBox1.SelectedText);
                richTextBox1.SelectedText = "";
            }
        }

        private void 自动换行ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            richTextBox1.WordWrap = !(richTextBox1.WordWrap);
        }

        /// <summary>
        /// 修改字体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 字体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Font = fontDialog1.Font;
                FontSize = fontDialog1.Font.Size;//更新字体大小
            }
        }
        /// <summary>
        /// 修改颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 颜色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                if (richTextBox1.SelectedText.Length > 0)
                {
                    // richTextBox1.SelectionBackColor = colorDialog1.Color;
                    richTextBox1.SelectionColor = colorDialog1.Color;
                }
                else
                {
                    //richTextBox1.BackColor = colorDialog1.Color;
                    richTextBox1.ForeColor = colorDialog1.Color;
                    FontColor = colorDialog1.Color;//更新字体颜色
                }
            }
        }
        /// <summary>
        /// 替换字符
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                string str = "\r\n" + textBox1.Text;
                string oldvalue = richTextBox1.Text.Replace(textBox1.Text, str);
                richTextBox1.Text = oldvalue;
                if (oldvalue == textBox1.Text)
                {
                    textBoxOut.Text += Environment.NewLine;
                    textBoxOut.Text += "未找到：" + oldvalue + Environment.NewLine;
                }
                else
                {
                    textBoxOut.Text += Environment.NewLine;
                    textBoxOut.Text += oldvalue + "替换成功" + Environment.NewLine;
                    richTextBox1.ForeColor = Color.Green;
                }
            }
            else
            {
                string oldvalue2 = richTextBox1.Text.Replace("5A A5", "\r\n5A A5");
                richTextBox1.Text = oldvalue2;
                if (oldvalue2 == "5A A5")
                {
                    textBoxOut.Text += Environment.NewLine;
                    textBoxOut.Text += "未找到：" + oldvalue2 + Environment.NewLine;
                }
                else
                {
                    textBoxOut.Text += Environment.NewLine;
                    textBoxOut.Text += oldvalue2 + "替换成功" + Environment.NewLine;
                    richTextBox1.ForeColor = Color.Green;
                }
            }
            textBoxOut.SelectionStart = textBoxOut.Text.Length;
            textBoxOut.ScrollToCaret();
        }
        #region 查找并高亮显示字符颜色

        /// <summary>
        /// 改变richTextBox中指定字符串的颜色
        /// </summary>
        /// <param name="str">需要查找的字符</param>
        /// <param name="color">高亮显示颜色</param>
        public void changeColor(string str, Color color)
        {
            ArrayList list = getIndexArray(richTextBox1.Text, str);
            for (int i = 0; i < list.Count; i++)
            {
                int index = (int)list[i];
                richTextBox1.Select(index, str.Length);
                richTextBox1.SelectionColor = color;//Color.Green; 
                //richTextBox1.SelectionFont = new Font(Font, FontStyle.Bold);             
                FontFamily fontFamily = FontFamily;
                float size = FontSize;
                richTextBox1.SelectionFont = new Font(fontFamily, size, FontStyle.Bold, GraphicsUnit.Point, ((byte)(134)));
            }
        }
        public ArrayList getIndexArray(String inputStr, String findStr)
        {
            ArrayList list = new ArrayList();
            int start = 0;
            while (start < inputStr.Length)
            {
                int index = inputStr.IndexOf(findStr, start);
                if (index >= 0)
                {
                    list.Add(index);
                    start = index + findStr.Length;
                }
                else
                {
                    break;
                }
            }
            return list;
        }

        #endregion
        private void btnFind_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            //richTextBox1.ForeColor = Color.Black;
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.SelectionFont = new Font(FontFamily, FontSize, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            richTextBox1.DeselectAll();//清空选择
            //richTextBox1.Text.IndexOf()
            changeColor(textBox1.Text, Color.Green);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                btnFind.Enabled = true;
            }
            else
            {
                btnFind.Enabled = false;
            }
        }

        private void 撤销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            textBoxOut.Text += "before: panelrigth.Width:" + panelrigth.Width.ToString() + "  this.Width:" + this.Width.ToString() + Environment.NewLine;
            panelrigth.Width = this.Width - 340;
            panelleft.Height = this.Height - 113;
            panelrigth.Height = this.Height - 113;
            //panelrigth.Location.X = 340;
            panelleft.Location = new Point(0, 50);
            panelrigth.Location = new Point(340, 50);
            textBoxOut.Text += "after: panelrigth.Width:" + panelrigth.Width.ToString() + "  this.Width:" + this.Width.ToString() + Environment.NewLine;
            textBoxOut.SelectionStart = textBoxOut.Text.Length;
            textBoxOut.ScrollToCaret();
        }

        private void 自动换行ToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            richTextBox1.WordWrap = !(richTextBox1.WordWrap);
        }

        private void 文本背景颜色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.BackColor = colorDialog1.Color;//更改richbox背景
                //richTextBox1.ForeColor = colorDialog1.Color;
                //FontColor = colorDialog1.Color;//更新字体颜色
            }
        }
    }
}
