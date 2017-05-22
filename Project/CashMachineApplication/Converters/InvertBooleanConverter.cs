using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace CashMachineApplication.Converters
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class InvertBooleanConverter : MarkupExtension,IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool original = (bool)value;
            return !original;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool original = (bool)value;
            return !original;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new InvertBooleanConverter();
        }
    }
}