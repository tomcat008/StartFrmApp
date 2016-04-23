using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace ModemCtlApp.Code
{
    public class Device
    {
        public class SMSReceivedEventArgs : EventArgs
        {
            public SMSReceivedEventArgs() { }
            public SMSReceivedEventArgs(int index) { newMsgIndex = index; }
            int newMsgIndex;

            public int NewMsgIndex
            {
                get
                {
                    return newMsgIndex;
                }

                set
                {
                    newMsgIndex = value;
                }
            }
        }
        public delegate void SMSReceivedEventHandler(object sender, SMSReceivedEventArgs e);
        SerialPort port;
        /// <summary>
        /// PDU短信编码解码器
        /// </summary>
        PDUEncoding pduEncoder = new PDUEncoding();
        public Device(SerialPort serialport)
        {
            port = serialport;
            port.DataReceived += Port_DataReceived;
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var sp = sender as SerialPort;
            string str = sp.ReadLine();
            if ((str.Length > 8) && (str.Substring(0, 6) == "+CMTI:"))
            {
                var newMsgIndex = Convert.ToInt32(str.Split(',')[1]);
                if (null != smsReceived)
                {
                    smsReceived(this, new SMSReceivedEventArgs(newMsgIndex));
                }
            }
        }

        private event SMSReceivedEventHandler smsReceived;
        /// <summary>
        /// 设备接收到短信事件
        /// </summary>
        public event SMSReceivedEventHandler SMSReceived
        {
            add { smsReceived += value; }
            remove { smsReceived -= value; }
        }

        //public event SerialDataReceivedEventHandler DataReceived
        //{
        //    add
        //    {
        //        port.DataReceived += value;
        //        ds.Add(value);
        //    }
        //    remove
        //    {
        //        port.DataReceived -= value;
        //        ds.Remove(value);
        //    }
        //}

        //public SerialPort Port
        //{
        //    get { return port; }
        //    set { port = value; }
        //}
        public int Index { get; set; }
        public string IMSI { get; set; }
        public string CCID { get; set; }
        public string PhoneNumber { get; set; }

        public bool ConnectionState { get { return port.IsOpen; } }

        public bool WorkState { get; set; }

        public string PortName
        {
            get { return port.PortName; }
            set { port.PortName = value; }
        }

        public string ExecuteCommand(Command cmd)
        {
            return ExecuteCommand(cmd.CommandText);
        }

        public string ExecuteCommand(string cmdText)
        {
            string str = string.Empty;
            //忽略接收缓冲区内容，准备发送
            port.DiscardInBuffer();
            //注销事件关联，为发送做准备
            port.DataReceived -= Port_DataReceived;
            try
            {
                port.WriteLine(cmdText);
                string temp = string.Empty;
                while ((temp.Trim() != "OK") && (temp.Trim() != "ERROR"))
                {
                    temp = port.ReadLine();
                    str += temp;
                }
                port.DataReceived += Port_DataReceived;
            }
            catch (Exception ex)
            {
                str = "ERROR";
            }
            return str;
        }

        public bool SendMsg(SendSMSCommand cmd)
        {
            //注销事件关联，为发送做准备
            port.DataReceived -= Port_DataReceived;
            try
            {
                port.WriteLine(cmd.CommandText);
                port.ReadTo(">");
                //忽略接收缓冲区内容，准备发送
                port.DiscardInBuffer();
                port.WriteLine(cmd.Msg);

                string temp = string.Empty;
                string result = string.Empty;
                while (temp.Trim() != "OK" && temp.Trim() != "ERROR")
                {
                    temp = port.ReadLine();
                    result += temp;
                }
                if (result.Substring(result.Length - 4, 3).Trim() != "OK")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex){
                return false;
            }
            finally
            {
                port.DataReceived += Port_DataReceived;
            }
        }

        public SMS ReadMsgByIndex(int index)
        {
            SMS sms = new SMS{  ReadState =  SMS.ReadSMSState.Empty};
            string temp = string.Empty;
            try
            {
                temp = ExecuteCommand("AT+CMGR=" + index.ToString());
                var tp = temp.Trim();
                if (tp.Equals("OK"))
                {
                    return sms;
                }
                else if (tp.Equals("ERROR"))
                {
                    sms.ReadState = SMS.ReadSMSState.Error;
                    return sms;
                }

                var temps = temp.Split((char)(13));
                if (temps.Length < 3)
                    return null;

                temp = temps[2];       //取出PDU串(char)(13)为0x0a即\r 按\r分为多个字符串 第3个是PDU串
                sms = pduEncoder.PDUDecoder(temp);
                sms.ReadState = SMS.ReadSMSState.Success;
                //DelMsgByIndex(index);
                return sms;
            }
            catch (Exception ex)
            {
                sms.ReadState = SMS.ReadSMSState.Error;
            }
            return sms;
        }

        /// <summary>
        /// 删除对应序号短信
        /// </summary>
        /// <param name="index">短信序号</param>
        /// <returns></returns>
        public bool DelMsgByIndex(int index)
        {
            if (ExecuteCommand("AT+CMGD=" + index.ToString()).Contains("OK"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Open()
        {
            port.Open();
        }

        public bool IsOpen()
        {
            return port.IsOpen;
        }

        public void Close()
        {
            port.Close();
        }

        public void DiscardInBuffer()
        {
            port.DiscardInBuffer();
        }
    }
}
