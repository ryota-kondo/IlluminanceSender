using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IlluminanceSender.Interfaces;

namespace IlluminanceSender.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IAllPageModel _model;

        private bool _startup;
        public bool StartUp
        {
            get => _startup;
            set
            {
                SetProperty(ref _startup, value);
                _model.Temp++;
                Title = $"Main Page{_model.Temp}";
            }
        }


        public MainPageViewModel(INavigationService navigationService, IAllPageModel model): base(navigationService)
        {
            this._model = model;
            Title = "Main Page";
        }
    }
}
