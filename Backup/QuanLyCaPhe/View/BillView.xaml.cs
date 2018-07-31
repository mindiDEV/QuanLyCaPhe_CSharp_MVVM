using QuanLyCaPhe.ViewModel;
using System.Windows.Controls;

namespace QuanLyCaPhe.View
{
    /// <summary>
    /// Interaction logic for OrderView.xaml
    /// </summary>
    public partial class BillView : UserControl
    {
        public BillView()
        {
            InitializeComponent();

            this.DataContext = new BillViewModel();
           
        }
    }
}