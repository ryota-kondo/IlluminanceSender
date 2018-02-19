
namespace IlluminanceSender.Interfaces
{
    // バックグラウンド用センサ取得
    public interface IBackgroundTask
    {
        void StartBackgroundTask();
        void StopBackgroundTask();
    }
}