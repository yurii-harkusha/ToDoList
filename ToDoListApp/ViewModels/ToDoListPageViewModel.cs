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
using Akavache;
using ToDoListApp.Interfaces;

namespace ToDoListApp.ViewModels
{
    public class ToDoListPageViewModel : BaseViewModel
    {
        private INavigationService _navigationService;
        private ObservableCollection<ToDoItem> _toDoItems;
        private bool _sortingByAplhabet;
        private string _sortingTypeImage;
        private ToDoItemSortHelper _toDoItemSortHelper;
        private IDataService _dataService;

        public ToDoListPageViewModel(INavigationService navigationService, 
            INavigationServiceManager nsm,
            IDataService toDoItemsCacheDataService)
            : base(navigationService, nsm)
        {
            _navigationService = navigationService;
            _dataService = toDoItemsCacheDataService;
            _toDoItemSortHelper = new ToDoItemSortHelper();
            SortingByAlphabet = Settings.SortingByAlphabet;
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }

        protected override async Task OnAppearingView(bool isFirstAppear)
        {
            await base.OnAppearingView(isFirstAppear);

            var toDoItemsList = await _dataService.GetToDoItemsAsync();

            if (toDoItemsList != null)
            {
                ToDoItems = SortToDoItems(new ObservableCollection<ToDoItem>(toDoItemsList));
            }
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
            Settings.SortingByAlphabet = SortingByAlphabet;
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

        private async void ToDoItemSelectedAsync(object arg)
        {
            var selectedToDoItem = ((ItemTappedEventArgs)arg).Item as ToDoItem;

            if (selectedToDoItem != null)
            {
                var navParams = new NavigationParameters
                {
                    { "toDoItem", selectedToDoItem }
                };
                await _navigationService.NavigateAsync(name: nameof(ToDoItemPage), parameters: navParams, useModalNavigation: false, animated: true);
            }
        }

        private async void RemoveItemAsync(object arg)
        {
            var selectedToDoItem = arg as ToDoItem;

            if (selectedToDoItem != null)
            {
                if (await UserDialogs.Instance.ConfirmAsync(AppResource.AreYouSureRemoveText,
                AppResource.RemoveText,
                AppResource.YesText,
                AppResource.CancelText))
                {
                    if (ToDoItems != null)
                    {
                        ToDoItems.Remove(ToDoItems.Where(x => x.Id == selectedToDoItem.Id).FirstOrDefault());
                        _dataService.SaveOrUpdateToDoItemsAsync(ToDoItems.ToList());
                    }
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
                    statusNameForQuestion = AppResource.ActiveText;
                }
                else
                {
                    statusNameForQuestion = AppResource.DoneText;
                }
                if (await UserDialogs.Instance.ConfirmAsync($"{AppResource.AreYouSureMarkItemText} {statusNameForQuestion}?", 
                    AppResource.ChangeStatusText, 
                    AppResource.YesText, 
                    AppResource.CancelText))
                {
                    bool isNewStatusValueDone = !isOldStatusValueDone;
                    ToDoItems.Where(x => x.Id == selectedToDoItem.Id).FirstOrDefault().IsDone = isNewStatusValueDone;
                    if (ToDoItems != null)
                    {
                        _dataService.SaveOrUpdateToDoItemsAsync(ToDoItems.ToList());
                    }
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
                    SortingTypeImage = Application.Current.Resources["SortByAlphabetImage"].ToString(); 
                }
                else
                {
                    SortingTypeImage = Application.Current.Resources["SortByDateImage"].ToString();
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

        public string CreateNewItemText
        {
            get
            {
                return AppResource.CreateNewItemText;
            }
        }

        public string CreatedText
        {
            get
            {
                return AppResource.CreatedText;
            }
        }

        public string StatusText
        {
            get
            {
                return AppResource.StatusText;
            }
        }
    }
}
