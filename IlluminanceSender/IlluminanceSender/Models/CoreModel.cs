using System;
using System.Collections.Generic;
using System.Text;
using IlluminanceSender.Interfaces;
using Prism.Mvvm;

namespace IlluminanceSender.Models
{
    class CoreModel : BindableBase,ICoreModel
    {
        public int Temp { get; set; }
        public ISensorManager SensorManager { get; set; }

        public CoreModel(ISensorManager sensorManager)
        {
            Temp = 12;

            this.SensorManager = sensorManager;
        }
    }
}
