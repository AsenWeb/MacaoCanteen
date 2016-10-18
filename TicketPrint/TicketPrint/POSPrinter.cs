using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
namespace TicketPrint
{
    public class POSPrinter
    {
const int OPEN_EXISTING = 3;

        string prnPort = "LPT1";
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr CreateFile(string lpFileName,
        int dwDesiredAccess,
        int dwShareMode,
        int lpSecurityAttributes,
        int dwCreationDisposition,
        int dwFlagsAndAttributes,
        int hTemplateFile);
        public POSPrinter()
        {
        
        }
        public POSPrinter(string prnPort)
        {
            this.prnPort = prnPort;//打印机端口
        }
        public string PrintLine(string str)
        {
            IntPtr iHandle = CreateFile(prnPort, 0x0A, 0, 0, OPEN_EXISTING, 0, 0);
            if (iHandle.ToInt32() == -1)
            {
                Console.WriteLine(iHandle.ToString());
                return "没有连接打印机或者打印机端口不是LPT1";
            }
            else
            {
                Console.WriteLine(iHandle.ToString());
                FileStream fs = new FileStream(iHandle, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
                sw.WriteLine("           小票单");
                sw.WriteLine();
                sw.WriteLine(str);
                sw.WriteLine("打印内容");
                sw.WriteLine("---------------------------");
                
                sw.Close();
                fs.Close();
                return "打印成功!";
            }
        }

    }
}
