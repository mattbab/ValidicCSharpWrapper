using System;
using Validic.Mobile.DemoApp.Views;
using Xamarin.Forms;

namespace Validic.Mobile.DemoApp.Menu
{
	public class MenuPage : ContentPage
	{		
		public Action<Page> OnMenuTap;

		public MenuPage ()
		{
			Icon = "masterIcon.png";
			Title = "Menu";
			BackgroundColor = Color.FromHex ("444444");

			Padding = new Thickness (10, 20);

			var layout = new StackLayout { 
				Spacing = 15, 
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			var header = new StackLayout () {
				Orientation = StackOrientation.Horizontal
			};

			header.Children.Add (new Image () {
				Source = "masterIcon.png",
				WidthRequest = 25,
				HeightRequest = 25
			});
					
			//header.Children.Add (new Label () {
			//	Text = "places",
			//	TextColor = Color.FromHex ("dddddd"),
			//	VerticalOptions = LayoutOptions.CenterAndExpand
			//});

			layout.Children.Add (header);

		    Action<Page> add = (page) =>
		    {
		        var label = new Label()
		        {
		            Text = page.Title,
		            TextColor = Color.FromHex("dddddd")
		        };
                label.GestureRecognizers.Add(new TapGestureRecognizer(view => OnMenuTap(page)));
		        layout.Children.Add(label);
		    };

		    add(new MainRecordView());
            add(new OrganizationListView());
			Content = layout;
		}
	}
}

