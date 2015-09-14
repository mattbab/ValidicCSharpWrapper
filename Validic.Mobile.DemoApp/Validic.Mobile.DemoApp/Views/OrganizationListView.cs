using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Validic.Core.AppLib.ViewModels;
using Validic.Core.Model;
using Validic.Logging;
using Validic.Mobile.DemoApp.Helpers;
using Xamarin.Forms;

namespace Validic.Mobile.DemoApp.Views
{
    public class OrganizationListView : ContentPage
    {
        private readonly ILog _log = LogManager.GetLogger("OrganizationListView");

        public OrganizationListView()
        {
            Title = "Organizations";
            BindingContextChanged += OnBindingContextChanged;
            Content = CreateView();
        }

        private View CreateView()
        {
            // Oganizations
            var listView1 = new ListView();
            listView1.SetBinding(ListView.ItemsSourceProperty, "MainRecords");
            listView1.SetBinding(ListView.SelectedItemProperty, "SelectedMainRecord");
            var template1 = new DataTemplate(typeof (TextCell));
            template1.SetBinding(TextCell.TextProperty, "OrganizationAuthenticationCredential.OrganizationId");
            template1.SetBinding(TextCell.DetailProperty, "OrganizationAuthenticationCredential.AccessToken");
            listView1.ItemTemplate = template1;
            listView1.ItemTapped += ListViewOganizationsOnItemTapped;
            listView1.ItemSelected += ListViewOganizationsOnItemSelected;

            // Fields
            var listView2 = new ListView();
            listView2.SetBinding(ListView.ItemsSourceProperty, "SelectedMainRecord.Organization.Fields");
            var template2 = new DataTemplate(typeof (TextCell));
            template2.SetBinding(TextCell.TextProperty, "Name");
            template2.SetBinding(TextCell.DetailProperty, "Value");
            listView2.ItemTemplate = template2;

            // Grid "Buttons"
            var gridButtons = new Grid()
            {
                Padding = new Thickness(5, 10, 0, 0),
                ColumnDefinitions = new ColumnDefinitionCollection()
                {
                    new ColumnDefinition {Width = GridLength.Auto},
                    new ColumnDefinition {Width = GridLength.Auto},
                    new ColumnDefinition {Width = GridLength.Auto},
                    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},
                }
            };

            // Button "Add"
            var buttonAdd = new Button {Text = "Add"};
            buttonAdd.SetBinding(Button.CommandProperty, "CommandAddOrganization");

            // Button "Delete"
            var buttonDelete = new Button {Text = "Delete"};
            buttonDelete.SetBinding(Button.CommandProperty, "CommandDeleteOrganization");
            buttonDelete.SetBinding(Button.IsEnabledProperty, "CanDeleteOrganization");

            // Button "Modify"
            var buttonModify = new Button {Text = "Modify"};
            buttonModify.SetBinding(Button.CommandProperty, "CommandModifyOrganization");
            buttonModify.SetBinding(Button.IsEnabledProperty, "CanModifyOrganization");

            gridButtons.Children.Add(buttonAdd, 0, 0);
            gridButtons.Children.Add(buttonDelete, 1, 0);
            gridButtons.Children.Add(buttonModify, 2, 0);

            var grid = new Grid()
            {
                Padding = new Thickness(5, 10, 0, 0),
                RowDefinitions = new RowDefinitionCollection()
                {
                    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)},
                    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)},
                    new RowDefinition {Height = GridLength.Auto},
                }
            };

            grid.Children.Add(listView1, 0, 0);
            grid.Children.Add(listView2, 0, 1);
            grid.Children.Add(gridButtons, 0, 2);

            return grid;
        }

        private void OnBindingContextChanged(object sender, EventArgs eventArgs)
        {
            var model = BindingContext as MainViewModel;
            if (model == null)
                return;

            model.AddOrganization += async () => await AddOrganization();
            model.DeleteOrganization += async () => await DeleteOrganization();
            model.ModifyOrganization += async () => await ModifyOrganization();
        }

        private async Task ModifyOrganization()
        {
            try
            {
                _log.Debug("ModifyOrganization");
                var model = BindingContext as MainViewModel;
                if (model == null)
                    return;

                var selectedMainRecord = model.SelectedMainRecord;
                var currentCredential = selectedMainRecord.OrganizationAuthenticationCredential;
                var newCredential = new OrganizationAuthenticationCredentials
                {
                    OrganizationId = currentCredential.OrganizationId,
                    AccessToken = currentCredential.AccessToken
                };

                var page = new OrganizationPage();
                page.BindingContext = newCredential;
                page.ClickedOk += () =>
                {
                    selectedMainRecord.OrganizationAuthenticationCredential = newCredential;
                };
                page.ClickedCancel += () => { };
                await Navigation.PushAsync(page);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
        }

        private async Task DeleteOrganization()
        {
            _log.Debug("DeleteOrganization");
            await Navigation.PushAsync(new OrganizationPage());
        }

        private async Task AddOrganization()
        {
            _log.Debug("AddOrganization");
            await Navigation.PushAsync(new OrganizationPage());
        }

        private void ListViewOganizationsOnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            _log.Debug("OnItemSelected: {0}", args.SelectedItem);
        }

        private void ListViewOganizationsOnItemTapped(object sender, ItemTappedEventArgs args)
        {
            _log.Debug("OnItemTapped: {0} ", args.Item);
        }
    }
}
