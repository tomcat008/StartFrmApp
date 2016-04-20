using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModemCtlApp
{
    public enum MT
    {
        接受的短消息存储到默认的内存位置包括class3不通知TE = 0,
        接收的短消息储存到默认的内存位置, 并且向TE发出通知包括class3,
        对于class2短消息储存到SIM卡并且向TE发出通知对于其他class直接将短消息转发到TE = 2,
        对于class3短消息直接转发到TE同mt2对于其他class同mt1 = 3
    }
}
