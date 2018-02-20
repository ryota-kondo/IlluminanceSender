using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IlluminanceSender.Models;
using Newtonsoft.Json;

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

        private static HttpClient client = new HttpClient();

        private bool OnOffFlag;

        private float threshold;
        private string url;

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
                    .SetOngoing(true)
                    .SetContentIntent(pendingIntent)
                    .Build();

                StartForeground(startId, navigate);

                if (lux > threshold && !OnOffFlag)
                {
                    SendLuxData(1);
                    OnOffFlag = true;
                }
                if (lux < threshold && OnOffFlag)
                {
                    SendLuxData(0);
                    OnOffFlag = false;
                }
            }
        }

        // サービスの初期設定
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            // 設定情報の読み込み
            var setting = new Setting();
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, "setting.json");
            if (File.Exists(filePath))
            {
                setting = JsonConvert.DeserializeObject<Setting> (File.ReadAllText(filePath));
                threshold = setting.Threshold;
                url = setting.Url;
            }

            // センサの設定
            _manager = (SensorManager)Context.GetSystemService(Context.SensorService);
            _lightSensor = _manager.GetDefaultSensor(SensorType.Light);
            _manager.RegisterListener(this, _lightSensor, SensorDelay.Normal);
            
            // 通知使いまわし用設定
            Intent _intent = new Intent(Context, typeof(MainActivity));
            pendingIntent = PendingIntent.GetActivity(Context, 0, _intent, 0);
            this.startId = startId;

            navigate = new Notification.Builder(Context)
                .SetContentTitle("照度センサ")
                .SetSmallIcon(Resource.Drawable.icon)
                .SetContentText($"照度計測中です。{lux}")
                .SetOngoing(true) //常駐
                .SetContentIntent(pendingIntent)
                .Build();

            StartForeground(this.startId, navigate);


            return StartCommandResult.NotSticky;
        }

        private async void SendLuxData(int flag)
        {
            var json = "{ \"light\" :" + flag+ "}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                await client.PostAsync(url, content);
            }
            catch (Exception e)
            {
                // トーストで知らせる？
                Console.WriteLine(e);
            }
            
        }
    }
}