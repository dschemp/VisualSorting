using System.Windows.Controls;

namespace VisualSorting.UserInterface
{
    /// <summary>
    ///     Interaktionslogik für UserInterfaceView.xaml
    /// </summary>
    public partial class UserInterfaceView : UserControl
    {
        public UserInterfaceView(UserInterfaceViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}