using System;
using System.Collections.Generic;
using System.Text;

namespace IlluminanceSender.Interfaces
{
    // フォアグラウンド用センサ取得
    public interface IFetchSensorData
    {
        float GetIlluminabce();
        void SetListener();
        void RemoveListener();
    }
}
