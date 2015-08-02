using System.Reflection;
using MasterDetail;
using Validic.Core.AppLib.ViewModels;
using Validic.Mobile.DemoApp.Views;
using Xamarin.Forms;

namespace Validic.Mobile.DemoApp
{
    public class App : Application
    {
        private readonly MainViewModel _viewModel = new MainViewModel();

        public App()
        {
            // The root page of your application
            // MainPage = new NavigationPage(new MainView());
            MainPage = new RootPage();
        }

        protected override void OnStart()
        {
            LoadModel(_viewModel);
            BindingContext = _viewModel;
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void LoadModel(MainViewModel model)
        {
            var assembly = typeof (App).GetTypeInfo().Assembly;
            // var resourceNames = assembly.GetManifestResourceNames();
            var name = "Validic.Mobile.DemoApp.Resources.validic.json";
            var stream = assembly.GetManifestResourceStream(name);
            model.LoadModel(stream, true);
        }
    }
}