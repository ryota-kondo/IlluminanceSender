using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IlluminanceSender.Interfaces;

namespace IlluminanceSender.Droid
{
    class SensorManager:ISensorManager
    {
        private double _count = 1;

        public double GetIlluminabce()
        {
            return 3.141592 + _count++;
        }
    }
}