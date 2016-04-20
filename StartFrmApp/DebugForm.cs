using ItDreamWorks.Modem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModemCtlApp
{
    //ATD13805489252;拨号命令
    public delegate void ReceviedMsg(string msg);
    public partial class DebugForm : Form
    {
        public DebugForm()
        {
            InitializeComponent();
        }

        private void txtShow_TextChanged(object sender, EventArgs e)
        {
            txtShow.Clear();
        }

        public void GetMsg(string msg)
        {
            if(txtShow.InvokeRequired)
            {
                ReceviedMsg a = new ReceviedMsg(GetMsg);
                txtShow.Invoke(a, msg);
            }
            else
            {
                txtShow.Text += (msg + "\r\n"); 
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            comPort1.Close();
            //if (!comPort1.IsOpen)
            comPort1.Open();
            comPort1.DiscardInBuffer();
            comPort1.DataReceived -= this.DataReceived;
            try
            {
                comPort1.WriteLine(txtCmd.Text);
                Thread.Sleep(1000);
                string str = string.Format("{0}", comPort1.ReadExisting());
                txtShow.Text += str;
                Thread.Sleep(1000);
                 str = string.Format("{0}", comPort1.ReadExisting());
                txtShow.Text += str;

            }
            catch (Exception ex)
            {
                txtShow.Text += string.Format("E:{0}-{1}\r\n", comPort1.PortName, ex.Message);

            }
            finally
            {
                comPort1.DataReceived += this.DataReceived;
            }

        }

        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (e.EventType == SerialData.Chars)
            {
                ReadData((ComPort)sender);
            }
        }

        private void ReadData(ComPort port)
        {
            int count = port.BytesToRead;
            byte[] buf = new byte[count];
            port.Read(buf, 0, count);
            var msg = (port.PortName + ":" + Encoding.UTF8.GetString(buf) + "\r\n");
            GetMsg(msg);
         }


        private void txtShow_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //comPort1.Close();
        }

        private void SendMsg_Click(object sender, EventArgs e)
        {
            comPort1.DataReceived -= this.DataReceived; ;

            comPort1.Write("AT+CMGS=\"13805489252\"\r");
            comPort1.ReadTo(">");
            comPort1.DiscardInBuffer();
            comPort1.WriteLine(txtCmd.Text+(char)(26));

            string temp = string.Empty;
            string result = string.Empty;
            while (temp.Trim() != "OK" && temp.Trim() != "ERROR")
            {
                temp = comPort1.ReadLine();
                result += temp;
            }
            txtShow.Text += result;
            //事件重新绑定 正常监视串口数据
            comPort1.DataReceived += this.DataReceived;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var coms = SerialPort.GetPortNames();
            cbo.DataSource = coms;
        }

        private void cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comPort1.IsOpen)
                comPort1.Close();
            comPort1.PortName = cbo.Text;
        }

    }
}
