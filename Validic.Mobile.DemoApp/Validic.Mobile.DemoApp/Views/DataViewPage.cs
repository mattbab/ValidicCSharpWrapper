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

        #region Members

        private readonly ILog _log = LogManager.GetLogger("DataViewPage");
        private ListView _listView;
        private object _lastTemplate = null;

        #endregion

        public DataViewPage()
        {
            // Define some data.
            _listView = new ListView { ItemTemplate = new DataTemplate(LoadTemplate) };

            // Build the page.
            Content = _listView;
        }

        private object LoadTemplate()
        {
            _log.Debug("LoadTemplate");
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
                    Show(CreateGenericGridView(BindingInfoLists.MeBindingList), 40, model.SelectedMainRecord.MeData);
                    break;
                case "PROFILE":
                    await model.SelectedMainRecord.GetOrganizationProfiles();
                    Show(CreateGenericGridView(BindingInfoLists.ProfileBindingList), 40, model.SelectedMainRecord.Profiles);
                    break;
                case "WEIGHT":
                    await model.SelectedMainRecord.GetOrganizationWeight();
                    Show(CreateGridView(BindingInfoLists.WeightBindingList), 400, model.SelectedMainRecord.Weights);
                    break;
                case "BIOMETRICS":
                    await model.SelectedMainRecord.GetOrganizationBiometrics();
                    Show(CreateGridView(BindingInfoLists.BiometricsBindingList), 900, model.SelectedMainRecord.Biometrics);
                    break;
                case "FITNESS":
                    await model.SelectedMainRecord.GetOrganizationFitnessData();
                    Show(CreateGridView(BindingInfoLists.FitnessBindingList), 400, model.SelectedMainRecord.FitnessData);
                    break;
                case "DIABETES":
                    await model.SelectedMainRecord.GetOrganizationDiabetesData();
                    Show(CreateGridView(BindingInfoLists.DiabetesBindingList), 400, model.SelectedMainRecord.DiabetesData);
                    break;

                case "NUTRITION":
                    await model.SelectedMainRecord.GetOrganizationNutritionData();
                    Show(CreateGridView(BindingInfoLists.NutritionBindingList), 400, model.SelectedMainRecord.NutritionData);
                    break;
                case "ROUTINE":
                    await model.SelectedMainRecord.GetOrganizationRoutineData();
                    Show(CreateGridView(BindingInfoLists.RoutineBindingList), 400, model.SelectedMainRecord.RoutineData);
                    break;
                case "SLEEP":
                    await model.SelectedMainRecord.GetOrganizationSleepData();
                    Show(CreateGridView(BindingInfoLists.SleepBindingList), 400, model.SelectedMainRecord.SleepData);
                    break;
                case "TOBACCO CESSATION":
                    await model.SelectedMainRecord.GetOrganizationTobaccoCessationData();
                    Show(CreateGridView(BindingInfoLists.TobaccoCessationBindingList), 400, model.SelectedMainRecord.TobaccoCessationData);
                    break;
            }
        }

        private void Show(View view, int  rowHeight, IEnumerable itemSource)
        {
            _lastTemplate = new ViewCell { View = view };
            _listView.RowHeight = rowHeight;
            _listView.ItemsSource = itemSource;
        }


        #region Stattic Functions

        private static View CreateGridView(Dictionary<string, string> bindingList)
        {
            return CreateGridView(BindingInfoLists.MesasurmentBindingList, bindingList);
        }

        private static Grid CreateGridView(Dictionary<string, string> bindingList1, Dictionary<string, string> bindingList2)
        {
            var bindingList = new Dictionary<string, string>();

            foreach (var item in bindingList1)
                bindingList.Add(item.Key, item.Value);

            foreach (var item in bindingList2)
                bindingList.Add(item.Key, item.Value);

            return CreateGenericGridView(bindingList);

        }

        private static Grid CreateGenericGridView(Dictionary<string,string> bindingList)
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