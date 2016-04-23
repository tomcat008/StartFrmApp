using ModemCtlApp.Code;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace ModemCtlApp
{
    public partial class SynFrm : Form
    {
        public SynFrm()
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
            if(ds.Count > 0)
            {
                flag = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback((obj) => {
                    for(int index = 0; index < ds.Count; index++)
                    {
                        var device = ds[index];
                        HttpRequestHelper.RequestData data = new HttpRequestHelper.RequestData();

                        data.Method = HttpRequestHelper.RequestMethod.PUT;
                        data.URL = "";
                        data.ContentType = "application/x-www-form-urlencoded";

                        data.AddKeyValue(HttpRequestHelper.RequestData.RequestDataKey.IMSI, device.IMSI);
                        data.AddKeyValue(HttpRequestHelper.RequestData.RequestDataKey.CCID, device.CCID);
                        data.AddKeyValue(HttpRequestHelper.RequestData.RequestDataKey.PhoneNumber, device.PhoneNumber);

                        HttpRequestHelper.GetHelper().AddRequestDataForThread(data);
                        waitCount--;
                        successCount++;
                        ChangeLabelCount();
                        Thread.Sleep(50);
                    }
                    flag = true;
                }), null);
            }
        }
        private void SynFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!flag)
            {
                MessageBox.Show("数据同步进行中，不能关闭该窗口！","错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }
        public void ChangeLabelCount()
        {
            if(this.InvokeRequired)
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
        private void SynFrm_Load(object sender, EventArgs e)
        {
            waitCount = ds.Count;
            psb.Maximum = waitCount;
            ChangeLabelCount();
        }
    }
}
