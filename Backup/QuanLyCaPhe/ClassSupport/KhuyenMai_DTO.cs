using QuanLyCaPhe.ViewModel;
using System;

namespace QuanLyCaPhe.ClassSupport
{
    public class KhuyenMai_DTO : BaseViewModel
    {
        private string maMon;
        private string tenMon;
        private int soLuong;
        private string sanPhamTang;
        private decimal giamGia;
        private double thanhTien;
        private string maKhuyenMai;
        private string tenChuongTrinh;

        private byte[] _hinhDaiDien;
        private DateTime _ngayBDKM;
        private DateTime _ngayKTKM;
        public string MaMon
        {
            get => maMon;
            set
            {
                maMon = value;
                RaisePropertyChanged("MaMon");
            }
        }

        public string TenMon
        {
            get
            {
                return tenMon;
            }
            set
            {
                tenMon = value;
                RaisePropertyChanged("TenMon");
            }
        }

        public string SanPhamTang
        {
            get => sanPhamTang;
            set
            {
                sanPhamTang = value;
                RaisePropertyChanged("SanPhamTang");
            }
        }

        public decimal GiamGia
        {
            get => giamGia;
            set
            {
                giamGia = value;
                RaisePropertyChanged("GiamGia");
            }
        }

        public int SoLuong
        {
            get => soLuong;
            set
            {
                soLuong = value;
                RaisePropertyChanged("SoLuong");
            }
        }

        public double ThanhTien
        {
            get => thanhTien;
            set
            {
                thanhTien = value;
                RaisePropertyChanged("ThanhTien");
            }
        }

        public string MaKhuyenMai
        {
            get => maKhuyenMai;
            set
            {
                maKhuyenMai = value;
                RaisePropertyChanged("MaKhuyenMai");
            }
        }

        public string TenCTKM
        {
            get => tenChuongTrinh;
            set
            {
                tenChuongTrinh = value;
                RaisePropertyChanged("TenCTKM");
            }
        }

        public DateTime NgayBDKM
        {
            get => _ngayBDKM;
            set
            {
                _ngayBDKM = value;
                RaisePropertyChanged("NgayBDKM");
            }
        }
        public DateTime NgayKTKM
        {
            get => _ngayKTKM;
            set
            {
                _ngayKTKM = value;
                RaisePropertyChanged("NgayKTKM");
            }
        }

        public byte[] HinhDaiDien
        {
            get => _hinhDaiDien;
            set
            {
                _hinhDaiDien = value;
                RaisePropertyChanged("HinhDaiDien");
            }
        }
    }
}