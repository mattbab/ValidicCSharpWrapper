using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PCLStorage;
using Validic.Core.AppLib.Helpers;
using Validic.Core.AppLib.Models;
using Validic.Core.AppLib.ViewModels;
using Validic.Mobile.DemoApp.Helpers;
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

        protected override async void OnStart()
        {
            await Load(_viewModel);
            BindingContext = _viewModel;
        }

        protected override async void OnSleep()
        {
            await Save(_viewModel);
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }


        string folderName = "Data";
        string fileName = "validic.json";

        private async Task Test1()
        {
            var text1 = "Hello!";
            await StorageHelper.WriteFileAsync(folderName, fileName, text1);
            var text2 = await StorageHelper.ReadFileAsync(folderName, fileName);
            var equal = text1.Equals(text2);
        }

        private async Task Load(MainViewModel viewModel)
        {
            var stream = await StorageHelper.GetFileStreamAsync(folderName, fileName);
            viewModel.LoadModel(stream);
        }

        private async Task Save(MainViewModel viewModel)
        {
            var stream = await StorageHelper.GetFileStreamAsync(folderName, fileName);
            viewModel.SaveModel(stream);
        }
    }
}