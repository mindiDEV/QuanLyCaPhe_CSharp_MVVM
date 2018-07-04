using QuanLyCaPhe.ViewModel;
using System.Windows.Controls;

namespace QuanLyCaPhe.View
{
    /// <summary>
    /// Interaction logic for UnitView.xaml
    /// </summary>
    public partial class UnitView : UserControl
    {
        public UnitView()
        {
            InitializeComponent();
            this.DataContext = new UnitViewModel();
        }
    }
}