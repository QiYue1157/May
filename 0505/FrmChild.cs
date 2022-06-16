using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _0505
{
    public partial class FrmChild : Form
    {
        //私有变量，保存传入的父窗体控件
        private Button[] btn = { null };
        private Form Form;
        public bool ChildIsClose = false;
        private static string str;
        List<FileSystemInfo> TheFolder2 = new List<FileSystemInfo> { };
        //DirectoryInfo[] DirectoryInfos;
        static long files = 0;
        static long directories = 0;
        static List<FileSystemInfo> infos1 = new List<FileSystemInfo> { };
        //List<Control> buttonList = new List<Control>();
        public FrmChild()
        {
            InitializeComponent();
            this.Text = "子窗体";
        }
        public FrmChild(params Button[] b)
        {
            InitializeComponent();
            for (int i = 0; i < b.Length; i++)
            {
                btn = b;
            }
        }
        public FrmChild(Form form, params Button[] b)
        {
            InitializeComponent();
            for (int i = 0; i < b.Length; i++)
            {
                btn = b;
            }
            this.Form = form;
            //form.Text = "MDI子窗体";
            this.Text = "MDI子窗体";
        }
        private void FrmChild_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (FrmFather.IsMDI == false)
            {
                FrmFather frmFather = (FrmFather)this.Owner;
                frmFather.Show();
            }
            else
            {
                FrmFather.IsMDI = false;
                ChildIsClose = true;

                for (int i = 0; i < btn.Length; i++)
                {
                    btn[i].Visible = true;
                }
                if (Form != null)
                {
                    Form.IsMdiContainer = false;
                }
            }
            //frmFather.Owner = this;
            //this.Close();
        }

        private void FrmChild_Load(object sender, EventArgs e)
        {
            btnDel.Visible = false;
            toolStripLabStat.Text = "已就绪";
            toolStripProgressBar1.Visible = false;
            toolStripSeparator2.Visible = false;
        }

        private void btnSel_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = "请选择文件目录";
            folderBrowserDialog1.ShowNewFolderButton = false;
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.DesktopDirectory;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                str = folderBrowserDialog1.SelectedPath;
                textBoxFile.Text = str;
                //TheFolder = new DirectoryInfo(str);
                btnDel.Visible = true;
                //btnDel.Text = "搜索";
            }
        }
        static List<FileSystemInfo> ListDirectoriesAndFiles(FileSystemInfo[] FSInfo, string SearchString)
        {
            // Check the parameters.
            if (FSInfo == null)
            {
                throw new ArgumentNullException("FSInfo");
            }
            if (SearchString == null || SearchString.Length == 0)
            {
                throw new ArgumentNullException("SearchString");
            }

            // Iterate through each item.
            foreach (FileSystemInfo i in FSInfo)
            {
                // Check to see if this is a DirectoryInfo object.
                if (i is DirectoryInfo)
                {
                    // Add one to the directory count.
                    directories++;
                    // Cast the object to a DirectoryInfo object.
                    DirectoryInfo dInfo = (DirectoryInfo)i;
                    infos1.Add(i);
                    // Iterate through all sub-directories.
                    ListDirectoriesAndFiles(dInfo.GetFileSystemInfos(SearchString), SearchString);
                }
                // Check to see if this is a FileInfo object.
                else if (i is FileInfo)
                {
                    // Add one to the file count.
                    files++;
                }
            }
            return infos1;
        }
        static void ListDirectoriesAndFiles(FileSystemInfo[] FSInfo)
        {
            // Check the FSInfo parameter.
            if (FSInfo == null)
            {
                throw new ArgumentNullException("FSInfo");
            }

            // Iterate through each item.
            foreach (FileSystemInfo i in FSInfo)
            {
                // Check to see if this is a DirectoryInfo object.
                if (i is DirectoryInfo)
                {
                    // Add one to the directory count.
                    directories++;
                    // Cast the object to a DirectoryInfo object.
                    DirectoryInfo dInfo = (DirectoryInfo)i;
                    // Iterate through all sub-directories.
                    ListDirectoriesAndFiles(dInfo.GetFileSystemInfos());
                }
                // Check to see if this is a FileInfo object.
                else if (i is FileInfo)
                {
                    // Add one to the file count.
                    files++;
                }
            }
        }
        //删除文件夹
        public bool DeleteDir(string file)
        {
            try
            {
                //去除文件夹和子文件的只读属性
                //去除文件夹的只读属性
                System.IO.DirectoryInfo fileInfo = new DirectoryInfo(file);
                fileInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;
                //去除文件的只读属性
                System.IO.File.SetAttributes(file, System.IO.FileAttributes.Normal);
                //判断文件夹是否还存在
                if (Directory.Exists(file))
                {
                    foreach (string f in Directory.GetFileSystemEntries(file))
                    {
                        if (File.Exists(f))
                        {
                            //如果有子文件删除文件
                            File.Delete(f);
                            //Console.WriteLine(f);
                            textBoxOut.Text += "已删除文件:" + f + Environment.NewLine;
                        }
                        else
                        {
                            //循环递归删除子文件夹
                            DeleteDir(f);
                        }
                    }
                    //删除空文件夹
                    Directory.Delete(file);
                }
                return true;

            }
            catch (Exception ex) // 异常处理
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一个文件夹下所有的文件
        /// </summary>
        /// <param name="srcPath"></param>
        private void DelectDirectorys(string srcPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)//判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true); //删除子目录和文件
                    }
                    else
                    {
                        File.Delete(i.FullName); //删除指定文件
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("删除文件夹文件错误：" + e.Message);
            }
        }
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (btnDel.Text == "删除")
            {
                //
                textBoxOut.Clear();
                //listBox1.Items.Clear();
                foreach (var item in TheFolder2)
                {
                    //File.Delete(item.FullName);
                    //DelectDirectorys(item.FullName);
                    if (DeleteDir(item.FullName))
                    {
                        textBoxOut.Text += "已删除" + item.FullName + Environment.NewLine;
                    }
                    else
                        textBoxOut.Text += "删除 " + item.FullName + " 失败" + Environment.NewLine;
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            // Create a new DirectoryInfo object.
            DirectoryInfo dir = new DirectoryInfo(str);
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException("The directory does not exist.");
            }
            // Call the GetFileSystemInfos method.
            FileSystemInfo[] infos = dir.GetFileSystemInfos();
            foreach (FileSystemInfo item in infos)
            {
                listBox1.Items.Add(item.FullName);
                TheFolder2.Add(item);// = item;
            }
            // Pass the result to the ListDirectoriesAndFiles
            // method defined below.
            List<FileSystemInfo> infos2 = ListDirectoriesAndFiles(infos, "History");
            foreach (FileSystemInfo item in infos2)
            {
                // listBox1.Items.Add(item.FullName);
                if (item.FullName.Contains("History"))//查找出所有History文件夹路径
                {
                    textBoxOut.Text += item.FullName + Environment.NewLine;
                }
            }
            //ListDirectoriesAndFiles(infos, "History");
            Debug.WriteLine("Directories: {0}", directories);
            Debug.WriteLine("Files: {0}", files);
        }

    }
}
