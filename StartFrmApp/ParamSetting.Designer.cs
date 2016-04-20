namespace ModemCtlApp
{
    partial class ParamSetting
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btn_add_call_number = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_call_number = new System.Windows.Forms.TextBox();
            this.lb_call_number = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtMEM3 = new System.Windows.Forms.ComboBox();
            this.txtMEM2 = new System.Windows.Forms.ComboBox();
            this.txtMEM1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radReadLeave = new System.Windows.Forms.RadioButton();
            this.radReadDelete = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtServiceCenter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_ok = new System.Windows.Forms.Button();
            this.btn_cancle = new System.Windows.Forms.Button();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.cMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.txtMode = new System.Windows.Forms.ComboBox();
            this.txtMT = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.cMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(428, 285);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btn_add_call_number);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txt_call_number);
            this.tabPage1.Controls.Add(this.lb_call_number);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(420, 259);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "取号号码";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btn_add_call_number
            // 
            this.btn_add_call_number.Location = new System.Drawing.Point(332, 113);
            this.btn_add_call_number.Name = "btn_add_call_number";
            this.btn_add_call_number.Size = new System.Drawing.Size(75, 23);
            this.btn_add_call_number.TabIndex = 3;
            this.btn_add_call_number.Text = "添加";
            this.btn_add_call_number.UseVisualStyleBackColor = true;
            this.btn_add_call_number.Click += new System.EventHandler(this.btn_add_call_number_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(242, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "号码";
            // 
            // txt_call_number
            // 
            this.txt_call_number.Location = new System.Drawing.Point(244, 71);
            this.txt_call_number.Name = "txt_call_number";
            this.txt_call_number.Size = new System.Drawing.Size(163, 21);
            this.txt_call_number.TabIndex = 1;
            // 
            // lb_call_number
            // 
            this.lb_call_number.FormattingEnabled = true;
            this.lb_call_number.ItemHeight = 12;
            this.lb_call_number.Location = new System.Drawing.Point(6, 6);
            this.lb_call_number.Name = "lb_call_number";
            this.lb_call_number.Size = new System.Drawing.Size(222, 244);
            this.lb_call_number.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtMT);
            this.tabPage2.Controls.Add(this.txtMEM3);
            this.tabPage2.Controls.Add(this.txtMode);
            this.tabPage2.Controls.Add(this.txtMEM2);
            this.tabPage2.Controls.Add(this.txtMEM1);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.txtServiceCenter);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(420, 259);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "短信设置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtMEM3
            // 
            this.txtMEM3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtMEM3.FormattingEnabled = true;
            this.txtMEM3.Items.AddRange(new object[] {
            "ME",
            "SM",
            "MT"});
            this.txtMEM3.Location = new System.Drawing.Point(206, 59);
            this.txtMEM3.Name = "txtMEM3";
            this.txtMEM3.Size = new System.Drawing.Size(52, 20);
            this.txtMEM3.TabIndex = 3;
            // 
            // txtMEM2
            // 
            this.txtMEM2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtMEM2.FormattingEnabled = true;
            this.txtMEM2.Items.AddRange(new object[] {
            "ME",
            "SM",
            "MT"});
            this.txtMEM2.Location = new System.Drawing.Point(143, 59);
            this.txtMEM2.Name = "txtMEM2";
            this.txtMEM2.Size = new System.Drawing.Size(52, 20);
            this.txtMEM2.TabIndex = 3;
            // 
            // txtMEM1
            // 
            this.txtMEM1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtMEM1.FormattingEnabled = true;
            this.txtMEM1.Items.AddRange(new object[] {
            "ME",
            "SM",
            "MT"});
            this.txtMEM1.Location = new System.Drawing.Point(75, 59);
            this.txtMEM1.Name = "txtMEM1";
            this.txtMEM1.Size = new System.Drawing.Size(52, 20);
            this.txtMEM1.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "消息提示";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radReadLeave);
            this.panel1.Controls.Add(this.radReadDelete);
            this.panel1.Location = new System.Drawing.Point(75, 138);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 38);
            this.panel1.TabIndex = 2;
            // 
            // radReadLeave
            // 
            this.radReadLeave.AutoSize = true;
            this.radReadLeave.Location = new System.Drawing.Point(112, 12);
            this.radReadLeave.Name = "radReadLeave";
            this.radReadLeave.Size = new System.Drawing.Size(71, 16);
            this.radReadLeave.TabIndex = 0;
            this.radReadLeave.Text = "读取删除";
            this.radReadLeave.UseVisualStyleBackColor = true;
            // 
            // radReadDelete
            // 
            this.radReadDelete.AutoSize = true;
            this.radReadDelete.Checked = true;
            this.radReadDelete.Location = new System.Drawing.Point(3, 12);
            this.radReadDelete.Name = "radReadDelete";
            this.radReadDelete.Size = new System.Drawing.Size(71, 16);
            this.radReadDelete.TabIndex = 0;
            this.radReadDelete.TabStop = true;
            this.radReadDelete.Text = "读取保留";
            this.radReadDelete.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "读取之后";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "短信存储";
            // 
            // txtServiceCenter
            // 
            this.txtServiceCenter.Location = new System.Drawing.Point(75, 23);
            this.txtServiceCenter.Name = "txtServiceCenter";
            this.txtServiceCenter.Size = new System.Drawing.Size(183, 21);
            this.txtServiceCenter.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "短信中心";
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(241, 307);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 1;
            this.btn_ok.Text = "确定";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_cancle
            // 
            this.btn_cancle.Location = new System.Drawing.Point(362, 307);
            this.btn_cancle.Name = "btn_cancle";
            this.btn_cancle.Size = new System.Drawing.Size(75, 23);
            this.btn_cancle.TabIndex = 1;
            this.btn_cancle.Text = "取消";
            this.btn_cancle.UseVisualStyleBackColor = true;
            // 
            // ofd
            // 
            this.ofd.Filter = "文件|*.txt";
            // 
            // cMenuStrip
            // 
            this.cMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemDelete});
            this.cMenuStrip.Name = "cMenuStrip";
            this.cMenuStrip.Size = new System.Drawing.Size(101, 26);
            // 
            // MenuItemDelete
            // 
            this.MenuItemDelete.Name = "MenuItemDelete";
            this.MenuItemDelete.Size = new System.Drawing.Size(100, 22);
            this.MenuItemDelete.Text = "删除";
            this.MenuItemDelete.Click += new System.EventHandler(this.MenuItemDelete_Click);
            // 
            // txtMode
            // 
            this.txtMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtMode.FormattingEnabled = true;
            this.txtMode.Items.AddRange(new object[] {
            "先将通知缓存起来再按照mt的值进行发送",
            "在数据线空闲的情况下通知TE否则不通知TE",
            "数据线空闲时直接通知TE否则先将通知缓存起来待数据线空闲时再行发送",
            "直接通知TE在数据线被占用的情况下通知TE的消息将混合在数据中一起传输"});
            this.txtMode.Location = new System.Drawing.Point(75, 102);
            this.txtMode.Name = "txtMode";
            this.txtMode.Size = new System.Drawing.Size(85, 20);
            this.txtMode.TabIndex = 0;
            // 
            // txtMT
            // 
            this.txtMT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtMT.DropDownWidth = 85;
            this.txtMT.FormattingEnabled = true;
            this.txtMT.Items.AddRange(new object[] {
            "接受的短消息存储到默认的内存位置包括class3不通知TE",
            "接收的短消息储存到默认的内存位置, 并且向TE发出通知包括class3",
            "对于class2短消息储存到SIM卡并且向TE发出通知对于其他class直接将短消息转发到TE",
            "对于class3短消息直接转发到TE同mt2对于其他class同mt1"});
            this.txtMT.Location = new System.Drawing.Point(177, 102);
            this.txtMT.Name = "txtMT";
            this.txtMT.Size = new System.Drawing.Size(81, 20);
            this.txtMT.TabIndex = 0;
            // 
            // ParamSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 339);
            this.Controls.Add(this.btn_cancle);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ParamSetting";
            this.Text = "参数设置";
            this.Load += new System.EventHandler(this.ParamSetting_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.cMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_cancle;
        private System.Windows.Forms.Button btn_add_call_number;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_call_number;
        private System.Windows.Forms.ListBox lb_call_number;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.ContextMenuStrip cMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem MenuItemDelete;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtServiceCenter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radReadDelete;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radReadLeave;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox txtMEM3;
        private System.Windows.Forms.ComboBox txtMEM2;
        private System.Windows.Forms.ComboBox txtMEM1;
        private System.Windows.Forms.ComboBox txtMT;
        private System.Windows.Forms.ComboBox txtMode;
    }
}