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
            Children.Add(new DataViewPage {Title = "Me"});
            Children.Add(new DataViewPage {Title = "Profile"});
            Children.Add(new DataViewPage {Title = "Weight"});
            Children.Add(new DataViewPage {Title = "Biometrics"});
            Children.Add(new DataViewPage {Title = "Fitness"});
            Children.Add(new DataViewPage {Title = "Diabetes"});
            Children.Add(new DataViewPage {Title = "Routine"});
            Children.Add(new DataViewPage {Title = "Sleep"});
            Children.Add(new DataViewPage {Title = "Tobacco Cessation"});
            Children.Add(new DataViewPage {Title = "Apps"});

            Appearing += OnAppearing;
        }

        private void OnAppearing(object sender, EventArgs eventArgs)
        {
            CurrentPage = Children[0];
        }
    }
}