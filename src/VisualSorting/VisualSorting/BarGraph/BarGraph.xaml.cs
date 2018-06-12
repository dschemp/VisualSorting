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
            get { return (IEnumerable<int>) GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); Draw(); }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.RegisterAttached("ItemsSource", typeof(IEnumerable<int>), typeof(BarGraph), new PropertyMetadata(null));
        #endregion

        public BarGraph()
        {
            InitializeComponent();
            Draw();
        }

        public void Draw()
        {
            if (ItemsSource == null) return;

            int max = 0;
            int count = ItemsSource.Count();
            foreach (int i in ItemsSource)
                max = (i > max) ? i : max;

            foreach(int i in ItemsSource)
            {
                // <Rectangle Height="{Binding}" Width="20" Fill="Gray" Stroke="Red" VerticalAlignment="Stretch"/>
                Rectangle rect = new Rectangle()
                {
                    Height = this.Height * (i / max),
                    Width = 1 / count,
                    Fill = new SolidColorBrush(Color.FromRgb(0x33, 0x33, 0x33)),
                    Stroke = new SolidColorBrush(Color.FromRgb(0x11, 0x11, 0x11)),
                    VerticalAlignment = VerticalAlignment.Bottom
                };
                BarGrid.Children.Add(rect);
            }
        }
    }
}
