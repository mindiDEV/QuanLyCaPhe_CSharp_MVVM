using QuanLyCaPhe.View;
using QuanLyCaPhe.ViewModel;
using System.Windows.Controls;

namespace QuanLyCaPhe.UserController
{
    /// <summary>
    /// Interaction logic for Collection.xaml
    /// </summary>
    public partial class Collection : UserControl
    {
        public Collection()
        {
            InitializeComponent();
        }

        private void ListMaterial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UnitView unit = null;
            MenuTypeView menuType = null;
            MenuGroupView menuGroup = null;
            MenuView menuView = null;

            MainWindow._GridMain.Children.Clear();
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemInventory":
                    break;

                case "ItemUnit":
                    {
                        if (unit == null)
                        {
                            this.DataContext = unit = new UnitView();
                            MainWindow._GridMain.Children.Add(unit);
                        }
                        break;
                    }

                case "ItemMenuType":
                    {
                        if (menuType == null)
                        {
                            this.DataContext = menuType = new MenuTypeView();
                            var menuTypeViewModel = menuType.DataContext as MenuTypeViewModel;
                            menuTypeViewModel.ClearTextBox();
                            MainWindow._GridMain.Children.Add(menuType);
                        }
                        break;
                    }

                case "ItemMenuGroup":
                    {
                        if (menuGroup == null)
                        {
                            this.DataContext = menuGroup = new MenuGroupView();
                            var menuGroupViewModel = menuGroup.DataContext as MenuGroupViewModel;
                            menuGroupViewModel.ClearTextBox();
                            MainWindow._GridMain.Children.Add(menuGroup);
                        }
                        break;
                    }

                case "ItemMenu":
                    if (menuView == null)
                    {
                        this.DataContext = menuView = new MenuView();
                        var menuViewModel = menuView.DataContext as MenuViewModel;
                        menuViewModel.ClearTextBox();
                        MainWindow._GridMain.Children.Add(menuView);
                    }
                    break;

                default:
                    break;
            }
        }

        private void ListCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                CustomerTypeView customerTypeView = null;
                CustomerView customerView = null;
                MainWindow._GridMain.Children.Clear();
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemCustomerType":
                    {
                        if (customerTypeView == null)
                        { 
                            this.DataContext = customerTypeView = new CustomerTypeView();
                            var customerTypeViewModel = customerTypeView.DataContext as CustomerTypeViewModel;
                            customerTypeViewModel.ClearTextBox();
                            MainWindow._GridMain.Children.Add(customerTypeView);
                        }
                        break;
                    }
                case "ItemCustomer":
                    if (customerView == null)
                    {    
                        this.DataContext = customerView = new CustomerView();
                        var customerViewModel = customerView.DataContext as CustomerViewModel;
                        customerViewModel.ClearTextBox();
                        MainWindow._GridMain.Children.Add(customerView);
                    }
                    break;

                default:
                    break;
            }
        }

        private void ListPromotion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PromotionView pv = null;
            PromotionDetailView pdv = null;
            MainWindow._GridMain.Children.Clear();
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemPromotion":
                    {
                        if (pv == null)
                        {
                            this.DataContext = pv = new PromotionView();
                            var promotionViewModel = pv.DataContext as PromotionViewModel;
                            promotionViewModel.ClearTextBox();
                            MainWindow._GridMain.Children.Add(pv);
                        }
                        break;
                    }
                case "ItemDetailPromotion":
                    {
                        if (pdv == null)
                        {
                            this.DataContext = pdv = new PromotionDetailView();
                            var promotionDetailViewModel = pdv.DataContext as PromotionDetailViewModel;
                            promotionDetailViewModel.LoadPromotionList();
                            promotionDetailViewModel.ClearTextBox();
                            MainWindow._GridMain.Children.Add(pdv);
                            
                        }
                        break;
                    }
                    

                default:
                    break;
            }
        }
    }
}