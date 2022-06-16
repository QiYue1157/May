using System;

class Program
{
    static void Main(string[] args)
    {
        GetComList();
    }

    private static void GetComList()
    {
        try
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_PnPEntity"))
            {
                Console.WriteLine("本机串口：");

                var hardInfos = searcher.Get();
                int index = 1;
                foreach (var hardInfo in hardInfos)
                {
                    if (hardInfo.Properties["Name"].Value != null && hardInfo.Properties["Name"].Value.ToString().Contains("(COM"))
                    {
                        String strComName = hardInfo.Properties["Name"].Value.ToString();
                        Console.WriteLine(index + ":" + strComName);//打印串口设备名称及串口号
                        index += 1;
                    }
                }
            }
            Console.ReadKey();
        }
        catch
        {

        }
    }
}
