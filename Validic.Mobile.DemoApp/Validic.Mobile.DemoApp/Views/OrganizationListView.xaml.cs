using System;
using System.Dynamic;
using System.Threading.Tasks;
using Validic.Core.AppLib.ViewModels;
using Validic.Core.Model;
using Validic.Logging;
using Xamarin.Forms;

namespace Validic.Mobile.DemoApp.Views
{
    public partial class OrganizationListView : ContentPage
    {
        private readonly ILog _log = LogManager.GetLogger("DataViewPage");
        public OrganizationListView()
        {
            InitializeComponent();
            Title = "Organizations";
            ListViewOganizations.ItemTapped += ListViewOganizationsOnItemTapped;
            ListViewOganizations.ItemSelected += ListViewOganizationsOnItemSelected;
            this.BindingContextChanged += OnBindingContextChanged;
         }

        private void OnBindingContextChanged(object sender, EventArgs eventArgs)
        {
            var model = BindingContext as Main;
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
                var model = BindingContext as Main;
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