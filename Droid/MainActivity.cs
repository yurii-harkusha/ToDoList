using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.Views;
using Android.OS;
using FFImageLoading.Forms.Droid;
using Microsoft.Practices.Unity;
using Prism.Unity;


namespace ToDoListApp.Droid
{
    [Activity(Label = "ToDoListApp", Icon = "@drawable/icon", Theme = "@style/MyTheme", ResizeableActivity = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity,IPlatformInitializer
    {
        public static int statusBarHeight = 0;
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            int resourceId = Resources.GetIdentifier("status_bar_height", "dimen", "android");
            if (resourceId > 0)
            {
                statusBarHeight = Resources.GetDimensionPixelSize(resourceId);
            }


            base.OnCreate(bundle);

            Xamarin.Forms.Forms.SetFlags("FastRenderers_Experimental");
            Xamarin.Forms.Forms.Init(this, bundle);


            UserDialogs.Init(this);
            
            CachedImageRenderer.Init(true);

            var app = new App(this);

            Window.SetSoftInputMode(SoftInput.AdjustResize);
            
            LoadApplication(app);
        }

        public void RegisterTypes(IUnityContainer container)
        {

        }
        
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {

        }
    }
}
