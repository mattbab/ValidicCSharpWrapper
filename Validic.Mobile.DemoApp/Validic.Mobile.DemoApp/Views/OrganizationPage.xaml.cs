using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Validic.Mobile.DemoApp.Views
{
    public partial class OrganizationPage : ContentPage
    {
        public Action ClickedOk;
        public Action ClickedCancel;

        private void OnClickedOk()
        {
            var tmp = ClickedOk;
            tmp?.Invoke();
        }
        private void OnClickedCancel()
        {
            var tmp = ClickedCancel;
            tmp?.Invoke();
        }
        public OrganizationPage()
        {
            InitializeComponent();
            ToolbarItemOk.Clicked += async (s, e) => await ToolbarItemOkClicked(s, e);
            ToolbarItemCancel.Clicked += async (s, e) => await ToolbarItemCancelClicked(s, e);
        }
        private async Task ToolbarItemCancelClicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PopAsync();
            OnClickedCancel();
        }

        private async Task ToolbarItemOkClicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PopAsync();
            OnClickedOk();
        }
    }
}
