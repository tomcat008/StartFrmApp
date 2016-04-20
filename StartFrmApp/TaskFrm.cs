using System;
using System.Windows.Forms;
using System.Collections.Generic;
using ModemCtlApp.Code;

namespace ModemCtlApp
{
    public partial class TaskFrm : Form
    {
        public TaskFrm()
        {
            InitializeComponent();
        }
        Code.TaskList taskList;
        private void TaskFrm_Load(object sender, EventArgs e)
        {
            foreach(var c in AppConfig.BaseCommands)
            {
                CommandList.Items.Add(c.CommandName);
            }
            taskList = this.Tag as Code.TaskList;
            
            InitTv();
        }

        public string[] ComNames { get; private set; }

        private void InitTv()
        {
            TaskTV.Nodes.Clear();
            TreeNode pervNode = null;
            for (var i = 0; i < taskList.TaskLink.Count; i++)
            {
                if (null == pervNode)
                {
                    pervNode = new TreeNode(taskList.TaskLink[i].TaskName);
                    pervNode.Tag = i;
                    TaskTV.Nodes.Add(pervNode);
                }
                else
                {
                    TreeNode theNode = new TreeNode(taskList.TaskLink[i].TaskName);
                    theNode.Tag = i;
                    pervNode.Nodes.Add(theNode);
                    pervNode = theNode;
                }
            }
            TaskTV.ExpandAll();
        }

        private void TaskTV_AfterSelect(object sender, TreeViewEventArgs e)
        {
            InitControl();
            var i = Convert.ToInt32(e.Node.Tag);
            var task = taskList.TaskLink[i];
            var cmd = task.Command;
            taskName.Text = task.TaskName;
            CommandList.Text = cmd.CommandName;
            Paramter.Text = cmd.Parameter;
            Duration.Text = cmd.Duration.ToString();
            if (cmd.ThisType == Command.CommandType.SendSMSCmd)
            {
                var sscmd = cmd as SendSMSCommand;
                Msg.Text = sscmd.MsgString;
            }
            btn2.Tag = i;
        }

        private void ModifyTask(object sender, EventArgs e)
        {
            var btn2 = sender as Button;
            
            var i = Convert.ToInt32(btn2.Tag);
            var task = taskList.TaskLink[i];
            var cmd = task.Command;
            task.TaskName = taskName.Text;

            cmd.CommandName = CommandList.Text;
            cmd.Parameter = Paramter.Text;

            foreach (var c in AppConfig.BaseCommands)
            {
                if (c.CommandName == CommandList.Text)
                {
                    cmd.ATCommandText = c.ATCommandText;
                    cmd.ThisType = c.ThisType;
                    break;
                }
            }


            if (cmd.ThisType == Command.CommandType.SendSMSCmd)
            {
                var sscmd = cmd as SendSMSCommand;
                Msg.Text = sscmd.MsgString;
            }
            if(!string.IsNullOrEmpty(Duration.Text))
            {
                cmd.Duration = int.Parse(Duration.Text);
            }
            btn2.Text = "添加";
            InitControl();
            InitTv();
        }

        private void AddTask(object sender, EventArgs e)
        {
            var btn = sender as Button;
            Command cmd = null;
            foreach (var c in AppConfig.BaseCommands)
            {
                if (c.CommandName == CommandList.Text)
                {
                    
                    if (c.ThisType == Command.CommandType.SendSMSCmd)
                    {
                        var scmd = new SendSMSCommand();
                        scmd.CommandName = c.CommandName;
                        scmd.ATCommandText = c.ATCommandText;
                        scmd.Parameter = Paramter.Text;

                        scmd.Msg = Msg.Text;
                        cmd = scmd;
                    }
                    else
                    {
                        cmd = new Command();
                        cmd.CommandName = c.CommandName;
                        cmd.ATCommandText = c.ATCommandText;
                        cmd.Parameter = Paramter.Text;
                        cmd.ThisType = c.ThisType;
                        if (!string.IsNullOrEmpty(Duration.Text))
                        {
                            cmd.Duration = int.Parse(Duration.Text);
                        }
                    }
                    
                    break;
                }
            }
            if (null != cmd)
            {
                GsmTask task = new GsmTask(taskName.Text, cmd);
                taskList.TaskLink.Add(task);
                InitControl();
                InitTv();
            }
        }

        private void InitControl()
        {
            taskName.Text = string.Empty;
            CommandList.Text = string.Empty;
            Paramter.Text = string.Empty;
            Msg.Text = string.Empty;
            Duration.Text = string.Empty;
        }

        private void DeleteTaskItem_Click(object sender, EventArgs e)
        {
            if (null == TaskTV.SelectedNode)
            {
                MessageBox.Show("请选择要删除的任务。", "错误",  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var result = MessageBox.Show("是否确认删除该任务？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                var i = Convert.ToInt32(TaskTV.SelectedNode.Tag);
                taskList.TaskLink.RemoveAt(i);
                InitControl();
                InitTv();
            }

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            //设置任务序列类型
            if(TaskForAll.Checked)
            {
                taskList.TheType = TaskList.TaskListType.AllPortTaskList;
                this.ComNames = null;
            }
            else
            {
                taskList.TheType = TaskList.TaskListType.SomePortTaskList;
                ComNames = ComList.Text.Split('\r','\n');
            }
            //设置任务系列循环信息
            if(AllR.Checked)
            {
                taskList.AllCount = (int)AllLoop.Value;
            }
            else
            {
                taskList.AllCount = 0;
                taskList.ItemCount = (int)SingleLoop.Value;
            }
            this.DialogResult = DialogResult.OK;
            this.FormClosing -= TaskFrm_FormClosing;
            this.Close();
        }

        private void TaskFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("是否确认关闭任务窗口？","提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void TaskForAll_CheckedChanged(object sender, EventArgs e)
        {
            ComList.Enabled = false;
        }

        private void TaskForSome_CheckedChanged(object sender, EventArgs e)
        {
            ComList.Enabled = true;
        }

        private void AllR_CheckedChanged(object sender, EventArgs e)
        {
            SingleLoop.Enabled = false;
        }

        private void SingleR_CheckedChanged(object sender, EventArgs e)
        {
            AllLoop.Enabled = false;
        }
    }
}
