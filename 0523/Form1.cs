using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static _0523.GetPort;
using static _0523.USBPort;
using static _0523.ConvertString;
using static _0523.MyLog;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;

namespace _0523
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //  TODO:  在  InitComponent  调用后添加任何初始化 
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //开启双缓冲
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }
        bool First_Init = true;
        bool ReveiceEnable = true; 
        //string FilePath = null;
        //Stream Savefilestream = null;
        //StreamReader Openfilestream = null;
        StreamWriter streamwrite = null;
        //private List<byte> reciveBuffer = new List<byte>();
       // private List<byte> sendBuffer = new List<byte>();
        private int reciveCount = 0;
        private int sendCount = 0;
        private void 关于AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAbout FrmAbout = new FrmAbout();
            FrmAbout.ShowDialog();
        }
        /// <summary>
        /// 手动扫描端口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 自定义CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getPortDeviceName();//扫描端口
        }
        /// <summary>
        /// 初始化combobox
        /// </summary>
        private void SettingInit()
        {
            // GetComList();
            getPortDeviceName();//扫描端口
            comboBoxBaud.SelectedIndex = 3;
            comboBoxData.SelectedIndex = 3;
            comboBoxFlow.SelectedIndex = 0;
            comboBoxParity.SelectedIndex = 0;
            comboBoxStop.SelectedIndex = 1;
            radioBtnRecHEX.Checked = true;
            radioBtnSendHEX.Checked = true;
        }
        #region 监视USB口
        /// <summary>
        /// 获取串口完整名字
        /// </summary>
        private ManagementObjectCollection GetComList()
        {
            ManagementObjectCollection hardInfos = null;
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_PnPEntity"))
                {
                    //Console.WriteLine("本机串口：");
                    this.textBoxReceive.Text += "本机串口：" + Environment.NewLine;
                    hardInfos = searcher.Get();
                    /*
                    int index = 1;
                    this.comboBoxPort.Items.Clear();
                    foreach (var hardInfo in hardInfos)
                    {
                        if (hardInfo.Properties["Name"].Value != null && hardInfo.Properties["Name"].Value.ToString().Contains("(COM"))
                        {
                            String strComName = hardInfo.Properties["Name"].Value.ToString();
                            //Console.WriteLine(index + ":" + strComName);//打印串口设备名称及串口号
                            this.textBoxReceive.Text += index + ":" + strComName + Environment.NewLine;
                            index += 1;
                            this.comboBoxPort.Items.Add(strComName);
                        }
                    }
                    this.comboBoxPort.SelectedIndex = 0;
                    */
                }
                //Console.ReadKey();
            }
            catch
            {
                hardInfos = null;
            }
            finally
            {
                hardInfos.Dispose();
            }
            return hardInfos;
        }

        /// <summary>
        /// 获取串口完整名字（包括驱动名字）
        /// 如果找不到类，需要添加System.Management引用，添加引用->程序集->System.Management
        /// </summary>
        Dictionary<String, String> coms = new Dictionary<String, String>();
        
        private void getPortDeviceName()
        {
            coms.Clear();
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher
            ("select * from Win32_PnPEntity where Name like '%(COM%'"))
            {
                var hardInfos = searcher.Get();
                if (hardInfos.Count == 0)
                {
                    //创建一个用来更新UI的委托 (主线程更新)
                    this.Invoke(
                         new Action(() =>
                         {
                             //cbPortItem.Text = null; //移除所有串口后显示为空
                         })
                     );
                }
                foreach (var hardInfo in hardInfos)
                {
                    if (hardInfo.Properties["Name"].Value != null)
                    {
                        string deviceName = hardInfo.Properties["Name"].Value.ToString();
                        int startIndex = deviceName.IndexOf("(");
                        int endIndex = deviceName.IndexOf(")");
                        string key = deviceName.Substring(startIndex + 1, deviceName.Length - startIndex - 2);
                        string name = deviceName.Substring(0, startIndex - 1);

                        if (name.Length > 6)//将超长度字符显示为...
                        {
                            name = name.Substring(0, 7) + "...";
                        }

                        // Console.WriteLine("key:" + key + ",name:" + name + ",deviceName:" + deviceName);
                        coms.Add(key, name);
                    }
                }

                //创建一个用来更新UI的委托 (主线程更新)
                this.Invoke(
                     new Action(() =>
                     {
                         //cbPortItem.Items.Clear();
                         textBoxReceive.Clear();
                         foreach (KeyValuePair<string, string> kvp in coms)
                         {
                             //textBoxReceive.AppendText(kvp.Key + " " + kvp.Value);//更新下拉列表中的串口
                             comboBoxPort.Items.Add(kvp.Key + " " + kvp.Value);
                             comboBoxPort.SelectedIndex = 0;
                         }
                     })
                 );
            }
        }
        public const int WM_DEVICE_CHANGE = 0x219;
        public const int DBT_DEVICEARRIVAL = 0x8000;
        public const int DBT_DEVICE_REMOVE_COMPLETE = 0x8004;

        /// <summary>
        /// 检测USB串口的拔插
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_DEVICE_CHANGE) // 捕获USB设备的拔出消息WM_DEVICECHANGE
            {
                switch (m.WParam.ToInt32())
                {
                    case DBT_DEVICE_REMOVE_COMPLETE: // USB拔出 
                        {
                            if (First_Init == false)
                            {
                                First_Init = true;
                                // MessageBox.Show("Test1");
                            }
                            else
                            {
                                //MessageBox.Show("Test4");
                                new Thread(
                                    new ThreadStart(
                                        new Action(() =>
                                        {
                                            getPortDeviceName();
                                        })
                                    )
                                ).Start();
                            }
                        }
                        break;
                    case DBT_DEVICEARRIVAL: // USB插入获取对应串口名称     
                        {
                            if (First_Init == false)
                            {
                                First_Init = true;
                                // MessageBox.Show("Test2");
                            }
                            else
                            {
                                //MessageBox.Show("Test3");
                                new Thread(
                                    new ThreadStart(
                                        new Action(() =>
                                        {
                                            getPortDeviceName();
                                        })
                                    )
                                ).Start();
                            }
                        }
                        break;
                }
            }
            base.WndProc(ref m);
        }
        #endregion
        private void Form1_Load(object sender, EventArgs e)
        {
            // Assembly.
            SettingInit();
            //toolStripBtnStop
            this.Focus();
        }
        /// <summary>
        /// 清空计数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripBtnClear_Click(object sender, EventArgs e)
        {
            textBoxReceive.Clear();
            toolStripLabRx.Text = "Rx: 0 Bytes";
            toolStripLabTx.Text = "Tx: 0 Bytes";
            reciveCount = 0;
            sendCount = 0;
        }
        private void OpenSerial()
        {
            try
            {
                if (btnSend.Text == "打开")
                {
                    string Port = comboBoxPort.SelectedItem.ToString();
                    Port = Port.Substring(0, 5);
                    serialPort1.PortName = Port;
                    serialPort1.BaudRate = Convert.ToInt32(comboBoxBaud.Text);
                    serialPort1.DataBits = Convert.ToInt32(comboBoxData.Text);
                    switch (comboBoxParity.SelectedIndex)
                    {
                        case 0://无奇偶校验
                            serialPort1.Parity = Parity.None;
                            break;
                        case 1://奇校验
                            serialPort1.Parity = Parity.Odd;
                            break;
                        case 2://偶校验
                            serialPort1.Parity = Parity.Even;
                            break;
                        case 3://校验位1
                            serialPort1.Parity = Parity.Mark;
                            break;
                        case 4://校验位0
                            serialPort1.Parity = Parity.Space;
                            break;
                        default://默认无奇偶校验
                            serialPort1.Parity = Parity.None;
                            break;
                    }
                    switch (comboBoxStop.SelectedIndex)
                    {
                        case 0://无停止位
                            serialPort1.StopBits = StopBits.None;
                            break;
                        case 1://停止位1
                            serialPort1.StopBits = StopBits.One;
                            break;
                        case 2://停止位1.5
                            serialPort1.StopBits = StopBits.OnePointFive;
                            break;
                        case 3://停止位2
                            serialPort1.StopBits = StopBits.Two;
                            break;
                        default:
                            serialPort1.StopBits = StopBits.One;
                            break;
                    }
                    serialPort1.Open();
                    if (serialPort1.IsOpen)
                    {
                        toolStripLabState.Text = serialPort1.PortName + "OPEN " +
                            serialPort1.BaudRate + "," +
                            serialPort1.DataBits + "," +
                            serialPort1.Parity + "," +
                            serialPort1.StopBits;
                        toolStripLabState.ForeColor = Color.Green;
                        toolStripLabState.Font = new Font("微软雅黑", 8F);
                    }
                    else
                    {
                        toolStripLabState.Text = serialPort1.PortName + "OPEN FAILED";
                        toolStripLabState.ForeColor = Color.Red;
                    }
                    btnSend.Text = "发送";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
        }
        #region 打开关闭串口
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen&&btnSend.Text =="打开")
            {
                OpenSerial();
            }
            else if(btnSend.Text=="发送")
            {
                SendData();
            }
        }
        /// <summary>
        /// 串口发送
        /// </summary>
        private void SendData()
        {
            if (serialPort1.IsOpen)//检查串口打开
            {
                try
                {
                   // serialPort1.WriteLine(textBoxSend.Text.Trim());
                    serialPort1.Write(textBoxSend.Text);
                    if (checkBoxShowSend.Checked)//显示发送
                    {
                        textBoxReceive.AppendText(textBoxSend.Text);
                    }
                    if (comboBoxSend.Items.Count>0)//仅添加不同项
                    {
                        for (int i = 0; i < comboBoxSend.Items.Count; i++)
                        {
                            if (!comboBoxSend.Items.Contains(textBoxSend.Text))
                            {
                                comboBoxSend.Items.Add(textBoxSend.Text);
                            }
                        }
                    }
                    else
                    {
                        comboBoxSend.Items.Add(textBoxSend.Text);
                    }
                    comboBoxSend.SelectedIndex = comboBoxSend.Items.Count - 1;
                    //sendCount = Encoding.Default.GetByteCount(textBoxSend.Text.Replace(" ", ""));
                    if (radioBtnSendHEX.Checked)
                    {
                        sendCount += textBoxSend.Text.Replace(" ", "").Length / 2;
                    }
                    else
                    {
                        sendCount += textBoxSend.Text.Length;
                    }
                    //sendCount += textBoxSend.Text.Length;
                    toolStripLabTx.Text = "Tx: " + sendCount + " Bytes";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    //throw;
                }
            }
            else//没打开串口则打开串口
            {
                //OpenSerial();
                //SendData();
            }
        }
        private void toolStripBtnStop_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                //this.serialPort1.DataReceived -= new System.IO.Ports.SerialDataReceivedEventHandler(this.myserialPort_rec);
                serialPort1.Close();
                serialPort1.Dispose();
                toolStripLabState.Text = serialPort1.PortName + "CLOSE";
                toolStripLabState.ForeColor = Color.Red;
                btnSend.Text = "打开";
            }
            checkBoxSendLoop.Checked = false;
            timer1.Stop();
        }

        private void toolStripBtnContinue_Click(object sender, EventArgs e)
        {
            ReveiceEnable = true;
            if (!serialPort1.IsOpen)
            {
                OpenSerial();
            }
        }
        #endregion
       /// <summary>
       /// 串口接收函数
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void myserialPort_rec(object sender, SerialDataReceivedEventArgs e)
        {
            //数据接收完整性
            //这里不知道是用延时好，还是循环读取缓冲区好，待验证……
            Thread.Sleep(100);
            if (serialPort1.IsOpen==false)
            {
                return;
            }
            if (ReveiceEnable)
            {
                if (serialPort1.BytesToRead > 0)
                {
                    byte[] a = new byte[serialPort1.BytesToRead];//读出缓冲区串口通信的字节                                              
                    int readbytes = 0;
                    while (serialPort1.Read(a, readbytes, serialPort1.BytesToRead - readbytes) <= 0) ;
                    string str=null;// = UTF8Encoding.UTF8.GetString(a);
                    for (int i = 0; i < a.Length; i++)
                    {
                        str += a[i].ToString("X2")+" ";
                        //reciveCount += i;
                    }
                   // reciveCount += a.Length;
                    if (radioBtnRecHEX.Checked)
                    {
                        reciveCount += textBoxReceive.Text.Replace(" ", "").Length / 2;
                    }
                    else
                    {
                        reciveCount += textBoxReceive.Text.Length;
                    }
                    this.BeginInvoke((Action)(()=>
                    {
                        textBoxReceive.Text += str;// + "\r\n";
                        toolStripLabRx.Text = "Rx: " + reciveCount + " Bytes";
                    }));
                }                
                //更新界面内容时UI不会卡
                //this.BeginInvoke((EventHandler)delegate
                //{
                //});
            }
            else
            {
                return;
            }
        }
        /// <summary>
        /// 暂停接收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripBtnPause_Click(object sender, EventArgs e)
        {
            ReveiceEnable = false;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (serialPort1.IsOpen)
            //{
            //    serialPort1.Close();
            //}
            Environment.Exit(0);
           // Application.Exit();
        }

        #region 防止窗体闪烁，放在主窗体任意位置
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED  
                if (this.IsXpOr2003 == true)
                {
                    cp.ExStyle |= 0x00080000;  // Turn on WS_EX_LAYERED
                    this.Opacity = 1;
                }
                return cp;
            }
        }
        private Boolean IsXpOr2003
        {
            get
            {
                OperatingSystem os = Environment.OSVersion;
                Version vs = os.Version;

                if (os.Platform == PlatformID.Win32NT)
                    if ((vs.Major == 5) && (vs.Minor != 0))
                        return true;
                    else
                        return false;
                else
                    return false;
            }
        }
        #endregion

        private void 保存SToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // MyLog myLog = new MyLog();
           // myLog.MyLogWrite(this.textBoxReceive.Text, filepath, false);
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
                    //Savefilestream = saveFileDialog1.OpenFile();
                    // string path = saveFileDialog1.
                    streamwrite = new StreamWriter(saveFileDialog1.FileName);
                    streamwrite.Write(textBoxReceive.Text);
                    //this.Text = FilePath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    streamwrite.Close();
                    saveFileDialog1.Dispose();
                   // Savefilestream.Close();
                }
            }

        }

        private void comboBoxSend_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxSend.Text = comboBoxSend.SelectedItem.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SendData();
        }

        private void checkBoxSendLoop_EnabledChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBoxSendLoop_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxSendLoop.Checked == true && serialPort1.IsOpen == true)
            {
                timer1.Interval = Convert.ToInt32(numericUpDown1.Value);
                timer1.Start();
            }
            else
            {
                timer1.Stop();
            }
        }

        private void 退出XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
