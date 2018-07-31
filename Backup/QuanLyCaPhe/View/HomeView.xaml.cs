using QuanLyCaPhe.ViewModel;
using System.Windows.Controls;

namespace QuanLyCaPhe.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
            this.DataContext = new HomeViewModel();
        }
    }
}