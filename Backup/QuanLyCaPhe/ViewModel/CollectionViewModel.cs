using QuanLyCaPhe.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyCaPhe.ViewModel
{
    public class CollectionViewModel:BaseViewModel
    {

        #region Property   
        public bool SelectedItemUnit { get; set; }

        public bool SelectedItemCustomer { get; set; }

        public bool SelectedItemCustomerType { get; set; }

        public bool SelectedItemMenu { get; set; }

        public bool SelectedItemMenuGroup { get; set; }

        public bool SelectedItemMenuType { get; set; }

        public bool SelectedItemPromotion { get; set; }

        public bool SelectedItemDetailPromotion { get; set; }

       
        #endregion

        #region Command Property
        public ICommand SelectedChangedListViewUnit { get; set; }
        public ICommand SelectedChangedListViewMenu { get; set; }
        public ICommand SelectedChangedListViewCustomer { get; set; }
        public ICommand SelectedChangedListViewPromotion { get; set; }
        
        #endregion

        public CollectionViewModel()
        {
            SelectedChangedListViewUnit = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                UnitView unitView = null;

                MainWindow._GridMain.Children.Clear();

                if (SelectedItemUnit)
                {
                    if (unitView == null)
                    {
                        unitView = new UnitView();
                        var unitViewModel = unitView.DataContext as UnitViewModel;
                        unitViewModel.ClearTextBox();
                        MainWindow._GridMain.Children.Add(unitView);
                    }

                }
               
            });

            SelectedChangedListViewCustomer = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CustomerView customerView = null;

                CustomerTypeView customerTypeView = null;

                MainWindow._GridMain.Children.Clear();

                if (SelectedItemCustomer)
                {
                    if (customerView == null)
                    {
                        customerView = new CustomerView();
                        var customerViewModel = customerView.DataContext as CustomerViewModel;
                        customerViewModel.ClearTextBox();
                        MainWindow._GridMain.Children.Add(customerView);
                    }

                }
                if (SelectedItemCustomerType)
                {
                    if (customerTypeView == null)
                    {
                        customerTypeView = new CustomerTypeView();
                        var customerTypeViewModel = customerTypeView.DataContext as CustomerTypeViewModel;
                        customerTypeViewModel.ClearTextBox();
                        MainWindow._GridMain.Children.Add(customerTypeView);
                    }

                }

            });

            SelectedChangedListViewMenu = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MenuGroupView menuGroupView = null;
                MenuView menuView = null;
                MenuTypeView menuTypeView = null;

                MainWindow._GridMain.Children.Clear();

                if (SelectedItemMenu)
                {
                    if (menuView == null)
                    {
                        menuView = new MenuView();
                        var menuViewModel = menuView.DataContext as MenuViewModel;
                        menuViewModel.ClearTextBox();
                        MainWindow._GridMain.Children.Add(menuView);
                    }

                }
                if (SelectedItemMenuType)
                {
                    if (menuTypeView == null)
                    {
                        menuTypeView = new MenuTypeView();
                        var menuTypeViewModel = menuTypeView.DataContext as MenuTypeViewModel;
                        menuTypeViewModel.ClearTextBox();
                        MainWindow._GridMain.Children.Add(menuTypeView);
                    }

                }

                if (SelectedItemMenuGroup)
                {
                    if (menuGroupView == null)
                    {
                        menuGroupView = new MenuGroupView();
                        var menuGroupViewModel = menuGroupView.DataContext as MenuGroupViewModel;
                        menuGroupViewModel.ClearTextBox();
                        MainWindow._GridMain.Children.Add(menuGroupView);
                    }

                }

            });

            SelectedChangedListViewPromotion = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                PromotionView promotionView = null;
                PromotionDetailView promotionDetailView = null;

                MainWindow._GridMain.Children.Clear();

                if (SelectedItemPromotion)
                {
                    if (promotionView == null)
                    {
                        try
                        {
                            promotionView = new PromotionView();
                            var promotionViewModel = promotionView.DataContext as PromotionViewModel;
                            promotionViewModel.ClearTextBox();
                            MainWindow._GridMain.Children.Add(promotionView); 

                        }
                        catch
                        {
                            WarningDialogs("Chưa load xong dữ liệu");
                        }
                        finally

                        {
                            
                            
                        }

                        
                    }

                }
                if (SelectedItemDetailPromotion)
                {
                    if (promotionDetailView == null)
                    {
                        try
                        {
                            promotionDetailView = new PromotionDetailView();
                            var promotionDetailViewModel = promotionDetailView.DataContext as PromotionDetailViewModel;
                            promotionDetailViewModel.ClearTextBox();
                            promotionDetailViewModel.LoadPromotionList();
                            MainWindow._GridMain.Children.Add(promotionDetailView);       

                        }
                        catch
                        {
                            WarningDialogs("Chưa load xong dữ liệu");
                        }
                        finally
                        {
                           
                        }
                       
                    }

                }

            });

        }
    }
}
