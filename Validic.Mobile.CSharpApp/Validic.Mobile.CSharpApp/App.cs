using System.Reflection;
using Validic.CSharp.AppLib.ViewModels;
using Validic.Mobile.CSharpApp.Views;
using Xamarin.Forms;

namespace Validic.Mobile.CSharpApp
{
    public class App : Application
    {
        private readonly MainViewModel _viewModel = new MainViewModel();

        public App()
        {
            // The root page of your application
            MainPage = new NavigationPage(new MainView());
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
            var assembly = typeof (MainView).GetTypeInfo().Assembly;
            var resourceNames = assembly.GetManifestResourceNames();
            var name = "Validic.Mobile.CSharpApp.Resources.validic.json";
            var stream = assembly.GetManifestResourceStream(name);
            model.LoadModel(stream, true);
        }
    }
}