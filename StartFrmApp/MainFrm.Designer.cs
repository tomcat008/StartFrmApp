namespace ModemCtlApp
{
    partial class MainFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tBtnConnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.tBtnGetDbPhoneNumner = new System.Windows.Forms.ToolStripMenuItem();
            this.tBtnCallGetPhoneNumber = new System.Windows.Forms.ToolStripMenuItem();
            this.tBtnSMSGetPhoneNumber = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tBtnRecvSMSing = new System.Windows.Forms.ToolStripButton();
            this.tBtnDo = new System.Windows.Forms.ToolStripButton();
            this.tBtnStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tBtnTask = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tBtnSetting = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tBtnDebug = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.ComTab = new System.Windows.Forms.TabPage();
            this.ComListView = new System.Windows.Forms.ListView();
            this.ComName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ConnectState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.WorkState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IMSI = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PhoneNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.ComCountLable = new System.Windows.Forms.ToolStripStatusLabel();
            this.WorkModelLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.LogBox = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ClearLogMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.GoTopMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.ComTab.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tBtnConnect,
            this.toolStripButton2,
            this.toolStripSeparator4,
            this.toolStripDropDownButton1,
            this.toolStripSeparator5,
            this.tBtnRecvSMSing,
            this.tBtnDo,
            this.tBtnStop,
            this.toolStripSeparator1,
            this.tBtnTask,
            this.toolStripSeparator2,
            this.tBtnSetting,
            this.toolStripSeparator3,
            this.tBtnDebug});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(989, 40);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tBtnConnect
            // 
            this.tBtnConnect.Image = ((System.Drawing.Image)(resources.GetObject("tBtnConnect.Image")));
            this.tBtnConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tBtnConnect.Name = "tBtnConnect";
            this.tBtnConnect.Size = new System.Drawing.Size(60, 37);
            this.tBtnConnect.Text = "连接设备";
            this.tBtnConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tBtnConnect.Click += new System.EventHandler(this.tBtnConnect_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(60, 37);
            this.toolStripButton2.Text = "断开设备";
            this.toolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton2.Click += new System.EventHandler(this.tBtnClose_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 40);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tBtnGetDbPhoneNumner,
            this.tBtnCallGetPhoneNumber,
            this.tBtnSMSGetPhoneNumber});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(69, 37);
            this.toolStripDropDownButton1.Text = "获取号码";
            this.toolStripDropDownButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tBtnGetDbPhoneNumner
            // 
            this.tBtnGetDbPhoneNumner.Name = "tBtnGetDbPhoneNumner";
            this.tBtnGetDbPhoneNumner.Size = new System.Drawing.Size(124, 22);
            this.tBtnGetDbPhoneNumner.Text = "网络取号";
            this.tBtnGetDbPhoneNumner.Click += new System.EventHandler(this.tBtnGetDbPhoneNumner_Click);
            // 
            // tBtnCallGetPhoneNumber
            // 
            this.tBtnCallGetPhoneNumber.Name = "tBtnCallGetPhoneNumber";
            this.tBtnCallGetPhoneNumber.Size = new System.Drawing.Size(124, 22);
            this.tBtnCallGetPhoneNumber.Text = "模式取号";
            this.tBtnCallGetPhoneNumber.Click += new System.EventHandler(this.tBtnTestCall_Click);
            // 
            // tBtnSMSGetPhoneNumber
            // 
            this.tBtnSMSGetPhoneNumber.Name = "tBtnSMSGetPhoneNumber";
            this.tBtnSMSGetPhoneNumber.Size = new System.Drawing.Size(124, 22);
            this.tBtnSMSGetPhoneNumber.Text = "短信取号";
            this.tBtnSMSGetPhoneNumber.Click += new System.EventHandler(this.tBtnSMSGetPhoneNumber_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 40);
            // 
            // tBtnRecvSMSing
            // 
            this.tBtnRecvSMSing.Image = ((System.Drawing.Image)(resources.GetObject("tBtnRecvSMSing.Image")));
            this.tBtnRecvSMSing.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tBtnRecvSMSing.Name = "tBtnRecvSMSing";
            this.tBtnRecvSMSing.Size = new System.Drawing.Size(60, 37);
            this.tBtnRecvSMSing.Text = "接收短信";
            this.tBtnRecvSMSing.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tBtnRecvSMSing.Click += new System.EventHandler(this.tBtnRecvSMSing_Click);
            // 
            // tBtnDo
            // 
            this.tBtnDo.Image = ((System.Drawing.Image)(resources.GetObject("tBtnDo.Image")));
            this.tBtnDo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tBtnDo.Name = "tBtnDo";
            this.tBtnDo.Size = new System.Drawing.Size(60, 37);
            this.tBtnDo.Text = "开始工作";
            this.tBtnDo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tBtnDo.Click += new System.EventHandler(this.tBtnDo_Click);
            // 
            // tBtnStop
            // 
            this.tBtnStop.Image = ((System.Drawing.Image)(resources.GetObject("tBtnStop.Image")));
            this.tBtnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tBtnStop.Name = "tBtnStop";
            this.tBtnStop.Size = new System.Drawing.Size(60, 37);
            this.tBtnStop.Text = "停止工作";
            this.tBtnStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tBtnStop.Click += new System.EventHandler(this.tBtnStop_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            // 
            // tBtnTask
            // 
            this.tBtnTask.Image = ((System.Drawing.Image)(resources.GetObject("tBtnTask.Image")));
            this.tBtnTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tBtnTask.Name = "tBtnTask";
            this.tBtnTask.Size = new System.Drawing.Size(60, 37);
            this.tBtnTask.Text = "任务管理";
            this.tBtnTask.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tBtnTask.Click += new System.EventHandler(this.tBtnTask_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 40);
            // 
            // tBtnSetting
            // 
            this.tBtnSetting.Image = ((System.Drawing.Image)(resources.GetObject("tBtnSetting.Image")));
            this.tBtnSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tBtnSetting.Name = "tBtnSetting";
            this.tBtnSetting.Size = new System.Drawing.Size(60, 37);
            this.tBtnSetting.Text = "参数设置";
            this.tBtnSetting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tBtnSetting.Click += new System.EventHandler(this.tBtnSetting_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 40);
            // 
            // tBtnDebug
            // 
            this.tBtnDebug.Image = ((System.Drawing.Image)(resources.GetObject("tBtnDebug.Image")));
            this.tBtnDebug.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tBtnDebug.Name = "tBtnDebug";
            this.tBtnDebug.Size = new System.Drawing.Size(60, 37);
            this.tBtnDebug.Text = "调试窗口";
            this.tBtnDebug.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tBtnDebug.Click += new System.EventHandler(this.tBtnDebug_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.ComTab);
            this.tabControl1.Location = new System.Drawing.Point(0, 43);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(989, 181);
            this.tabControl1.TabIndex = 4;
            // 
            // ComTab
            // 
            this.ComTab.Controls.Add(this.ComListView);
            this.ComTab.Location = new System.Drawing.Point(4, 22);
            this.ComTab.Name = "ComTab";
            this.ComTab.Padding = new System.Windows.Forms.Padding(3);
            this.ComTab.Size = new System.Drawing.Size(981, 155);
            this.ComTab.TabIndex = 0;
            this.ComTab.Text = "设备列表";
            this.ComTab.UseVisualStyleBackColor = true;
            // 
            // ComListView
            // 
            this.ComListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ComListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ComName,
            this.ConnectState,
            this.WorkState,
            this.IMSI,
            this.PhoneNumber});
            this.ComListView.FullRowSelect = true;
            this.ComListView.GridLines = true;
            this.ComListView.Location = new System.Drawing.Point(6, 6);
            this.ComListView.MultiSelect = false;
            this.ComListView.Name = "ComListView";
            this.ComListView.Size = new System.Drawing.Size(969, 143);
            this.ComListView.TabIndex = 1;
            this.ComListView.UseCompatibleStateImageBehavior = false;
            this.ComListView.View = System.Windows.Forms.View.Details;
            // 
            // ComName
            // 
            this.ComName.Text = "端口号";
            // 
            // ConnectState
            // 
            this.ConnectState.Text = "连接状态";
            // 
            // WorkState
            // 
            this.WorkState.Text = "工作状态";
            // 
            // IMSI
            // 
            this.IMSI.Text = "IMSI";
            // 
            // PhoneNumber
            // 
            this.PhoneNumber.Text = "本机号码";
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ComCountLable,
            this.WorkModelLabel});
            this.statusBar.Location = new System.Drawing.Point(0, 376);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(989, 22);
            this.statusBar.TabIndex = 6;
            this.statusBar.Text = "statusStrip1";
            // 
            // ComCountLable
            // 
            this.ComCountLable.Name = "ComCountLable";
            this.ComCountLable.Size = new System.Drawing.Size(131, 17);
            this.ComCountLable.Text = "toolStripStatusLabel1";
            // 
            // WorkModelLabel
            // 
            this.WorkModelLabel.Name = "WorkModelLabel";
            this.WorkModelLabel.Size = new System.Drawing.Size(131, 17);
            this.WorkModelLabel.Text = "toolStripStatusLabel1";
            // 
            // LogBox
            // 
            this.LogBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogBox.BackColor = System.Drawing.Color.White;
            this.LogBox.ContextMenuStrip = this.contextMenuStrip1;
            this.LogBox.Location = new System.Drawing.Point(10, 230);
            this.LogBox.Multiline = true;
            this.LogBox.Name = "LogBox";
            this.LogBox.ReadOnly = true;
            this.LogBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogBox.Size = new System.Drawing.Size(969, 143);
            this.LogBox.TabIndex = 7;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClearLogMenuItem,
            this.toolStripSeparator6,
            this.GoTopMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 54);
            // 
            // ClearLogMenuItem
            // 
            this.ClearLogMenuItem.Name = "ClearLogMenuItem";
            this.ClearLogMenuItem.Size = new System.Drawing.Size(124, 22);
            this.ClearLogMenuItem.Text = "清空日志";
            this.ClearLogMenuItem.Click += new System.EventHandler(this.ClearLogMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(121, 6);
            // 
            // GoTopMenuItem
            // 
            this.GoTopMenuItem.Name = "GoTopMenuItem";
            this.GoTopMenuItem.Size = new System.Drawing.Size(124, 22);
            this.GoTopMenuItem.Text = "回到顶端";
            this.GoTopMenuItem.Click += new System.EventHandler(this.GoTopMenuItem_Click);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(989, 398);
            this.Controls.Add(this.LogBox);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MainFrm";
            this.Text = "MainFrm";
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ComTab.ResumeLayout(false);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tBtnConnect;
        private System.Windows.Forms.ToolStripButton tBtnDo;
        private System.Windows.Forms.ToolStripButton tBtnStop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tBtnTask;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage ComTab;
        private System.Windows.Forms.ListView ComListView;
        private System.Windows.Forms.ColumnHeader ComName;
        private System.Windows.Forms.ColumnHeader ConnectState;
        private System.Windows.Forms.ColumnHeader WorkState;
        private System.Windows.Forms.ColumnHeader IMSI;
        private System.Windows.Forms.ColumnHeader PhoneNumber;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tBtnSetting;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tBtnDebug;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem tBtnGetDbPhoneNumner;
        private System.Windows.Forms.ToolStripMenuItem tBtnCallGetPhoneNumber;
        private System.Windows.Forms.ToolStripMenuItem tBtnSMSGetPhoneNumber;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel ComCountLable;
        private System.Windows.Forms.ToolStripStatusLabel WorkModelLabel;
        private System.Windows.Forms.TextBox LogBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ClearLogMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem GoTopMenuItem;
        private System.Windows.Forms.ToolStripButton tBtnRecvSMSing;
    }
}