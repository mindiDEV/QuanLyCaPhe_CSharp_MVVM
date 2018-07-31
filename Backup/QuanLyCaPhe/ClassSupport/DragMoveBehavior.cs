using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace QuanLyCaPhe.ClassSupport
{
    internal class DragMoveBehavior:Behavior<UIElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            Window parentWindow = Window.GetWindow(AssociatedObject);

            AssociatedObject.MouseLeftButtonDown += (sender, e) =>
            {
                if (parentWindow != null)
                {
                    parentWindow.DragMove();
                }
            };
        }
    }
}
