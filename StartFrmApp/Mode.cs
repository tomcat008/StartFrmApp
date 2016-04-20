using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModemCtlApp
{
    public enum Mode
    {
        先将通知缓存起来再按照mt的值进行发送=0,
        在数据线空闲的情况下通知TE否则不通知TE=1,
        数据线空闲时直接通知TE否则先将通知缓存起来待数据线空闲时再行发送=2,
        直接通知TE在数据线被占用的情况下通知TE的消息将混合在数据中一起传输=3
    }
}
