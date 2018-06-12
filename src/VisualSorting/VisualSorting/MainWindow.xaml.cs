using ModernChrome;
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
using VisualSorting.UserInterface;

namespace VisualSorting
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        #region Eigenschaften / Variablen
        public static MainWindow Instance;

        public static Brush Border
        {
            get { return Instance.BorderBrush; }
            set { Instance.BorderBrush = value; }
        }
        public readonly static Brush BLUE_BRUSH   = new SolidColorBrush(Color.FromRgb(0x0, 0x7A, 0xCC));
        public readonly static Brush ORANGE_BRUSH = new SolidColorBrush(Color.FromRgb(0xCA, 0x51, 0x00));
        public readonly static Brush GREEN_BRUSH  = new SolidColorBrush(Color.FromRgb(0x00, 0x80, 0x00));

        public static string StatusbarText
        {
            get { return Instance.StatusBarText.Text; }
            set { Instance.StatusBarText.Text = value; }
        }
        public readonly static string STATUS_IDLE    = "Warte auf Aufgabe...";
        public readonly static string STATUS_WORKING = "Sortiere...";
        public readonly static string STATUS_DONE    = "Fertig...";
        #endregion

        public MainWindow()
        {
            Instance = this;
            InitializeComponent();

            Border = BLUE_BRUSH;
            StatusbarText = STATUS_IDLE;

            var view = new UserInterfaceView(new UserInterfaceViewModel());
            GridUserControlLayout.Children.Add(view);
        }
    }
}
