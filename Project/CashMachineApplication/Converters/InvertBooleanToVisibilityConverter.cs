using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace CashMachineApplication.Converters
{
    public class InvertBooleanToVisibilityConverter : MarkupExtension, IValueConverter
    {
        private readonly BooleanToVisibilityConverter _booleanToVisibilityConverter = new BooleanToVisibilityConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool valBool;
            if (value != null && Boolean.TryParse(value.ToString(), out valBool))
            {
               return _booleanToVisibilityConverter.Convert(!valBool, targetType, parameter, culture);
            }
            return _booleanToVisibilityConverter.Convert(value, targetType,parameter,culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new InvertBooleanToVisibilityConverter();
        }
    }
}