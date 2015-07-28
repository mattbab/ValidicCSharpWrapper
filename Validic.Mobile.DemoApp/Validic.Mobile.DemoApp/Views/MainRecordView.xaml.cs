using System;
using System.Threading.Tasks;
using Validic.Core.AppLib.ViewModels;
using Xamarin.Forms;

namespace Validic.Mobile.DemoApp.Views
{
    public partial class MainRecordView : TabbedPage
    {
        public MainRecordView()
        {
            InitializeComponent();
            CurrentPageChanged += async (s, e) => await UpdatePageAsync();
            this.Appearing += OnAppearing;
        }

        private async void OnAppearing(object sender, EventArgs eventArgs)
        {
            CurrentPage = Children[0];
            await Task.Delay(1000);
            await UpdatePageAsync();
        }



        private async Task UpdatePageAsync()
        {
            var title = CurrentPage.Title;
            if (title.ToUpper() == "ME")
            {
                var model = BindingContext as MainViewModel;
                if (model == null)
                    return;

                await model.SelectedMainRecord.GetOrganizationMeDataAsync();
            }
        }
    }
}