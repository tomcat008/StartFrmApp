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
    public class HttpRequestHelper : IDisposable
    {
        private HttpRequestHelper()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
            {
                while (startWork)
                {
                    lock (HttpRequestHelper.helper.queue)
                    {
                        if (HttpRequestHelper.helper.queue.Count > 0)
                        {
                            var requestData = HttpRequestHelper.helper.queue.Dequeue();
                            Request(requestData);
                        }
                    }
                    Thread.Sleep(50);
                }
            }), null);
        }

        private static HttpRequestHelper helper = new HttpRequestHelper();
        public static HttpRequestHelper GetHelper()
        {
            return helper;
        }

        private Queue<RequestData> queue = new Queue<RequestData>();
        public enum RequestMethod
        {
            GET = 1, POST = 2, PUT = 3, DELETE = 4
        }
        public class RequestData
        {
            public class RequestDataKey
            {
                public const String IMSI = "IMSI";
                public const String CCID = "CCID";
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
            public Encoding Encoding { get; set; }

            private Hashtable Datas = new Hashtable();

            public void AddKeyValue(string key, string value)
            {
                if (Datas.ContainsKey(key))
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
                foreach (var key in Datas.Keys)
                {
                    sb.AppendFormat("{0}={1}&", key, Datas[key]);
                }
                if (Datas.Count > 0)
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
                Monitor.Pulse(this.queue);
            }
        }

        private bool startWork = true;

        public void StopWork()
        {
            startWork = false;
        }

        public void Dispose()
        {
            startWork = false;
        }

        private void Request(RequestData data)
        {
            if (data.Method == RequestMethod.POST)
            {
                Post(data);
            }
            else if (data.Method == RequestMethod.GET)
            {
                Get(data);
            }
            else if (data.Method == RequestMethod.PUT)
            {
                Put(data);
            }
            else if (data.Method == RequestMethod.DELETE)
            {
                Delete(data);
            }
        }
        private void Post(RequestData data)
        {
            var requestClient = (HttpWebRequest)WebRequest.Create(data.URL);
            requestClient.ContentType = data.ContentType;
            requestClient.Method = "POST";
            byte[] btBodys = data.GetRequestDataBody();
            requestClient.ContentLength = btBodys.Length;
            var xx = new  WebClient();
            using (var stream = requestClient.GetRequestStream())
            {
                stream.Write(btBodys, 0, btBodys.Length);
                stream.Close();
            }
            requestClient.GetRequestStreamAsync();
        }
        private void Get(RequestData data)
        {
            var requestClient = (HttpWebRequest)WebRequest.Create(data.GetRequestDataUrl());
            requestClient.Method = data.Method.ToString();
            requestClient.GetRequestStreamAsync();
        }
        private void Put(RequestData data)
        {
            var client = new WebClient();
            client.UploadDataAsync(new Uri(data.URL),"PUT",data.GetRequestDataBody());
        }
        private void Delete(RequestData data)
        {

        }
    }
}
