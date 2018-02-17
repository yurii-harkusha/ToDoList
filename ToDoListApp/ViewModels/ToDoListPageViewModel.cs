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

namespace ToDoListApp.ViewModels
{
    public class ToDoListPageViewModel : BaseViewModel
    {
        private INavigationService _navigationService;
        private  ObservableCollection<ToDoItem> _toDoItemsObservableCollection;
        private FakeDataService _fakeDataService;
        private ObservableCollection<ToDoItem> _toDoItems;

        public ToDoListPageViewModel(INavigationService navigationService, INavigationServiceManager nsm)
            : base(navigationService, nsm)
        {
            _navigationService = navigationService;
            _fakeDataService = new FakeDataService();
            ToDoItems = _fakeDataService.GetToDoItemsObservableCollection();
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

        public ICommand Item2SelectedCommand
        {
            get
            {
                return new DelegateCommand<object>(ToDoItem2SelectedAsync);
            }
        }

        private void ToDoItemSelectedAsync(object obj)
        {

        }
        private void ToDoItem2SelectedAsync(object obj)
        {

        }

        public ObservableCollection<ToDoItem> ToDoItems
        {
            get
            {
                return _toDoItems;
            }
            set
            {
                SetProperty(ref _toDoItems, value);
            }
        }

    }
}
