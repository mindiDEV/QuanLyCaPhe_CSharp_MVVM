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
    
    public partial class ChuongTrinhKhuyenMai:BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChuongTrinhKhuyenMai()
        {
            this.ChiTietKhuyenMais = new HashSet<ChiTietKhuyenMai>();
        }

        private string _maCTKM;
        private string _tenCTKM;
        private string _motaCTKM;
        private Nullable<System.DateTime> _ngayBDKM;
        private Nullable<System.DateTime> _ngayKTKM;
        private Nullable<bool> _daXoa;



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
        public string TenCTKM
        {
            get
            {
                return _tenCTKM;
            }
            set
            {
                if (_tenCTKM != value)
                {
                    _tenCTKM = value;
                    RaisePropertyChanged("TenCTKM");
                }
            }
        }
        public string MoTaCTKM
        {
            get
            {
                return _motaCTKM;
            }
            set
            {
                if (_motaCTKM != value)
                {
                    _motaCTKM = value;
                    RaisePropertyChanged("MoTaCTKM");
                }
            }
        }
        public Nullable<System.DateTime> NgayBDKM
        {
            get
            {
                return _ngayBDKM;
            }
            set
            {
                if (_ngayBDKM != value)
                {
                    _ngayBDKM = value;
                    RaisePropertyChanged("NgayBDKM");
                }
            }
        }
        public Nullable<System.DateTime> NgayKTKM
        {
            get
            {
                return _ngayKTKM;
            }
            set
            {
                if (_ngayKTKM != value)
                {
                    _ngayKTKM = value;
                    RaisePropertyChanged("NgayKTKM");
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietKhuyenMai> ChiTietKhuyenMais { get; set; }
    }
}
