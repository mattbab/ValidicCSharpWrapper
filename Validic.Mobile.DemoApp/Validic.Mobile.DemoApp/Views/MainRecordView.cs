using Validic.Core.AppLib.ViewModels;
using Xamarin.Forms;

namespace Validic.Mobile.DemoApp.Views
{
	public class MainRecordView : TabbedPage
	{
	    private const string Name = "Data View";
        public MainRecordView ()
		{
            Title = Name;
            Children.Add(new DataViewPage { Title = "Me" });
            Children.Add(new DataViewPage { Title = "Profile" });
            Children.Add(new DataViewPage { Title = "Weight" });
            Children.Add(new DataViewPage { Title = "Biometrics" });
            Children.Add(new DataViewPage { Title = "Fitness" });
            Children.Add(new DataViewPage { Title = "Diabetes" });
            Children.Add(new DataViewPage { Title = "Routine" });
            Children.Add(new DataViewPage { Title = "Sleep" });
            Children.Add(new DataViewPage { Title = "Tobacco Cessation" });
            Children.Add(new DataViewPage { Title = "Apps" });
        }

        protected override void OnAppearing()
	    {
	        base.OnAppearing();
            var model = BindingContext as MainViewModel;
            if(model == null)
                return;

            Title = string.Format("{0} - {1}", model.SelectedMainRecord.Organization.Name, Name);
	    }
    }
}

