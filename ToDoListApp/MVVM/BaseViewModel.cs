using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace ToDoListApp.XAML.Behaviors
{
    public class BaseViewModel : BindableBase, INavigatedAware, IDestructible, INavigatingAware
    {
        protected readonly INavigationService NavigationService;
        protected bool IsViewActiveNow;
        private readonly INavigationServiceManager _nsm;
        private bool _isAppeared;
        private bool _isLoading;
        private bool _isNavigatingBack;
        private DelegateCommand<object> _navigateBackCommand;
        private string _viewTitle;

        public INavigationService NavigationServicePublic { get { return NavigationService; } }

        protected BaseViewModel(INavigationService navigationService,
            INavigationServiceManager nsm)
        {
            NavigationService = navigationService;
            _nsm = nsm;
            IsLoading = true;
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        public bool IsNavigatingBack
        {
            get { return _isNavigatingBack; }
            set { SetProperty(ref _isNavigatingBack, value); }
        }

        public ICommand NavigateBackCommand => _navigateBackCommand ?? (_navigateBackCommand = new DelegateCommand<object>(NavigateBackCommandExecute));

        public Color NavigationBarColor { get; set; }

        public bool OverrideNavigationBar { get; set; }

        public string ViewTitle
        {
            get { return _viewTitle; }
            set { SetProperty(ref _viewTitle, value); }
        }

        protected ContentPage View { get; private set; }

        public async Task OnAppearing()
        {
            IsViewActiveNow = true;

            _nsm.Attach(NavigationService);

            if (_isAppeared)
            {
                await OnAppearingView(false);
                return;
            }

            _isAppeared = true;

            await OnAppearingView(true);
        }

        public async Task OnDisappearing()
        {
            IsViewActiveNow = false;
            await OnDisappearingView();
        }
          

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        public void SetView(ContentPage view)
        {
            View = view;
        }
        
        protected virtual Task OnAppearingView(bool isFirstAppear)
        {
            return Task.FromResult(0);
        }
        
        protected virtual Task OnDisappearingView()
        {
            return Task.FromResult(0);
        }

        private async void NavigateBackCommandExecute(object obj)
        {
            IsNavigatingBack = true;
            await NavigationService.GoBackAsync();
        }

        void IDestructible.Destroy()
        {
            View = null;
            IsViewActiveNow = false;
            Destroy();
        }

        protected virtual void Destroy()
        {

        }
    }
}