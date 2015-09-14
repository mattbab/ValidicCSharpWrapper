using Validic.Mobile.DemoApp.Menu;
using Xamarin.Forms;

namespace Validic.Mobile.DemoApp.Views
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

