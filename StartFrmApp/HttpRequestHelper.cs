using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModemCtlApp
{
    public class HttpRequestHelper
    {
        private HttpRequestHelper() { }

        private static HttpRequestHelper helper = new HttpRequestHelper();
        public static HttpRequestHelper GetHelper()
        {
            return helper;
        }

        private Queue<RequestData> queue = new Queue<RequestData>();
        public enum RequestMethod
        {
            GET=1,POST=2
        }
        public class RequestData
        {
            public class RequestDataKey
            {
                public const String IMSI = "IMSI";
                public const String PhoneNumber = "PhoneNumber";
                public const String Phone = "phone";

            }
            public RequestData()
            {
                this.Encoding = System.Text.Encoding.UTF8;
            }
            public RequestMethod Method { get; set; }
            public String URL { get; set; }
            public String ContentType { get; set; }
            public Encoding Encoding {get;set;}

            private Hashtable Datas = new Hashtable();

            public void AddKeyValue(string key,string value)
            {
                if(Datas.ContainsKey(key))
                {
                    Datas[key] = value;
                }
                else
                {
                    Datas.Add(key, value);
                }
            }
            public void RemoveKey(string key)
            {
                Datas.Remove(key);
            }
            //public byte[] GetRequestDataBody(out int bodyLength)
            public byte[] GetRequestDataBody()
            {
                StringBuilder sb = new StringBuilder();
                foreach(var key in Datas.Keys)
                {
                    sb.AppendFormat("{0}={1}&", key, Datas[key]);
                }
                if(Datas.Count > 0)
                {
                    sb.Remove(sb.Length - 1, 1);
                }
                //bodyLength = sb.Length;
                return this.Encoding.GetBytes(sb.ToString());
            }
            public String GetRequestDataUrl()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(URL);
                sb.Append('?');
                foreach (var key in Datas.Keys)
                {
                    sb.AppendFormat("{0}={1}&", key, Datas[key]);
                }
                if (Datas.Count > 0)
                {
                    sb.Remove(sb.Length - 1, 1);
                }
                return sb.ToString();
            }
        }
        public void AddRequestData(RequestData request)
        {
            queue.Enqueue(request);
        }
        public void AddRequestDataForThread(RequestData request)
        {
            lock (this.queue)
            {
                queue.Enqueue(request);
                Monitor.Pulse(this);
            }
        }

        private bool startWork = false;
        public void StartWork()
        {
            if (startWork)
                return;

            startWork = true;
            ThreadPool.QueueUserWorkItem(new WaitCallback((obj) => {
                while(startWork)
                {
                    lock(HttpRequestHelper.helper.queue)
                    {
                        Monitor.Wait(HttpRequestHelper.helper.queue);
                        var requestData = HttpRequestHelper.helper.queue.Dequeue();
                        
                        if(requestData.Method == RequestMethod.POST)
                        {
                            var requestClient = (HttpWebRequest)WebRequest.Create(requestData.URL);
                            requestClient.ContentType = requestData.ContentType;
                            requestClient.Method = requestData.Method.ToString();
                            byte[] btBodys = requestData.GetRequestDataBody();
                            requestClient.ContentLength = btBodys.Length;
                            using (var stream = requestClient.GetRequestStream())
                            {
                                stream.Write(btBodys, 0, btBodys.Length);
                                stream.Close();
                            }
                            requestClient.GetRequestStreamAsync();
                        }
                        else
                        {
                            var requestClient = (HttpWebRequest)WebRequest.Create(requestData.GetRequestDataUrl());
                            requestClient.Method = requestData.Method.ToString();
                            requestClient.GetRequestStreamAsync();
                        }
                    }
                }
            }), null);
        }
        public void StopWork()
        {
            startWork = false;
        }
        public void Post()
        {

        }
    }
}
