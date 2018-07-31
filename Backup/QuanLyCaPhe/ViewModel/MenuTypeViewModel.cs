using GalaSoft.MvvmLight.Messaging;
using QuanLyCaPhe.Message;
using QuanLyCaPhe.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace QuanLyCaPhe.ViewModel
{
    public class MenuTypeViewModel : BaseViewModel
    {
        #region Property

        private ObservableCollection<LoaiThucDon> _List;
        public ObservableCollection<LoaiThucDon> List { get => _List; set { _List = value; RaisePropertyChanged("List"); } }

        private string _tenLoaiThucDon;

        private string _maLoaiThucDon;

        public string TenLoaiThucDon { get => _tenLoaiThucDon; set { if (_tenLoaiThucDon != value) _tenLoaiThucDon = value; RaisePropertyChanged("TenLoaiThucDon"); } }

        public string MaLoaiThucDon { get => _maLoaiThucDon; set { if (_maLoaiThucDon != value) _maLoaiThucDon = value; RaisePropertyChanged("MaLoaiThucDon"); } }

        private LoaiThucDon _SelectedItem;

        public LoaiThucDon SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                RaisePropertyChanged();
                if (SelectedItem != null)
                {
                    MaLoaiThucDon = SelectedItem.MaLoaiThucDon;
                    TenLoaiThucDon = SelectedItem.TenLoaiThucDon;
                    IsEnabledMenuTypeCode = false;
                }
            }
        }

        private bool _isEnabledMenuTypeCode;
        public bool IsEnabledMenuTypeCode { get => _isEnabledMenuTypeCode; set { if (_isEnabledMenuTypeCode != value) _isEnabledMenuTypeCode = value; RaisePropertyChanged("IsEnabledMenuTypeCode"); } }

        private bool _daXoa = false;
        public bool DaXoa
        {
            get => _daXoa;
            set
            {
                _daXoa = value;
                RaisePropertyChanged();
                
            }
        }

        #endregion Property

        #region Command Property

        private ICommand addMenuTypeCommand;
        public ICommand AddMenuTypeCommand { get => addMenuTypeCommand; set { addMenuTypeCommand = value; RaisePropertyChanged(); } }

        private ICommand updateMenuTypeCommand;
        public ICommand UpdateMenuTypeCommand { get => updateMenuTypeCommand; set { updateMenuTypeCommand = value; RaisePropertyChanged(); } }

        private ICommand deleteMenuTypeCommand;

        public ICommand DeleteMenuTypeCommand { get => deleteMenuTypeCommand; set { deleteMenuTypeCommand = value; RaisePropertyChanged(); } }

        private ICommand createNewMenuTypeCommand;
        public ICommand CreateNewMenuTypeCommand { get => createNewMenuTypeCommand; set { createNewMenuTypeCommand = value; RaisePropertyChanged(); } }

        #endregion Command Property

        #region Constructor

        public MenuTypeViewModel()
        {

            IsEnabledMenuTypeCode = true;

            MaLoaiThucDon = "LTD";

            LoadMenuTypeList();


            AddMenuTypeCommand = new RelayCommand<object>((p) =>
            {
                if ((string.IsNullOrEmpty(MaLoaiThucDon)) || (string.IsNullOrEmpty(TenLoaiThucDon)))
                {
                    return false;
                }
                if (!isSymbolAndNumber(MaLoaiThucDon) || (!isSymbolAndNumber(TenLoaiThucDon)))
                {
                    return false;
                }

                var list = DataProvider.Instance.Database.LoaiThucDons.Where(x => x.MaLoaiThucDon == MaLoaiThucDon).Count();
                if (list != 0)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                AddMenuType_Execute();
            });

            UpdateMenuTypeCommand = new RelayCommand<object>((p) =>
            {
                if (((string.IsNullOrEmpty(TenLoaiThucDon) && string.IsNullOrEmpty(MaLoaiThucDon)) || SelectedItem == null))
                    return false;

                if (!isSymbolAndNumber(MaLoaiThucDon) || (!isSymbolAndNumber(TenLoaiThucDon)))
                {
                    return false;
                }

                var list = DataProvider.Instance.Database.LoaiThucDons.Where(x => x.TenLoaiThucDon == TenLoaiThucDon && x.MaLoaiThucDon == MaLoaiThucDon).Count();
                if (list != 0)
                {
                    return false;
                }
                return true;
            },
              (p) =>
              {
                 
                  UpdteMenuType_Execute();
              });

            DeleteMenuTypeCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            }, (p) =>
            {
                DeleteMenuType_Execute();
            });

            CreateNewMenuTypeCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(TenLoaiThucDon) && string.IsNullOrEmpty(MaLoaiThucDon))
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                ClearTextBox();
            });
        }

        private void LoadMenuTypeList()
        {
            List = new ObservableCollection<LoaiThucDon>(DataProvider.Instance.Database.LoaiThucDons.Where(x => x.DaXoa == DaXoa).ToList());

        }

        private void UpdteMenuType_Execute()
        {
            UserMessage msg = new UserMessage();
            var res = DataProvider.Instance.Database.LoaiThucDons.SingleOrDefault(x => x.MaLoaiThucDon == SelectedItem.MaLoaiThucDon);
            if (res != null)
            {
                try
                {
                    IsEnabledMenuTypeCode = false;
                    res.TenLoaiThucDon = TenLoaiThucDon;
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
                Messenger.Default.Send<UserMessage>(msg);
                ClearTextBox();
            }

        }

        private void DeleteMenuType_Execute()
        {
            UserMessage msg = new UserMessage();
            if (ConfirmDialog("Bạn có chắc chắn muốn xoá loại thực đơn <<" + SelectedItem.TenLoaiThucDon + ">> không ? "))
            {
                try
                {
                    var menuType = DataProvider.Instance.Database.LoaiThucDons.SingleOrDefault(x => x.MaLoaiThucDon == SelectedItem.MaLoaiThucDon);
                    List.Remove(menuType);
                    menuType.DaXoa = true;
                    DataProvider.Instance.Database.SaveChanges();
                    RaisePropertyChanged("List");
                    msg.Message = "Dữ liệu đã xoá thành công";
                }
                catch (System.Exception ex)
                {
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        MessageBox.Show(ex.InnerException.GetBaseException().ToString());
                    }
                    msg.Message = "Có vấn đề trong xoá dữ liệu";
                }
                
                
            }
            Messenger.Default.Send<UserMessage>(msg);
            ClearTextBox();
        }

        private void AddMenuType_Execute()
        {
            UserMessage msg = new UserMessage();
            try
            {
                var menuType = new LoaiThucDon() { MaLoaiThucDon = MaLoaiThucDon, TenLoaiThucDon = TenLoaiThucDon, DaXoa = DaXoa };
                DataProvider.Instance.Database.LoaiThucDons.Add(menuType);
                DataProvider.Instance.Database.SaveChanges();
                List.Add(menuType);
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

        #endregion Constructor

        public bool ClearTextBox()
        {
            if (MaLoaiThucDon != null)
            {
                MaLoaiThucDon = "LTD";
                TenLoaiThucDon = string.Empty;
                SelectedItem = null;
                IsEnabledMenuTypeCode = true;
                return true;
            }
            return false;
        }
    }
}