using Xamarin.Forms;

namespace ToDoListApp.XAML.Behaviors
{
    public class BaseContentPage : ContentPage
    {
        public static BaseContentPage CurrentActive;
        
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            CurrentActive = this;

            if (BindingContext is BaseViewModel viewModel)
            {
                viewModel.SetView(this);
                await viewModel.OnAppearing();
            }
        }
        
        protected override async void OnDisappearing()
        {
            if (BindingContext is BaseViewModel viewModel)
            {
                await viewModel.OnDisappearing();
            }

            base.OnDisappearing();
        }
    }
}