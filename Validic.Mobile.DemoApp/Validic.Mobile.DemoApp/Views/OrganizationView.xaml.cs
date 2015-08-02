using System;
using Xamarin.Forms;

namespace Validic.Mobile.DemoApp.Views
{
    public partial class OrganizationView : ContentPage
    {
        public OrganizationView()
        {
            InitializeComponent();
            Title = "Organizations";
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            var page = new MainRecordView();
            // page.CurrentPage = page.Children[0];
            // load services on the next page
            await Navigation.PushAsync(page);
//            page.SelectFirstPage();
//            await page.UpdatePageAsync();
        }
    }
}