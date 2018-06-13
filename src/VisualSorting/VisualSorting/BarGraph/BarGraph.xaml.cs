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
        public IEnumerable<int> Items
        {
            get { return (IEnumerable<int>) GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); /*Draw();*/ }
        }

        // Using a DependencyProperty as the backing store for Items. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.RegisterAttached("Items", typeof(IEnumerable<int>), typeof(BarGraph), new PropertyMetadata(null));
        #endregion

        public BarGraph()
        {
            this.DataContext = this;
            InitializeComponent();
            //Draw();
        }

        public void Draw()
        {
            if (Items == null) return;

            int max = 0;
            int count = Items.Count();
            foreach (int i in Items)
                max = (i > max) ? i : max;

            foreach(int i in Items)
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
                //BarGrid.Children.Add(rect);
            }
        }
    }
}
