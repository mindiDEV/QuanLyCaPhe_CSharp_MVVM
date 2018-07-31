using QuanLyCaPhe.Message;
using QuanLyCaPhe.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace QuanLyCaPhe.ViewModel
{
    public class MenuGroupViewModel : BaseViewModel
    {
        public class MenuDetailInfo
        {
            public string TenLoaiThucDon { get; set; }
            public int MaNhomThucDon { get; set; }
            public string TenNhomThucDon { get; set; }
            public string GhiChu { get; set; }
            public bool IsEnabled { get; set; }


        }

        #region Property

        #region Cách 1

        //private IEnumerable<MenuDetailInfo> _List;
        //public IEnumerable<MenuDetailInfo> List { get => _List; set { _List = value; } }

        //private IEnumerable<LoaiThucDon> _menuTypeList;
        //public IEnumerable<LoaiThucDon> MenuTypeList { get => _menuTypeList; set => _menuTypeList = value; }

        #endregion Cách 1

        #region Cách 2

        private ObservableCollection<NhomThucDon> _List;
        public ObservableCollection<NhomThucDon> List { get => _List; set { _List = value; RaisePropertyChanged("List"); } }

        private ObservableCollection<LoaiThucDon> _menuTypeList;
        public ObservableCollection<LoaiThucDon> MenuTypeList { get => _menuTypeList; set { _menuTypeList = value; RaisePropertyChanged("MenuTypeList"); } }

        #endregion Cách 2

        private string _tenNhomThucDon;

        private string _tenLoaiThucDon = "Đồ uống";

        private string _maNhomThucDon;

        private string _ghiChu;

        private string _maLoaiThucDon;

        private string _maHienThi_NhomThucDon;

        private string _tenHienThi_NhomThucDon;

        private bool _isEnabledMenuGroupCode;

        public string TenLoaiThucDon { get => _tenLoaiThucDon; set { if (_tenLoaiThucDon != value) _tenLoaiThucDon = value; RaisePropertyChanged("TenLoaiThucDon"); } }

        public string TenNhomThucDon { get => _tenNhomThucDon; set { if (_tenNhomThucDon != value) _tenNhomThucDon = value; RaisePropertyChanged("TenNhomThucDon"); } }

        public string MaNhomThucDon { get => _maNhomThucDon; set { if (_maNhomThucDon != value) _maNhomThucDon = value; RaisePropertyChanged("MaNhomThucDon"); } }

        public string GhiChu { get => _ghiChu; set { if (_ghiChu != value) _ghiChu = value; RaisePropertyChanged("GhiChu"); } }
        public string MaLoaiThucDon { get => _maLoaiThucDon; set { if (_maLoaiThucDon != value) _maLoaiThucDon = value; RaisePropertyChanged("MaLoaiThucDon"); } }

        public string MaHienThi_NhomThucDon { get => _maHienThi_NhomThucDon; set { if (_maHienThi_NhomThucDon != value) _maHienThi_NhomThucDon = value; RaisePropertyChanged("MaHienThi_NhomThucDon"); } }

        public string TenHienThi_NhomThucDon { get => _tenHienThi_NhomThucDon; set { if (_tenHienThi_NhomThucDon != value) _tenHienThi_NhomThucDon = value; RaisePropertyChanged("TenHienThi_NhomThucDon"); } }

        private NhomThucDon _SelectedItem;

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
        public NhomThucDon SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                RaisePropertyChanged();
                if (SelectedItem != null)
                {
                    {
                        MaNhomThucDon = SelectedItem.MaNhomThucDon;
                        TenNhomThucDon = SelectedItem.TenNhomThucDon;
                        GhiChu = SelectedItem.GhiChu;
                        SelectedMenuType = SelectedItem.LoaiThucDon;
                        IsEnabledMenuGroupCode = false;
                    }
                }
            }
        }

        public bool IsEnabledMenuGroupCode
        {
            get => _isEnabledMenuGroupCode;
            set
            {
                _isEnabledMenuGroupCode = value;
                RaisePropertyChanged();
                
            }
        }

        //Get SelectedItem event of MenuType
        private LoaiThucDon _SelectedMenuType;

        public LoaiThucDon SelectedMenuType
        {
            get
            {
                return _SelectedMenuType;
            }
            set
            {
                _SelectedMenuType = value;
                RaisePropertyChanged();
                if (SelectedMenuType != null)
                {
                    TenLoaiThucDon = SelectedMenuType.TenLoaiThucDon;

                    if (SelectedMenuType.MaLoaiThucDon == "LTDMA")
                    {
                        MaHienThi_NhomThucDon = "Mã nhóm món ăn ( * )";
                        TenHienThi_NhomThucDon = "Tên nhóm món ăn ( * )";
                    }
                    else if (SelectedMenuType.MaLoaiThucDon == "LTDDU")
                    {
                        MaHienThi_NhomThucDon = "Mã nhóm đồ uống ( * )";
                        TenHienThi_NhomThucDon = "Tên nhóm đồ uống ( * )";
                    }
                    else if (SelectedMenuType.MaLoaiThucDon == "LTDK")
                    {
                        MaHienThi_NhomThucDon = "Mã nhóm khác ( * )";
                        TenHienThi_NhomThucDon = "Tên nhóm khác ( * )";
                    }
                }
            }
        }

        #endregion Property

        #region Command Property

        private ICommand addMenuGroupCommand;
        public ICommand AddMenuGroupCommand { get => addMenuGroupCommand; set { addMenuGroupCommand = value; RaisePropertyChanged(); } }

        private ICommand updateMenuGroupCommand;
        public ICommand UpdateMenuGroupCommand { get => updateMenuGroupCommand; set { updateMenuGroupCommand = value; RaisePropertyChanged(); } }

        private ICommand deleteMenuGroupCommand;
        public ICommand DeleteMenuGroupCommand { get => deleteMenuGroupCommand; set { deleteMenuGroupCommand = value; RaisePropertyChanged(); } }

        private ICommand createNewMenuGroupCommand;
        public ICommand CreateNewMenuGroupCommand { get => createNewMenuGroupCommand; set { createNewMenuGroupCommand = value; RaisePropertyChanged(); } }

        #endregion Command Property

        #region Constructor

        public MenuGroupViewModel()
        {
            //GetMenuDetails();

            //GetComboBoxOfMenuType();

            IsEnabledMenuGroupCode = true;

            MaHienThi_NhomThucDon = "Mã nhóm ( * )";

            TenHienThi_NhomThucDon = "Tên nhóm ( * )";

            MaNhomThucDon = "NTD";

            //Hiển thị tất cả dữ liệu của nhóm và loại THỰC ĐƠN
            LoadAll();

            AddMenuGroupCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(TenNhomThucDon) || string.IsNullOrEmpty(MaNhomThucDon) || SelectedMenuType==null)
                {
                    return false;
                }

                if (!isSymbol(MaNhomThucDon) || !isSymbolAndNumber(TenNhomThucDon))
                {
                    return false;
                }
                var checkId = DataProvider.Instance.Database.NhomThucDons.Where(x => x.MaNhomThucDon == MaNhomThucDon).Count();
                if (checkId != 0)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                AddMenuGroup();
            });

            UpdateMenuGroupCommand = new RelayCommand<object>((p) =>
            {
                if (((string.IsNullOrEmpty(TenHienThi_NhomThucDon) || string.IsNullOrEmpty(MaHienThi_NhomThucDon))|| SelectedItem == null))
                {
                    return false;
                }
                if (!isSymbol(MaNhomThucDon) || !isSymbolAndNumber(TenNhomThucDon))
                {
                    return false;
                }

                var checkId = DataProvider.Instance.Database.NhomThucDons.Where(x =>x.MaNhomThucDon == MaNhomThucDon && x.TenNhomThucDon == TenNhomThucDon && x.GhiChu == GhiChu && x.MaLoaiThucDon == SelectedMenuType.MaLoaiThucDon).Count();
                if (checkId != 0)
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                UpdateMenuGroup();
            });

            DeleteMenuGroupCommand = new RelayCommand<object>(
                (p) =>
                {
                    if (SelectedItem == null || SelectedMenuType == null)
                    {
                        return false;
                    }
                    return true;
                },
                (p) =>
                {
                    DeleteMenuGroup();
                });

            CreateNewMenuGroupCommand = new RelayCommand<object>(
                (p) =>
                {
                    if (SelectedItem == null || SelectedMenuType == null)
                    {
                        return false;
                    }
                    return true;
                },
                (p) =>
                {
                    ClearTextBox();
                });
        }

        private void DeleteMenuGroup()
        {
            UserMessage msg = new UserMessage();
            if (ConfirmDialog("Bạn có chắc chắn muốn xoá nhóm thực đơn <<" + SelectedItem.TenNhomThucDon + ">> không ? "))
            {
                try
                {
                    var menuGroup = DataProvider.Instance.Database.NhomThucDons.SingleOrDefault(x => x.MaNhomThucDon == SelectedItem.MaNhomThucDon);
                    List.Remove(menuGroup);
                    menuGroup.DaXoa = true;
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

        private void LoadAll()
        {
            GetListMenuGroup();

            GetListComboBoxMenuType();
        }

        #endregion Constructor

        #region Methods

        //Get list menu and display on ListView
        /*public IEnumerable<MenuDetailInfo> GetMenuDetails()
        {
            List = (from ntd
                       in DataProvider.Instance.Database.NhomThucDons
                    join ltd in DataProvider.Instance.Database.LoaiThucDons
                    on ntd.MaLoaiThucDon equals ltd.MaLoaiThucDon
                    select new MenuDetailInfo
                    {
                        TenLoaiThucDon = ltd.TenLoaiThucDon,
                        MaNhomThucDon = ntd.MaNhomThucDon,
                        TenNhomThucDon = ntd.TenNhomThucDon,
                        GhiChu = ntd.GhiChu
                    }).ToList();

            return List;
        }*/

        //Get list of combobox and display on combobox
        /*public IEnumerable<LoaiThucDon> GetComboBoxOfMenuType()
        {
            MenuTypeList = DataProvider.Instance.Database.LoaiThucDons.ToList();
            return MenuTypeList;
        }*/

        private void AddMenuGroup()
        {
            UserMessage msg = new UserMessage();
            try
            {
                var menuGroup = new NhomThucDon()
                {
                    MaNhomThucDon = MaNhomThucDon,
                    TenNhomThucDon = TenNhomThucDon,
                    GhiChu = GhiChu,
                    MaLoaiThucDon = SelectedMenuType.MaLoaiThucDon,
                    DaXoa = DaXoa
                };

                if (menuGroup.GhiChu == "")
                {
                    menuGroup.GhiChu = "Không có";
                }


                DataProvider.Instance.Database.NhomThucDons.Add(menuGroup);
                DataProvider.Instance.Database.SaveChanges();
                List.Add(menuGroup);
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

        private void UpdateMenuGroup()
        {
            UserMessage msg = new UserMessage();
            try
            {
                var menuGroup = DataProvider.Instance.Database.NhomThucDons.Where(x => x.MaNhomThucDon == SelectedItem.MaNhomThucDon).SingleOrDefault();
                if (menuGroup != null)
                {

                    IsEnabledMenuGroupCode = false;
                    menuGroup.TenNhomThucDon = TenNhomThucDon;
                    menuGroup.GhiChu = GhiChu;
                    menuGroup.MaLoaiThucDon = SelectedMenuType.MaLoaiThucDon;
                    DataProvider.Instance.Database.SaveChanges();
                    msg.Message = "Cập nhật thành công";
                }
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

        private bool GetListMenuGroup()
        {
            if (List == null)
            {
                List = new ObservableCollection<NhomThucDon>(DataProvider.Instance.Database.NhomThucDons.Where(x=>x.DaXoa == DaXoa).ToList());
                return true;
            }
            return false;
        }

        private bool GetListComboBoxMenuType()
        {
            if (MenuTypeList == null)
            {
                MenuTypeList = new ObservableCollection<LoaiThucDon>(DataProvider.Instance.Database.LoaiThucDons.Where(x=>x.DaXoa==DaXoa).ToList());
                return true;
            }
            return false;
        }

        public bool ClearTextBox()
        {
            if (MaNhomThucDon != null)
            {
                MaNhomThucDon = "NTD";
                TenNhomThucDon = string.Empty;
                GhiChu = string.Empty;
                SelectedItem = null;
                SelectedMenuType = null;
                IsEnabledMenuGroupCode = true;
                return true;
            }
            return false;
        }

        #endregion Methods
    }
}