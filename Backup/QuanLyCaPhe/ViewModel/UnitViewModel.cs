using QuanLyCaPhe.Model;
using QuanLyCaPhe.View;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLyCaPhe.Message;
using GalaSoft.MvvmLight.Messaging;

namespace QuanLyCaPhe.ViewModel
{
    public class UnitViewModel : BaseViewModel
    {
        #region Property

        private ObservableCollection<DonViTinh> _List;
        public ObservableCollection<DonViTinh> List { get => _List; set { _List = value; RaisePropertyChanged("List"); } }

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

        private bool _daXoa = false;
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
                    _daXoa = value; RaisePropertyChanged("DaXoa");
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

                if (!isSymbol(MaDonViTinh) || !isSymbolAndNumber(TenDonViTinh))
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
                if (((string.IsNullOrEmpty(MaDonViTinh) || string.IsNullOrEmpty(TenDonViTinh)) || SelectedItem == null))
                    return false;

                if (!isSymbol(MaDonViTinh) || !isSymbolAndNumber(TenDonViTinh))
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
            List = new ObservableCollection<DonViTinh>(DataProvider.Instance.Database.DonViTinhs.Where(x => x.DaXoa == DaXoa).ToList());
        }
        public bool ClearTextBox()
        {
            try
            {
                MaDonViTinh = "DVT";
                TenDonViTinh = string.Empty;
                GhiChu = string.Empty;
                IsEnabledUnitCode = true;
                SelectedItem = null;
                return true;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
        }

        private void AddUnit_Execute()
        {
            UserMessage msg = new UserMessage();
            try
            {
                var unit = new DonViTinh() { MaDonViTinh = MaDonViTinh, TenDonViTinh = TenDonViTinh, GhiChu = GhiChu, DaXoa = DaXoa };
                if (unit.GhiChu == "")
                {
                    unit.GhiChu = "Không có";
                }
                DataProvider.Instance.Database.DonViTinhs.Add(unit);
                DataProvider.Instance.Database.SaveChanges();
                List.Add(unit);
                msg.Message = "Thêm dữ liệu thành công";
            }
            catch (System.Exception ex)
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    MessageBox.Show(ex.InnerException.GetBaseException().ToString());
                }
                msg.Message = "Có vấn đề trong thêm dữ liệu";
            }
            Messenger.Default.Send<UserMessage>(msg);
            ClearTextBox();
        }

        private void UpdateUnit_Execute()
        {
            UserMessage msg = new UserMessage();
            var res = DataProvider.Instance.Database.DonViTinhs.SingleOrDefault(x => x.MaDonViTinh == SelectedItem.MaDonViTinh);
            if (res != null)
            {
                try
                {
                    res.TenDonViTinh = TenDonViTinh;
                    res.GhiChu = GhiChu;
                    DataProvider.Instance.Database.SaveChanges();
                    msg.Message = "Cập nhật thành công";
                    
                }
                catch (System.Exception ex)
                {
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        MessageBox.Show(ex.InnerException.GetBaseException().ToString());
                    }
                    msg.Message = "Có vấn đề trong cập nhật dữ liệu";
                }
            }
            Messenger.Default.Send<UserMessage>(msg);
            ClearTextBox();
        }

        private void DeleteUnit_Execute()
        {
            UserMessage msg = new UserMessage();
            if (ConfirmDialog("Bạn có chắc chắn muốn xoá đơn vị tính <<" + SelectedItem.TenDonViTinh + ">> không ? "))
            {
                var unit = DataProvider.Instance.Database.DonViTinhs.SingleOrDefault(x => x.MaDonViTinh == SelectedItem.MaDonViTinh);
                List.Remove(unit);
                unit.DaXoa = true;
                DataProvider.Instance.Database.SaveChanges();
                RaisePropertyChanged("List");
                msg.Message = "Dữ liệu đã xoá thành công";
               
            }
           
            Messenger.Default.Send<UserMessage>(msg);
            ClearTextBox();
        }

        #endregion Methods
    }
}