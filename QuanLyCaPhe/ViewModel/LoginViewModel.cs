using QuanLyCaPhe.Model;
using QuanLyCaPhe.View;
using System;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyCaPhe.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        #region Properties

        private Window tmp;
        private string _tenTaiKhoan;
        private string _matKhau;
        private static string _maNhanVien;
        public bool IsLogin;

        public string TenTaiKhoan { get => _tenTaiKhoan; set { _tenTaiKhoan = value; } }
        public string MatKhau { get => _matKhau; set { _matKhau = value; RaisePropertyChanged(); } }
        public static string MaNhanVien { get => _maNhanVien; set { _maNhanVien = value; } }

        #endregion Properties

        #region Command Properties

        public ICommand LoginWindowCommand { get; set; }

        public ICommand CloseWindowCommand { get; set; }

        public ICommand PasswordChangedCommand { get; set; }

        public ICommand LoginByQrCode { get; set; }

        #endregion Command Properties

        #region Constructor

        public LoginViewModel()
        {
            IsLogin = false;
            TenTaiKhoan = "";
            MatKhau = "";
            LoginWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                tmp = p;
                LoginWindow(p);
            });

            CloseWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                IsLogin = false;
                p.Close();
            });

            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) =>
            {
                return true;
            },
            (p) =>
            {
                MatKhau = p.Password;
            });

            LoginByQrCode = new RelayCommand<Window>((p) =>
            {
                tmp = p;
                return true;
            },
            (p) =>
            {
                ShowWebcamView(p);
            });
        }

        #endregion Constructor

        #region Methods

        private void ShowWebcamView(Window p)
        {
            WebcamView webcamView = new WebcamView();
            webcamView.ShowDialog();
            var webcamViewVM = webcamView.DataContext as WebcamViewModel;
            if (webcamViewVM.IsLoginByQrCode)
            {
                IsLogin = true;

                tmp.Close();
            }
        }

        private void LoginWindow(Window p)
        {
            if (p == null)
            {
                return;
            }

            BackgroundWorker worker = new BackgroundWorker();
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerAsync();
        }

        private bool CheckAccount()
        {
            string passWordEncode = MD5Hash(Base64Encode(MatKhau));
            var account = DataProvider.Instance.Database.TaiKhoans.Where(x => x.TenTaiKhoan == TenTaiKhoan && x.MatKhau == passWordEncode).Count();
            if (account > 0)
            {
                IsLogin = true;

                var a = from s in DataProvider.Instance.Database.TaiKhoans
                        where s.TenTaiKhoan == TenTaiKhoan
                        select s.MaNhanVien;

                MaNhanVien = a.SingleOrDefault();
                return true;
            }
            else
            {
                IsLogin = false;
                WarningDialogs("Sai tên đăng nhập hoặc mật khẩu");
                return false;
            }
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            LoginView.ProgressBarLV.Value = e.ProgressPercentage;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            int sum = 0;
            worker.ReportProgress(0, String.Format("Processing 1"));
            for (int i = 0; i < 20; i++)
            {
                Thread.Sleep(100);
                worker.ReportProgress((sum += 5));
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (CheckAccount())
            {
                tmp.Close();
            }

            LoginView.ProgressBarLV.Value = 0;
        }

        #endregion Methods
    }
}