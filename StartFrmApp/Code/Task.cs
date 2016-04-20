using System.Threading;
using System.Collections.Generic;
using System;

namespace ModemCtlApp.Code
{
    public class TaskList
    {
        public class TaskListRunEventArgs : EventArgs
        {
            public TaskListRunEventArgs() { }
            public TaskListRunEventArgs(string msg,int all,int item)
            {
                Msg = msg;
                AllCount = all;
                ItemCount = item;
            }
            public string Msg { get; set; }
            public int AllCount { get; set; }
            public int ItemCount { get; set; }
        }

        public delegate void TaskListRunHandler(object sender, TaskListRunEventArgs e);
        public enum TaskListType
        {
            /// <summary>
            /// 所以端口都要执行的任务列表
            /// </summary>
            AllPortTaskList,
            /// <summary>
            /// 某系端口需要执行的任务列表
            /// </summary>
            SomePortTaskList
        }

        List<GsmTask> link;
        public TaskList()
        {
            link = new List<GsmTask>();
        }

        /// <summary>
        /// 任务关联设备集合
        /// </summary>
        public List<Device> Devices
        {
            get { return devices; }
            set
            {
                devices = value;
                ItemCounts = new int[value.Count];
                //for (var i = 0; i < itemCounts.Length; i++)
                //{
                //    itemCounts[i] = ItemCount;
                //}
            }
        }
        private List<Device> devices;
        public List<GsmTask> TaskLink
        {
            get { return link; }
        }

        public TaskListType TheType { get; set; }
        /// <summary>
        /// 任务队列是否停止执行
        /// </summary>
        public bool IsStop { get; set; }
        public int ItemCount { get; set; }
        private int[] itemCounts;
        /// <summary>
        /// 全局任务实际执行总数
        /// </summary>
        public int Count
        {
            get
            {
                return count;
            }

            private set
            {
                lock (lockCountObj)
                {
                    count = value;
                }
            }
        }
        /// <summary>
        /// 全局任务总数
        /// </summary>
        public int AllCount { get; set; }
        public int[] ItemCounts
        {
            get
            {
                return itemCounts;
            }

            private set
            {
                itemCounts = value;
            }
        }

        private object lockCountObj = new object();
        private object lockItemCountObj = new object();
        private int count;

        public event TaskListRunHandler TaskRun;

        public void Run()
        {
            if (null == Devices || Devices.Count < 1)
                throw new Exception("不存在可用设备,请重连设备并再次保存任务！");
            if (AllCount > 0)
            {
                foreach (var d in Devices)
                {
                    var tasks = TaskLink;
                    var device = d;
                    ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
                    {
                        if (IsStop || Count == AllCount)
                        {
                            return;
                        }
                        foreach (var t in tasks)
                        {
                            t.Execute(device);
                        }
                        Count += 1;

                        if(null != TaskRun)
                        {
                            TaskRun(this, new TaskListRunEventArgs(
                                string.Format("{0}-{1}成功执行任务",device.PortName,device.PhoneNumber),
                                Count, 0));
                        }
                    }));
                }
            }
            else
            {
                for (int i = 0; i< Devices.Count;i++)
                {
                    var tasks = TaskLink;
                    var device = Devices[i];
                    var j = i;
                    ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
                    {
                        if(IsStop || itemCounts[j] == ItemCount)
                        {
                            return; 
                        }
                        foreach (var t in tasks)
                        {
                            t.Execute(device);
                        }
                        lock(lockItemCountObj)
                        {
                            itemCounts[j]++;
                        }
                        Count += 1;
                        if (null != TaskRun)
                        {
                            TaskRun(this, new TaskListRunEventArgs(
                                string.Format("{0}-{1}成功执行任务", device.PortName, device.PhoneNumber), 
                                Count, itemCounts[j]));
                        }
                    }));
                }
            }
        }
    }
    public class GsmTask
    {
        public GsmTask()
        {

        }
        public GsmTask(string taskName,Command cmd)
        {
            TaskName = taskName;
            this.cmd = cmd;
        }
        Command cmd;
        public string TaskName { get; set; }

        public Command Command { get { return cmd; } }
        

        public void Execute(Device device)
        {
            if (cmd.ThisType == Command.CommandType.SendSMSCmd)
            {
                device.SendMsg((SendSMSCommand)cmd);
            }
            else
            {
                device.ExecuteCommand(cmd);
            }
            Thread.Sleep(cmd.Duration);
        }

    }
}
