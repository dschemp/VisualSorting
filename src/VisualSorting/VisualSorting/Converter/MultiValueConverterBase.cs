using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace VisualSorting.Converter
{
    public abstract class MultiValueConverterBase : MarkupExtension, IMultiValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
        
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(values);
        }
        
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return ConvertBack(value);
        }
        
        public abstract object Convert(object[] values);
        
        public virtual object[] ConvertBack(object value)
        {
            throw new NotImplementedException();
        }
    }
}
