using ModemCtlApp.Code;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace ModemCtlApp
{
    public partial class DelSmsFrm : Form
    {
        public enum DeleteSmsType
        {
            已读短信 = 1,
            已读已发送 = 2,
            已读已发送未发送 = 3,
            全部 = 4
        }
        public DelSmsFrm()
        {
            InitializeComponent();
        }
        bool flag = true;//是否可关闭该窗口
        int waitCount = 0;
        int successCount = 0;
        List<Device> ds = new List<Device>(128);
        public List<Device> Devices
        {
            set { ds = value; }
        }
        public void AddDevice(Device device)
        {
            ds.Add(device);
        }
        private void Btn_Click(object sender, EventArgs e)
        {
            if (ds.Count > 0)
            {
                var tp = (int)((DeleteSmsType)Enum.Parse(typeof(DeleteSmsType), DeleteType.Text));
                string cmdText = string.Format("AT+CMGD=1,{0}", tp);
                flag = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
                {
                    for (int index = 0; index < ds.Count; index++)
                    {
                        var device = ds[index];
                        string str = device.ExecuteCommand(cmdText);
                        if(str.Contains("OK"))
                        {
                            waitCount--;
                            successCount++;
                            ChangeLabelCount();
                        }
                        Thread.Sleep(50);
                    }
                    flag = true;
                }), null);
            }
        }
        private void DelSmsFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!flag)
            {
                MessageBox.Show("短信删除中，不能关闭该窗口！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }
        public void ChangeLabelCount()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    ChangeLabelCount();
                }));
                return;
            }
            label1.Text = string.Format("待同步号码数：{0}", waitCount);
            label2.Text = string.Format("同步成功数：{0}", successCount);
            psb.Value = successCount;
        }
        public void ChangePs()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    ChangePs();
                }));
                return;
            }
            psb.Value = ds.Count;
        }
        private void DelSmsFrm_Load(object sender, EventArgs e)
        {
            waitCount = ds.Count;
            psb.Maximum = waitCount;
            ChangeLabelCount();
        }

    }
}
