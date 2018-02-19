using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IlluminanceSender.Interfaces;
using IlluminanceSender.Models;
using Newtonsoft.Json;
using Prism.Services;
using Xamarin.Forms;

namespace IlluminanceSender.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IPageDialogService _pageDialogService;
        private readonly ICoreModel _model;

        // 適用ボタン
        public DelegateCommand SaveCommand { get; set; }

        // ON/OFFトグルスイッチ
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

        // 現在の照度値
        private string _illNum;
        public string IllNum
        {
            get => _illNum;
            set => SetProperty(ref _illNum, value);
        }

        // URL入力欄
        private string _url;
        public string Url
        {
            get => _url;
            set => SetProperty(ref _url, value);
        }


        // スレッショルド入力欄
        private string _threshold;
        public string Threshold
        {
            get => _threshold;
            set => SetProperty(ref _threshold, value);
        }

        // コンストラクタ
        public MainPageViewModel(IPageDialogService pageDialogService,INavigationService navigationService, ICoreModel model) : base(navigationService)
        {
            _pageDialogService = pageDialogService;
            this._model = model;

            SaveCommand = new DelegateCommand(Save);

            // 設定読み込み
            Setting data = new Setting();

            var json = _model.SaveAndLoad.LoadData();
            if (json != "")
            {
                data = JsonConvert.DeserializeObject<Setting>(json);
            }
            else
            {
                data.OnOff = false;
                data.Url = "http://";
                data.Threshold = 25;
                Save();
            }

            Title = "照度アプリ";

            _model.FetchSensorData.SetListener();
            StartTimer();

            // 設定情報の画面への反映
            StartSwitch = data.OnOff;
            Url = data.Url;
            Threshold = $"{data.Threshold}";
        }

        private void SwitchChange()
        {
            if (StartSwitch)
            {
                _model.BackgroundTask.StartBackgroundTask();
            }
            else
            {
                _model.BackgroundTask.StopBackgroundTask();
            }
        }

        private void Save()
        {
            var settingJson = new Setting();
            settingJson.OnOff = StartSwitch;
            settingJson.Threshold = float.Parse(Threshold);
            settingJson.Url = Url;

            var json = JsonConvert.SerializeObject(settingJson);

            _model.SaveAndLoad.SaveData(json);


            _pageDialogService.DisplayAlertAsync("Infomation", "データを保存しました", "OK");
        }


        private void StartTimer()
        {
            Device.StartTimer(
                TimeSpan.FromSeconds(1),
                () =>
                {
                    IllNum = $"{_model.FetchSensorData.GetIlluminabce()}";
                    return true;
                });
        }
    }
}
