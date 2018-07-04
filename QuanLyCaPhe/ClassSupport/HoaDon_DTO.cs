using QuanLyCaPhe.ViewModel;

namespace QuanLyCaPhe.ClassSupport
{
    public class HoaDon_DTO : BaseViewModel
    {
        private string _maNhomThucDon;

        private string _maMon;

        private string _tenMon;

        private double _giaBan;

        private int _soLuong;

        private double _thanhTien;

        public string MaMon
        {
            get
            {
                return _maMon;
            }
            set
            {
                _maMon = value;
                RaisePropertyChanged();
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
                _tenMon = value;
                RaisePropertyChanged();
            }
        }

        public double GiaBan
        {
            get
            {
                return _giaBan;
            }
            set
            {
                _giaBan = value;
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

        public double ThanhTien
        {
            get
            {
                return _thanhTien;
            }
            set
            {
                _thanhTien = value;
                RaisePropertyChanged();
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
                _maNhomThucDon = value;
                RaisePropertyChanged();
            }
        }
    }
}