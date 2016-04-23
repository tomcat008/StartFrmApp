using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Configuration;
using System.Linq;
using ModemCtlApp.Code;

namespace ModemCtlApp
{
    public partial class MainFrm : Form
    {
        protected class ListViewSetting
        {
            public enum ColumnIndex
            {
                端口号 = 0, 连接状态 = 1, 工作状态 = 2, 本机号码 = 3, IMSI = 4, CCID = 5
            }
        }
        public MainFrm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 发送测号短信的手机号码
        /// </summary>
        string SendMsgPhoneNumber = string.Empty;
        /// <summary>
        /// 所有设备集合
        /// </summary>
        List<Device> AllDevices = null;
        /// <summary>
        /// 所有可用设备集合
        /// </summary>
        List<Device> ReadyDevices = null;
        /// <summary>
        /// 所有不可用设备集合
        /// </summary>
        List<Device> DisableDevices = null;
        /// <summary>
        /// 呼叫号码集合
        /// </summary>
        List<CallNumber> CallNumbers = null;
        /// <summary>
        /// 工作任务链表
        /// </summary>
        TaskList taskList;

        /// <summary>
        /// 每4个设备一个线程负责处理检测IMSI
        /// </summary>
        int splitCount = 4;

        /// <summary>
        /// 可用设备集合线程访问互斥对象
        /// </summary>
        object readyDevicesObj = null;
        /// <summary>
        /// 不可用设备集合线程访问互斥对象
        /// </summary>
        object disableDevicesObj = null;
        /// <summary>
        /// PDU短信编码解码器
        /// </summary>
        PDUEncoding pduEncoder = new PDUEncoding();
        /// <summary>
        /// 取号标识
        /// </summary>
        bool getPhoneNumberFlag = false;
        /// <summary>
        /// 是否进入工作模式标识
        /// </summary>
        bool workFlag = true;
        private string serverCenter;
        private string mem1;
        private string mem2;
        private string mem3;
        private Mode mode;
        private MT mt;
        private bool readDelet;
        private bool fristCheckIMSI = true;//是否首次检测设备的IMSI

        /// <summary>
        /// 记录本机IMSI和待拨号码的远程API地址
        /// </summary>
        private string IMSI_CallNumber_Handler_Url = string.Empty;

        /// <summary>
        /// 获取ISMI对应手机号码的远程API地址
        /// </summary>
        private string Get_PhoneNumber_Handler_Url = string.Empty;
        /// <summary>
        /// 短信取号上报远程API地址
        /// </summary>
        private string Report_IMSI_PhoneNumber_Handler_Url = string.Empty;
        /// <summary>
        /// 收到短信上报地址
        /// </summary>
        private string Report_SMS_Handler_Url = string.Empty;
        public void WriteLog(object sender, TaskList.TaskListRunEventArgs e)
        {
            if (LogBox.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    WriteLogMainThread(sender, e);
                }));
            }
        }
        public void WriteLogMainThread(object sender, TaskList.TaskListRunEventArgs e)
        {
            if (e.ItemCount == 0)
            {
                LogBox.AppendText(string.Format("{0}-任务共执行{1}次-{2:yy-MM-dd HH:mm:ss}\r\n", e.Msg, e.AllCount));
            }
            else
            {
                LogBox.AppendText(string.Format("{0}-任务共执行{1}次-当前端口执行{2}次-{3:yy-MM-dd HH:mm:ss}\r\n", e.Msg, e.AllCount, e.ItemCount));
            }
            LogBox.AppendText(Environment.NewLine);
            LogBox.ScrollToCaret();
        }
        public void WriteLog(string msg)
        {
            if (LogBox.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    WriteLogMainThread(msg);
                }));
            }
        }
        public void WriteLogMainThread(string msg)
        {
            LogBox.AppendText(msg);
            LogBox.AppendText(Environment.NewLine);
            LogBox.ScrollToCaret();
        }
        public void ChangeComListView(string title, int subItemIndex, string text)
        {
            if (ComListView.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    ChangeComListViewMainThread(title, subItemIndex, text);
                }));
            }
        }
        private void ChangeComListViewMainThread(string title, int subItemIndex, string text)
        {
            foreach (ListViewItem item in ComListView.Items)
            {
                if (item.Text.Equals(title))
                {
                    item.SubItems[subItemIndex].Text = text;
                    break;
                }
            }
        }
        public void ChangeComListView(int rowIndex, int subItemIndex, string text)
        {
            if (ComListView.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    ChangeComListView(rowIndex, subItemIndex, text);
                }));
                return;
            }
            ComListView.Items[rowIndex].SubItems[subItemIndex].Text = text;
        }

        public void ChangeCountStatusLab(string countMsg)
        {
            if (statusBar.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    ChangeCountStatusLab(countMsg);
                }));
                return;
            }
            ComCountLable.Text = countMsg;
        }
        public void ChangeModelStatusLab(string modelMsg)
        {
            if (statusBar.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    ChangeModelStatusLab(modelMsg);
                }));
                return;
            }
            WorkModelLabel.Text = modelMsg;
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            //多线程网络并非请求
            System.Net.ServicePointManager.DefaultConnectionLimit = 20;
            taskList = new TaskList();
            //获取远程API配置
            Report_IMSI_PhoneNumber_Handler_Url = ConfigurationManager.AppSettings["Report_IMSI_PhoneNumber_Handler_Url"];
            IMSI_CallNumber_Handler_Url = ConfigurationManager.AppSettings["IMSI_CallNumber_Handler_Url"];
            Get_PhoneNumber_Handler_Url = ConfigurationManager.AppSettings["Get_PhoneNumber_Handler_Url"];
            Report_SMS_Handler_Url = ConfigurationManager.AppSettings["Report_SMS_Handler_Url"];
            readyDevicesObj = new object();
            disableDevicesObj = new object();

            var names = SerialPort.GetPortNames();
            AllDevices = new List<Device>(names.Length);
            DisableDevices = new List<Device>(names.Length);
            ReadyDevices = new List<Device>(names.Length);
            CallNumbers = new List<CallNumber>() { new CallNumber { PhoneNumber = "13805489252", State = 0 } };
            int index = 0;
            foreach (var n in names)
            {
                SerialPort port = new SerialPort(n, 115200, Parity.None, 8, StopBits.One);
                Device dev = new Device(port);
                dev.Index = index;
                AllDevices.Add(dev);
                ListViewItem item = new ListViewItem(new string[] { dev.PortName, dev.ConnectionState ? "已连接" : "未连接", dev.WorkState ? "工作中" : "空闲", dev.IMSI, dev.PhoneNumber });
                ComListView.Items.Add(item);
                index++;
            }
            ComCountLable.Text = string.Format("设备总数：{0}，可用设备数：0", AllDevices.Count);
            WorkModelLabel.Text = "模式：未连接";
        }

        /// <summary>
        /// 上报短信取号结果到远程
        /// </summary>
        /// <param name="imsi">本地SIM卡IMSI号</param>
        /// <param name="phoneNumber">本机号码</param>
        private void PostIMSIPhoneNumberToRemote(string imsi, string phoneNumber)
        {
            #region 代码分离前
            //var request = (HttpWebRequest)WebRequest.Create(Report_IMSI_PhoneNumber_Handler_Url);
            //request.ContentType = "application/x-www-form-urlencoded";
            //request.Method = "POST";
            //string body = string.Format("IMSI={0}&PhoneNumber={1}", imsi, phoneNumber);
            //byte[] btBodys = Encoding.UTF8.GetBytes(body);
            //request.ContentLength = btBodys.Length;
            //using (var stream = request.GetRequestStream())
            //{
            //    stream.Write(btBodys, 0, btBodys.Length);
            //    stream.Close();
            //}
            //request.GetRequestStreamAsync();
            #endregion
            HttpRequestHelper.RequestData data = new HttpRequestHelper.RequestData();

            data.Method = HttpRequestHelper.RequestMethod.POST;
            data.URL = Report_IMSI_PhoneNumber_Handler_Url;
            data.ContentType = "application/x-www-form-urlencoded";

            data.AddKeyValue(HttpRequestHelper.RequestData.RequestDataKey.IMSI, imsi);
            data.AddKeyValue(HttpRequestHelper.RequestData.RequestDataKey.PhoneNumber, phoneNumber);

            HttpRequestHelper.GetHelper().AddRequestDataForThread(data);
        }

        private void PostIMSIPhoneNumberToRemote(Device device)
        {
            HttpRequestHelper.RequestData data = new HttpRequestHelper.RequestData();

            data.Method = HttpRequestHelper.RequestMethod.POST;
            data.URL = Report_IMSI_PhoneNumber_Handler_Url;
            data.ContentType = "application/x-www-form-urlencoded";

            data.AddKeyValue(HttpRequestHelper.RequestData.RequestDataKey.IMSI, device.IMSI);
            data.AddKeyValue(HttpRequestHelper.RequestData.RequestDataKey.CCID, device.CCID);
            data.AddKeyValue(HttpRequestHelper.RequestData.RequestDataKey.PhoneNumber, device.PhoneNumber);

            HttpRequestHelper.GetHelper().AddRequestDataForThread(data);
        }

        private void PostSMSToRemote(string fromAddress, string toAddress, string contentText)
        {
            var request = (HttpWebRequest)WebRequest.Create(Report_SMS_Handler_Url);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            string body = string.Format("FromAddress={0}&ToAddress={1}&ContentText={2}", fromAddress, toAddress, contentText);
            byte[] btBodys = Encoding.UTF8.GetBytes(body);
            request.ContentLength = btBodys.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(btBodys, 0, btBodys.Length);
                stream.Close();
            }
            request.GetRequestStreamAsync();
        }


        public void CheckIMSIOver()
        {
            MessageBox.Show("IMSI检测完成");
        }
        private void tBtnConnect_Click(object sender, EventArgs e)
        {
            #region---------线程方式连接设备(废弃)----------
            //Thread thread = new Thread(() => {
            //    for (int i = 0; i < AllDevices.Count; i++)
            //    {
            //        AllDevices[i].Prot.Open();
            //        if (AllDevices[i].ConnectionState)
            //        {
            //            ChangeComListView(i, 1, "已连接");
            //        }
            //    }
            //});
            //thread.Start();
            #endregion

            #region---------线程池方式连接设备----------
            //ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
            //{
            //    for (int i = 0; i < AllDevices.Count; i++)
            //    {
            //        AllDevices[i].Prot.Open();
            //        if (AllDevices[i].ConnectionState)
            //        {
            //            ChangeComListView(i, 1, "已连接");
            //        }
            //    }
            //}));
            #endregion

            //清空日志
            LogBox.Clear();
            //工作状态
            WorkModelLabel.Text = "模式：已连接";
            //连接设备并测ISMI
            if (fristCheckIMSI)
            {
                #region------------初始检测全部设备的IMSI，采用线程池方式检测------------

                for (int i = 0; i < AllDevices.Count; i++)
                {
                    AllDevices[i].PhoneNumber = null;
                    ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
                    {
                        int x = (int)obj;
                        AllDevices[x].Open();
                        if (AllDevices[x].ConnectionState)
                        {
                            ChangeComListView(x, (int)ListViewSetting.ColumnIndex.连接状态, "已连接");
                        }
                        else
                        {
                            ChangeComListView(x, (int)ListViewSetting.ColumnIndex.连接状态, "未连接");
                            lock (disableDevicesObj)
                            {
                                DisableDevices.Add(AllDevices[x]);
                            }
                            return;
                        }
                        string str = AllDevices[x].ExecuteCommand("AT+CIMI");
                        Regex reg = new Regex(@"\d{15}", RegexOptions.Multiline);
                        var match = reg.Match(str);
                        if (match.Success)
                        {
                            AllDevices[x].IMSI = match.Value;
                            //AllDevices[x].SMSReceived += MainFrm_SMSReceived;

                            str = AllDevices[x].ExecuteCommand("AT+CCID");
                            reg = new Regex(@"\d{20}", RegexOptions.Multiline);
                            match = reg.Match(str);
                            if (match.Success)
                            {
                                AllDevices[x].CCID = match.Value;
                                lock (readyDevicesObj)
                                {
                                    ReadyDevices.Add(AllDevices[x]);
                                    ChangeComListView(x, (int)ListViewSetting.ColumnIndex.IMSI, AllDevices[x].IMSI);
                                    ChangeComListView(x, (int)ListViewSetting.ColumnIndex.CCID, AllDevices[x].CCID);
                                    ChangeCountStatusLab(string.Format("设备总数：{0}，可用设备数：{1}", AllDevices.Count, ReadyDevices.Count));
                                }
                            }
                        }
                        else
                        {
                            lock (disableDevicesObj)
                            {
                                DisableDevices.Add(AllDevices[x]);
                                ChangeComListView(x, (int)ListViewSetting.ColumnIndex.IMSI, "无");
                                ChangeComListView(x, (int)ListViewSetting.ColumnIndex.CCID, "无");
                            }
                        }

                    }), i);

                }
                #endregion
                fristCheckIMSI = false;
            }
            else
            {
                #region------------二次检测不可用设备的IMSI，采用单线程式检测------------
                //单线程检测不可用设备情况
                ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
                {
                    for (int i = 0; i < DisableDevices.Count; i++)
                    {
                        if (!DisableDevices[i].ConnectionState)
                        {
                            DisableDevices[i].Open();
                            if (DisableDevices[i].ConnectionState)
                            {
                                ChangeComListView(i, (int)ListViewSetting.ColumnIndex.连接状态, "已连接");
                            }
                            else
                            {
                                continue;
                            }
                        }
                        string str = DisableDevices[i].ExecuteCommand("AT+CIMI");
                        Regex reg = new Regex(@"\d{15}", RegexOptions.Multiline);
                        var match = reg.Match(str);
                        if (match.Success)
                        {
                            DisableDevices[i].IMSI = match.Value;
                            //DisableDevices[i].SMSReceived += MainFrm_SMSReceived;

                            str = AllDevices[i].ExecuteCommand("AT+CCID");
                            reg = new Regex(@"\d{20}", RegexOptions.Multiline);
                            match = reg.Match(str);
                            if (match.Success)
                            {
                                DisableDevices[i].CCID = match.Value;
                                ReadyDevices.Add(DisableDevices[i]);
                                ChangeComListView(DisableDevices[i].PortName, (int)ListViewSetting.ColumnIndex.IMSI, DisableDevices[i].IMSI);
                                ChangeComListView(DisableDevices[i].PortName, (int)ListViewSetting.ColumnIndex.CCID, DisableDevices[i].CCID);
                                ChangeCountStatusLab(string.Format("设备总数：{0}，可用设备数：{1}", AllDevices.Count, ReadyDevices.Count));
                                DisableDevices.RemoveAt(i);//从不可用设备集合中移除可用设备
                                i--;
                            }
                        }
                    }
                }));
                #endregion
            }
            //workFlag = true;
        }

        private void tBtnClose_Click(object sender, EventArgs e)
        {
            //设置标志位
            workFlag = false;
            tBtnSMSGetPhoneNumber.Checked = false;
            //工作状态信息
            WorkModelLabel.Text = "模式：已断开";
            for (int i = 0; i < AllDevices.Count; i++)
            {
                AllDevices[i].Close();
                //if (!AllDevices[i].ConnectionState)
                {
                    ChangeComListView(i, (int)ListViewSetting.ColumnIndex.连接状态, "已断开");
                    ChangeComListView(i, (int)ListViewSetting.ColumnIndex.工作状态, "空闲");
                    ChangeComListView(i, (int)ListViewSetting.ColumnIndex.IMSI, "无");
                    ChangeComListView(i, (int)ListViewSetting.ColumnIndex.CCID, "无");
                    ChangeComListView(i, (int)ListViewSetting.ColumnIndex.本机号码, "");
                }
            }
            ReadyDevices.Clear();
            DisableDevices.Clear();
            fristCheckIMSI = true;


            //ThreadPool.QueueUserWorkItem(new WaitCallback((obj) => { 
            // for (int i = 0; i < AllDevices.Count; i++)
            // {
            //     AllDevices[i].Prot.Close();
            //     if (AllDevices[i].ConnectionState)
            //     {
            //         ChangeComListView(i, 1, "已断开");
            //         ChangeComListView(i, 2, "");
            //     }
            // }
            //}));
        }

        private void tBtnDo_Click(object sender, EventArgs e)
        {
            if (!workFlag)
            {
                MessageBox.Show("当前设备处于非工作模式，请检查设备是否处于取号模式。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            taskList.IsStop = false;
            taskList.TaskRun += WriteLog;
            taskList.Run();
        }

        private void tBtnTask_Click(object sender, EventArgs e)
        {
            TaskFrm frm = new TaskFrm();
            frm.Tag = taskList;
            var result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (taskList.TheType == TaskList.TaskListType.AllPortTaskList)
                {
                    taskList.Devices = ReadyDevices;
                }
                else
                {
                    var comNames = frm.ComNames;
                    List<Device> des = new List<Device>(comNames.Length);

                    foreach (var n in comNames)
                    {
                        var device = ReadyDevices.FirstOrDefault(d => d.PortName == n);
                        if (null != device)
                        {
                            des.Add(device);
                        }
                    }
                    taskList.Devices = des;
                }
            }
        }

        private void tBtnGetIMSI_Click(object sender, EventArgs e)
        {
            if (fristCheckIMSI)
            {
                #region------------初始检测全部设备的IMSI，采用线程池方式检测(废弃，原因线程池无需线程分组)------------
                ////检测IMSI线程数量
                //int checkIMSIThreadCount = AllDevices.Count % splitCount == 0 ? AllDevices.Count / splitCount : AllDevices.Count / splitCount + 1;

                ////
                ////多线程信号量
                ////ManualResetEvent eventX = new ManualResetEvent(false);
                ////int count = 0;
                //for (int j = 0; j < checkIMSIThreadCount; j++)
                //{
                //    ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
                //    {
                //        int z = (int)obj;
                //        for (int i = z * splitCount; ((i < AllDevices.Count) && (i < z * splitCount + splitCount)); i++)
                //        {
                //            AllDevices[i].Prot.WriteLine("AT+CIMI");
                //            Thread.Sleep(300);
                //            string str = AllDevices[i].Prot.ReadExisting();
                //            Regex reg = new Regex(@"\d{15}", RegexOptions.Multiline);
                //            var match = reg.Match(str);
                //            if (match.Success)
                //            {
                //                AllDevices[i].IMSI = match.Value;
                //                //AllDevices[i].Prot.DataReceived += DataReceived;
                //                lock (readyDevicesObj)
                //                {
                //                    ReadyDevices.Add(AllDevices[i]);
                //                    ChangeComListView(i, 3, AllDevices[i].IMSI);
                //                }
                //            }
                //            else
                //            {
                //                lock (disableDevicesObj)
                //                {
                //                    DisableDevices.Add(AllDevices[i]);
                //                    ChangeComListView(i, 3, "无");
                //                }
                //            }
                //        }
                //    }), j);

                //}
                #endregion
                #region------------初始检测全部设备的IMSI，采用线程池方式检测------------

                for (int i = 0; i < AllDevices.Count; i++)
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
                    {
                        int x = (int)obj;

                        string str = AllDevices[x].ExecuteCommand("AT+CIMI");
                        Regex reg = new Regex(@"\d{15}", RegexOptions.Multiline);
                        var match = reg.Match(str);
                        if (match.Success)
                        {
                            AllDevices[x].IMSI = match.Value;
                            lock (readyDevicesObj)
                            {
                                ReadyDevices.Add(AllDevices[x]);
                                ChangeComListView(x, (int)ListViewSetting.ColumnIndex.IMSI, AllDevices[x].IMSI);
                            }
                        }
                        else
                        {
                            lock (disableDevicesObj)
                            {
                                DisableDevices.Add(AllDevices[x]);
                                ChangeComListView(x, (int)ListViewSetting.ColumnIndex.IMSI, "无");
                            }
                        }

                    }), i);

                }
                #endregion
                fristCheckIMSI = false;
            }
            else
            {
                #region------------二次检测不可用设备的IMSI，采用单线程式检测------------
                //单线程检测不可用设备情况
                ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
                {
                    for (int i = 0; i < DisableDevices.Count; i++)
                    {
                        string str = DisableDevices[i].ExecuteCommand("AT+CIMI");
                        Regex reg = new Regex(@"\d{15}", RegexOptions.Multiline);
                        var match = reg.Match(str);
                        if (match.Success)
                        {
                            DisableDevices[i].IMSI = match.Value;
                            //AllDevices[i].Prot.DataReceived += DataReceived;
                            ReadyDevices.Add(DisableDevices[i]);
                            ChangeComListView(DisableDevices[i].PortName, (int)ListViewSetting.ColumnIndex.IMSI, match.Value);
                            DisableDevices.RemoveAt(i);//从不可用设备集合中移除可用设备
                            i--;
                        }
                    }
                }));
                #endregion
            }
        }

        private void tBtnSetting_Click(object sender, EventArgs e)
        {
            ParamSetting frm = new ParamSetting();
            frm.CallNumbers = this.CallNumbers;
            var result = frm.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                serverCenter = frm.ServiceCenter;
                mem1 = frm.MEM1;
                mem2 = frm.MEM2;
                mem3 = frm.MEM3;
                mode = frm.Mode;
                mt = frm.MT;
                readDelet = frm.ReadDelete;
                CallNumbers = frm.CallNumbers;
            }
        }

        /// <summary>
        /// 还应简化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tBtnTestCall_Click(object sender, EventArgs e)
        {
            WorkModelLabel.Text = "模式：模式取号";
            if (null != CallNumbers && CallNumbers.Count > 0)
            {
                #region---------------多线程方式拨号取号（参考）-------------------
                //方法里面是最初设计思路，有可借鉴之处请咨询查看
                //多线程存在问题：在程序退出后，线程可能还在执行。
                //var actions = new Action[ReadyDevices.Count];
                //for (int i = 0; i < ReadyDevices.Count; i++)
                //{
                //    var j = i;
                //    actions[i] = () =>
                //    {

                //        CallNumber callNumber = null;
                //        //int count = 0;//重试次数
                //        #region---------------查找可用待拨号码-------------------
                //        while (true)
                //        {
                //            lock (readyDevicesObj)
                //            {
                //                foreach (var c in CallNumbers)
                //                {
                //                    if (c.State == 0)
                //                    {
                //                        callNumber = c;
                //                        c.State = 1;
                //                        break;
                //                    }
                //                }
                //            }
                //            if (null == callNumber)
                //            {
                //                Thread.Sleep(100);
                //            }
                //            else
                //            {
                //                break;
                //            }
                //        }
                //        #endregion
                //        #region-----------在远程服务器中记录本机IMSI和待拨号码信息------------
                //        //进行web请求上传，先发布请求然后实际拨号
                //        //如果设备拨号不成功，那么请求发送的时间将不会被服务器更改
                //        while (true)
                //        {
                //            var request = (HttpWebRequest)WebRequest.Create(
                //            string.Format("http://192.168.1.200/ComHandler.ashx?imsi={0}&phone={1}",
                //            ReadyDevices[j].IMSI, callNumber.PhoneNumber)
                //            );
                //            request.Method = "get";
                //            var response = (HttpWebResponse)request.GetResponse();
                //            StreamReader reader = new StreamReader(response.GetResponseStream());
                //            var str = reader.ReadToEnd();
                //            if (str == "0")
                //            {
                //                Thread.Sleep(2000);
                //                continue;
                //            }
                //            else
                //            {
                //                break;
                //            }
                //        }
                //        #endregion
                //        //count = 0;
                //        ReadyDevices[j].Prot.DataReceived -= DataReceived;
                //        #region-----------在拨号错误的情况下，循环3次测试拨号操作（废弃）------------
                //        //while (true)
                //        //{
                //        //    //实际拨号操作
                //        //    string cmd = string.Format("ATD{0};", callNumber.PhoneNumber);
                //        //    ReadyDevices[j].Prot.DiscardInBuffer();
                //        //    ReadyDevices[j].Prot.WriteLine(cmd);
                //        //    Thread.Sleep(500);
                //        //    string str = ReadyDevices[j].Prot.ReadExisting();
                //        //    if (str.Contains("ERROR"))
                //        //    {
                //        //        count++;
                //        //        if (count < 3)
                //        //            continue;
                //        //        else
                //        //        {
                //        //            callNumber.State = 0;
                //        //            return;
                //        //        }
                //        //    }
                //        //    else
                //        //    {
                //        //        break;
                //        //    }
                //        //}
                //        #endregion
                //        #region-----------在拨号错误的情况下，中断后续操作即提前结束该线程------------
                //        string cmd = string.Format("ATD{0};", callNumber.PhoneNumber);
                //        ReadyDevices[j].Prot.DiscardInBuffer();
                //        ReadyDevices[j].Prot.WriteLine(cmd);
                //        Thread.Sleep(500);
                //        var strs = ReadyDevices[j].Prot.ReadExisting();
                //        if (strs.Contains("ERROR"))
                //        {
                //            callNumber.State = 0;
                //            return;
                //        }
                //        Thread.Sleep(2000);
                //        ReadyDevices[j].Prot.WriteLine("AT+CHUP");
                //        callNumber.State = 0;
                //        ReadyDevices[j].Prot.DataReceived += DataReceived;
                //        #endregion
                //        #region-----------获取远程服务器上该ISMI对应的手机号码------------
                //        //获取远程号码
                //        var request2 = (HttpWebRequest)WebRequest.Create(
                //        string.Format("http://192.168.1.200/GetNumberHandler.ashx?imsi={0}",
                //        ReadyDevices[j].IMSI)
                //        );
                //        request2.Method = "get";
                //        var response2 = (HttpWebResponse)request2.GetResponse();
                //        var reader2 = new StreamReader(response2.GetResponseStream());
                //        var s = reader2.ReadLine();
                //        //运行到这里出错可能性较小，所以可以不用判断
                //        //var flog = string.IsNullOrEmpty(str);
                //        //if(flog)
                //        //{
                //        //    ChangeComListView(ReadyDevices[j].Index, 4, "获取失败");
                //        //}
                //        //else
                //        //{
                //        //    ChangeComListView(ReadyDevices[j].Index, 4, s);
                //        //}

                //        ChangeComListView(ReadyDevices[j].Index, 4, s);
                //        #endregion
                //    };
                //}
                //启动上面创建的多线程
                //for (int i = 0; i < actions.Length; i++)
                //{
                //    Thread thread = new Thread(new ThreadStart(actions[i]));
                //    thread.Start();
                //}
                #endregion

                #region---------------线程池方式拨号取号（简化版）(还应再次简化为A线程池线程负责上报，B线程池1个线程负责取号。)-------------------
                for (int i = 0; i < ReadyDevices.Count; i++)
                {
                    if (!string.IsNullOrEmpty(ReadyDevices[i].PhoneNumber))
                    {
                        continue;//跳过已经取号成功的串口设备
                    }
                    ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
                    {
                        int j = (int)obj;
                        CallNumber callNumber = null;//线程待拨号码
                        #region---------------查找可用待拨号码-------------------
                        while (true)
                        {
                            lock (readyDevicesObj)
                            {
                                foreach (var c in CallNumbers)
                                {
                                    if (c.State == 0)
                                    {
                                        callNumber = c;
                                        c.State = 1;
                                        break;
                                    }
                                }
                            }
                            if (null == callNumber)
                            {
                                Thread.Sleep(100);
                            }
                            else
                            {
                                break;
                            }
                        }
                        #endregion
                        #region-----------在远程服务器中记录本机IMSI和待拨号码信息------------
                        //while (true)
                        //{
                        //    var request = (HttpWebRequest)WebRequest.Create(
                        //    string.Format("{0}?imsi={1}&phone={2}", IMSI_CallNumber_Handler_Url,
                        //    ReadyDevices[j].IMSI, callNumber.PhoneNumber)
                        //    );
                        //    request.Method = "get";
                        //    var response = (HttpWebResponse)request.GetResponse();
                        //    StreamReader reader = new StreamReader(response.GetResponseStream());
                        //    var str = reader.ReadToEnd();
                        //    if (str == "0")
                        //    {
                        //        Thread.Sleep(2000);
                        //        continue;
                        //    }
                        //    else
                        //    {
                        //        break;
                        //    }
                        //}
                        #endregion
                        #region -----------在远程服务器中记录本机IMSI和待拨号码信息------------
                        HttpRequestHelper.RequestData data = new HttpRequestHelper.RequestData();
                        data.Method = HttpRequestHelper.RequestMethod.GET;
                        data.URL = IMSI_CallNumber_Handler_Url;

                        data.AddKeyValue(HttpRequestHelper.RequestData.RequestDataKey.IMSI, ReadyDevices[j].IMSI);
                        data.AddKeyValue(HttpRequestHelper.RequestData.RequestDataKey.Phone, callNumber.PhoneNumber);

                        HttpRequestHelper.GetHelper().AddRequestDataForThread(data);
                        #endregion
                        #region-----------在拨号错误的情况下，中断后续操作即提前结束该线程------------
                        string cmd = string.Format("ATD{0};", callNumber.PhoneNumber);
                        ReadyDevices[j].DiscardInBuffer();
                        var strs = ReadyDevices[j].ExecuteCommand(cmd);
                        WriteLog(string.Format("{0}-{1}", ReadyDevices[j].IMSI, strs));
                        if (strs.Contains("ERROR"))
                        {
                            callNumber.State = 0;
                            return;
                        }
                        Thread.Sleep(10000);
                        ReadyDevices[j].ExecuteCommand("AT+CHUP");
                        Thread.Sleep(2000);
                        #endregion
                        #region-----------获取远程服务器上该ISMI对应的手机号码------------
                        var request2 = (HttpWebRequest)WebRequest.Create(
                        string.Format("{0}/{1}", Get_PhoneNumber_Handler_Url,
                        ReadyDevices[j].IMSI)
                        );
                        request2.Method = "get";
                        var response2 = (HttpWebResponse)request2.GetResponse();
                        var reader2 = new StreamReader(response2.GetResponseStream());
                        var s = reader2.ReadLine();
                        ReadyDevices[j].PhoneNumber = s;
                        ChangeComListView(ReadyDevices[j].PortName, (int)ListViewSetting.ColumnIndex.本机号码, s);
                        callNumber.State = 0;
                        #endregion
                    }), i);
                }
                #endregion

            }
        }

        private void tBtnDebug_Click(object sender, EventArgs e)
        {
            //+8613010380500
            DebugForm frm = new DebugForm();
            frm.Show();
            //SMS sms = new SMS();
            //var strs = sms.PDUEncoding("8613805489252", "测试");
            //int len = (strs[0].Length - Convert.ToInt32(strs[0].Substring(0, 2), 16) * 2 - 2) / 2;
            //ReadyDevices[2].Prot.WriteLine("AT+CMGS=" + len.ToString());
            //Thread.Sleep(100);
            //ReadyDevices[2].Prot.WriteLine(strs[0] + (char)(26));
        }

        private void tBtnGetDbPhoneNumner_Click(object sender, EventArgs e)
        {
            WorkModelLabel.Text = "模式：网络取号";
            #region---------------线程池方式远程取号(需要修改为单线程模式，单线程模式简单快捷。可避免线程过多造成请求过多问题)-------------------
            for (int i = 0; i < ReadyDevices.Count; i++)
            {
                if (!string.IsNullOrEmpty(ReadyDevices[i].PhoneNumber))
                {
                    continue;//跳过已经取号成功的串口设备
                }
                ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
                {
                    Device d = (Device)obj;
                    //CallNumber callNumber = null;//线程待拨号码
                    #region-----------获取远程服务器上该ISMI对应的手机号码------------
                    var request2 = (HttpWebRequest)WebRequest.Create(
                     string.Format("{0}/{1}", Get_PhoneNumber_Handler_Url,
                     d.IMSI)
                     );
                    request2.Method = "get";
                    //request2.Headers.Add( HttpRequestHeader.Accept,"text/xml");
                    var response2 = (HttpWebResponse)request2.GetResponse();
                    var reader2 = new StreamReader(response2.GetResponseStream());
                    var s = reader2.ReadLine();
                    d.PhoneNumber = s;
                    ChangeComListView(d.PortName, (int)ListViewSetting.ColumnIndex.本机号码, s);
                    //callNumber.State = 0;
                    string str = string.Format("{0}网络取号结束，{1}，时间：{2:yyyy-MM-dd HH:mm:ss}\r\n",
                    d.PortName, s, DateTime.Now);
                    WriteLog(str);
                    #endregion
                }), ReadyDevices[i]);
            }
            #endregion
        }

        private void tBtnSMSGetPhoneNumber_Click(object sender, EventArgs e)
        {
            if (!tBtnSMSGetPhoneNumber.Checked)
            {
                DlgFrm frm = new DlgFrm();
                var result = frm.ShowDialog();
                if (result != DialogResult.OK)
                    return;

                SendMsgPhoneNumber = frm.Tag.ToString();

                foreach (var d in ReadyDevices)
                {
                    d.SMSReceived -= MainFrm_SMSReceived;
                    //d.SMSReceived += MainFrm_TestSMSReceived;
                }
                tBtnSMSGetPhoneNumber.Checked = true;
                //将工作模式设为假
                workFlag = false;
                WorkModelLabel.Text = "模式：短信取号";
                GetPhoneNumberSMS();
            }
            else
            {
                tBtnSMSGetPhoneNumber.Checked = false;
                WorkModelLabel.Text = "模式：已连接";
            }
        }
        /// <summary>
        /// 测号短信接收操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ex"></param>
        private void MainFrm_TestSMSReceived(object sender, Device.SMSReceivedEventArgs ex)
        {
            var d = sender as Device;
            var msg = d.ReadMsgByIndex(ex.NewMsgIndex);
            if (msg.ReadState == SMS.ReadSMSState.Success)
            {
                //设置ListView上的手机号码信息
                SetPhoneNumber(d.PortName, msg.Msg);
                //上报短信取号信息
                PostIMSIPhoneNumberToRemote(d.IMSI, msg.Msg);
            }
        }
        /// <summary>
        /// 读取上位机发送的电话号码短信
        /// </summary>
        private void GetPhoneNumberSMS()
        {
            #region------------单线程，读取上位机发送的电话号码短信------------

            //ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
            //{
            //    int c = ReadyDevices.Count;
            //    int i = 0;
            //    while (!workFlag && tBtnSMSGetPhoneNumber.Checked)
            //    {
            //        if(c == 0)
            //        {
            //            break;
            //        }
            //        if (i >= ReadyDevices.Count)
            //            i = 0;
            //        Device d = ReadyDevices[i];
            //        if (!string.IsNullOrEmpty(d.PhoneNumber))
            //        {
            //            c--;
            //            i++;
            //            continue;
            //        }
            //        var msg = d.ReadMsgByIndex(1);
            //        if (msg.ReadState == SMS.ReadSMSState.Success)
            //        {
            //            if (msg.Phone.Contains(SendMsgPhoneNumber))
            //            {
            //                d.PhoneNumber = msg.Msg;
            //                //设置ListView上的手机号码信息
            //                SetPhoneNumber(d.PortName, msg.Msg);
            //                //上报短信取号信息
            //                PostIMSIPhoneNumberToRemote(d.IMSI, msg.Msg);
            //                c--;

            //                string str = string.Format("{0}读取手机号码短信结束，时间：{1:yyyy-MM-dd HH:mm:ss}\r\n",
            //                d.PortName, DateTime.Now);
            //                WriteLog(str);
            //            }
            //        }
            //        i++;
            //        Thread.Sleep(1000);
            //    }

            //}), null);
            #endregion
            #region 多线程读取短信，出现未知网络超时错误
            for (int i = 0; i < ReadyDevices.Count; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
                {
                    Device d = (Device)obj;
                    int y = 1;

                    while (!workFlag && tBtnSMSGetPhoneNumber.Checked)
                    {
                        if (y > 10)
                            y = 1;

                        var msg = d.ReadMsgByIndex(y);

                        y++;
                        if (msg.ReadState == SMS.ReadSMSState.Success)
                        {
                            if (msg.Phone.Contains(SendMsgPhoneNumber))
                            {
                                //设置ListView上的手机号码信息
                                d.PhoneNumber = msg.Msg;
                                SetPhoneNumber(d.PortName, msg.Msg);
                                //上报短信取号信息
                                //PostIMSIPhoneNumberToRemote(d.IMSI, msg.Msg);
                                PostIMSIPhoneNumberToRemote(d);
                                break;
                            }
                        }
                        Thread.Sleep(1000);
                    }
                    string str = string.Format("{0}读取手机号码短信结束，时间：{1:yyyy-MM-dd HH:mm:ss}\r\n",
                    d.PortName, DateTime.Now);
                    WriteLog(str);

                }), ReadyDevices[i]);

            }
            #endregion
        }
        /// <summary>
        /// 显示取到的手机号码
        /// </summary>
        /// <param name="comName">串口名称</param>
        /// <param name="numberText">串口对应的手机号码</param>
        private void SetPhoneNumber(string comName, string numberText)
        {
            try
            {

                if (ComListView.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(() =>
                    {
                        foreach (ListViewItem item in ComListView.Items)
                        {
                            if (item.Text.Equals(comName))
                            {
                                item.SubItems[(int)ListViewSetting.ColumnIndex.本机号码].Text = numberText;
                                break;
                            }
                        }
                    }));
                    return;
                }
                foreach (ListViewItem item in ComListView.Items)
                {
                    if (item.Text.Equals(comName))
                    {
                        item.SubItems["本机号码"].Text = numberText;
                        break;
                    }
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// 普通短信接收操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFrm_SMSReceived(object sender, Device.SMSReceivedEventArgs e)
        {
            var d = sender as Device;
            var msg = d.ReadMsgByIndex(e.NewMsgIndex);
            var str = string.Empty;
            if (msg.ReadState != SMS.ReadSMSState.Success)
            {
                str = string.Format("{0}收到短信通知，读取短信{1}失败.时间：{2:yyyy-MM-dd HH:mm:ss}\r\n",
                d.PortName, e.NewMsgIndex, DateTime.Now);
                WriteLog(str);
                return;
            }
            //上报收取短信

            try
            {
                PostSMSToRemote(msg.Phone, d.PhoneNumber, msg.Msg);
                str = string.Format("{0}收到短信，来自：{1}，时间：{2:yyyy-MM-dd HH:mm:ss}\r\n内容：{3}\r\n",
                d.PhoneNumber, msg.Phone, DateTime.Now, msg.Msg);
            }
            catch (Exception ex)
            {
                str = string.Format("收短信后出错，来自：{0}，时间：{1:yyyy-MM-dd HH:mm:ss}\r\n内容：{2}\r\n错误原因：{3}\r\n",
                msg.Phone, DateTime.Now, msg.Msg, ex.Message);
            }
            WriteLog(str);
        }

        private void tBtnStop_Click(object sender, EventArgs e)
        {
            taskList.IsStop = true;
        }

        private void ClearLogMenuItem_Click(object sender, EventArgs e)
        {
            LogBox.Clear();
        }

        private void GoTopMenuItem_Click(object sender, EventArgs e)
        {
            LogBox.Select(0, 0);
            LogBox.ScrollToCaret();
        }


        private void tBtnRecvSMSing_Click(object sender, EventArgs e)
        {
            if (!workFlag)
            {
                workFlag = true;
                tBtnSMSGetPhoneNumber.Checked = false;
                foreach (var d in ReadyDevices)
                {
                    //d.SMSReceived -= MainFrm_TestSMSReceived;
                    d.SMSReceived += MainFrm_SMSReceived;
                }

                WorkModelLabel.Text = "模式：短信接收";
            }
            else
            {
                workFlag = false;
                tBtnSMSGetPhoneNumber.Checked = false;
                foreach (var d in ReadyDevices)
                {
                    //d.SMSReceived -= MainFrm_TestSMSReceived;
                    d.SMSReceived -= MainFrm_SMSReceived;
                }

                WorkModelLabel.Text = "模式：已连接";
            }

        }

        private void SynDataBtn_Click(object sender, EventArgs e)
        {
            SynFrm frm = new SynFrm();
            foreach(var d in ReadyDevices)
            {
                if( !string.IsNullOrEmpty(d.PhoneNumber) &&
                    !string.IsNullOrEmpty(d.IMSI) &&
                    !string.IsNullOrEmpty(d.CCID))
                {
                    frm.AddDevice(d);
                }
            }
            frm.ShowDialog();
        }
    }
}
