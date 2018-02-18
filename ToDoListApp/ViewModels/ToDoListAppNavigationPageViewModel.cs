using Prism.Navigation;
using System.Threading.Tasks;
using ToDoListApp.XAML.Behaviors;

namespace ToDoListApp.ViewModels
{
    public class ToDoListAppNavigationPageViewModel : BaseViewModel
    {
        private INavigationService _navigationService;

        public ToDoListAppNavigationPageViewModel(INavigationService navigationService, INavigationServiceManager nsm)
            : base(navigationService, nsm)
        {
            _navigationService = navigationService;
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }

        protected override async Task OnAppearingView(bool isFirstAppear)
        {
            await base.OnAppearingView(isFirstAppear);
        }
        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }
    }
}