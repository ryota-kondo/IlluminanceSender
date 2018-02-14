using IlluminanceSender.Interfaces;
using IlluminanceSender.Models;
using Prism;
using Prism.Ioc;
using IlluminanceSender.ViewModels;
using IlluminanceSender.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Unity;
using Unity.Lifetime;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace IlluminanceSender
{
    public partial class App : PrismApplication
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();

            containerRegistry.Register<IAllPageModel, AllPageModel>();
        }
    }
}
