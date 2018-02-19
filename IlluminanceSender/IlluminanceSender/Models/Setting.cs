using System;
using System.Collections.Generic;
using System.Text;

namespace IlluminanceSender.Models
{
    public struct Setting
    {
        public bool OnOff { get; set; }
        public string Url { get; set; }
        public float Threshold { get; set; }
    }
}
