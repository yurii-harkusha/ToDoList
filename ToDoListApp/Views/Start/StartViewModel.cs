using System.Threading.Tasks;
using ToDoListApp.XAML.Behaviors;
using Prism.Navigation;

namespace ToDoListApp.Views.Start
{
    public class StartViewModel : BaseViewModel
    {
        public StartViewModel(INavigationService navigationService, INavigationServiceManager nsm)
            : base(navigationService, nsm)
        {
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            
           
            
        }

        protected override async Task OnAppearingView(bool isFirstAppear)
        {

        }
    }
}