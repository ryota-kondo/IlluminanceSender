using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using IlluminanceSender.Interfaces;

namespace IlluminanceSender.Models
{
    class SendServer : ISendServer
    {
        private HttpClient client;

        public string url { get; set; }
        public float th { get; set; }

        private float oldLux;

        public SendServer(string url, float th)
        {
            client = new HttpClient();

            this.url = url;
            this.th = th;
        }

        public void SetSetting(string url, float th)
        {
            this.url = url;
            this.th = th;
        }


        private void SendLuxData(int status)
        {
            var json = "{ \"light\" :" + status + "}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                client.PostAsync(url, content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        public void CheckLux(float lux)
        {
            if (Math.Abs(oldLux - lux) > 10)
            {
                if (lux > th)
                {
                    SendLuxData(1);
                }
                else
                {
                    SendLuxData(0);
                }
            }
            oldLux = lux;
        }
    }
}
