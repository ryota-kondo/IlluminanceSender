using System;
using System.Collections.Generic;
using System.Text;

namespace IlluminanceSender.Interfaces
{
    public interface ISaveAndLoad
    {
        void SaveData(string json);
        string LoadData();
    }
}
