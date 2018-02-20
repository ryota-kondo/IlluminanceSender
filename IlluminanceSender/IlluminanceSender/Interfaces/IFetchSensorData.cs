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
