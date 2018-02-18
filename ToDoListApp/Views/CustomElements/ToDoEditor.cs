using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ToDoListApp.Views.CustomElements
{
    public class ToDoEditor : Editor
    {
        public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create<ToDoEditor, string>(view => view.Placeholder, String.Empty);

        public ToDoEditor()
        {
        }

        public string Placeholder
        {
            get
            {
                return (string)GetValue(PlaceholderProperty);
            }

            set
            {
                SetValue(PlaceholderProperty, value);
            }
        }
    }
}
