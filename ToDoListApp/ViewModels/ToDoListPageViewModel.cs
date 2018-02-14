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

namespace ToDoListApp.ViewModels
{
    public class ToDoListPageViewModel : BaseViewModel
    {
        private INavigationService _navigationService;
        private  ObservableCollection<ToDoItem> _toDoItemsObservableCollection;

        public ToDoListPageViewModel(INavigationService navigationService, INavigationServiceManager nsm)
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

        public ICommand ItemSelectedCommand
        {
            get
            {
                return new DelegateCommand<object>(ToDoItemSelectedAsync);
            }
        }

        private void ToDoItemSelectedAsync(object obj)
        {

        }
    }
}
