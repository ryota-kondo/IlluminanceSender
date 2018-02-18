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

        private double _count;

        private bool _startup;
        public bool StartUp
        {
            get => _startup;
            set => SetProperty(ref _startup, value);
        }

        private string _illNum;
        public string IllNum
        {
            get => _illNum;
            set => SetProperty(ref _illNum, value);
        }

        private string _onOffText;
        public string OnOffText
        {
            get => _onOffText;
            set => SetProperty(ref _onOffText, value);
        }


        public MainPageViewModel(INavigationService navigationService, ICoreModel model) : base(navigationService)
        {
            this._model = model;
            Title = "Main Page";
            StartTimer();
        }


        private void StartTimer()
        {
            Device.StartTimer(
                TimeSpan.FromSeconds(1),
                () =>
                {
                    // タイマーを繰り返すかどうかの判定
                    var keepRecurring = _count < 10;
                    if (!keepRecurring)
                    {
                        // タイマー終了時の処理
                        _count = 0;
                    }

                    // カウントをラベルのテキストに設定
                    IllNum = $"{_model.SensorManager.GetIlluminabce()}";
                    return keepRecurring;
                });
        }
    }
}
