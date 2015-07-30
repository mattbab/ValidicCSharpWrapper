using System.Diagnostics;
using Validic.Core.AppLib.ViewModels;
using Validic.Logging;
using Xamarin.Forms;

namespace Validic.Mobile.DemoApp.Views
{
    public static class GridHelper
    {

        public static void AddRow(this Grid grid, int col, int row, string path, string stringFormat = null)
        {
            // grid.Children.Add(item, Col, Row);
            grid.Children.Add(new Label { Text = path }, col, row);
            grid.Children.Add(CreateLabel(path, stringFormat), col, row);
        }

        public static Label CreateLabel(string path, string stringFormat = null)
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
                CreateMeTemplate();


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
                    CreateMeTemplate();
                    _listView.RowHeight = 40;
                    _listView.ItemsSource = model.SelectedMainRecord.MeData;
                    _log.Debug("_listView.ItemsSource = model.SelectedMainRecord.MeData: Finish");
                    break;

                case "FITNESS":
                    await model.SelectedMainRecord.GetOrganizationFitnessData();
                    CreateFitnessTemplate();
                    _listView.RowHeight = 300;
                    _listView.ItemsSource = model.SelectedMainRecord.FitnessData;
                    _log.Debug("_listView.ItemsSource = model.SelectedMainRecord.MeData: Finish");
                    break;
            }
        }



        private void CreateMeTemplate()
        {
            // Return an assembled ViewCell.
            _lastTemplate = new ViewCell
            {

                View = new StackLayout
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
                }
            };
        }

        private void CreateFitnessTemplate()
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
            // Mesasurment
            grid.AddRow(0, 0, "Id");
            grid.AddRow(0, 1, "Time", "{0:MM/dd/yyy hh:mm:ss tt}");
            grid.AddRow(0, 2, "Timestamp", "{0:MM/dd/yyy hh:mm:ss tt}");
            grid.AddRow(0, 3, "UtcOffset");
            grid.AddRow(0, 4, "LastUpdated", "{0:MM/dd/yyy hh:mm:ss tt}");
            grid.AddRow(0, 5, "Source");
            grid.AddRow(0, 6, "SourceName");
            grid.AddRow(0, 7, "Extras");
            grid.AddRow(0, 8, "UserId");
            // Fitness  0,
            grid.AddRow(0, 9, "Type");
            grid.AddRow(0, 10, "Intensity");
            grid.AddRow(0, 11, "StartTime", "{0:MM/dd/yyy hh:mm:ss tt}");
            grid.AddRow(0, 12, "Distance", "{0:0.##}");
            grid.AddRow(0, 13, "Duration");
            grid.AddRow(0, 14, "Calories", "{0:0.##}");



            _lastTemplate = new ViewCell
            {
                View = grid
            };
        }
        private void CreateCompactFitnessTemplate()
        {
            var grid = new Grid
            {
                Padding = new Thickness(5, 10, 0, 0),
                ColumnDefinitions =
                {
                    new ColumnDefinition {Width = new GridLength(100, GridUnitType.Absolute)},
                    new ColumnDefinition {Width = new GridLength(100, GridUnitType.Absolute)},
                    new ColumnDefinition {Width = new GridLength(100, GridUnitType.Absolute)},
                    new ColumnDefinition {Width = new GridLength(100, GridUnitType.Absolute)},
                    new ColumnDefinition {Width = new GridLength(100, GridUnitType.Absolute)},
                    new ColumnDefinition {Width = new GridLength(100, GridUnitType.Absolute)},
                    new ColumnDefinition {Width = new GridLength(100, GridUnitType.Absolute)},
                },
                RowDefinitions =
                {
                    new RowDefinition {Height = GridLength.Auto},
                },
            };

            // Mesasurment
            // Fitness  0,
            grid.AddRow(0,0, "Type");
            grid.AddRow(1,0, "Intensity");
            grid.AddRow(2,0, "StartTime", "{0:MM/dd/yyy hh:mm:ss tt}");
            grid.AddRow(3,0, "Distance", "{0:0.##}");
            grid.AddRow(4,0, "Duration");
            grid.AddRow(5,0, "Calories", "{0:0.##}");

            _lastTemplate = new ViewCell
            {
                View = grid
            };
        }
    }
}