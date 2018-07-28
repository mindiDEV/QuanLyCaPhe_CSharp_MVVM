using QuanLyCaPhe.Model;
using QuanLyCaPhe.UserController;
using QuanLyCaPhe.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyCaPhe.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public List<string> list_detail;

        private bool _isLoadedMainWindow;

        
        public bool IsLoadedMainWindow { get => _isLoadedMainWindow; set => _isLoadedMainWindow = value; }

        private NhatKyDangNhap nhatKyDangNhap = new NhatKyDangNhap();

        public ICommand LoadedWindow { get; set; }
        public ICommand SelectedChangedListView { get; set; }
        public ICommand MouseDownListView { get; set; }

        public bool SelectedItemHome { get; set; }

        public bool SelectedItemCollection { get; set; }

        public bool SelectedItemBill { get; set; }

        public bool SelectedItemCustomer { get; set; }

        public bool SelectedItemEmployee { get; set; }

        public bool SelectedItemPromotion { get; set; }

        public bool SelectedItemDetailPromotion { get; set; }

        private int _selectedIndexListView;

        public int SelectedIndexListView { get => _selectedIndexListView; set { _selectedIndexListView = value; RaisePropertyChanged("SelectedIndexListView"); } }

        private bool _isActive;
        public bool IsActive { get => _isActive; set { _isActive = value; RaisePropertyChanged("IsActive"); } }

        public MainViewModel()
        {
            IsActive = false;


            LoadedWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                IsLoadWindow(p);

            });

            MouseDownListView = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Collection collectionView = null;
                BillView billView = null;
                EmployeeView employeeView = null;
                CustomerView customerView = null;
                HomeView homeView = null;
                PromotionView promotionView = null;
                PromotionDetailView promotionDetailView = null;

                MainWindow._GridMain.Children.Clear();

                if (SelectedIndexListView == -1)
                {
                    return;
                }
                else if (SelectedIndexListView == 0)
                {
                        homeView = new HomeView();
                        var homeViewVM = homeView.DataContext as HomeViewModel;
                        homeViewVM.LoadBillList();
                        homeViewVM.LoadCustomerList();
                        homeViewVM.LoadEmployeeList();
                        homeViewVM.LoadPromotionList();
                        MainWindow._GridMain.Children.Add(homeView);
                }
                else if (SelectedIndexListView == 1)
                {
                    collectionView = new Collection();
                    MainWindow._GridMain.Children.Add(collectionView);

                }
                else if (SelectedIndexListView == 2)
                {

                        billView = new BillView();
                        var billViewModel = billView.DataContext as BillViewModel;
                        billViewModel.ClearValue();
                        billViewModel.GetDrinkListWhenStartup();
                        MainWindow._GridMain.Children.Add(billView);
                }
                else if(SelectedIndexListView == 3)
                {

                    customerView = new CustomerView();
                    var customer = customerView.DataContext as CustomerViewModel;
                    customer.ClearTextBox();
                    MainWindow._GridMain.Children.Add(customerView);
                }
                else if(SelectedIndexListView == 4)
                {
                    employeeView = new EmployeeView();
                    var employee = employeeView.DataContext as EmployeeViewModel;
                    employee.ClearTextBox();
                    MainWindow._GridMain.Children.Add(employeeView);
                }
                else if(SelectedIndexListView == 5)
                {

                    promotionView = new PromotionView();
                    var promotionVM = promotionView.DataContext as PromotionViewModel;
                    promotionVM.ClearTextBox();
                    promotionVM.LoadPromotionList();
                    MainWindow._GridMain.Children.Add(promotionView);

                }
                else if(SelectedIndexListView == 6)
                {
                    promotionDetailView = new PromotionDetailView();
                    var promotionDetailVM = promotionDetailView.DataContext as PromotionDetailViewModel;
                    promotionDetailVM.LoadAll();
                    MainWindow._GridMain.Children.Add(promotionDetailView);
                }
                


            });

        }

        private void P_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                nhatKyDangNhap.NgayDangXuat = DateTime.Now;
                DataProvider.Instance.Database.NhatKyDangNhaps.Add(nhatKyDangNhap);
                DataProvider.Instance.Database.SaveChanges();
            }
            catch (Exception)
            {
                return;
            }
        }

        public void IsLoadWindow(Window p)
        {
            IsLoadedMainWindow = true;

            if (p == null)
            {
                return;
            }

            p.Hide();

            LoginView login = new LoginView();

            login.ShowDialog();

            p.Closing += P_Closing;

            var loginVM = login.DataContext as LoginViewModel;

            if (loginVM.IsLogin)
            {
                login.Close();

                //Giữ các danh sách CODE_ACTION để phân quyền
                list_detail = list_per(id_per(LoginViewModel.getTenTaiKhoan));

                nhatKyDangNhap.TenTaiKhoan = loginVM.TenTaiKhoan;

                nhatKyDangNhap.NgayDangNhap = DateTime.Now;

                //Phân quyền theo từng user
                if (checkper("MANAGEMENT") == true)
                {
                    IsActive = true;
                    HomeView homeView = new HomeView();
                    MainWindow._GridMain.Children.Add(homeView);

                }
                else
                {
                    IsActive = false;
                    BillView billView = new BillView();
                    MainWindow._GridMain.Children.Add(billView);

                }

              
                p.Show();
            }
            else
            {
                p.Close();
            }
        }

        private FrameworkElement GetWindowParent(UserControl p)
        {
            FrameworkElement parent = p;

            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }

            return parent;
        }

        private string id_per(string id_user)
        {
            string id = "";
            try
            {
                var query = DataProvider.Instance.Database.QuanHeNguoiDung_QuyenHan.Where(x => x.TenTaiKhoan == id_user).SingleOrDefault();
                if (query != null)
                {
                    if (query.DinhChi.ToString() == "False")
                    {
                        id = query.MaQuyenHan.ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi xảy ra khi truy vấn dữ liệu hoặc kết nối với server thất bại !", ex.Message);

            }
            finally
            {

            }
            return id;
        }

        private List<string> list_per(string id_per)
        {
            List<string> termsList = new List<string>();
            try
            {
                var query = DataProvider.Instance.Database.ChiTietQuyenHans.Where(x => x.MaQuyenHan == id_per).SingleOrDefault();
                if (query != null)
                {
                    termsList.Add(query.MaHanhDong.ToString());
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Lỗi xảy ra khi truy vấn dữ liệu hoặc kết nối với server thất bại !", ex.Message);

            }
            finally
            {

            }
            return termsList;
        }
        public Boolean checkper(string code)
        {
            Boolean check = false;
            foreach (string item in list_detail)
            {
                if (item == code)
                {
                    check = true;
                }
            }
            return check;
        }
    }
}