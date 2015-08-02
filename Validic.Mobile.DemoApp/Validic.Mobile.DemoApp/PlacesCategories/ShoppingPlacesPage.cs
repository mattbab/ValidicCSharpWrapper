using Xamarin.Forms;

namespace Validic.Mobile.DemoApp.PlacesCategories
{
	public class ShoppingPlacesPage : ContentPage
	{
		public ShoppingPlacesPage ()
		{
			Title = "shopping places";
			Content = new Label () {
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Text = "Shopping"
			};
		}
	}
}

