using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VisualSorting.BarGraph
{
    /// <summary>
    /// Interaktionslogik für BarGraph.xaml
    /// </summary>
    public partial class BarGraph : UserControl
    {
        #region Attributes
        public IEnumerable<int> ItemsSource
        {
            get { return (IEnumerable<int>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable<int>), typeof(BarGraph), new PropertyMetadata(0));
        #endregion

        public BarGraph()
        {
            InitializeComponent();
        }
    }
}
