using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace QuanLyCaPhe.ClassSupport
{
    public static class PasswordBehaviors
    {
        public static void SetIsClear(DependencyObject target, bool value)
        {
            target.SetValue(IsClearProperty, value);
        }

        public static readonly DependencyProperty IsClearProperty =
                                                  DependencyProperty.RegisterAttached("IsClear",
                                                  typeof(bool),
                                                  typeof(PasswordBehaviors),
                                                  new UIPropertyMetadata(false, OnIsClear));

        private static void OnIsClear(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is bool && ((bool)e.NewValue) == true)
            {
                PasswordBox MyPasswordBox = sender as PasswordBox;

                if (MyPasswordBox != null)
                {
                    MyPasswordBox.Clear();
                }
            }
        }
    }
}
