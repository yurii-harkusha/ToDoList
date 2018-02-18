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

namespace ToDoListApp.ViewModels
{
    public class ToDoListPageViewModel : BaseViewModel
    {
        private INavigationService _navigationService;
        private ObservableCollection<ToDoItem> _toDoItems;
        private bool _sortingByAplhabet;
        private string _sortingTypeImage;
        private ToDoItemSortHelper _toDoItemSortHelper;
        private ToDoItemsCacheDataService _toDoItemsCacheDataService;

        public ToDoListPageViewModel(INavigationService navigationService, 
            INavigationServiceManager nsm,
            ToDoItemsCacheDataService toDoItemsCacheDataService)
            : base(navigationService, nsm)
        {
            _navigationService = navigationService;
            _toDoItemsCacheDataService = toDoItemsCacheDataService;
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

            var toDoItemsList = await _toDoItemsCacheDataService.GetToDoItemsAsync();

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
                if (await UserDialogs.Instance.ConfirmAsync(Application.Current.Resources["AreYouSureRemoveText"].ToString(),
                Application.Current.Resources["RemoveText"].ToString(),
                Application.Current.Resources["YesText"].ToString(),
                Application.Current.Resources["CancelText"].ToString()))
                {
                    if (ToDoItems != null)
                    {
                        ToDoItems.Remove(ToDoItems.Where(x => x.Id == selectedToDoItem.Id).FirstOrDefault());
                        _toDoItemsCacheDataService.SaveOrUpdateToDoItemsAsync(ToDoItems.ToList());
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
                    statusNameForQuestion = Application.Current.Resources["ActiveText"].ToString();
                }
                else
                {
                    statusNameForQuestion = Application.Current.Resources["DoneText"].ToString();
                }
                if (await UserDialogs.Instance.ConfirmAsync($"{Application.Current.Resources["AreYouSureMarkItemText"].ToString()} {statusNameForQuestion}?", 
                    Application.Current.Resources["ChangeStatusText"].ToString(), 
                    Application.Current.Resources["YesText"].ToString(), 
                    Application.Current.Resources["CancelText"].ToString()))
                {
                    bool isNewStatusValueDone = !isOldStatusValueDone;
                    ToDoItems.Where(x => x.Id == selectedToDoItem.Id).FirstOrDefault().IsDone = isNewStatusValueDone;
                    if (ToDoItems != null)
                    {
                        _toDoItemsCacheDataService.SaveOrUpdateToDoItemsAsync(ToDoItems.ToList());
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
    }
}
