using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IlluminanceSender.Interfaces;
using Xamarin.Forms;

namespace IlluminanceSender.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly ICoreModel _model;

        private bool _startSwitch;
        public bool StartSwitch
        {
            get => _startSwitch;
            set
            {
                SetProperty(ref _startSwitch, value);
                SwitchChange();
            }
        }

        private string _illNum;
        public string IllNum
        {
            get => _illNum;
            set => SetProperty(ref _illNum, value);
        }

        public MainPageViewModel(INavigationService navigationService, ICoreModel model) : base(navigationService)
        {
            this._model = model;
            Title = "照度アプリ";
            StartTimer();
        }

        private void SwitchChange()
        {
            if (StartSwitch)
            {
                StartTimer();
                _model.FetchSensorData.SetListener();
            }
            else
            {
                _model.FetchSensorData.RemoveListener();
            }
        }


        private void StartTimer()
        {
            Device.StartTimer(
                TimeSpan.FromSeconds(1),
                () =>
                {
                    IllNum = $"LUX => {_model.FetchSensorData.GetIlluminabce()}";
                    if (!StartSwitch)
                    {
                        IllNum = $"LUX => No Data";
                    }
                    return StartSwitch;
                });
        }
    }
}
