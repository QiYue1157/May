using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace _0509
{
    static class Program
    {


        #region 方法四：使用的Win32函数的声明

        /// <summary>
        /// 找到某个窗口与给出的类别名和窗口名相同窗口
        /// 非托管定义为：http://msdn.microsoft.com/en-us/library/windows/desktop/ms633499(v=vs.85).aspx
        /// </summary>
        /// <param name="lpClassName">类别名</param>
        /// <param name="lpWindowName">窗口名</param>
        /// <returns>成功找到返回窗口句柄,否则返回null</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 切换到窗口并把窗口设入前台,类似 SetForegroundWindow方法的功能
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="fAltTab">True代表窗口正在通过Alt/Ctrl +Tab被切换</param>
        [DllImport("user32.dll ", SetLastError = true)]
        static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        /// <summary>
        /// 设置窗口的显示状态
        /// Win32 函数定义为：http://msdn.microsoft.com/en-us/library/windows/desktop/ms633548(v=vs.85).aspx
        /// </summary>
        /// <param name = "hWnd" > 窗口句柄 </ param >
        /// < param name="cmdShow">指示窗口如何被显示</param>
        /// <returns>如果窗体之前是可见，返回值为非零；如果窗体之前被隐藏，返回值为零</returns>
        [DllImport("user32.dll", EntryPoint = "ShowWindow", CharSet = CharSet.Auto)]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);
        public const int SW_RESTORE = 9;
        public static IntPtr formhwnd;
        #endregion
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            bool createdNew;
            Mutex mutex = new Mutex(true, Application.ProductName, out createdNew);
            if (createdNew)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FrmTime());
                mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("程序已启动！");
                Application.Exit();
            }
            
            /*
            #region 方法四: 可以是托盘中的隐藏程序显示出来
            // 方法四相对于方法三而言应该可以说是一个改进，
            // 因为方法三只能是最小化的窗体显示出来，如果隐藏到托盘中则不能把运行的程序显示出来
            // 具体问题可以看这个帖子：http://social.msdn.microsoft.com/Forums/zh-CN/6398fb10-ecc2-4c03-ab25-d03544f5fcc9
            Process currentproc = Process.GetCurrentProcess();
            Process[] processcollection = Process.GetProcessesByName(currentproc.ProcessName.Replace(".vshost", string.Empty));
            // 该程序已经运行，
            if (processcollection.Length >= 1)
            {
                foreach (Process process in processcollection)
                {
                    if (process.Id != currentproc.Id)
                    {
                        // 如果进程的句柄为0，即代表没有找到该窗体，即该窗体隐藏的情况时
                        if (process.MainWindowHandle.ToInt32() == 0)
                        {
                            // 获得窗体句柄
                            formhwnd = FindWindow(null, "FrmTime");
                            // 重新显示该窗体并切换到带入到前台
                            ShowWindow(formhwnd, SW_RESTORE);
                            SwitchToThisWindow(formhwnd, true);
                        }
                        else
                        {
                            // 如果窗体没有隐藏，就直接切换到该窗体并带入到前台
                            // 因为窗体除了隐藏到托盘，还可以最小化
                            SwitchToThisWindow(process.MainWindowHandle, true);
                        }
                    }
                }
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FrmTime());
            }
            #endregion
            */

        }
    }
}
