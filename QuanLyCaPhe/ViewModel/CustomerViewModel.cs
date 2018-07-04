using QuanLyCaPhe.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace QuanLyCaPhe.ViewModel
{
    public class CustomerViewModel : BaseViewModel
    {
        #region Property

        private ObservableCollection<KhachHang> _List;
        public ObservableCollection<KhachHang> List { get => _List; set { _List = value; RaisePropertyChanged("List"); } }

        private ObservableCollection<LoaiKhachHang> _CustomerTypeList;
        public ObservableCollection<LoaiKhachHang> CustomerTypeList { get => _CustomerTypeList; set { _CustomerTypeList = value; RaisePropertyChanged("CustomerTypeList"); } }

        private int _maKhachHang;

        private string _tenKhachHang;

        private string _diaChi;

        private string _gioiTinh;

        private string _dienThoaiDiDong;

        private string _tenLoaiKhachHang;

        private string _maLoaiKhachHang;

        private int _IsSelectedSex;

        public int IsSelectedSex
        {
            get => _IsSelectedSex;
            set
            {
                if (_IsSelectedSex != value)
                {
                    _IsSelectedSex = value;

                    RaisePropertyChanged("IsSelectedSex");
                }
            }
        }

        public int MaKhachHang
        {
            get => _maKhachHang;
            set
            {
                if (_maKhachHang != value)
                {
                    _maKhachHang = value;
                    RaisePropertyChanged("MaKhachHang");
                }
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
                if (_tenKhachHang != value)
                {
                    _tenKhachHang = value; RaisePropertyChanged("TenKhachHang");
                }
            }
        }

        public string DiaChi
        {
            get
            {
                return _diaChi;
            }
            set
            {
                if (_diaChi != value)
                {
                    _diaChi = value; RaisePropertyChanged("DiaChi");
                }
            }
        }

        public string GioiTinh
        {
            get
            {
                return _gioiTinh;
            }
            set
            {
                if (_gioiTinh != value)
                {
                    _gioiTinh = value;

                    RaisePropertyChanged("GioiTinh");
                }
            }
        }

        public string DienThoaiDiDong
        {
            get
            {
                return _dienThoaiDiDong;
            }
            set
            {
                if (_dienThoaiDiDong != value)
                {
                    _dienThoaiDiDong = value; RaisePropertyChanged("DienThoaiDiDong");
                }
            }
        }

        public string MaLoaiKhachHang
        {
            get => _maLoaiKhachHang;
            set
            {
                if (_maLoaiKhachHang != value)
                {
                    _maLoaiKhachHang = value; RaisePropertyChanged("MaLoaiKhachHang");
                }
            }
        }

        public string TenLoaiKhachHang
        {
            get => _tenLoaiKhachHang;
            set
            {
                if (_tenLoaiKhachHang != value)
                {
                    _tenLoaiKhachHang = value; RaisePropertyChanged("TenLoaiKhachHang");
                }
            }
        }

        private KhachHang _SelectedItem;

        public KhachHang SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                RaisePropertyChanged();
                if (SelectedItem != null)
                {
                    {
                        MaKhachHang = SelectedItem.MaKhachHang;
                        TenKhachHang = SelectedItem.TenKhachHang;
                        DiaChi = SelectedItem.DiaChi;
                        DienThoaiDiDong = SelectedItem.DienThoaiDiDong;
                        if (SelectedItem.GioiTinh == "Nam")
                        {
                            IsSelectedSex = 0;
                        }
                        else if (SelectedItem.GioiTinh == "Nữ")
                        {
                            IsSelectedSex = 1;
                        }
                        SelectedCustomerType = SelectedItem.LoaiKhachHang;
                    }
                }
            }
        }

        private LoaiKhachHang _SelectedCustomerType;

        public LoaiKhachHang SelectedCustomerType
        {
            get => _SelectedCustomerType;
            set
            {
                _SelectedCustomerType = value;
                RaisePropertyChanged();
                if (SelectedCustomerType != null)
                {
                    {
                        TenLoaiKhachHang = SelectedCustomerType.TenLoaiKhachHang;
                    }
                }
            }
        }

        #endregion Property

        #region Command Property

        private ICommand addCustomerCommand;
        public ICommand AddCustomerCommand { get => addCustomerCommand; set { addCustomerCommand = value; RaisePropertyChanged(); } }

        private ICommand updateCustomerCommand;
        public ICommand UpdateCustomerCommand { get => updateCustomerCommand; set { updateCustomerCommand = value; RaisePropertyChanged(); } }

        private ICommand _deleteCustomerCommand;

        public ICommand DeleteCustomerCommand { get => _deleteCustomerCommand; set { _deleteCustomerCommand = value; RaisePropertyChanged(); } }

        private ICommand _createNewCustomerCommand;
        public ICommand CreateNewCustomerCommand { get => _createNewCustomerCommand; set { _createNewCustomerCommand = value; RaisePropertyChanged(); } }

        #endregion Command Property

        #region Constructor

        public CustomerViewModel()
        {
            GetListCustomer();
            GetListCustomerType();

            AddCustomerCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(TenKhachHang) || string.IsNullOrEmpty(TenLoaiKhachHang))
                {
                    return false;
                }
                else if (!isSymbol(TenKhachHang))
                {
                    return false;
                }

                var list = DataProvider.Instance.Database.KhachHangs.Where(x => x.TenKhachHang == TenKhachHang && x.MaKhachHang == MaKhachHang).Count();
                if (list != 0)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                AddCustomer_Execute();
            });

            UpdateCustomerCommand = new RelayCommand<object>((p) =>
            {
                return true;
            },
              (p) =>
              {
                  UpdateCustomer_Execute();
              });

            DeleteCustomerCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            }, (p) =>
            {
                DeleteCustomer_Execute();
            });

            CreateNewCustomerCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ClearTextBox();
            });
        }

        #endregion Constructor

        #region Methods

        private bool ClearTextBox()
        {
            if (TenKhachHang != null)
            {
                //MaKhachHang = null;
                TenKhachHang = string.Empty;
                DienThoaiDiDong = string.Empty;
                DiaChi = string.Empty;
                SelectedCustomerType = null;
            }
            return false;
        }

        private void GetListCustomerType()
        {
            CustomerTypeList = new ObservableCollection<LoaiKhachHang>(DataProvider.Instance.Database.LoaiKhachHangs);
        }

        private void GetListCustomer()
        {
            List = new ObservableCollection<KhachHang>(DataProvider.Instance.Database.KhachHangs);
        }

        private void AddCustomer_Execute()
        {
            string sex = "";
            sex = IsSelectedSex == 0 ? "Nam" : "Nữ";

            var Customer = new KhachHang()
            {
                MaKhachHang = MaKhachHang,
                TenKhachHang = TenKhachHang,
                DiaChi = DiaChi,
                GioiTinh = sex,
                DienThoaiDiDong = DienThoaiDiDong,
                MaLoaiKhachHang = SelectedCustomerType.MaLoaiKhachHang,
            };
            DataProvider.Instance.Database.KhachHangs.Add(Customer);
            DataProvider.Instance.Database.SaveChanges();
            List.Add(Customer);
            ClearTextBox();
        }

        private void UpdateCustomer_Execute()
        {
            var res = DataProvider.Instance.Database.KhachHangs.SingleOrDefault(x => x.MaKhachHang == SelectedItem.MaKhachHang);
            if (res != null)
            {
                res.MaKhachHang = MaKhachHang;
                res.TenKhachHang = TenKhachHang;
                res.DiaChi = DiaChi;
                if (IsSelectedSex == 0)
                {
                    res.GioiTinh = "Nam";
                }
                else if (IsSelectedSex == 1)
                {
                    res.GioiTinh = "Nữ";
                }
                res.DienThoaiDiDong = DienThoaiDiDong;
                res.MaLoaiKhachHang = SelectedCustomerType.MaLoaiKhachHang;
                DataProvider.Instance.Database.SaveChanges();
                ClearTextBox();
            }
        }

        private void DeleteCustomer_Execute()
        {
        }

        #endregion Methods
    }
}