using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Print;
using System.Drawing;
using System.Drawing.Printing;

namespace TicketPrint
{
    public partial class P_Main : Form
    {
        /// <summary>
        /// 打印机名称
        /// </summary>
        public string PrintName = "GP-5890X Series";

        public P_Main()
        {
            InitializeComponent();
        }

        private void P_Main_Load(object sender, EventArgs e)
        {           
            PrintInit();
            SetPaperHeight();
            printPreviewDialog1.Document = Pd_Ticket;
            printPreviewDialog1.ShowDialog();
            //Pd_Ticket.Print();

            string Title = "A001";
        }

        /// <summary>
        /// 初始化打印机
        /// </summary>
        public void PrintInit() {
            Pd_Ticket.PrintController = new StandardPrintController();
            Pd_Ticket.PrinterSettings.PrinterName = PrintName;
            Pd_Ticket.PrintPage += new PrintPageEventHandler(Pd_Ticket_PrintPage);
            Pd_Ticket.EndPrint += new PrintEventHandler(Pd_Ticket_EndPrint);
        }

        void Pd_Ticket_EndPrint(object sender, PrintEventArgs e)
        {
            //LPT LPTPrint = new LPT("LPT1");
            //LPTPrint.Cut();
        }

        void Pd_Ticket_PrintPage(object sender, PrintPageEventArgs e)
        {


            int PaperWidth = Pd_Ticket.DefaultPageSettings.PaperSize.Width;
            int X = 0;
            int Y = 0;
            int Top = 25;
            Font Font = new Font(new FontFamily("微软雅黑"), 18, FontStyle.Bold);
            StringFormat StrFormat = new StringFormat()
            {
                Alignment = StringAlignment.Near
            };

            #region 订单基础信息

            /*绘制标题*/
            e.Graphics.DrawString("-----A001------", Font, Brushes.Black, new Rectangle(0, 0, PaperWidth, 0), StrFormat);
            Y = Y + 40;

            /*设置内容样式*/
            Font = new Font(new FontFamily("微软雅黑"), 9, FontStyle.Regular);
            StrFormat.Alignment = StringAlignment.Near;

            /*订单类型*/
            e.Graphics.DrawString("订单类型：【现场取餐】", Font, Brushes.Black, new Rectangle(X, Y, PaperWidth, 0), StrFormat);
            Y = Y + Top;
            /*订单类型*/
            e.Graphics.DrawString("取餐单号：A001", Font, Brushes.Black, new Rectangle(X, Y, PaperWidth, 0), StrFormat);
            Y = Y + Top;
            /*下单人*/
            e.Graphics.DrawString("   下单人：Asen", Font, Brushes.Black, new Rectangle(X, Y, PaperWidth, 0), StrFormat);
            Y = Y + Top;
            /*下单时间*/
            e.Graphics.DrawString("下单时间：9:45分", Font, Brushes.Black, new Rectangle(X, Y, PaperWidth, 0), StrFormat);
            Y = Y + Top + 10;
            
            #endregion


            #region 收货地址
            /*绘制收货地址标题*/
            StrFormat.Alignment = StringAlignment.Near;
            Font = new Font(new FontFamily("微软雅黑"), 18, FontStyle.Bold);
            e.Graphics.DrawString("----收货地址----", Font, Brushes.Black, new Rectangle(X, Y, PaperWidth, 0), StrFormat);
            Y = Y + Top + 15;


            StrFormat.Alignment = StringAlignment.Near;
            Font = new Font(new FontFamily("微软雅黑"), 10, FontStyle.Regular);

            /*收货人*/
            e.Graphics.DrawString("收货人：林先生", Font, Brushes.Black, new Rectangle(X, Y, PaperWidth, 0), StrFormat);
            Y = Y + Top;

            /*收货人*/
            e.Graphics.DrawString("电话：132679936868", Font, Brushes.Black, new Rectangle(X, Y, PaperWidth, 0), StrFormat);
            Y = Y + Top;

            /*地址*/
            e.Graphics.DrawString("地址：", Font, Brushes.Black, new Rectangle(X, Y, PaperWidth, 0), StrFormat);
            Y = Y + Top;

            string Loaction = "珠海市香洲区华南明宇三栋二单元";
            e.Graphics.DrawString(Loaction, Font, Brushes.Black, new Rectangle(X, Y, PaperWidth - 30, 0), StrFormat);
            Y = Y + Top;
            ComputeItemTop(13, Loaction, ref Y, 6, 0);

            #endregion

            #region 菜品信息

            /*绘制菜品标题*/
            StrFormat.Alignment = StringAlignment.Near;
            Font = new Font(new FontFamily("微软雅黑"), 18, FontStyle.Bold);
            e.Graphics.DrawString("------菜品------", Font, Brushes.Black, new Rectangle(X, Y, PaperWidth, 0), StrFormat);
            Y = Y + Top + 15;

            /*设置内容样式*/
            Font = new Font(new FontFamily("微软雅黑"), 10, FontStyle.Regular);
            StrFormat.Alignment = StringAlignment.Near;

            e.Graphics.DrawString("餐品名称", Font, Brushes.Black, new Rectangle(X, Y, PaperWidth, 0), StrFormat);
            e.Graphics.DrawString("数量", Font, Brushes.Black, new Rectangle(110, Y, PaperWidth, 0), StrFormat);
            e.Graphics.DrawString("价格", Font, Brushes.Black, new Rectangle(145, Y, PaperWidth, 0), StrFormat);
            Y = Y + Top;

            e.Graphics.DrawLine(new Pen(Color.Black, 2), 1, Y, PaperWidth, Y);
            Y = Y + 5;
            /*标题最多7个字*/
            /*下单时间*/

            /*DataItem*/
            List<string> MenuName = new List<string>();
            MenuName.Add("超级酸甜排骨");
            MenuName.Add("超级卤肉饭以");

            for (int i = 0; i < MenuName.Count; i++)
            {

                string foodName = MenuName[i];
                e.Graphics.DrawString(foodName, Font, Brushes.Black, new Rectangle(X, Y, 105, 0), StrFormat);
                e.Graphics.DrawString("*13", Font, Brushes.Black, new Rectangle(110, Y, PaperWidth, 0), StrFormat);
                e.Graphics.DrawString("¥1460", Font, Brushes.Black, new Rectangle(145, Y, PaperWidth, 0), StrFormat);
                Y = Y + Top;
                ComputeItemTop(7, foodName, ref Y, Top, 4);
            }


            e.Graphics.DrawLine(new Pen(Color.Black, 2), 1, Y, PaperWidth, Y);
            Y = Y + 5;

            #endregion



            #region 菜品总计

            /*打印总计*/
            Font = new Font(new FontFamily("微软雅黑"), 14, FontStyle.Bold);
            StrFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString("总计:¥40.5", Font, Brushes.Black, new Rectangle(-35, Y, PaperWidth, 0), StrFormat);
            #endregion

        }


        /// <summary>
        /// 设置任敏机打印内容的高度
        /// </summary>
        public void SetPaperHeight() {
            int OrderBaseHeight = 34;
            int AddressBaseHeight = 35;
            int MenuBaseHeight = 17 + (8 * 2);
            int SumBaseHeight = 8;


            GetOrderHeight(ref OrderBaseHeight);
            GetAddressHeight(ref AddressBaseHeight, "珠海市香洲区华南明宇三栋二单元1502");

            /*测试部分*/
            List<string> MenuName = new List<string>();
            MenuName.Add("超级酸甜排骨");
            MenuName.Add("超级卤肉饭以");

            for (int i = 0; i < MenuName.Count; i++)
            {
                GetMenuHeight(ref MenuBaseHeight, MenuName[i]);
            }
            int PageHeight = OrderBaseHeight + AddressBaseHeight + MenuBaseHeight + SumBaseHeight;
            Pd_Ticket.DefaultPageSettings.PaperSize = new PaperSize("Custom", Convert.ToInt32(58 / 25.4f * 100), Convert.ToInt32(PageHeight / 25.4f * 100));
        }

        /// <summary>
        /// 计算基础信息超出预算的高度
        /// </summary>
        /// <param name="height"></param>
        public void GetOrderHeight(ref int height) {


        }

        /// <summary>
        /// 计算菜单项超出预算的高度
        /// </summary>
        /// <param name="Height">菜单基础高度</param>
        /// <param name="MenuName">菜单名称</param>
        public void GetMenuHeight(ref int Height,string MenuName) {
            int MaxCount = 7;
            int Top = 6;
            ComputeBlockHeight(MenuName, MaxCount, ref Height, Top);
        }

        /// <summary>
        /// 计算地址超出预算的高度
        /// </summary>
        /// <param name="Height">地址基础高度</param>
        /// <param name="Loaction">详细地址信息</param>
        public void GetAddressHeight(ref int Height,string Loaction)
        {
            int MaxCount = 13;
            int Top = 6;
            ComputeBlockHeight(Loaction, MaxCount, ref Height, Top);
        }

        public void ComputeBlockHeight(string Content,int MaxCount,ref int height,int Top) {
            if (Content.Length > MaxCount)
            {
                double Row = Math.Floor((Convert.ToDouble(Content.Length / MaxCount)));
                int MarginTop = Top * Convert.ToInt32(Row );
                height = height + MarginTop;
            }
   
        }

        public void ComputeItemTop(int _MaxCount,string Content, ref int Y, int Top, int FillTop)
        {
            int MaxCount = _MaxCount;
            if (Content.Length > MaxCount)
            {
                double Row = Math.Floor((Convert.ToDouble(Content.Length / 7)));
                int MarginTop = (Top - FillTop) * Convert.ToInt32(Row);
                Y = Y + MarginTop;
            }
        }


        #region 【方法】自定义打印纸张类型/英寸为单位
        /// <summary>
        /// 自定义打印纸张类型
        /// </summary>
        /// <param name="PaperName">纸张名称</param>
        protected void SetPaperType(PrintDocument CurrentPrint, string PaperName, double Width_MM, double Height_MM)
        {
            Pd_Ticket.DefaultPageSettings.PaperSize = new PaperSize(PaperName, Convert.ToInt32(Width_MM / 25.4f * 100), Convert.ToInt32(Height_MM / 25.4f * 100));
        }
        #endregion






    }
}
