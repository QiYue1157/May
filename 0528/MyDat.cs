using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;

namespace _0528
{
    class MyDat
    {
        public static void WriteDat()
        {
            //使用“另存为”对话框中输入的文件名实例化FileStream对象
            FileStream myStream = new FileStream("license.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            //使用FileStream对象实例化BinaryWriter二进制写入流对象
            BinaryWriter myWriter = new BinaryWriter(myStream);
            //以二进制方式向创建的文件中写入内容
            myWriter.Write("fdsfdsf");
            //关闭当前二进制写入流
            myWriter.Close();
            //关闭当前文件流
            myStream.Close();
        }
        public static string ReadDat()
        {

            FileStream myStream = new FileStream("license.dat", FileMode.Open, FileAccess.Read);
            //使用FileStream对象实例化BinaryReader二进制写入流对象
            BinaryReader myReader = new BinaryReader(myStream);
            if (myReader.PeekChar() != -1)
            {
                //以二进制方式读取文件中的内容
                return Convert.ToString(myReader.ReadString());
            }
            //关闭当前二进制读取流
            myReader.Close();
            //关闭当前文件流
            myStream.Close();
            return null;
        }
        private static byte[] key = new byte[] { 78, 56, 61, 94, 12, 88, 56, 63, 66, 111, 102, 77, 1, 186, 97, 45 };
        private static byte[] iv = new byte[] { 36, 34, 42, 122, 242, 87, 2, 90, 59, 117, 123, 63, 72, 171, 130, 61 };

        private static IFormatter S_Formatter = null;

        static MyDat()
        {
            S_Formatter = new BinaryFormatter();//创建一个序列化的对象
        }
        /// <summary>
        /// 采用Rijndael128位加密二进制可序列化对象至文件
        /// </summary>
        /// <param name="para">二进制对象</param>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static bool EncryptObject(object para, string filePath)
        {
            //创建.bat文件 如果之前错在.bat文件则覆盖，无则创建
            try
            {
                using (Stream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
                {
                    RijndaelManaged RMCrypto = new RijndaelManaged();
                    CryptoStream csEncrypt = new CryptoStream(fs, RMCrypto.CreateEncryptor(key, iv), CryptoStreamMode.Write);
                    S_Formatter.Serialize(csEncrypt, para);//将数据序列化后给csEncrypt
                    csEncrypt.Close();
                    fs.Close();
                    return true;
                }
            }
            catch (FileNotFoundException ex)
            {
                //System.Windows.Forms.MessageBox.Show("fileNotFound");
                //using (Stream fs = new FileStream(filePath, FileMode., FileAccess.Read, FileShare.None))
                //{
                //    RijndaelManaged RMCrypto = new RijndaelManaged();
                //    CryptoStream csEncrypt = new CryptoStream(fs, RMCrypto.CreateEncryptor(key, iv), CryptoStreamMode.Write);
                //    S_Formatter.Serialize(csEncrypt, para);//将数据序列化后给csEncrypt
                //    csEncrypt.Close();
                //    fs.Close();
                //    return true;
                //}
                return false;
            }
        }

        /// <summary>
        /// 从采用Rijndael128位加密的文件读取二进制对象
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>二进制对象</returns>
        public static object DecryptObject(string filePath)
        {
            //打开.bat文件
            using (Stream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                object para;
                RijndaelManaged RMCrypto = new RijndaelManaged();
                CryptoStream csEncrypt = new CryptoStream(fs, RMCrypto.CreateDecryptor(key, iv), CryptoStreamMode.Read);
                para = S_Formatter.Deserialize(csEncrypt); //将csEncrypt反序列化回原来的数据格式；
                csEncrypt.Close();
                fs.Close();
                return para;
            }
        }
    }
}
