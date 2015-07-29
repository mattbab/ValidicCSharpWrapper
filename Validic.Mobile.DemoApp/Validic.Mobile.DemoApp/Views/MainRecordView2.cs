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

            Appearing += OnAppearing;
        }

        private void OnAppearing(object sender, EventArgs eventArgs)
        {
            CurrentPage = Children[0];
        }
    }
}