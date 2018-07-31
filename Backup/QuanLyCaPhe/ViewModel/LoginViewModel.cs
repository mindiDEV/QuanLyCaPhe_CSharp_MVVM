using QuanLyCaPhe.ClassSupport;
using QuanLyCaPhe.Model;
using QuanLyCaPhe.View;
using System;
using System.Collections.Generic;
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

        public string TenTaiKhoan { get => _tenTaiKhoan; set { _tenTaiKhoan = value; RaisePropertyChanged(); } }
        public string MatKhau { get => _matKhau; set { _matKhau = value; RaisePropertyChanged(); } }
        public static string MaNhanVien { get => _maNhanVien; set { _maNhanVien = value; } }

        public static string getTenTaiKhoan { get; set; }
        #endregion Properties

        #region Command Properties

        public ICommand LoginWindowCommand { get; set; }

        public ICommand CloseWindowCommand { get; set; }

        public ICommand PasswordChangedCommand { get; set; }

        public ICommand LoginByQrCode { get; set; }

        public PasswordBox getPasswordBox { get; set; }

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
                getPasswordBox = p;
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
                if(!string.IsNullOrEmpty(WebcamViewModel.getDecoded))
                {
                    try
                    {
                        List<string> listDecoded = WebcamViewModel.getDecoded.Split(' ').ToList();
                        string _taiKhoanQrCode = listDecoded[0];
                        string _matKhauQrCode = listDecoded[1];
                        var checkQrCode = DataProvider.Instance.Database.TaiKhoans.Where(x => x.TenTaiKhoan == _taiKhoanQrCode && x.QRCode == _matKhauQrCode).ToList();
                        if (checkQrCode.Count() != 0)
                        {
                            MessageBox.Show("Đăng nhập thành công");
                            getTenTaiKhoan = _taiKhoanQrCode;
                            IsLogin = true;
                            tmp.Close();
                        }
                        else
                        {
                            MessageBox.Show("Đăng nhập thất bại");
                            IsLogin = false;
                            return;

                        }
                    }
                    catch
                    {
                        MessageBox.Show("Kiểm tra lại tên đăng nhập và mật khẩu");
                        return;
                    }
                    
                }
                
            });


        }

        #endregion Constructor

        #region Methods

        private void ShowWebcamView(Window p)
        {
            WebcamView webcamView = new WebcamView();
            webcamView.ShowDialog();
            
        }

        private void LoginWindow(Window p)
        {
            if (p == null)
            {
                return;
            }

            if (CheckAccount())
            {
                getTenTaiKhoan = TenTaiKhoan;
                tmp.Close();
            }
            else
            {
                if (IsLogin == false)
                {

                    WarningDialogs("Sai tên đăng nhập hoặc mật khẩu");
                    TenTaiKhoan = "";
                    getPasswordBox.Password = "";
                    PasswordBehaviors.SetIsClear(getPasswordBox, true);
                    return;
                }

            }

        }

        private bool CheckAccount()
        {
            string TrangThaiLamViec = "Đang làm";

            string passWordEncode = MD5Hash(Base64Encode(MatKhau));
            try
            {
                var checkAccount = DataProvider.Instance.Database.TaiKhoans.Where(x => x.TenTaiKhoan == TenTaiKhoan && x.MatKhau == passWordEncode).Count();

                var getDataAccount = DataProvider.Instance.Database.TaiKhoans.Where(x => x.TenTaiKhoan == TenTaiKhoan && x.MatKhau == passWordEncode).SingleOrDefault();

                var getStatusWork = DataProvider.Instance.Database.NhanViens.Where(x => x.MaNhanVien == getDataAccount.MaNhanVien).SingleOrDefault();

                if (checkAccount > 0)
                {
                    if (getStatusWork.TrangThaiLamViec == TrangThaiLamViec)
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
                        WarningDialogs("Tài khoản không tồn tại!!!");
                        TenTaiKhoan = "";
                        getPasswordBox.Password = "";
                        PasswordBehaviors.SetIsClear(getPasswordBox, true);
                        return false;
                    }

                }
                
            }

            catch
            {

                return false;
            }
                 
            return true;
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

      
        #endregion Methods
    }
}