﻿namespace TicketPrint
{
    partial class P_Server
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Pd_Ticket = new System.Drawing.Printing.PrintDocument();
            this.SuspendLayout();
            // 
            // P_Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 263);
            this.Name = "P_Server";
            this.Text = "点餐小票打印";
            this.Load += new System.EventHandler(this.P_Server_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Drawing.Printing.PrintDocument Pd_Ticket;
    }
}

