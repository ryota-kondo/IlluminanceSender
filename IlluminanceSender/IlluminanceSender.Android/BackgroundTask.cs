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
using Android.Provider;

namespace IlluminanceSender.Droid
{
    class BackgroundTask : IBackgroundTask
    {
        private static readonly Context Context = Android.App.Application.Context;
        private readonly Intent _intent = new Intent(Context, typeof(SensorService));

        public void StartBackgroundTask()
        {
            Context.StartService(_intent);
        }

        public void StopBackgroundTask()
        {
            Context.StopService(_intent);
        }
    }
}