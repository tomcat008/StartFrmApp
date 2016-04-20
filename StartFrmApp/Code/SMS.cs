namespace ModemCtlApp.Code
{
    public class SMS
    {
        public enum ReadSMSState { Error = -1, Empty = 0, Success = 1 }
        string msgCenter;
        string phone;
        string msg;
        string time;

        public string MsgCenter
        {
            get
            {
                return msgCenter;
            }

            set
            {
                msgCenter = value;
            }
        }

        public string Phone
        {
            get
            {
                return phone;
            }

            set
            {
                phone = value;
            }
        }

        public string Msg
        {
            get
            {
                return msg;
            }

            set
            {
                msg = value;
            }
        }

        public string Time
        {
            get
            {
                return time;
            }

            set
            {
                time = value;
            }
        }

        public ReadSMSState ReadState { get; set; }
    }
}
