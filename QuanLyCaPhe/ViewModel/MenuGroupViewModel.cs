using QuanLyCaPhe.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

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
        public ObservableCollection<NhomThucDon> List { get => _List; set { _List = value; } }

        private ObservableCollection<LoaiThucDon> _menuTypeList;
        public ObservableCollection<LoaiThucDon> MenuTypeList { get => _menuTypeList; set { _menuTypeList = value; } }

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

            GetListMenuGroup();
            GetListComboBoxMenuType();

            AddMenuGroupCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(TenNhomThucDon) || string.IsNullOrEmpty(MaNhomThucDon) || SelectedMenuType==null)
                {
                    return false;
                }

                if (!isSymbolAndNumber(MaNhomThucDon) && !isSymbolAndNumber(TenNhomThucDon))
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
                if (((string.IsNullOrEmpty(TenHienThi_NhomThucDon) && string.IsNullOrEmpty(MaHienThi_NhomThucDon))|| SelectedItem == null))
                {
                    return false;
                }
                if (!isSymbolAndNumber(MaNhomThucDon) && !isSymbol(TenNhomThucDon))
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
                    
                    return true;
                },
                (p) =>
                {
                    WarningDialogs("Dữ liệu không thể xoá vì đang có ràng buộc. Xin vui lòng thử lại vào dịp khác!!!");
                });

            CreateNewMenuGroupCommand = new RelayCommand<object>(
                (p) =>
                {
                    if (SelectedItem == null && SelectedMenuType == null)
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
            var menuGroup = new NhomThucDon() { MaNhomThucDon = MaNhomThucDon, TenNhomThucDon = TenNhomThucDon, GhiChu = GhiChu, MaLoaiThucDon = SelectedMenuType.MaLoaiThucDon };
            if (menuGroup.GhiChu == null)
            {
                menuGroup.GhiChu = "Không có";
            }   
            

            DataProvider.Instance.Database.NhomThucDons.Add(menuGroup);
            DataProvider.Instance.Database.SaveChanges();
            List.Add(menuGroup);
            ClearTextBox();
        }

        private void UpdateMenuGroup()
        {
            var menuGroup = DataProvider.Instance.Database.NhomThucDons.Where(x => x.MaNhomThucDon == SelectedItem.MaNhomThucDon).SingleOrDefault();
            if(menuGroup!=null)
            {
                IsEnabledMenuGroupCode = false;
                menuGroup.TenNhomThucDon = TenNhomThucDon;
                menuGroup.GhiChu = GhiChu;
                menuGroup.MaLoaiThucDon = SelectedMenuType.MaLoaiThucDon;
                DataProvider.Instance.Database.SaveChanges();
                ClearTextBox();
            }
           
        }

        private bool GetListMenuGroup()
        {
            if (List == null)
            {
                List = new ObservableCollection<NhomThucDon>(DataProvider.Instance.Database.NhomThucDons);
                return true;
            }
            return false;
        }

        private bool GetListComboBoxMenuType()
        {
            if (MenuTypeList == null)
            {
                MenuTypeList = new ObservableCollection<LoaiThucDon>(DataProvider.Instance.Database.LoaiThucDons);
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