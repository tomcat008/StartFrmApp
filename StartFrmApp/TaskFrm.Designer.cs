namespace ModemCtlApp
{
    partial class TaskFrm
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
            this.TaskTV = new System.Windows.Forms.TreeView();
            this.CMS = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteTaskItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.taskName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CommandList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Paramter = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Msg = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.BtnSave = new System.Windows.Forms.Button();
            this.btn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.Duration = new System.Windows.Forms.TextBox();
            this.btn2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TaskForAll = new System.Windows.Forms.RadioButton();
            this.TaskForSome = new System.Windows.Forms.RadioButton();
            this.ComList = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SingleR = new System.Windows.Forms.RadioButton();
            this.AllR = new System.Windows.Forms.RadioButton();
            this.AllLoop = new System.Windows.Forms.NumericUpDown();
            this.SingleLoop = new System.Windows.Forms.NumericUpDown();
            this.CMS.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AllLoop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SingleLoop)).BeginInit();
            this.SuspendLayout();
            // 
            // TaskTV
            // 
            this.TaskTV.ContextMenuStrip = this.CMS;
            this.TaskTV.Location = new System.Drawing.Point(12, 12);
            this.TaskTV.Name = "TaskTV";
            this.TaskTV.Size = new System.Drawing.Size(210, 362);
            this.TaskTV.TabIndex = 0;
            this.TaskTV.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TaskTV_AfterSelect);
            // 
            // CMS
            // 
            this.CMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteTaskItem});
            this.CMS.Name = "CMS";
            this.CMS.Size = new System.Drawing.Size(125, 26);
            // 
            // DeleteTaskItem
            // 
            this.DeleteTaskItem.Name = "DeleteTaskItem";
            this.DeleteTaskItem.Size = new System.Drawing.Size(124, 22);
            this.DeleteTaskItem.Text = "删除任务";
            this.DeleteTaskItem.Click += new System.EventHandler(this.DeleteTaskItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(248, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "任务名称";
            // 
            // taskName
            // 
            this.taskName.Location = new System.Drawing.Point(307, 12);
            this.taskName.Name = "taskName";
            this.taskName.Size = new System.Drawing.Size(357, 21);
            this.taskName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(248, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "执行操作";
            // 
            // CommandList
            // 
            this.CommandList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CommandList.FormattingEnabled = true;
            this.CommandList.Location = new System.Drawing.Point(307, 45);
            this.CommandList.Name = "CommandList";
            this.CommandList.Size = new System.Drawing.Size(357, 20);
            this.CommandList.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(248, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "命令参数";
            // 
            // Paramter
            // 
            this.Paramter.Location = new System.Drawing.Point(307, 77);
            this.Paramter.Name = "Paramter";
            this.Paramter.Size = new System.Drawing.Size(357, 21);
            this.Paramter.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Msg);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(228, 141);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(445, 88);
            this.panel1.TabIndex = 6;
            // 
            // Msg
            // 
            this.Msg.Location = new System.Drawing.Point(79, 6);
            this.Msg.MaxLength = 60;
            this.Msg.Multiline = true;
            this.Msg.Name = "Msg";
            this.Msg.Size = new System.Drawing.Size(357, 75);
            this.Msg.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "短信内容";
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(307, 351);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(92, 23);
            this.BtnSave.TabIndex = 7;
            this.BtnSave.Text = "保存任务计划";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btn
            // 
            this.btn.Location = new System.Drawing.Point(450, 351);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(75, 23);
            this.btn.TabIndex = 7;
            this.btn.Text = "添加";
            this.btn.UseVisualStyleBackColor = true;
            this.btn.Click += new System.EventHandler(this.AddTask);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(248, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "持续时间";
            // 
            // Duration
            // 
            this.Duration.Location = new System.Drawing.Point(307, 110);
            this.Duration.Name = "Duration";
            this.Duration.Size = new System.Drawing.Size(357, 21);
            this.Duration.TabIndex = 5;
            // 
            // btn2
            // 
            this.btn2.Location = new System.Drawing.Point(589, 351);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(75, 23);
            this.btn2.TabIndex = 7;
            this.btn2.Text = "修改";
            this.btn2.UseVisualStyleBackColor = true;
            this.btn2.Click += new System.EventHandler(this.ModifyTask);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ComList);
            this.groupBox1.Controls.Add(this.TaskForSome);
            this.groupBox1.Controls.Add(this.TaskForAll);
            this.groupBox1.Location = new System.Drawing.Point(250, 235);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(214, 110);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "任务序列指派";
            // 
            // TaskForAll
            // 
            this.TaskForAll.AutoSize = true;
            this.TaskForAll.Checked = true;
            this.TaskForAll.Location = new System.Drawing.Point(10, 20);
            this.TaskForAll.Name = "TaskForAll";
            this.TaskForAll.Size = new System.Drawing.Size(95, 16);
            this.TaskForAll.TabIndex = 0;
            this.TaskForAll.Text = "全局任务序列";
            this.TaskForAll.UseVisualStyleBackColor = true;
            this.TaskForAll.CheckedChanged += new System.EventHandler(this.TaskForAll_CheckedChanged);
            // 
            // TaskForSome
            // 
            this.TaskForSome.AutoSize = true;
            this.TaskForSome.Location = new System.Drawing.Point(111, 20);
            this.TaskForSome.Name = "TaskForSome";
            this.TaskForSome.Size = new System.Drawing.Size(95, 16);
            this.TaskForSome.TabIndex = 0;
            this.TaskForSome.Text = "特殊任务序列";
            this.TaskForSome.UseVisualStyleBackColor = true;
            this.TaskForSome.CheckedChanged += new System.EventHandler(this.TaskForSome_CheckedChanged);
            // 
            // ComList
            // 
            this.ComList.Enabled = false;
            this.ComList.Location = new System.Drawing.Point(10, 42);
            this.ComList.Multiline = true;
            this.ComList.Name = "ComList";
            this.ComList.Size = new System.Drawing.Size(196, 62);
            this.ComList.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.SingleLoop);
            this.groupBox2.Controls.Add(this.AllLoop);
            this.groupBox2.Controls.Add(this.SingleR);
            this.groupBox2.Controls.Add(this.AllR);
            this.groupBox2.Location = new System.Drawing.Point(470, 235);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(194, 110);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "任务序列循环";
            // 
            // SingleR
            // 
            this.SingleR.AutoSize = true;
            this.SingleR.Location = new System.Drawing.Point(94, 20);
            this.SingleR.Name = "SingleR";
            this.SingleR.Size = new System.Drawing.Size(71, 16);
            this.SingleR.TabIndex = 0;
            this.SingleR.Text = "单项循环";
            this.SingleR.UseVisualStyleBackColor = true;
            this.SingleR.CheckedChanged += new System.EventHandler(this.SingleR_CheckedChanged);
            // 
            // AllR
            // 
            this.AllR.AutoSize = true;
            this.AllR.Checked = true;
            this.AllR.Location = new System.Drawing.Point(10, 20);
            this.AllR.Name = "AllR";
            this.AllR.Size = new System.Drawing.Size(71, 16);
            this.AllR.TabIndex = 0;
            this.AllR.Text = "全局循环";
            this.AllR.UseVisualStyleBackColor = true;
            this.AllR.CheckedChanged += new System.EventHandler(this.AllR_CheckedChanged);
            // 
            // AllLoop
            // 
            this.AllLoop.Location = new System.Drawing.Point(10, 43);
            this.AllLoop.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.AllLoop.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.AllLoop.Name = "AllLoop";
            this.AllLoop.Size = new System.Drawing.Size(70, 21);
            this.AllLoop.TabIndex = 1;
            this.AllLoop.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // SingleLoop
            // 
            this.SingleLoop.Enabled = false;
            this.SingleLoop.Location = new System.Drawing.Point(94, 43);
            this.SingleLoop.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.SingleLoop.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SingleLoop.Name = "SingleLoop";
            this.SingleLoop.Size = new System.Drawing.Size(70, 21);
            this.SingleLoop.TabIndex = 2;
            this.SingleLoop.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // TaskFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 386);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn2);
            this.Controls.Add(this.btn);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Duration);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Paramter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CommandList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.taskName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TaskTV);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TaskFrm";
            this.Text = "任务管理";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TaskFrm_FormClosing);
            this.Load += new System.EventHandler(this.TaskFrm_Load);
            this.CMS.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AllLoop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SingleLoop)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView TaskTV;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox taskName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CommandList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Paramter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox Msg;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button btn;
        private System.Windows.Forms.ContextMenuStrip CMS;
        private System.Windows.Forms.ToolStripMenuItem DeleteTaskItem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Duration;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton TaskForSome;
        private System.Windows.Forms.RadioButton TaskForAll;
        private System.Windows.Forms.TextBox ComList;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton SingleR;
        private System.Windows.Forms.RadioButton AllR;
        private System.Windows.Forms.NumericUpDown AllLoop;
        private System.Windows.Forms.NumericUpDown SingleLoop;
    }
}