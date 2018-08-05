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

namespace VisualSorting.UserInterface
{
    /// <summary>
    /// Interaktionslogik für BarGraph.xaml
    /// </summary>
    public partial class BarGraph : UserControl
    {
        private static BarGraph _instance;

        public BarGraph()
        {
            InitializeComponent();
            _instance = this;
            this.SizeChanged += (sender, args) => UpdateUi();
        }

        #region Dependency Properties

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource", typeof(IEnumerable<int>), typeof(BarGraph), new PropertyMetadata(default(IEnumerable<int>), PropertyChangedCallback_UpdateUi));

        public static readonly DependencyProperty FillProperty = DependencyProperty.Register(
            "Fill", typeof(Brush), typeof(BarGraph), new PropertyMetadata(Brushes.Black));

        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(
            "StrokeThickness", typeof(int), typeof(BarGraph), new PropertyMetadata(2));

        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(
            "Stroke", typeof(Brush), typeof(BarGraph), new PropertyMetadata(Brushes.Transparent));

        #endregion

        #region Dependency Values

        public IEnumerable<int> ItemsSource
        {
            get => (IEnumerable<int>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public Brush Fill
        {
            get => (Brush)GetValue(FillProperty);
            set => SetValue(FillProperty, value);
        }

        public Brush Stroke
        {
            get => (Brush)GetValue(StrokeProperty);
            set => SetValue(StrokeProperty, value);
        }
        public int StrokeThickness
        {
            get => (int)GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }

        #endregion

        private static void PropertyChangedCallback_UpdateUi(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            _instance.UpdateUi();
        }

        private void UpdateUi()
        {
            if (ItemsSource == null || ItemsSource?.Count() == 0)
                return;

            Canvas.Children.Clear();

            var rects = new List<Rectangle>();

            var itemCount = ItemsSource.Count();
            var elementWidth = this.ActualWidth / itemCount;
            var singleElementHeight = this.ActualHeight / ItemsSource.Max();

            for (var n = 0; n < ItemsSource.Count(); n++)
            {
                var elementHeight = singleElementHeight * ItemsSource.ElementAt(n);
                var rect = new Rectangle()
                {
                    Stroke = this.Stroke,
                    StrokeThickness = this.StrokeThickness,
                    Fill = this.Fill,
                    Width = elementWidth,
                    Height = elementHeight
                };
                Canvas.SetBottom(rect, 0);
                Canvas.SetLeft(rect, n * elementWidth);
                Canvas.Children.Add(rect);
            }
        }
    }
}
