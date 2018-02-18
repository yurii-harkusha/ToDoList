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
using Akavache;

namespace ToDoListApp.ViewModels
{
    public class ToDoItemPageViewModel : BaseViewModel
    {
        private INavigationService _navigationService;
        private ToDoItem _currentToDoItem;
        private ToDoItemsCacheDataService _toDoItemsCacheDataService;

        public ToDoItemPageViewModel(INavigationService navigationService, 
            INavigationServiceManager nsm,
            ToDoItemsCacheDataService toDoItemsCacheDataService)
            : base(navigationService, nsm)
        {
            _navigationService = navigationService;
            _toDoItemsCacheDataService = toDoItemsCacheDataService;
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);


            if (parameters != null)
            {
                var param = parameters["toDoItem"] as ToDoItem;

                if (param != null)
                {
                    CurrentToDoItem = param;
                }
                else
                {
                    CurrentToDoItem = new ToDoItem()
                    {
                        Id = Guid.NewGuid().ToString().ToUpper(),
                        CreatedDate = DateTime.Now
                    };
                }
            }
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

        public ICommand RemoveCurrentToDoItemCommand
        {
            get
            {
                return new DelegateCommand<object>(RemoveCurrentToDoItemAsync);
            }
        }

        private async void SaveToDoItemAsync(object arg)
        {
            if (CurrentToDoItem != null)
            {
                var toDoItems = await _toDoItemsCacheDataService.GetToDoItemsAsync();
                if (toDoItems != null)
                {
                    var currentItemInCache = toDoItems.Where(x => x.Id == CurrentToDoItem.Id).FirstOrDefault();
                    if (currentItemInCache != null)
                    {
                        toDoItems.Remove(toDoItems.Where(x => x.Id == CurrentToDoItem.Id).FirstOrDefault());
                        CurrentToDoItem.ModifiedDate = DateTime.Now;
                        toDoItems.Add(CurrentToDoItem);
                    }
                    else
                    {
                        toDoItems.Add(CurrentToDoItem);
                    }
                }
                else
                {
                    toDoItems = new List<ToDoItem>();
                    toDoItems.Add(CurrentToDoItem);
                }
                _toDoItemsCacheDataService.SaveOrUpdateToDoItemsAsync(toDoItems);
            }            
            await _navigationService.GoBackAsync(null, false, true);
        }

        private async void RemoveCurrentToDoItemAsync(object arg)
        {
            if (await UserDialogs.Instance.ConfirmAsync($"Are you sure that you want to remove current item?", "Remove", "Yes", "Cancel"))
            {
                var toDoItems = await _toDoItemsCacheDataService.GetToDoItemsAsync();
                if (toDoItems != null)
                {
                    if(toDoItems.Where(x => x.Id == CurrentToDoItem.Id).FirstOrDefault() != null)
                    {
                        toDoItems.Remove(toDoItems.Where(x => x.Id == CurrentToDoItem.Id).FirstOrDefault());
                        _toDoItemsCacheDataService.SaveOrUpdateToDoItemsAsync(toDoItems.ToList());
                    }
                }
                await _navigationService.GoBackAsync(null, false, true);
            }
        }

        public ToDoItem CurrentToDoItem
        {
            get
            {
                return _currentToDoItem;
            }
            set
            {
                SetProperty(ref _currentToDoItem, value);
            }
        }
    }
}
