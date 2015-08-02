using Xamarin.Forms;

namespace Validic.Mobile.DemoApp.PlacesCategories
{
	public class LearningPlacesPage : ContentPage
	{
		public LearningPlacesPage ()
		{
			Title = "learning places";
			Content = new Label () {
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Text = "Learning"
			};
		}
	}
}

