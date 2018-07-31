//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyCaPhe.Model
{
    using QuanLyCaPhe.ViewModel;
    using System;
    using System.Collections.Generic;
    
    public partial class ChiTietKhuyenMai:BaseViewModel
    {
        private string _maChiTietKM;
        private string _maCTKM;
        private string _maMon;
        private string _sanPhamTang;
        private Nullable<int> _giamGia;
        private Nullable<int> _dieuKien;
        private Nullable<bool> _daXoa;


        public string MaChiTietKM
        {
            get
            {
                return _maChiTietKM;
            }
            set
            {
                if (_maChiTietKM != value)
                {
                    _maChiTietKM = value;
                    RaisePropertyChanged("MaChiTietKM");
                }
            }
        }
        public string SanPhamTang
        {
            get
            {
                return _sanPhamTang;
            }
            set
            {
                if (_sanPhamTang != value)
                {
                    _sanPhamTang = value;
                    RaisePropertyChanged("SanPhamTang");
                }
            }
        }
        public string MaCTKM
        {
            get
            {
                return _maCTKM;
            }
            set
            {
                if (_maCTKM != value)
                {
                    _maCTKM = value;
                    RaisePropertyChanged("MaCTKM");
                }
            }
        }
        public string MaMon
        {
            get
            {
                return _maMon;
            }
            set
            {
                if (_maMon != value)
                {
                    _maMon = value;
                    RaisePropertyChanged("MaMon");
                }
            }
        }
        public Nullable<int> GiamGia
        {
            get
            {
                return _giamGia;
            }
            set
            {
                if (_giamGia != value)
                {
                    _giamGia = value;
                    RaisePropertyChanged("GiamGia");
                }
            }
        }
        public Nullable<int> DieuKien
        {
            get
            {
                return _dieuKien;
            }
            set
            {
                if (_dieuKien != value)
                {
                    _dieuKien = value;
                    RaisePropertyChanged("DieuKien");
                }
            }
        }
        public Nullable<bool> DaXoa
        {
            get
            {
                return _daXoa;
            }
            set
            {
                if (_daXoa != value)
                {
                    _daXoa = value;
                    RaisePropertyChanged("DaXoa");
                }
            }
        }

        public virtual ChuongTrinhKhuyenMai ChuongTrinhKhuyenMai { get; set; }
        public virtual ThucDon ThucDon { get; set; }
    }
}