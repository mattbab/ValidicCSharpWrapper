using System;
using Xamarin.Forms;

namespace Validic.Mobile.DemoApp.Views
{
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            InitializeComponent();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            var page = new OrganizationView();
            // load services on the next page
            await Navigation.PushAsync(page);
        }
    }
}