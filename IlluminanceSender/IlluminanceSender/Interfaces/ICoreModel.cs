using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace IlluminanceSender.Interfaces
{
    public interface ICoreModel : INotifyPropertyChanged
    {
        int Temp { get; set; }

        ISensorManager SensorManager { get; set; }
    }
}
