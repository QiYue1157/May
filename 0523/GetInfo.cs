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
                Console.WriteLine("�������ڣ�");

                var hardInfos = searcher.Get();
                int index = 1;
                foreach (var hardInfo in hardInfos)
                {
                    if (hardInfo.Properties["Name"].Value != null && hardInfo.Properties["Name"].Value.ToString().Contains("(COM"))
                    {
                        String strComName = hardInfo.Properties["Name"].Value.ToString();
                        Console.WriteLine(index + ":" + strComName);//��ӡ�����豸���Ƽ����ں�
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
