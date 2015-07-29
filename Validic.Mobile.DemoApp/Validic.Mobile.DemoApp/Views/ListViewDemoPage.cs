using System.Diagnostics;
using Validic.Core.AppLib.ViewModels;
using Validic.Logging;
using Xamarin.Forms;

namespace Validic.Mobile.DemoApp.Views
{
    internal class ListViewDemoPage : ContentPage
    {
        private readonly ILog _log = LogManager.GetLogger("ListViewDemoPage");

        private readonly ListView _listView;

        public ListViewDemoPage()
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
            if (_lastTemplate != null)
                return _lastTemplate;

            _log.Debug("LoadTemplate");

            // Create views with bindings for displaying each property.
            var nameLabel = new Label();
            nameLabel.SetBinding(Label.TextProperty, new Binding("Me.Id", BindingMode.OneWay));

            //var birthdayLabel = new Label();
            //birthdayLabel.SetBinding(Label.TextProperty, new Binding("Birthday", BindingMode.OneWay, null, null, "Born {0:d}"));

            //var boxView = new BoxView();
            //boxView.SetBinding(BoxView.ColorProperty, "FavoriteColor");

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
                                nameLabel
                                //birthdayLabel
                            }
                        }
                    }
                }
            };
            return _lastTemplate;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _log.Debug("[OnAppearing] : Title = {0}", Title);
            var title = Title;
            if (title.ToUpper() == "ME")
            {
                var model = BindingContext as MainViewModel;
                if (model == null)
                    return;


                await model.SelectedMainRecord.GetOrganizationMeDataAsync();
                _listView.ItemsSource = model.SelectedMainRecord.MeData;
                _log.Debug("_listView.ItemsSource = model.SelectedMainRecord.MeData: Finish");
            }
        }
    }
}