using Prism;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListApp.XAML.Behaviors;
using Xamarin.Forms;

namespace ToDoListApp.Views.Pages
{
    public class ToDoListAppNavigationPage : NavigationPage
    {
        private BaseViewModel _childViewModel;

        public Page ChildPage { get; set; }

        public ToDoListAppNavigationPage(Page root) : base(root)
        {
            ChildPage = root;

            this.BarTextColor = Color.Black;
            this.BarBackgroundColor = Color.FromHex("#0d0077");

            if (ChildPage != null)
            {
                ChildPage.BindingContextChanged += ChildPage_BindingContextChanged;
                _childViewModel = ChildPage.BindingContext as BaseViewModel;
            }
        }

        void ChildPage_BindingContextChanged(object sender, EventArgs e)
        {
            if (ChildPage?.BindingContext != null)
            {
                _childViewModel = ChildPage.BindingContext as BaseViewModel;
            }
        }
    }
}
