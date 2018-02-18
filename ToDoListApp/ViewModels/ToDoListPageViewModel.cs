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
using ToDoListApp.Helpers;

namespace ToDoListApp.ViewModels
{
    public class ToDoListPageViewModel : BaseViewModel
    {
        private INavigationService _navigationService;
        private ObservableCollection<ToDoItem> _toDoItemsObservableCollection;
        private FakeDataService _fakeDataService;
        private ObservableCollection<ToDoItem> _toDoItems;
        private ToDoItem _selectedToDoItem;
        private bool _sortingByAplhabet;
        private string _sortingTypeImage;
        private ToDoItemSortHelper _toDoItemSortHelper;

        public ToDoListPageViewModel(INavigationService navigationService, INavigationServiceManager nsm)
            : base(navigationService, nsm)
        {
            _navigationService = navigationService;
            _toDoItemSortHelper = new ToDoItemSortHelper();
            _fakeDataService = new FakeDataService();
            SortingByAlphabet = true;
            ToDoItems = SortToDoItems(_fakeDataService.GetToDoItemsObservableCollection());
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

        public ICommand ChangeItemStatusCommand
        {
            get
            {
                return new DelegateCommand<object>(ChangeItemStatusAsync);
            }
        }

        public ICommand RemoveItemCommand
        {
            get
            {
                return new DelegateCommand<object>(RemoveItemAsync);
            }
        }

        public ICommand CreateNewItemCommand
        {
            get
            {
                return new DelegateCommand<object>(CreateNewItemAsync);
            }
        }

        public ICommand ChangeSortingTypeForToDoItemsCommand
        {
            get
            {
                return new DelegateCommand<object>(ChangeSortingTypeForToDoItems);
            }
        }

        private void ChangeSortingTypeForToDoItems(object obj)
        {
            SortingByAlphabet = !SortingByAlphabet;
            ToDoItems = SortToDoItems(ToDoItems);
        }

        private ObservableCollection<ToDoItem>SortToDoItems(ObservableCollection<ToDoItem> _toDoItems)
        {
            if(SortingByAlphabet)
            {
                return _toDoItemSortHelper.SortByAlphabet(_toDoItems);
            }
            else
            {
                return _toDoItemSortHelper.SortByCreatedDate(_toDoItems);
            }
        }

        private async void CreateNewItemAsync(object obj)
        {
            await _navigationService.NavigateAsync(name: nameof(ToDoItemPage), parameters: null, useModalNavigation: false, animated: true);
        }

        private void ToDoItemSelectedAsync(object arg)
        {
            var selectedToDoItem = ((ItemTappedEventArgs)arg).Item as ToDoItem;

            if (selectedToDoItem != null)
            {

            }
        }

        private async void RemoveItemAsync(object arg)
        {
            var selectedToDoItem = arg as ToDoItem;

            if (selectedToDoItem != null)
            {
                if (await UserDialogs.Instance.ConfirmAsync($"Are you sure that you want to remove the item?", "Remove", "Yes", "Cancel"))
                {
                    ToDoItems.Remove(ToDoItems.Where(x => x.Id == selectedToDoItem.Id).FirstOrDefault());
                }
            }
        }
        private async void ChangeItemStatusAsync(object arg)
        {
            var selectedToDoItem = arg as ToDoItem;

            if (selectedToDoItem != null)
            {
                bool isOldStatusValueDone = ToDoItems.Where(x => x.Id == selectedToDoItem.Id).FirstOrDefault().IsDone;
                string statusNameForQuestion;
                if (isOldStatusValueDone)
                {
                    statusNameForQuestion = "active";
                }
                else
                {
                    statusNameForQuestion = "done";
                }

                if (await UserDialogs.Instance.ConfirmAsync($"Are you sure that you want to mark the item as {statusNameForQuestion}?", "Change status", "Yes", "Cancel"))
                {
                    bool isNewStatusValueDone = !isOldStatusValueDone;
                    ToDoItems.Where(x => x.Id == selectedToDoItem.Id).FirstOrDefault().IsDone = isNewStatusValueDone;
                }
            }
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

        public bool SortingByAlphabet
        {
            get
            {
                return _sortingByAplhabet;
            }
            set
            {
                SetProperty(ref _sortingByAplhabet, value);
                if(_sortingByAplhabet)
                {
                    SortingTypeImage = "sort_by_alphabet.png";
                }
                else
                {
                    SortingTypeImage = "sort_by_date.png";
                }
            }
        }

        public string SortingTypeImage
        {
            get
            {
                return _sortingTypeImage;
            }
            set
            {
                SetProperty(ref _sortingTypeImage, value);
            }
        }
    }
}
