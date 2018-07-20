using QuanLyCaPhe.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace QuanLyCaPhe.ViewModel
{
    public class EmployeeViewModel : BaseViewModel
    {
        #region Property

        private ObservableCollection<NhanVien> _List;
        public ObservableCollection<NhanVien> List { get => _List; set { _List = value; } }

        private string _maNhanVien;

        private string _tenNhanVien;

        private string _gioiTinh;

        private string _diaChi;

        private string _CMND;

        private DateTime? _ngayCap;

        private string _noiCapCMND;

        private string _dienThoaiDiDong;

        private DateTime? _ngaySinh;

        private string _trangThaiLamViec;

        private int? _IsSelectedSex;

        private int? _IsSelectedStatus;

        public int? IsSelectedStatus
        {
            get => _IsSelectedStatus;
            set
            {
                if (_IsSelectedStatus != value)
                {
                    _IsSelectedStatus = value;

                    RaisePropertyChanged("IsSelectedStatus");
                }
            }
        }

        public int? IsSelectedSex
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

        public string MaNhanVien
        {
            get => _maNhanVien;
            set
            {
                if (_maNhanVien != value)
                {
                    _maNhanVien = value;
                    RaisePropertyChanged("MaNhanVien");
                }
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
                if (_tenNhanVien != value)
                {
                    _tenNhanVien = value; RaisePropertyChanged("TenNhanVien");
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

        public DateTime? NgayCap
        {
            get => _ngayCap;
            set
            {
                if (_ngayCap != value)
                {
                    _ngayCap = value; RaisePropertyChanged("NgayCap");
                }
            }
        }

        public string CMND
        {
            get => _CMND;
            set
            {
                if (_CMND != value)
                {
                    _CMND = value; RaisePropertyChanged("CMND");
                }
            }
        }

        public string NoiCapCMND
        {
            get => _noiCapCMND;
            set
            {
                if (_noiCapCMND != value)
                {
                    _noiCapCMND = value; RaisePropertyChanged("NoiCapCMND");
                }
            }
        }

        public DateTime? NgaySinh
        {
            get => _ngaySinh;
            set
            {
                if (_ngaySinh != value)
                {
                    _ngaySinh = value; RaisePropertyChanged("NgaySinh");
                }
            }
        }

        public string TrangThaiLamViec
        {
            get => _trangThaiLamViec;
            set
            {
                if (_trangThaiLamViec != value)
                {
                    _trangThaiLamViec = value; RaisePropertyChanged("TrangThaiLamViec");
                }
            }
        }

        private NhanVien _SelectedItem;

        public NhanVien SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                RaisePropertyChanged();
                if (SelectedItem != null)
                {
                    {
                        MaNhanVien = SelectedItem.MaNhanVien;
                        TenNhanVien = SelectedItem.TenNhanVien;
                        if (SelectedItem.GioiTinh == "Nữ")
                        {
                            IsSelectedSex = 1;
                        }
                        else IsSelectedSex = 0;
                        DiaChi = SelectedItem.DiaChi;
                        CMND = SelectedItem.CMND;
                        NgayCap = Convert.ToDateTime(SelectedItem.NgayCap);
                        NoiCapCMND = SelectedItem.NoiCapCMND;
                        NgaySinh = Convert.ToDateTime(SelectedItem.NgaySinh);
                        DienThoaiDiDong = SelectedItem.DienThoaiDiDong;
                        if (SelectedItem.TrangThaiLamViec == "Nghỉ việc")
                        {
                            IsSelectedStatus = 1;
                        }
                        else IsSelectedStatus = 0;
                    }
                }
            }
        }

        #endregion Property

        #region Command Property

        private ICommand addEmployeeCommand;
        public ICommand AddEmployeeCommand { get => addEmployeeCommand; set { addEmployeeCommand = value; RaisePropertyChanged(); } }

        private ICommand updateEmployeeCommand;
        public ICommand UpdateEmployeeCommand { get => updateEmployeeCommand; set { updateEmployeeCommand = value; RaisePropertyChanged(); } }

        private ICommand _deleteEmployeeCommand;

        public ICommand DeleteEmployeeCommand { get => _deleteEmployeeCommand; set { _deleteEmployeeCommand = value; RaisePropertyChanged(); } }

        private ICommand _createNewCEmployeeCommand;
        public ICommand CreateNewEmployeeCommand { get => _createNewCEmployeeCommand; set { _createNewCEmployeeCommand = value; RaisePropertyChanged(); } }

        #endregion Command Property

        #region Constructor

        public EmployeeViewModel()
        {
            MaNhanVien = "NV";

            GetListEmployee();

            AddEmployeeCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(MaNhanVien) || string.IsNullOrEmpty(TenNhanVien))
                {
                    return false;
                }

                if (!isSymbol(MaNhanVien))
                {
                    return false;
                }

                var employeeCheckID = DataProvider.Instance.Database.NhanViens.Where(x => x.MaNhanVien == MaNhanVien).Count();
                if (employeeCheckID != 0)
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                AddEmployee_Execute();
            });

            UpdateEmployeeCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                {
                    return false;
                }
                return true;
            },
              (p) =>
              {
                  UpdateEmployee_Execute();
              });

            DeleteEmployeeCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            }, (p) =>
            {
                DeleteEmployee_Execute();
            });

            CreateNewEmployeeCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                ClearTextBox();
            });
        }

        #endregion Constructor

        #region Methods

        public bool ClearTextBox()
        {
            if(MaNhanVien!=null)
            {
                MaNhanVien = string.Empty;
                TenNhanVien = string.Empty;
                DiaChi = string.Empty;
                CMND = string.Empty;
                DienThoaiDiDong = string.Empty;
                IsSelectedStatus = null;
                IsSelectedSex = null;
                NoiCapCMND = string.Empty;
                NgayCap = null;
                NgaySinh = null;
                SelectedItem = null;
                return true;
            }
            return false;
        }

        private void GetListEmployee()
        {
            List = new ObservableCollection<NhanVien>(DataProvider.Instance.Database.NhanViens);
        }

        private void AddEmployee_Execute()
        {
            NhanVien nv = new NhanVien();
            nv.MaNhanVien = MaNhanVien;
            nv.TenNhanVien = TenNhanVien;
            nv.DiaChi = DiaChi;
            nv.CMND = CMND;
            nv.NgayCap = NgayCap;
            nv.NoiCapCMND = NoiCapCMND;
            nv.NgaySinh = NgaySinh;
            nv.DienThoaiDiDong = DienThoaiDiDong;
            if (IsSelectedSex == 0)
            {
                nv.GioiTinh = "Nam";
            }
            else if (IsSelectedSex == 1)
            {
                nv.GioiTinh = "Nữ";
            }
            if (IsSelectedStatus == 0)
            {
                nv.TrangThaiLamViec = "Đang làm";
            }
            else if (IsSelectedStatus == 1)
            {
                nv.TrangThaiLamViec = "Nghỉ việc";
            }
            DataProvider.Instance.Database.NhanViens.Add(nv);
            DataProvider.Instance.Database.SaveChanges();
            List.Add(nv);
            ClearTextBox();
        }

        private void UpdateEmployee_Execute()
        {
            var employeeUpdate = DataProvider.Instance.Database.NhanViens.Where(x => x.MaNhanVien == SelectedItem.MaNhanVien).SingleOrDefault();
            employeeUpdate.MaNhanVien = MaNhanVien;
            employeeUpdate.TenNhanVien = TenNhanVien;
            employeeUpdate.DiaChi = DiaChi;
            employeeUpdate.CMND = CMND;
            employeeUpdate.NgayCap = NgayCap;
            employeeUpdate.NoiCapCMND = NoiCapCMND;
            employeeUpdate.DienThoaiDiDong = DienThoaiDiDong;
            if (IsSelectedSex == 1)
            {
                employeeUpdate.GioiTinh = "Nữ";
            }
            else employeeUpdate.GioiTinh = "Nam";
            if (IsSelectedStatus == 1)
            {
                employeeUpdate.TrangThaiLamViec = "Nghỉ việc";
            }
            else employeeUpdate.TrangThaiLamViec = "Đang làm";
            DataProvider.Instance.Database.SaveChanges();
            ClearTextBox();
        }

        private void DeleteEmployee_Execute()
        {
            var employeeID = DataProvider.Instance.Database.NhanViens.Where(x => x.MaNhanVien == SelectedItem.MaNhanVien).SingleOrDefault();
            List.Remove(employeeID);
            DataProvider.Instance.Database.NhanViens.Remove(employeeID);
            DataProvider.Instance.Database.SaveChanges();
            ClearTextBox();
        }

        #endregion Methods
    }
}