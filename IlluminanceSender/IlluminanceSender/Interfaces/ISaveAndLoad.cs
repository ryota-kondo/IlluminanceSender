namespace IlluminanceSender.Interfaces
{
    public interface ISaveAndLoad
    {
        void SaveSetting(string json);
        string LoadSetting();
    }
}
