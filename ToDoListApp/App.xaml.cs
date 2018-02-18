using Akavache;
using ToDoListApp.Views.Start;
using ToDoListApp.XAML.Behaviors;
using Microsoft.Practices.Unity;
using Prism.Navigation;
using Prism.Unity;
using ToDoListApp.ViewModels;
using ToDoListApp.Views.Pages;

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

            this.Container.RegisterTypeForNavigation<ToDoListAppNavigationPage, ToDoListAppNavigationPageViewModel>()
                .RegisterTypeForNavigation<ToDoListPage, ToDoListPageViewModel>()
                .RegisterTypeForNavigation<ToDoItemPage, ToDoItemPageViewModel>();

            ContainerManager.Container = Container;
        }
        
        protected override void OnInitialized()
        {
            InitializeComponent();

            BlobCache.ApplicationName = "ToDoListApp";

            AutoMapperConfiguration.Execute();

            var dummy = NavigationService.NavigateAsync(StartPageResolve());
        }
        protected string StartPageResolve()
        {
            return "ToDoListAppNavigationPage/ToDoListPage";
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
