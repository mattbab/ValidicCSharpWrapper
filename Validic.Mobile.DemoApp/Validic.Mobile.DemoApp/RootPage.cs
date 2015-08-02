using Validic.Mobile.DemoApp.Menu;
using Validic.Mobile.DemoApp.Views;
using Xamarin.Forms;

namespace Validic.Mobile.DemoApp
{
	public class RootPage : MasterDetailPage
	{
		public RootPage ()
		{			
			var menuPage = new MenuPage ();
			menuPage.OnMenuTap = (page) => { 
				IsPresented = false;
				Detail = new NavigationPage (page);
			};

			Master = menuPage;
			Detail = new NavigationPage(new MainRecordView ());
		}
	}
}

