using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0523
{
    public class MyLog
    {
        ///// <summary>
        ///// Log记录
        ///// </summary>      
        ///// <param name="mystr">记录内容</param>
        ///// <param name="myPath">存放目录</param>



        /// <summary>
        /// Log记录
        /// </summary>
        /// <param name="mystr">记录内容</param>
        /// <param name="myPath">存放目录</param>
        /// <param name="IsShowTime">是否显示时间</param>
        public void MyLogWrite(string mystr, string myPath,bool IsShowTime)
        {
            //log存放路径
            string myypath = myPath + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            StreamWriter sw = new StreamWriter(myypath, true);
            string tempSendstr;
            if (IsShowTime)
            {
                tempSendstr = DateTime.Now.ToString("HH:mm:ss.fff") + "  " + mystr;
            }
            else
            {
                tempSendstr = mystr;
            }
            sw.WriteLine(tempSendstr);
            sw.Close();
        }
        public string  MyLogRead(string myPath, bool IsShowTime)
        {
            string ReadData;
            StreamReader sw = new StreamReader(myPath);
            ReadData = sw.ReadToEnd();
            return ReadData;
        }
    }
}
