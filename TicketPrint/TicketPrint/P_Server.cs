using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Imaging;
using System.Management;
using System.Threading;

using System.Drawing;
using Microsoft.Win32.SafeHandles;

namespace TicketPrint
{
    public partial class P_Server : Form
    {
        public P_Server()
        {
            InitializeComponent();
        }



        /// <summary>
        /// OpenPrinter 打开指定的打印机，并获取打印机的句柄 
        /// </summary>
        /// <param name="szPrinter">要打开的打印机的名字</param>
        /// <param name="hPrinter">用于装载打印机的句柄</param>
        /// <param name="pd">PRINTER_DEFAULTS，这个结构保存要载入的打印机信息</param>
        /// <returns>bool</returns>
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);









        /// <summary>
        /// WritePrinter 将发送目录中的数据写入打印机 
        /// </summary>
        /// <param name="hPrinter">指定一个已打开的打印机的句柄（用openprinter取得）</param>
        /// <param name="pBytes">任何类型，包含了要写入打印机的数据的一个缓冲区或结构</param>
        /// <param name="dwCount">dwCount缓冲区的长度</param>
        /// <param name="dwWritten">指定一个Long型变量，用于装载实际写入的字节数</param>
        /// <returns>bool</returns>
        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, byte[] pBytes, Int32 dwCount, out Int32 dwWritten);


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }


        /// <summary>
        /// StartDocPrinter 在后台打印的级别启动一个新文档
        /// </summary>
        /// <param name="hPrinter">指定一个已打开的打印机的句柄（用openprinter取得）</param>
        /// <param name="level">1或2（仅用于win95）</param>
        /// <param name="di">包含一个DOC_INFO_1或DOC_INFO_2结构得缓冲区</param>
        /// <returns>bool 注: 在应用程序的级别并非有用。后台打印程序用它标识一个文档的开始</returns>
        [DllImport("Winspool.drv", EntryPoint = "StartDocPrinterW", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        /// <summary>
        /// StartPagePrinter 在打印作业中指定一个新页的开始 
        /// </summary>
        /// <param name="hPrinter">指定一个已打开的打印机的句柄（用openprinter取得）</param>
        /// <returns>bool注:在应用程序的级别并非特别有用</returns>
        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int StartPagePrinter(IntPtr hPrinter); 





        private void P_Server_Load(object sender, EventArgs e)
        {
            

            //   LPTControl CurrLptCtrl=new LPTControl("LPT1");
            //CurrLptCtrl.Open();
            //string teststr="123456";
            //foreach(char c in teststr){
            //    char  LF = (char)(10); 
            //    CurrLptCtrl.Write((c+LF).ToString());
            //}


            //CurrLptCtrl.Close();
            

            IntPtr Print=new IntPtr();
            IntPtr PrintMes=new IntPtr();
            int IsOpen=OpenPrinter("GP-5890X Series",out Print,PrintMes);
            
            int count = 0;

            byte[] kc =   System.Text.Encoding.ASCII.GetBytes("Asen");

            byte[] kc2 = System.Text.Encoding.ASCII.GetBytes("10");
            //GCHandle hObject = GCHandle.Alloc(kc, GCHandleType.Pinned);
            //GCHandle hObject2 = GCHandle.Alloc(kc2, GCHandleType.Pinned);
            //IntPtr pObject = hObject.AddrOfPinnedObject();
            //IntPtr pObject2 = hObject2.AddrOfPinnedObject();

            DOCINFOA Df = new DOCINFOA();
            Df.pDataType = "string";
            Df.pDocName = "测试";
            Df.pOutputFile = null;
            int test=StartDocPrinter(Print, 1, Df);
            int isprint=StartPagePrinter(Print);
           
            //bool pos=StartDocPrinter(Print, 3, Df);

            //StartPagePrinter(Print);
            bool pp=WritePrinter(Print, kc, kc.Length, out count);
            bool s= WritePrinter(Print, kc2, kc.Length, out count);
  
            //WritePrinter(Print, pObject2,1, out count);
            //WritePrinter(Print, kc, 30, out count);

            //WritePrinter(Print,);
            //Pd_Ticket.PrinterSettings.PrinterName = "GP-5890X Series";
            //Pd_Ticket.Print();
        }







    }




 /// <summary>
 /// LPTControl 的摘要说明。
 /// </summary>
 public class LPTControl
 {
  private string LptStr="lpt1";
  public LPTControl(string l_LPT_Str)
  {
   //
   // TODO: 在此处添加构造函数逻辑
   //
   LptStr=l_LPT_Str;
  }
  [StructLayout(LayoutKind.Sequential)]
   private struct OVERLAPPED
  {
   int Internal;
   int InternalHigh;
   int Offset;
   int OffSetHigh;
   int hEvent;
  }
  [DllImport("kernel32.dll")]
  private static extern int CreateFile(
   string lpFileName,
   uint dwDesiredAccess,
   int dwShareMode,
   int lpSecurityAttributes,
   int dwCreationDisposition,
   int dwFlagsAndAttributes,
   int hTemplateFile
   );
  [DllImport("kernel32.dll")]
  private static extern bool WriteFile(
   int hFile,
   byte[] lpBuffer,
   int nNumberOfBytesToWrite,
   ref int lpNumberOfBytesWritten,
   ref OVERLAPPED lpOverlapped
   );
  [DllImport("kernel32.dll")]
  private static extern bool CloseHandle(
   int hObject
   );
  private int iHandle;
  public bool Open()
  {
   iHandle = CreateFile(LptStr, 0x40000000, 0, 0, 3, 0, 0);
   if (iHandle != -1)
   {
    return true;
   }
   else
   {
    return false;
   }
  }
  public bool Write(String Mystring)
  {
   if (iHandle != -1)
   {
    OVERLAPPED x = new OVERLAPPED();
    int i = 0;

byte[] mybyte = System.Text.Encoding.Default.GetBytes(Mystring);
    bool b = WriteFile(iHandle, mybyte, mybyte.Length, ref i, ref x);
    return b;
   }
   else
   {
    throw new Exception("不能连接到打印机!");
   }
  }

public bool Write(byte[] mybyte)
  {
   if (iHandle != -1)
   {
    OVERLAPPED x = new OVERLAPPED();
    int i = 0;
    WriteFile(iHandle, mybyte, mybyte.Length,
     ref i, ref x);
    return true;
   }
   else
   {
    throw new Exception("不能连接到打印机!");
   }
  }
  public bool Close()
  {
   return CloseHandle(iHandle);
  }

}




}
