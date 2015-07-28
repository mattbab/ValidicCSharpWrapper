using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Validic.Core.AppLib.ViewModels;
using Xamarin.Forms;

namespace Validic.Mobile.DemoApp.Views
{
    internal class ListViewDemoPage : ContentPage
    {
        private readonly ListView _listView;


        public ListViewDemoPage()
        {
            Label header = new Label
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
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header,
                    _listView
                }
            };
        }


        private object LoadTemplate()
        {

            // Create views with bindings for displaying each property.
            var nameLabel = new Label();
            nameLabel.SetBinding(Label.TextProperty, new Binding("Me.Id", BindingMode.OneWay));

            //var birthdayLabel = new Label();
            //birthdayLabel.SetBinding(Label.TextProperty, new Binding("Birthday", BindingMode.OneWay, null, null, "Born {0:d}"));

            //var boxView = new BoxView();
            //boxView.SetBinding(BoxView.ColorProperty, "FavoriteColor");

            // Return an assembled ViewCell.
            return new ViewCell
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
                                nameLabel,
                                //birthdayLabel
                            }
                        }
                    }
                }
            };
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var title = Title;
            if (title.ToUpper() == "ME")
            {
                var model = BindingContext as MainViewModel;
                if (model == null)
                    return;


                Debug.WriteLine("A");
                await model.SelectedMainRecord.GetOrganizationMeDataAsync();
                Debug.WriteLine("B");
                _listView.ItemsSource = model.SelectedMainRecord.MeData;
            }
        }
    }
}




