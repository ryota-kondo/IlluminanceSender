using System;
using System.Collections.Generic;
using System.Text;

namespace IlluminanceSender.Interfaces
{
    public interface IFetchSensorData
    {
        float GetIlluminabce();
        void SetListener();
        void RemoveListener();
    }
}
