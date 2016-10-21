using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Messaging;


namespace MacaoCanteenService
{
    public partial class M_Main : Form
    {
        public string QueenName = @".\Private$\MaCaoQueen";

        public M_Main()
        {

            InitializeComponent();
        }

        private void M_Main_Load(object sender, EventArgs e)
        {
            CreateQueen();
        }


       /*
        SQL缓存依赖，需要一个一个表添加,webConfig的数据库连接不能用.edmx的，最好自己写一个数据库连接。
        C:/WINDOWS/Microsoft.NET/Framework/v2.0.50727/aspnet_regsql -S localhost -E -d  MSPetShop4 -ed      
        C:\Users\Administrator>aspnet_regsql.exe -S . -U sa -P 123 -ed -d MacaoCateen -et -t Banner

        */
        /*Cache
         * 
                SqlCacheDependencyAdmin.EnableNotifications(System.Configuration.ConfigurationManager.ConnectionStrings["Ma"].ConnectionString);
                SqlCacheDependencyAdmin.EnableTableForNotifications(System.Configuration.ConfigurationManager.ConnectionStrings["Ma"].ConnectionString, "Banner");
                SqlCacheDependency scd = new SqlCacheDependency("MacaoCanteen", "Banner");
                if (HttpRuntime.Cache.Get("test") != null)
                {
                    string ioi = HttpRuntime.Cache.Get("test").ToString();
                }
           
                if (HttpRuntime.Cache.Get("test") == null)
                {
                    HttpRuntime.Cache.Insert("test", "12333", scd);
                }
         */


        //发送Mes,使用Json格式转换发送吧。
        //public void SendMes() {
        //    Banner _Banner = DB.Banner.Where(i => i.Id == 1).ToList()[0];
        //    MessageQueue mq = new MessageQueue(@".\Private$\MaCaoQueen");
        //    mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
        //    mq.Send(_Banner);
        //}

        #region 创建消息队列
        /// <summary>
        /// 创建消息队列
        /// </summary>
        public void CreateQueen() {
            MessageQueue MesQueen = null;
            if (MessageQueue.Exists(QueenName))
            {
                MessageQueue.Delete(QueenName);
                MesQueen = MessageQueue.Create(QueenName);
            }
            else
            {
                MesQueen = MessageQueue.Create(QueenName);
            }
            MesQueen.ReceiveCompleted += new ReceiveCompletedEventHandler(QueenReceiveCompleted);
            MesQueen.BeginReceive();        
        }
        #endregion

        #region 【方法】异步获取消息队列
        /// <summary>
        /// 异步获取消息队列
        /// </summary>
        /// <param name="source">消息队列对象</param>
        /// <param name="asyncResult">异步对象</param>
        public void QueenReceiveCompleted(Object source, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue MesQueen = (MessageQueue)source;
            try
            {
                
                MesQueen.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                System.Messaging.Message Content = MesQueen.EndReceive(asyncResult.AsyncResult);
                string Mes = Content.Body.ToString();
                
            }
            catch(MessageQueueException MQEx){
                MesQueen.BeginReceive();
            
            }
            return;
        }
        #endregion
    }

}
