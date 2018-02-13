using Android.App;
using Android.OS;

namespace ToDoListApp.Droid
{
    [Activity(Theme = "@style/Theme.Splash",
        Label = "ToDoListApp",
        Icon = "@drawable/icon",
        MainLauncher = true,
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait,
        NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            System.Threading.Thread.Sleep(500);
            this.StartActivity(typeof(MainActivity));
        }
    }
}