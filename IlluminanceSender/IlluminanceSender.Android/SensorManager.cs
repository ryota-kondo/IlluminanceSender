using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IlluminanceSender.Interfaces;
using Xamarin.Forms;

namespace IlluminanceSender.Droid
{
    class FetchSensorData :Activity, ISensorEventListener, IFetchSensorData
    {
        private static readonly Context Context = Android.App.Application.Context;

        private readonly SensorManager _manager;
        private readonly Sensor _lightSensor;

        private float lux;

        public FetchSensorData()
        {
            _manager = (SensorManager)Context.GetSystemService(Context.SensorService);
            _lightSensor = _manager.GetDefaultSensor(SensorType.Light);
        }

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
            // accuracy に変更があった時の処理
        }

        public void OnSensorChanged(SensorEvent e)
        {
            if (e.Sensor.Type == SensorType.Light)
            {
                lux = e.Values[0];

            }
        }

        public float GetIlluminabce()
        {
            return lux;
        }

        public void SetListener()
        {
            _manager.RegisterListener(this, _lightSensor, SensorDelay.Normal);
        }
        public void RemoveListener()
        {
            _manager.UnregisterListener(this);
        }
    }
}