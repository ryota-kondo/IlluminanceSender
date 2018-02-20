using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace IlluminanceSender.Interfaces
{
    public interface ISendServer
    {
        // HttpClient client { get; set; }

        string url { get; set; }
        float th { get; set; }

        void CheckLux(float lux);
        // void SendLuxData(int status);
    }
}
