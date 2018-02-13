using System;
using System.Collections.Generic;
using System.Linq;
using FFImageLoading.Forms.Touch;
using Foundation;
using Microsoft.Practices.Unity;
using Prism;
using Prism.Unity;
using UIKit;

namespace ToDoListApp.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate,IPlatformInitializer
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Xamarin.Forms.Forms.Init();
            CachedImageRenderer.Init();

            LoadApplication(this.CreateApplication());

            return base.FinishedLaunching(app, options);
            
        }

        public PrismApplication CreateApplication()
        {
            return new App(this);
        }

        public void RegisterTypes(IUnityContainer container)
        {

        }

    }
}
