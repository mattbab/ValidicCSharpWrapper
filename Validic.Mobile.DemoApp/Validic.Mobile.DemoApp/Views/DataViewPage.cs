using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Validic.Core.AppLib.ViewModels;
using Validic.Logging;
using Validic.Mobile.DemoApp.Helpers;
using Xamarin.Forms;

namespace Validic.Mobile.DemoApp.Views
{
    internal class DataViewPage : ContentPage
    {
        #region Constants


        #endregion


        private readonly ILog _log = LogManager.GetLogger("DataViewPage");

        private ListView _listView;

        public DataViewPage()
        {
            CreateListView();
        }

        public void CreateListView()
        {
            var header = new Label
            {
                Text = Title,
                Font = Font.BoldSystemFontOfSize(50),
                HorizontalOptions = LayoutOptions.Center
            };


            // Define some data.
            _listView = new ListView
            {
                // Source of data items.
                // AK ItemsSource = people,
                // Define template for displaying each item.
                // (Argument of DataTemplate constructor is called for 
                //      each item; it must return a Cell derivative.)
                ItemTemplate = new DataTemplate(LoadTemplate)
            };

            // Accomodate iPhone status bar.
            Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            // Build the page.
            Content = new StackLayout
            {
                Children =
                {
                    header,
                    _listView
                }
            };
        }

        private object _lastTemplate = null;

        private object LoadTemplate()
        {
            _log.Debug("LoadTemplate");
            if (_lastTemplate == null)
                CreateMeView();


            return _lastTemplate;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _log.Debug("[OnAppearing] : Title = {0}", Title);
            var model = BindingContext as MainViewModel;
            if (model == null)
                return;

            var title = Title.ToUpper();
            switch (title)
            {
                case "ME":
                    await model.SelectedMainRecord.GetOrganizationMeDataAsync();
                    Show(CreateMeView(), 40, model.SelectedMainRecord.MeData);
                    break;
                case "PROFILE":
                    await model.SelectedMainRecord.GetOrganizationProfiles();
                    Show(CreateGenericDataView(BindingInfoLists.ProfileBindingList), 40, model.SelectedMainRecord.Profiles);
                    break;
                case "FITNESS":
                    await model.SelectedMainRecord.GetOrganizationFitnessData();
                    Show(CreateDataView(BindingInfoLists.FitnessBindingList), 400, model.SelectedMainRecord.FitnessData);
                    break;
                case "BIOMETRICS":
                    await model.SelectedMainRecord.GetOrganizationBiometrics();
                    Show(CreateDataView(BindingInfoLists.BiometricsBindingList), 900, model.SelectedMainRecord.Biometrics);
                    break;
            }
        }

        private void Show(View view, int  rowHeight, IEnumerable itemSource)
        {
            _lastTemplate = new ViewCell { View = view };
            _listView.RowHeight = rowHeight;
            _listView.ItemsSource = itemSource;
        }

        private View CreateMeView()
        {
            // Return an assembled ViewCell.

            var view = new StackLayout
            {
                Padding = new Thickness(0, 5),
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    //boxView,
                    new StackLayout
                    {
                        VerticalOptions = LayoutOptions.Center,
                        Spacing = 0,
                        Children =
                        {
                            GridHelper.CreateLabel("Me.Id")
                            //birthdayLabel
                        }
                    }
                }
            };
            return view;
        }

        #region Stattic Functions

        private static View CreateDataView(Dictionary<string, string> bindingList)
        {
            return CreateDataView(BindingInfoLists.MesasurmentBindingList, bindingList);
        }

        private static View CreateDataView(Dictionary<string, string> bindingList1, Dictionary<string, string> bindingList2)
        {
            var bindingList = new Dictionary<string, string>();

            foreach (var item in bindingList1)
                bindingList.Add(item.Key, item.Value);

            foreach (var item in bindingList2)
                bindingList.Add(item.Key, item.Value);

            return CreateGenericDataView(bindingList);

        }

        private static View CreateGenericDataView(Dictionary<string,string> bindingList)
        {
            var rowDefinitions = new RowDefinitionCollection();
            var colDefinitions = new ColumnDefinitionCollection()
            {
                new ColumnDefinition {Width = new GridLength(100, GridUnitType.Absolute)},
                new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},
            };

            // Add Rows
            foreach (var item in bindingList)
                rowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            var grid = new Grid
            {
                Padding = new Thickness(5, 10, 0, 0),
                ColumnDefinitions = colDefinitions,
                RowDefinitions = rowDefinitions
            };

            // Add Data
            var row = 0;
            foreach (var item in bindingList)
                grid.AddRow(row++, item.Key, item.Value);

            return grid;
        }

        #endregion
    }
}