using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ToDoListApp.Droid.Renderers;
using ToDoListApp.Views.CustomElements;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ToDoEditor), typeof(ToDoEditorRenderer))]
namespace ToDoListApp.Droid.Renderers
{
    public class ToDoEditorRenderer : EditorRenderer
    {
        public ToDoEditorRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(
            ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            //Add placeholer
            if (e.NewElement != null)
            {
                var element = e.NewElement as ToDoEditor;
                this.Control.Hint = element.Placeholder;
            }

            SetToDoEditorBackgroundColor();
        }

        private void SetToDoEditorBackgroundColor()
        {
            if (Control != null && Element != null)
            {
                var view = (Element as ToDoEditor);

                if (view != null)
                {
                    var nativeEditText = (EditText)Control;
                    var shape = new ShapeDrawable(new RectShape());
                    shape.Paint.Alpha = 0;
                    shape.Paint.SetStyle(Paint.Style.Stroke);
                    Control.SetBackgroundDrawable(shape);
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == ToDoEditor.PlaceholderProperty.PropertyName)
            {
                var element = this.Element as ToDoEditor;
                this.Control.Hint = element.Placeholder;
            }
        }
    }
}


