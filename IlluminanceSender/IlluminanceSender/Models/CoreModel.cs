using System;
using System.Collections.Generic;
using System.Text;
using IlluminanceSender.Interfaces;
using Prism.Mvvm;

namespace IlluminanceSender.Models
{
    class CoreModel : BindableBase,ICoreModel
    {
        public IFetchSensorData FetchSensorData { get; set; }
        public IBackgroundTask BackgroundTask { get; set; }
        public ISaveAndLoad SaveAndLoad { get; set; }

        public CoreModel(IFetchSensorData fetchSensorData,IBackgroundTask backgroundTask,ISaveAndLoad saveAndLoad)
        {
            this.FetchSensorData = fetchSensorData;
            this.BackgroundTask = backgroundTask;
            this.SaveAndLoad = saveAndLoad;
        }
    }
}
