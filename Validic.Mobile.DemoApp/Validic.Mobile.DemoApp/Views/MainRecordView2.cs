using System;
using System.Threading.Tasks;
using Validic.Core.AppLib.ViewModels;
using Xamarin.Forms;

namespace Validic.Mobile.DemoApp.Views
{
    public class MainRecordView2 : TabbedPage
    {
        public MainRecordView2()
        {
            Title = "Static Tabs Sample 3";
            Children.Add(new ListViewDemoPage {Title = "Me"});
            Children.Add(new ListViewDemoPage {Title = "Profile"});
            Children.Add(new ListViewDemoPage {Title = "Weight"});
            Children.Add(new ListViewDemoPage {Title = "Biometrics"});
            Children.Add(new ListViewDemoPage {Title = "Fitness"});
            Children.Add(new ListViewDemoPage {Title = "Diabetes"});
            Children.Add(new ListViewDemoPage {Title = "Routine"});
            Children.Add(new ListViewDemoPage {Title = "Sleep"});
            Children.Add(new ListViewDemoPage {Title = "Tobacco Cessation"});
            Children.Add(new ListViewDemoPage {Title = "Apps"});

            CurrentPageChanged += async (s, e) => await UpdatePageAsync();
            Appearing += OnAppearing;
        }

        private async void OnAppearing(object sender, EventArgs eventArgs)
        {
            CurrentPage = Children[0];
            // await Task.Delay(1000);
            // await UpdatePageAsync();
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