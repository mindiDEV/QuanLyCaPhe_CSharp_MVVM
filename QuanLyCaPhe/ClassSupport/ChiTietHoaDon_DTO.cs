using QuanLyCaPhe.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCaPhe.ClassSupport
{
    public class ChiTietHoaDon_DTO:BaseViewModel
    {
        private string _maHoaDon;

        private DateTime _ngayXuatHoaDon;

        private string _tenKhachHang;

        private string _tenNhanVien;

        private int _soLuong;

        private double _tongHoaDon;

        public string MaHoaDon
        {
            get
            {
                return _maHoaDon;
            }
            set
            {
                _maHoaDon = value;
                RaisePropertyChanged();
            }
        }

        public DateTime NgayXuatHoaDon
        {
            get
            {
                return _ngayXuatHoaDon;
            }
            set
            {
                _ngayXuatHoaDon = value;
                RaisePropertyChanged();
            }
        }

        public string TenKhachHang
        {
            get
            {
                return _tenKhachHang;
            }
            set
            {
                _tenKhachHang = value;
                RaisePropertyChanged();
            }
        }

        public string TenNhanVien
        {
            get
            {
                return _tenNhanVien;
            }
            set
            {
                _tenNhanVien = value;
                RaisePropertyChanged();
            }
        }

        public int SoLuong
        {
            get
            {
                return _soLuong;
            }
            set
            {
                _soLuong = value;
                RaisePropertyChanged();
            }
        }
        public double TongHoaDon
        {
            get
            {
                return _tongHoaDon;
            }
            set
            {
                _tongHoaDon = value;
                RaisePropertyChanged();
            }
        }

    }
}
