using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using VisualSorting.Extensions;

namespace VisualSorting.Converter
{
    public class BarGraphRectHeightConverter : ConverterBase
    {
        public override object Convert(object obj, object parameter)
        {
            ItemsControl itemsControl = parameter as ItemsControl;
            int value = (int)obj;

            if (itemsControl == null)
                return null;

            IEnumerable<int> nums = (IEnumerable<int>)itemsControl.ItemsSource;

            if (nums == null)
                return null;

            return (value * nums.GetMax()) / itemsControl.Height;
        }
    }
}
