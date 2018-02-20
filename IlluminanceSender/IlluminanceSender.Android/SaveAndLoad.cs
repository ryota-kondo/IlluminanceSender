using System.IO;
using IlluminanceSender.Interfaces;

namespace IlluminanceSender.Droid
{
    class SaveAndLoad : ISaveAndLoad
    {
        private string fileName = "setting.json";

        public string LoadSetting()
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, fileName);
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            return "";
        }

        public void SaveSetting(string json)
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, fileName);
            File.WriteAllText(filePath, json);
        }
    }
}