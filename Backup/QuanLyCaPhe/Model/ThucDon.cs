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

    public partial class ThucDon : BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ThucDon()
        {
            this.ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
            this.ChiTietKhuyenMais = new HashSet<ChiTietKhuyenMai>();
        }

        private string _maMon;
        private string _tenMon;
        private Nullable<decimal> _giaBan;
        private byte[] _hinhDaiDien;
        private string _ghiChu;
        private string _maDonViTinh;
        private string _maNhomThucDon;
        private NhomThucDon _nhomThucDon;
        private DonViTinh _donViTinh;
        private bool _daXoa;

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
        public string TenMon
        {
            get
            {
                return _tenMon;
            }
            set
            {
                if (_tenMon != value)
                {
                    _tenMon = value;
                    RaisePropertyChanged("TenMon");
                }
            }
        }
        public Nullable<decimal> GiaBan
        {
            get
            {
                return _giaBan;
            }
            set
            {
                if (_giaBan != value)
                {
                    _giaBan = value;
                    RaisePropertyChanged("GiaBan");
                }
            }
        }
        public string GhiChu
        {
            get
            {
                return _ghiChu;
            }
            set
            {
                if (_ghiChu != value)
                {
                    _ghiChu = value;
                    RaisePropertyChanged("GhiChu");
                }
            }
        }
        public string MaDonViTinh
        {
            get
            {
                return _maDonViTinh;
            }
            set
            {
                if (_maDonViTinh != value)
                {
                    _maDonViTinh = value;
                    RaisePropertyChanged("MaDonViTinh");
                }
            }
        }
        public string MaNhomThucDon
        {
            get
            {
                return _maNhomThucDon;
            }
            set
            {
                if (_maNhomThucDon != value)
                {
                    _maNhomThucDon = value;
                    RaisePropertyChanged("MaNhomThucDon");
                }
            }
        }
        public byte[] HinhDaiDien
        {
            get
            {
                return _hinhDaiDien;
            }
            set
            {
                if (_hinhDaiDien != value)
                {
                    _hinhDaiDien = value;
                    RaisePropertyChanged("HinhDaiDien");
                }
            }
        }
        public bool DaXoa
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietKhuyenMai> ChiTietKhuyenMais { get; set; }
        public virtual DonViTinh DonViTinh
        {
            get
            {
                return _donViTinh;
            }
            set
            {
                if (_donViTinh != value)
                {
                    _donViTinh = value;
                    RaisePropertyChanged("DonViTinh");
                }
            }
        }
        public virtual NhomThucDon NhomThucDon
        {
            get
            {
                return _nhomThucDon;
            }
            set
            {
                if (_nhomThucDon != value)
                {
                    _nhomThucDon = value;
                    RaisePropertyChanged("NhomThucDon");
                }
            }
        }
    }
}