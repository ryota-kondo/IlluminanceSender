using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace IlluminanceSender.Interfaces
{
    public interface ICoreModel : INotifyPropertyChanged
    {
        IFetchSensorData FetchSensorData { get; set; }
    }
}
