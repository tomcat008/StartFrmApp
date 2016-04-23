namespace ModemCtlApp
{
    partial class DelSmsFrm
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
            this.Btn = new System.Windows.Forms.Button();
            this.psb = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DeleteType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // Btn
            // 
            this.Btn.Location = new System.Drawing.Point(322, 13);
            this.Btn.Name = "Btn";
            this.Btn.Size = new System.Drawing.Size(75, 23);
            this.Btn.TabIndex = 7;
            this.Btn.Text = "开始删除";
            this.Btn.UseVisualStyleBackColor = true;
            this.Btn.Click += new System.EventHandler(this.Btn_Click);
            // 
            // psb
            // 
            this.psb.Location = new System.Drawing.Point(8, 53);
            this.psb.Name = "psb";
            this.psb.Size = new System.Drawing.Size(389, 23);
            this.psb.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(125, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "删除成功数：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "待删除设备数：";
            // 
            // DeleteType
            // 
            this.DeleteType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeleteType.FormattingEnabled = true;
            this.DeleteType.Items.AddRange(new object[] {
            "已读短信",
            "已读已发送",
            "已读已发送未发送",
            "全部"});
            this.DeleteType.Location = new System.Drawing.Point(234, 15);
            this.DeleteType.Name = "DeleteType";
            this.DeleteType.Size = new System.Drawing.Size(72, 20);
            this.DeleteType.TabIndex = 8;
            // 
            // DelSmsFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 94);
            this.Controls.Add(this.DeleteType);
            this.Controls.Add(this.Btn);
            this.Controls.Add(this.psb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DelSmsFrm";
            this.Text = "删除短信";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DelSmsFrm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn;
        private System.Windows.Forms.ProgressBar psb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox DeleteType;
    }
}