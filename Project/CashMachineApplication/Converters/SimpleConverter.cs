using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace CashMachineApplication.Converters
{
    public class SimpleConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new SimpleConverter();
        }
    }
}
