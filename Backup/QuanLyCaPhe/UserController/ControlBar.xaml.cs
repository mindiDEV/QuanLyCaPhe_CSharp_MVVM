using QuanLyCaPhe.ViewModel;
using System.Windows.Controls;

namespace QuanLyCaPhe.UserController
{
    /// <summary>
    /// Interaction logic for ControlBar.xaml
    /// </summary>
    public partial class ControlBar : UserControl
    {
        private ControlBarViewModel _controlBarViewModel;
        public ControlBarViewModel ControlBarViewModel { get => _controlBarViewModel; set => _controlBarViewModel = value; }

        public ControlBar()
        {
            InitializeComponent();
            this.DataContext = ControlBarViewModel = new ControlBarViewModel();
        }
    }
}