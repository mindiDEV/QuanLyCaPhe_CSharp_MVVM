using GalaSoft.MvvmLight.Messaging;
using QuanLyCaPhe.Message;
using QuanLyCaPhe.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace QuanLyCaPhe.ViewModel
{
    public class CustomerTypeViewModel : BaseViewModel
    {
        #region Property

        private ObservableCollection<LoaiKhachHang> _List;
        public ObservableCollection<LoaiKhachHang> List { get => _List; set { _List = value; RaisePropertyChanged("List"); } }

        private string _tenLoaiKhachHang;

        private string _maLoaiKhachHang;

        private string _ghiChu;

        public string TenLoaiKhachHang { get => _tenLoaiKhachHang; set { if (_tenLoaiKhachHang != value) _tenLoaiKhachHang = value; RaisePropertyChanged("TenLoaiKhachHang"); } }

        public string MaLoaiKhachHang { get => _maLoaiKhachHang; set { if (_maLoaiKhachHang != value) _maLoaiKhachHang = value; RaisePropertyChanged("MaLoaiKhachHang"); } }

        public string GhiChu { get => _ghiChu; set { if (_ghiChu != value) _ghiChu = value; RaisePropertyChanged("GhiChu"); } }

        private LoaiKhachHang _SelectedItem;

        public LoaiKhachHang SelectedItem { get => _SelectedItem; set { _SelectedItem = value; RaisePropertyChanged(); if (SelectedItem != null) { MaLoaiKhachHang = SelectedItem.MaLoaiKhachHang; TenLoaiKhachHang = SelectedItem.TenLoaiKhachHang; GhiChu = SelectedItem.GhiChu; IsEnabledCustomerTypeCode = false; } } }

        private bool _isEnabledCustomerTypeCode;
        public bool IsEnabledCustomerTypeCode
        {
            get => _isEnabledCustomerTypeCode;
            set
            {
                if (_isEnabledCustomerTypeCode != value)
                {
                    _isEnabledCustomerTypeCode = value;
                    RaisePropertyChanged("IsEnabledCustomerTypeCode");
                }
            }
        }

        private bool _daXoa;
        public bool DaXoa
        {
            get => _daXoa;
            set
            {
                if (_daXoa != value)
                {
                    _daXoa = value;
                    RaisePropertyChanged("DaXoa");
                }
            }
        }

        #endregion Property

        #region Command Property

        private ICommand addCustomerTypeCommand;
        public ICommand AddCustomerTypeCommand { get => addCustomerTypeCommand; set { addCustomerTypeCommand = value; RaisePropertyChanged(); } }

        private ICommand updateCustomerTypeCommand;
        public ICommand UpdatCustomerTypeCommand { get => updateCustomerTypeCommand; set { updateCustomerTypeCommand = value; RaisePropertyChanged(); } }

        private ICommand deleteCustomerTypeCommand;

        public ICommand DeleteCustomerTypeCommand { get => deleteCustomerTypeCommand; set { deleteCustomerTypeCommand = value; RaisePropertyChanged(); } }

        private ICommand createNewCustomerTypeCommand;
        public ICommand CreateNewCustomerTypeCommand { get => createNewCustomerTypeCommand; set { createNewCustomerTypeCommand = value; RaisePropertyChanged(); } }

        #endregion Command Property

        #region Constructor

        public CustomerTypeViewModel()
        {
            IsEnabledCustomerTypeCode = true;

            MaLoaiKhachHang = "LKH";

            List = new ObservableCollection<LoaiKhachHang>(DataProvider.Instance.Database.LoaiKhachHangs.Where(x=>x.DaXoa == DaXoa).ToList());

            AddCustomerTypeCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(MaLoaiKhachHang) || string.IsNullOrEmpty(TenLoaiKhachHang))
                {
                    return false;
                }
                if ((!isSymbolAndNumber(MaLoaiKhachHang)) || (!isSymbolAndNumber(TenLoaiKhachHang)))
                {
                    return false;
                }

                var list = DataProvider.Instance.Database.LoaiKhachHangs.Where(x => x.MaLoaiKhachHang == MaLoaiKhachHang).Count();
                if (list != 0)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                AddCustomerType_Execute();
            });

            UpdatCustomerTypeCommand = new RelayCommand<object>((p) =>
            {
                if ((string.IsNullOrEmpty(MaLoaiKhachHang) && string.IsNullOrEmpty(TenLoaiKhachHang)) || SelectedItem == null)
                    return false;

                if ((!isSymbolAndNumber(MaLoaiKhachHang)) || (!isSymbolAndNumber(TenLoaiKhachHang)))
                {
                    return false;
                }
                if (DataProvider.Instance.Database.LoaiKhachHangs.Where(x => x.TenLoaiKhachHang == TenLoaiKhachHang && x.MaLoaiKhachHang == MaLoaiKhachHang && x.GhiChu == GhiChu).Count() != 0)
                {
                    return false;
                }
                return true;
            },
              (p) =>
              {
                  UpdateCustomerType_Execute();
              });

            DeleteCustomerTypeCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            }, (p) =>
            {
                DeleteCustomerType_Execute();
            });

            CreateNewCustomerTypeCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(MaLoaiKhachHang))
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                ClearTextBox();
            });
        }

        public bool ClearTextBox()
        {
            if (MaLoaiKhachHang != null)
            {
                MaLoaiKhachHang = "LKH";
                TenLoaiKhachHang = string.Empty;
                GhiChu = string.Empty;
                SelectedItem = null;
                IsEnabledCustomerTypeCode = true;
                return true;
            }
            return false;
        }

        private void AddCustomerType_Execute()
        {
            UserMessage msg = new UserMessage();
            try
            {
                var customerType = new LoaiKhachHang()
                {
                    MaLoaiKhachHang = MaLoaiKhachHang,
                    TenLoaiKhachHang = TenLoaiKhachHang,
                    GhiChu = GhiChu,
                    DaXoa = DaXoa,
                };
                if (customerType.GhiChu == "")
                {
                    customerType.GhiChu = @"Không có";
                }
                DataProvider.Instance.Database.LoaiKhachHangs.Add(customerType);
                DataProvider.Instance.Database.SaveChanges();
                List.Add(customerType);
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

        private void DeleteCustomerType_Execute()
        {
            UserMessage msg = new UserMessage();
            if (ConfirmDialog("Bạn có chắc chắn muốn xoá loại khách hàng <<" + SelectedItem.TenLoaiKhachHang + ">> không ? "))
            {
                var customerType = DataProvider.Instance.Database.LoaiKhachHangs.SingleOrDefault(x => x.MaLoaiKhachHang == SelectedItem.MaLoaiKhachHang);
                List.Remove(customerType);
                customerType.DaXoa = true;
                DataProvider.Instance.Database.SaveChanges();
                RaisePropertyChanged("List");
                msg.Message = "Dữ liệu đã xoá thành công";
            }
            ClearTextBox();
            Messenger.Default.Send<UserMessage>(msg);
        }

        private void UpdateCustomerType_Execute()
        {
            UserMessage msg = new UserMessage();
            var res = DataProvider.Instance.Database.LoaiKhachHangs.SingleOrDefault(x => x.TenLoaiKhachHang == SelectedItem.TenLoaiKhachHang && x.MaLoaiKhachHang == SelectedItem.MaLoaiKhachHang);
            if (res != null)
            {
                try
                {
                    res.MaLoaiKhachHang = MaLoaiKhachHang;
                    res.TenLoaiKhachHang = TenLoaiKhachHang;
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

        #endregion Constructor
    }
}