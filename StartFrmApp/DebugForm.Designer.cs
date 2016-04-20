namespace ModemCtlApp
{
    partial class DebugForm
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
            this.comPort1 = new ItDreamWorks.Modem.ComPort();
            this.btn = new System.Windows.Forms.Button();
            this.txtCmd = new System.Windows.Forms.TextBox();
            this.txtShow = new System.Windows.Forms.TextBox();
            this.SendMsg = new System.Windows.Forms.Button();
            this.cbo = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // comPort1
            // 
            this.comPort1.BaudRate = 115200;
            this.comPort1.PortName = "COM16";
            // 
            // btn
            // 
            this.btn.Location = new System.Drawing.Point(245, 12);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(75, 23);
            this.btn.TabIndex = 0;
            this.btn.Text = "发送命令";
            this.btn.UseVisualStyleBackColor = true;
            this.btn.Click += new System.EventHandler(this.btn_Click);
            // 
            // txtCmd
            // 
            this.txtCmd.Location = new System.Drawing.Point(103, 12);
            this.txtCmd.Name = "txtCmd";
            this.txtCmd.Size = new System.Drawing.Size(136, 21);
            this.txtCmd.TabIndex = 1;
            // 
            // txtShow
            // 
            this.txtShow.Location = new System.Drawing.Point(12, 50);
            this.txtShow.Multiline = true;
            this.txtShow.Name = "txtShow";
            this.txtShow.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtShow.Size = new System.Drawing.Size(308, 199);
            this.txtShow.TabIndex = 2;
            this.txtShow.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtShow_MouseDoubleClick);
            // 
            // SendMsg
            // 
            this.SendMsg.Location = new System.Drawing.Point(348, 12);
            this.SendMsg.Name = "SendMsg";
            this.SendMsg.Size = new System.Drawing.Size(75, 23);
            this.SendMsg.TabIndex = 0;
            this.SendMsg.Text = "发送短信";
            this.SendMsg.UseVisualStyleBackColor = true;
            this.SendMsg.Click += new System.EventHandler(this.SendMsg_Click);
            // 
            // cbo
            // 
            this.cbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo.FormattingEnabled = true;
            this.cbo.Location = new System.Drawing.Point(13, 12);
            this.cbo.Name = "cbo";
            this.cbo.Size = new System.Drawing.Size(84, 20);
            this.cbo.TabIndex = 3;
            this.cbo.SelectedIndexChanged += new System.EventHandler(this.cbo_SelectedIndexChanged);
            // 
            // DebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 261);
            this.Controls.Add(this.cbo);
            this.Controls.Add(this.txtShow);
            this.Controls.Add(this.txtCmd);
            this.Controls.Add(this.SendMsg);
            this.Controls.Add(this.btn);
            this.Name = "DebugForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ItDreamWorks.Modem.ComPort comPort1;
        private System.Windows.Forms.Button btn;
        private System.Windows.Forms.TextBox txtCmd;
        private System.Windows.Forms.TextBox txtShow;
        private System.Windows.Forms.Button SendMsg;
        private System.Windows.Forms.ComboBox cbo;
    }
}

