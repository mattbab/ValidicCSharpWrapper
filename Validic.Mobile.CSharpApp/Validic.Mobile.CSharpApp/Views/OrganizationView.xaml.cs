using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using Xamarin.Forms;

namespace Validic.Mobile.CSharpApp.Views
{
    public partial class OrganizationView : ContentPage
    {
        public OrganizationView()
        {
            InitializeComponent();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            var page = new MainRecordView();
            page.CurrentPage = page.Children[0];
            // load services on the next page
            await Navigation.PushAsync(page);
//            page.SelectFirstPage();
//            await page.UpdatePageAsync();
        }
    }
}