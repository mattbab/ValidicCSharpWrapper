using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Validic.Mobile.DemoApp.Views
{
    public class OrganizationPage : ContentPage
    {
        public Action ClickedOk;
        public Action ClickedCancel;

        public OrganizationPage()
        {
            Content = CreateView();
            var buttonOk = new ToolbarItem {Text = "OK"};
            var buttonCancel = new ToolbarItem { Text = "Cancel" };
            ToolbarItems.Add(buttonOk);
            ToolbarItems.Add(buttonCancel);
            buttonOk.Clicked += async (s, e) => await ToolbarItemOkClicked(s, e);
            buttonCancel.Clicked += async (s, e) => await ToolbarItemCancelClicked(s, e);
        }

        private View CreateView()
        {
            // Grid "Buttons"
            var grid = new Grid()
            {
                Padding = new Thickness(5, 10, 0, 0),
                ColumnDefinitions = new ColumnDefinitionCollection()
                {
                    new ColumnDefinition {Width = new GridLength(5, GridUnitType.Absolute)},
                    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},
                    new ColumnDefinition {Width = new GridLength(5, GridUnitType.Absolute)},
                },
                RowDefinitions = new RowDefinitionCollection()
                {
                    new RowDefinition {Height = new GridLength(5, GridUnitType.Absolute)},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)},
                    new RowDefinition {Height = new GridLength(5, GridUnitType.Absolute)},
                }
            };

            // "ID"
            var labelId = new Label {Text = "ID"};
            var editorId = new Editor();
            editorId.SetBinding(Editor.TextProperty, "OrganizationId");

            // "Token"
            var labelToken = new Label { Text = "Token" };
            var editorToken = new Editor();
            editorToken.SetBinding(Editor.TextProperty, "AccessToken");

            grid.Children.Add(labelId, 1, 1);
            grid.Children.Add(editorId, 1, 2);
            grid.Children.Add(labelToken, 1, 3);
            grid.Children.Add(editorToken, 1, 4);

            return grid;
        }

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
