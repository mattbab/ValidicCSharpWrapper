using System.Collections;
using System.Diagnostics;
using Validic.Core.AppLib.ViewModels;
using Validic.Logging;
using Xamarin.Forms;

namespace Validic.Mobile.DemoApp.Views
{
    public static class GridHelper
    {

        public static void AddRow(this Grid grid, int row, string path, string stringFormat = null)
        {
            // grid.Children.Add(item, Col, Row);
            grid.Children.Add(new Label { Text = path, TextColor = Color.Olive}, 0, row);
            grid.Children.Add(CreateLabel(path, stringFormat), 1, row);
        }

        public static Label CreateLabel(string path)
        {
            var label = new Label();
            label.SetBinding(Label.TextProperty, new Binding(path, BindingMode.OneWay));
            return label;
        }
        public static Label CreateLabel(string path, string stringFormat)
        {
            var label = new Label();
            label.SetBinding(Label.TextProperty, new Binding(path, BindingMode.OneWay, null, null, stringFormat));
            return label;
        }
    }

    internal class ListViewDemoPage : ContentPage
    {
        private readonly ILog _log = LogManager.GetLogger("ListViewDemoPage");

        private ListView _listView;

        public ListViewDemoPage()
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

                case "FITNESS":
                    await model.SelectedMainRecord.GetOrganizationFitnessData();
                    Show(CreateFitnessView(), 400, model.SelectedMainRecord.FitnessData);
                    break;
                case "BIOMETRICS":
                    await model.SelectedMainRecord.GetOrganizationBiometrics();
                    Show(CreateBiometricsView(), 900, model.SelectedMainRecord.Biometrics);
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

        private View CreateFitnessView()
        {
            var grid = new Grid
            {
                Padding = new Thickness(5, 10, 0, 0),
                ColumnDefinitions =
                {
                    new ColumnDefinition {Width = new GridLength(100, GridUnitType.Absolute)},
                    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},
                },
                RowDefinitions =
                {
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                },
            };

            // Mesasurment
            grid.AddRow( 0, "Id");
            grid.AddRow( 1, "Time", "{0:MM/dd/yyy hh:mm:ss tt}");
            grid.AddRow( 2, "Timestamp", "{0:MM/dd/yyy hh:mm:ss tt}");
            grid.AddRow( 3, "UtcOffset");
            grid.AddRow( 4, "LastUpdated", "{0:MM/dd/yyy hh:mm:ss tt}");
            grid.AddRow( 5, "Source");
            grid.AddRow( 6, "SourceName");
            grid.AddRow( 7, "Extras");
            grid.AddRow( 8, "UserId");
            // Fitness  
            grid.AddRow( 9, "Type");
            grid.AddRow( 10, "Intensity");
            grid.AddRow( 11, "StartTime", "{0:MM/dd/yyy hh:mm:ss tt}");
            grid.AddRow( 12, "Distance", "{0:0.##}");
            grid.AddRow( 13, "Duration");
            grid.AddRow( 14, "Calories", "{0:0.##}");
            return grid;  
        }

        private View CreateBiometricsView()
        {
            var grid = new Grid
            {
                Padding = new Thickness(5, 10, 0, 0),
                ColumnDefinitions =
                {
                    new ColumnDefinition {Width = GridLength.Auto},
                    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},
                },
                RowDefinitions =
                {
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    //
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    //
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    //
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                },
            };

            // Mesasurment
            grid.AddRow(0, "Id");
            grid.AddRow(1, "Time", "{0:MM/dd/yyy hh:mm:ss tt}");
            grid.AddRow(2, "Timestamp", "{0:MM/dd/yyy hh:mm:ss tt}");
            grid.AddRow(3, "UtcOffset");
            grid.AddRow(4, "LastUpdated", "{0:MM/dd/yyy hh:mm:ss tt}");
            grid.AddRow(5, "Source");
            grid.AddRow(6, "SourceName");
            grid.AddRow(7, "Extras");
            grid.AddRow(8, "UserId");
            // Fitness  
            grid.AddRow(9, "BloodCalcium"    );
            grid.AddRow(10, "BloodChromium"   );
            grid.AddRow(11, "BloodFolicAcid"  );
            grid.AddRow(12, "BloodMagnesium"  );
            grid.AddRow(13, "BloodPotassium"  );
            grid.AddRow(14, "BloodSodium"     );
            grid.AddRow(15, "BloodVitaminB12" );
            grid.AddRow(16, "BloodZinc"       );
            grid.AddRow(17, "CreatineKinase"  );
            grid.AddRow(18, "Crp"             );
            grid.AddRow(19, "Diastolic"       );
            grid.AddRow(20, "Ferritin"        );
            grid.AddRow(21, "Hdl"             );
            grid.AddRow(22, "Hscrp"           );
            grid.AddRow(23, "Il6"             );
            grid.AddRow(24, "Ldl"             );
            grid.AddRow(25, "RestingHeartrate");
            grid.AddRow(26, "Systolic"        );
            grid.AddRow(27, "Testosterone"    );
            grid.AddRow(28, "TotalCholesterol");
            grid.AddRow(29, "Tsh"             );
            grid.AddRow(30, "UricAcid"        );
            grid.AddRow(31, "VitaminD"        );
            grid.AddRow(32, "WhiteCellCount"  );

            return grid;
        }
    }
}