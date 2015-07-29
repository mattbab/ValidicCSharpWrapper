using Android.App;
using Android.Content.PM;
using Android.OS;
using Validic.Logging;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Validic.Mobile.DemoApp.Droid
{
    [Activity(Label = "Validic.Mobile.CSharpApp", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            LogManager.GetLogger = (t) => new Validic.Logging.Android.Logger(t);
            Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}