using System;
using System.Text;

namespace ModemCtlApp.Code
{
    /// <summary>
    /// 要执行的命令
    /// </summary>
    public class Command
    {
        /// <summary>
        /// 命令类型
        /// </summary>
        public enum CommandType {
            /// <summary>
            /// 无返回命令
            /// </summary>
            NothingCmd,
            /// <summary>
            /// Bool类型命令
            /// </summary>
            BooleanCmd,
            /// <summary>
            /// 发送短信命令
            /// </summary>
            SendSMSCmd
        }
        public Command()
        {
            ThisType = CommandType.NothingCmd;
        }
        public Command(string commandName)
        {
            this.CommandName = commandName;
            ThisType = CommandType.NothingCmd;
        }
        private CommandType thisType;

        /// <summary>
        /// 命令参数
        /// </summary>
        public string Parameter{ get; set; }
        /// <summary>
        /// 基本AT命令
        /// 这些AT命令仅支持：设置（=），拨号（空格; ）等设置性操作。
        /// 不支持：测试（?）操作
        /// </summary>
        public string ATCommandText { get; set; }
        public string CommandName { get; set; }
        /// <summary>
        /// 命令文本
        /// </summary>
        public virtual string CommandText { get { return string.Format(ATCommandText, Parameter); } }
        private int duration = 500;
        /// <summary>
        /// 当前命令类型
        /// </summary>
        public CommandType ThisType
        {
            get
            {
                return thisType;
            }

            set
            {
                thisType = value;
            }
        }
        /// <summary>
        /// 持续时候,命令执行后延迟（默认500ms）。
        /// </summary>
        public int Duration
        {
            get
            {
                return duration;
            }

            set
            {
                duration = value;
            }
        }
    }

    public class SendSMSCommand:Command
    {
        string msg,str;
        public string Msg
        {
            get
            {
                return msg;
            }
            set
            {
                str = value;
                msg = string.Format("0011000D91{0}000801{1}{2}",
                  reverserNumber(Parameter),
                  contentEncoding(value),
                  Convert.ToChar(26));
            }
        }

        public string MsgString { get { return str; } }
        public override string CommandText
        {
            get
            {
                return string.Format("AT+CMGS={0}",getLength(msg));
            }
        }
        //获取短信内容的字节数
        private string getLength(string txt)
        {
            int i = 0;
            string s = "";
            i = txt.Length * 2;
            i += 15;
            s = i.ToString();
            return s;
        }
        //将手机号码转换为内存编码
        private string reverserNumber(string phone)
        {
            string str = "";
            //检查手机号码是否按照标准格式写，如果不是则补上
            if (phone.Substring(0, 2) != "86")
            {
                phone = string.Format("86{0}", phone);
            }
            char[] c = this.getChar(phone);
            for (int i = 0; i <= c.Length - 2; i += 2)
            {
                str += c[i + 1].ToString() + c[i].ToString();
            }
            return str;
        }
        //汉字解码为16进制
        private string contentEncoding(string content)
        {
            Encoding encodingUTF = System.Text.Encoding.BigEndianUnicode;
            string s = "";
            byte[] encodeByte = encodingUTF.GetBytes(content);
            for (int i = 0; i <= encodeByte.Length - 1; i++)
            {
                s += BitConverter.ToString(encodeByte, i, 1);
            }
            s = string.Format("{0:X2}{1}", s.Length / 2, s);
            return s;
        }
        private char[] getChar(string phone)
        {
            if (phone.Length % 2 == 0)
            {
                return Convert.ToString(phone).ToCharArray();
            }
            else
            {
                return Convert.ToString(phone + "F").ToCharArray();
            }
        }

    }
}
