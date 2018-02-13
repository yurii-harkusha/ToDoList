using Akavache;
using ToDoListApp.Views.Start;
using ToDoListApp.XAML.Behaviors;
using Microsoft.Practices.Unity;
using Prism.Navigation;
using Prism.Unity;

namespace ToDoListApp
{
    public partial class App : PrismApplication
    {
        public App()
        {
            InitializeComponent();
        }

        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
            InitializeComponent();
        }
        
        protected override void RegisterTypes()
        {

            Container.RegisterType<INavigationService, NavigationServiceManager>(new ContainerControlledLifetimeManager());
            var nsm = Container.Resolve<INavigationService>() as NavigationServiceManager;
            if (nsm != null)
            {
                Container.RegisterInstance<INavigationServiceManager>(nsm);
                nsm.Attach(Container.Resolve<INavigationService>("UnityPageNavigationService"));
            }

            this.Container.RegisterTypeForNavigation<StartView, StartViewModel>();
                //.RegisterTypeForNavigation<LoginView, LoginViewModel>();

            ContainerManager.Container = Container;
        }
        
        protected override void OnInitialized()
        {
            InitializeComponent();

            BlobCache.ApplicationName = "ToDoListApp";

            AutoMapperConfiguration.Execute();

            var dummy = NavigationService.NavigateAsync(nameof(StartView));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
