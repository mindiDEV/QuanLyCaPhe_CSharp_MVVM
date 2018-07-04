using QuanLyCaPhe.UserController;
using QuanLyCaPhe.View;
using QuanLyCaPhe.ViewModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace QuanLyCaPhe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Grid _GridMain { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                return;
            }
            _GridMain = GridMain;
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Collection cl = null;
            BillView bill = null;
            EmployeeView ev = null;
            CustomerView cv = null;
            HomeView hv = null;
            PromotionView pv = null;
            GridMain.Children.Clear();
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemHome":
                    if (hv == null)
                    {
                        this.DataContext = hv = new HomeView();
                        var homeViewVM = hv.DataContext as HomeViewModel;
                        homeViewVM.LoadBillList();
                        homeViewVM.LoadCustomerList();
                        homeViewVM.LoadEmployeeList();
                        GridMain.Children.Add(hv);
                    }
                    break;

                case "ItemCollection":
                    if (cl == null)
                    {
                        this.DataContext = cl = new Collection();
                        GridMain.Children.Add(cl);
                    }

                    break;

                case "ItemCustomer":
                    if (cv == null)
                    {
                        this.DataContext = cv = new CustomerView();
                        
                        GridMain.Children.Add(cv);
                    }

                    break;

                case "ItemBill":
                    if (bill == null)
                    {
                        this.DataContext = bill = new BillView();
                        GridMain.Children.Add(bill);
                    }
                    break;

                case "ItemEmployee":
                    if (ev == null)
                    {
                        this.DataContext = ev = new EmployeeView();
                        GridMain.Children.Add(ev);
                    }
                    break;

                default:

                    break;

                case "ItemPromotion":
                    if (pv == null)
                    {
                        this.DataContext = pv = new PromotionView();
                        GridMain.Children.Add(pv);
                    }
                    break;

                
            }
        }

        private void ItemCollection_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Collection cl = null;
            GridMain.Children.Clear();
            if (cl == null)
            {
                this.DataContext = cl = new Collection();
                GridMain.Children.Add(cl);
            }
        }
    }
}