using QuanLyCaPhe.Model;
using QuanLyCaPhe.View;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyCaPhe.ViewModel
{
    public class UnitViewModel : BaseViewModel
    {
        #region Property

        private ObservableCollection<DonViTinh> _List;
        public ObservableCollection<DonViTinh> List { get => _List; set { _List = value; } }

        private string _tenDonViTinh;

        private string _ghiChu;

        private string _maDonViTinh;


        private bool _isEnabledUnitCode;

        public string TenDonViTinh
        {
            get
            {
                return _tenDonViTinh;
            }
            set
            {
                if (_tenDonViTinh != value)
                {
                    _tenDonViTinh = value; RaisePropertyChanged("TenDonViTinh");
                }
            }
        }

        public string MaDonViTinh { get => _maDonViTinh; set { if (_maDonViTinh != value) _maDonViTinh = value; RaisePropertyChanged("MaDonViTinh"); } }

        public string GhiChu { get => _ghiChu; set { if (_ghiChu != value) _ghiChu = value; RaisePropertyChanged("GhiChu"); } }

        public bool IsEnabledUnitCode
        {
            get => _isEnabledUnitCode;
            set
            {
                if (_isEnabledUnitCode != value)
                {
                    _isEnabledUnitCode = value;
                    RaisePropertyChanged("IsEnabledUnitCode");
                }
            }
        }


        private DonViTinh _SelectedItem;

        public DonViTinh SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                RaisePropertyChanged();
                if (SelectedItem != null)
                {
                    MaDonViTinh = SelectedItem.MaDonViTinh;
                    TenDonViTinh = SelectedItem.TenDonViTinh;
                    GhiChu = SelectedItem.GhiChu;
                    IsEnabledUnitCode = false;
                }
            }
        }

        #endregion Property

        #region Command Property

        private ICommand addUnitCommand;
        public ICommand AddUnitCommand { get => addUnitCommand; set { addUnitCommand = value; RaisePropertyChanged(); } }

        private ICommand updateUnitCommand;
        public ICommand UpdateUnitCommand { get => updateUnitCommand; set { updateUnitCommand = value; RaisePropertyChanged(); } }

        private ICommand _deleteUnitCommand;

        public ICommand DeleteUnitCommand { get => _deleteUnitCommand; set { _deleteUnitCommand = value; RaisePropertyChanged(); } }

        private ICommand _createNewUnitCommand;
        public ICommand CreateNewUnitCommand { get => _createNewUnitCommand; set { _createNewUnitCommand = value; RaisePropertyChanged(); } }


        #endregion Command Property

        #region Constructor 
        public UnitViewModel()
        {
            IsEnabledUnitCode = true;

            MaDonViTinh = "DVT";

            LoadUnitList();

            AddUnitCommand = new RelayCommand<object>((p) =>
            {
                //Ghi chú có thể null
                if (string.IsNullOrEmpty(MaDonViTinh) || string.IsNullOrEmpty(TenDonViTinh))
                {
                    return false;
                }

                if (!isSymbolAndNumber(MaDonViTinh) && (!isSymbolAndNumber(TenDonViTinh)))
                {
                    return false;
                }

                var list = DataProvider.Instance.Database.DonViTinhs.Where(x => x.MaDonViTinh == MaDonViTinh).Count();
                if (list != 0)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                AddUnit_Execute();
            });

            UpdateUnitCommand = new RelayCommand<object>((p) =>
            {
                if (((string.IsNullOrEmpty(MaDonViTinh) && string.IsNullOrEmpty(TenDonViTinh)) || SelectedItem == null))
                    return false;

                if (!isSymbolAndNumber(MaDonViTinh) && (!isSymbolAndNumber(TenDonViTinh)))
                {
                    return false;
                }
                var list = DataProvider.Instance.Database.DonViTinhs.Where(x => x.GhiChu == GhiChu && x.TenDonViTinh == TenDonViTinh).Count();
                if (list != 0)
                {
                    return false;
                }
                return true;
            },
              (p) =>
              {
                  UpdateUnit_Execute();
              });

            DeleteUnitCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            }, (p) =>
            {
                DeleteUnit_Execute();
            });

            CreateNewUnitCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(MaDonViTinh) && string.IsNullOrEmpty(TenDonViTinh))
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                ClearTextBox();
            });

            

            
        }

       
       
        #endregion Constructor

        #region Methods
        private void LoadUnitList()
        {
            List = new ObservableCollection<DonViTinh>(DataProvider.Instance.Database.DonViTinhs);
        }
        private bool ClearTextBox()
        {
            if (MaDonViTinh != null)
            {
                MaDonViTinh = "DVT";
                TenDonViTinh = string.Empty;
                GhiChu = string.Empty;
                IsEnabledUnitCode = true;
                SelectedItem = null;
                return true;
            }
            return false;
        }

        private void AddUnit_Execute()
        {
            var unit = new DonViTinh() { MaDonViTinh = MaDonViTinh, TenDonViTinh = TenDonViTinh, GhiChu = GhiChu };
            if(unit.GhiChu == null)
            {
                unit.GhiChu = "Không có";
            }
            DataProvider.Instance.Database.DonViTinhs.Add(unit);
            DataProvider.Instance.Database.SaveChanges();
            List.Add(unit);
            ClearTextBox();
        }

        private void UpdateUnit_Execute()
        {
            var res = DataProvider.Instance.Database.DonViTinhs.SingleOrDefault(x => x.MaDonViTinh == SelectedItem.MaDonViTinh);
            if (res != null)
            {
                
                res.TenDonViTinh = TenDonViTinh;
                res.GhiChu = GhiChu;
                DataProvider.Instance.Database.SaveChanges();
                ClearTextBox();
            }
        }

        private void DeleteUnit_Execute()
        {
            if (ConfirmDialog("Bạn có chắc chắn muốn xoá đơn vị tính <<" + SelectedItem.TenDonViTinh + ">> không ? "))
            {
                var unit = DataProvider.Instance.Database.DonViTinhs.SingleOrDefault(x => x.MaDonViTinh == SelectedItem.MaDonViTinh);
                List.Remove(unit);

                DataProvider.Instance.Database.DonViTinhs.Remove(unit);
                DataProvider.Instance.Database.SaveChanges();
                RaisePropertyChanged("List");
                ClearTextBox();
            }
            else
            {
                ClearTextBox();
            }
        }

        #endregion Methods
    }
}