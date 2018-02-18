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

namespace IlluminanceSender.Droid
{
    [Service]
    class SensorService : Service, ISensorEventListener
    {
        private static readonly Context Context = Android.App.Application.Context;
        private SensorManager _manager;
        private Sensor _lightSensor;
        private float lux;

        Notification navigate;
        PendingIntent pendingIntent;
        private int startId;

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
            // 後で調べる
        }

        public override IBinder OnBind(Intent intent) => null;

        // センサデータ取得
        public void OnSensorChanged(SensorEvent e)
        {
            if (e.Sensor.Type == SensorType.Light)
            {
                lux = e.Values[0];

                navigate = new Notification.Builder(Context)
                    .SetContentTitle("照度センサ")
                    .SetSmallIcon(Resource.Drawable.icon)
                    .SetContentText($"照度計測中です。{lux}")
                    .SetOngoing(true) //常駐させる
                    .SetContentIntent(pendingIntent)
                    .Build();

                StartForeground(startId, navigate);
            }
        }

        // サービスの初期設定
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            // センサの設定
            _manager = (SensorManager)Context.GetSystemService(Context.SensorService);
            _lightSensor = _manager.GetDefaultSensor(SensorType.Light);
            _manager.RegisterListener(this, _lightSensor, SensorDelay.Normal);
            this.startId = startId;

            Intent _intent = new Intent(Context, typeof(MainActivity));
            pendingIntent = PendingIntent.GetActivity(Context, 0, _intent, 0);

            navigate = new Notification.Builder(Context)
                .SetContentTitle("照度センサ")
                .SetSmallIcon(Resource.Drawable.icon)
                .SetContentText($"照度計測中です。{lux}")
                .SetOngoing(true) //常駐させる
                .SetContentIntent(pendingIntent)
                .Build();

            StartForeground(this.startId, navigate);


            return StartCommandResult.NotSticky;
        }
    }
}