using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModemCtlApp.Code
{
    /// <summary>
    /// 程序配置信息
    /// </summary>
    [Serializable]
    public class AppConfig
    {
        /// <summary>
        /// 每4个设备一个线程负责处理检测IMSI
        /// </summary>
        int splitCount = 4;
        /// <summary>
        /// 设备IMSI分组数量
        /// </summary>
        public int SplitCount
        {
            get
            {
                return splitCount;
            }

            set
            {
                splitCount = value;
            }
        }
        public static List<Command> BaseCommands = new List<Command>()
        {
            new Command() {  CommandName="拨号", ThisType= Command.CommandType.NothingCmd,
                Duration =10000, ATCommandText="ATD{0};" },
            new Command() {  CommandName="拨号挂断", ThisType= Command.CommandType.BooleanCmd,
                ATCommandText="AT+CHUP" },
            new Command() {  CommandName="短信格式设置", ThisType= Command.CommandType.BooleanCmd,
                ATCommandText="AT+CMGF={0}" },
            new Command() {  CommandName="短信载体设置", ThisType= Command.CommandType.BooleanCmd,
                ATCommandText="AT+CPMS=\"{0}\"" },
            new SendSMSCommand {  CommandName="发送短信", ThisType= Command.CommandType.SendSMSCmd,
            ATCommandText="AT+CMGS==\"{0}\""}
        };
    }
}
