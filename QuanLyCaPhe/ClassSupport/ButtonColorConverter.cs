using QuanLyCaPhe.Model;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace QuanLyCaPhe.ClassSupport
{
    public class ButtonColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var table = value as Ban;

            if (table.TrangThai == "Có người")
            {
                return Brushes.Pink;
            }
            return Brushes.LightGreen;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}