using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoListApp.XAML.Behaviors;
using ToDoListApp.Models;
using ToDoListApp.Services;
using Xamarin.Forms;
using Acr.UserDialogs;

namespace ToDoListApp.ViewModels
{
    public class ToDoItemPageViewModel : BaseViewModel
    {
        private INavigationService _navigationService;

        public ToDoItemPageViewModel(INavigationService navigationService, INavigationServiceManager nsm)
            : base(navigationService, nsm)
        {
            _navigationService = navigationService;
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
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

        public ICommand SaveToDoItemCommand
        {
            get
            {
                return new DelegateCommand<object>(SaveToDoItemAsync);
            }
        }


        private async void SaveToDoItemAsync(object arg)
        {
            
        }
    }
}
