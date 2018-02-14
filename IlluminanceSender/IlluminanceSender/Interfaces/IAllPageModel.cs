using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace IlluminanceSender.Interfaces
{
    public interface IAllPageModel : INotifyPropertyChanged
    {
        int Temp { get; set; }
    }
}
