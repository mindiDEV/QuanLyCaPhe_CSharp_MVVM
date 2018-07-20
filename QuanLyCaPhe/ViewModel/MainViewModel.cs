using QuanLyCaPhe.Model;
using QuanLyCaPhe.View;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyCaPhe.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private bool _isLoadedMainWindow;
        public bool IsLoadedMainWindow { get => _isLoadedMainWindow; set => _isLoadedMainWindow = value; }

        private NhatKyDangNhap nhatKyDangNhap = new NhatKyDangNhap();

        public ICommand LoadedWindow { get; set; }

        public MainViewModel()
        {
            LoadedWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                IsLoadWindow(p);
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
                //Khi đăng nhập thành công, sẽ show màn hình order đầu tiên
                //HomeView hv = new HomeView();
                BillView bv = new BillView();
                MainWindow._GridMain.Children.Add(bv);

                nhatKyDangNhap.TenTaiKhoan = loginVM.TenTaiKhoan;
                nhatKyDangNhap.NgayDangNhap = DateTime.Now;

                p.ShowDialog();
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
    }
}