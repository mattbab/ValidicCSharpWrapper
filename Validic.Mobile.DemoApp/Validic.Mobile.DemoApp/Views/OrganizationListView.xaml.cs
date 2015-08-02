using System;
using System.Dynamic;
using Validic.Core.AppLib.ViewModels;
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
            var model = BindingContext as MainViewModel;
            if (model == null)
                return;

            model.AddOrganization += AddOrganization;
            model.DeleteOrganization += DeleteOrganization;
            model.ModifyOrganization += ModifyOrganization;
        }

        private void ModifyOrganization()
        {
            _log.Debug("ModifyOrganization");
            Navigation.PushModalAsync(new OrganizationPage());
        }

        private void DeleteOrganization()
        {
            _log.Debug("DeleteOrganization");
        }

        private void AddOrganization()
        {
            _log.Debug("AddOrganization");
            Navigation.PushModalAsync(new OrganizationPage());
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