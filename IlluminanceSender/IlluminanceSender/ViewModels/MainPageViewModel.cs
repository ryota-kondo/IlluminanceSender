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

        private SendServer sendServer;

        private bool forgroundFlag;

        // 適用ボタン
        public DelegateCommand SaveCommand { get; set; }

        // ON/OFFトグルスイッチ(フォアグラウンドのみ)
        private bool _startSwitch;
        public bool StartSwitch
        {
            get => _startSwitch;
            set => SetProperty(ref _startSwitch, value);
        }

        // ForgroundOrBackgroundトグルスイッチ
        private bool _forgroundOrBackground;
        public bool ForgroundOrBackground
        {
            get => _forgroundOrBackground;
            set => SetProperty(ref _forgroundOrBackground, value);
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

            SaveCommand = new DelegateCommand(SettingSave);

            // 設定読み込み
            Setting data = new Setting();
            var json = _model.SaveAndLoad.LoadSetting();
            if (json != "")
            {
                data = JsonConvert.DeserializeObject<Setting>(json);
            }
            else
            {
                data.OnOff = false;
                data.Url = "http://";
                data.Threshold = 25;

                _model.SaveAndLoad.SaveSetting(JsonConvert.SerializeObject(data));
            }

            sendServer = new SendServer(data.Url,data.Threshold);

            Title = "照度アプリ";

            _model.FetchSensorData.SetListener();
            StartTimer();

            // 設定情報の画面への反映
            StartSwitch = data.OnOff;
            ForgroundOrBackground = data.ForgroundOrBackground;
            forgroundFlag = data.ForgroundOrBackground;
            Url = data.Url;
            Threshold = $"{data.Threshold}";
        }

        private void SettingSave()
        {
            var settingJson = new Setting();
            settingJson.OnOff = StartSwitch;
            settingJson.Threshold = float.Parse(Threshold);
            settingJson.Url = Url;
            settingJson.ForgroundOrBackground = ForgroundOrBackground;

            var json = JsonConvert.SerializeObject(settingJson);

            _model.SaveAndLoad.SaveSetting(json);


            _pageDialogService.DisplayAlertAsync("Infomation", "設定を適用しました", "OK");

            //　設定の動作を適用
            sendServer.SetSetting(settingJson.Url,settingJson.Threshold);
            _model.BackgroundTask.StopBackgroundTask();
            if (settingJson.ForgroundOrBackground)
            {
                _model.BackgroundTask.StartBackgroundTask();
            }
            forgroundFlag = settingJson.ForgroundOrBackground;
        }


        private void StartTimer()
        {
            Device.StartTimer(
                TimeSpan.FromSeconds(1),
                () =>
                {
                    var lux = _model.FetchSensorData.GetIlluminabce();
                    IllNum = $"{lux}";
                    if (forgroundFlag)
                    {
                        sendServer.CheckLux(lux);
                    }
                    return true;
                });
        }
    }
}
