using Microsoft.Win32;
using QuanLyCaPhe.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using QuanLyCaPhe.Message;
using GalaSoft.MvvmLight.Messaging;

namespace QuanLyCaPhe.ViewModel
{
    public class MenuViewModel : BaseViewModel
    {
        #region Properties

        private string _maMon;

        public string MaMon
        {
            get
            {
                return _maMon;
            }
            set
            {
                if (_maMon != value)
                {
                    _maMon = value;
                    RaisePropertyChanged("MaMon");
                }
            }
        }

        private string _tenMon;

        public string TenMon
        {
            get
            {
                return _tenMon;
            }
            set
            {
                if (_tenMon != value)
                {
                    _tenMon = value;
                    RaisePropertyChanged("TenMon");
                }
            }
        }

        private double _giaBan;

        public double GiaBan
        {
            get
            {
                return _giaBan;
            }
            set
            {
                if (_giaBan != value)
                {
                    _giaBan = value;
                    RaisePropertyChanged("GiaBan");
                }
            }
        }

        private string _ghiChu;

        public string GhiChu
        {
            get
            {
                return _ghiChu;
            }
            set
            {
                if (_ghiChu != value)
                {
                    _ghiChu = value;
                    RaisePropertyChanged("GhiChu");
                }
            }
        }

        private string _tenLoaiThucDon;

        public string TenLoaiThucDon
        {
            get
            {
                return _tenLoaiThucDon;
            }
            set
            {
                if (_tenLoaiThucDon != value)
                {
                    _tenLoaiThucDon = value;
                    RaisePropertyChanged("TenLoaiThucDon");
                }
            }
        }

        private string _maLoaiThucDon;

        public string MaLoaiThucDon
        {
            get
            {
                return _maLoaiThucDon;
            }
            set
            {
                if (_maLoaiThucDon != value)
                {
                    _maLoaiThucDon = value;
                    RaisePropertyChanged("MaLoaiThucDon");
                }
            }
        }

        private string _tenDonViTinh;

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
                    _tenDonViTinh = value;
                    RaisePropertyChanged("TenDonViTinh");
                }
            }
        }

        private string _maDonViTinh;

        public string MaDonViTinh
        {
            get
            {
                return _maDonViTinh;
            }
            set
            {
                if (_maDonViTinh != value)
                {
                    _maDonViTinh = value;
                    _maDonViTinh = value; RaisePropertyChanged("MaDonViTinh");
                }
            }
        }

        private string _maNhomThucDon;

        public string MaNhomThucDon
        {
            get
            {
                return _maNhomThucDon;
            }
            set
            {
                if (_maNhomThucDon != value)
                {
                    _maNhomThucDon = value;
                    RaisePropertyChanged("MaNhomThucDon");
                }
            }
        }

        private ImageSource _hinhDaiDien;

        public ImageSource HinhDaiDien
        {
            get
            {
                return _hinhDaiDien;
            }
            set
            {
                if (_hinhDaiDien != value)
                {
                    _hinhDaiDien = value;
                    RaisePropertyChanged("HinhDaiDien");
                }
            }
        }

        private ImageSource _imageSource;

        public ImageSource ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                if (_imageSource != value)
                {
                    _imageSource = value;
                    RaisePropertyChanged("ImageSource");
                }
            }
        }

        private bool _isEnabledMaMon;

        public bool IsEnabledMaMon
        {
            get
            {
                return _isEnabledMaMon;
            }
            set
            {
                if (_isEnabledMaMon != value)
                {
                    _isEnabledMaMon = value;
                    RaisePropertyChanged("IsEnabledMaMon");
                }
            }
        }


        

        private string _tenNhomThucDon;

        public string TenNhomThucDon
        {
            get
            {
                return _tenNhomThucDon;
            }
            set
            {
                if (_tenNhomThucDon != value)
                {
                    _tenNhomThucDon = value;
                    RaisePropertyChanged("TenNhomThucDon");
                }
            }
        }

        private ThucDon _SelectedItem;

        public ThucDon SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                RaisePropertyChanged();
                if (SelectedItem != null)
                {
                    {
                        MaMon = SelectedItem.MaMon;
                        TenMon = SelectedItem.TenMon;
                        GiaBan = Convert.ToDouble(SelectedItem.GiaBan);
                        GhiChu = SelectedItem.GhiChu;
                        HinhDaiDien = ByteToImage(SelectedItem.HinhDaiDien);
                        ImageSource = ByteToImage(SelectedItem.HinhDaiDien);
                        SelectedMenuGroup = SelectedItem.NhomThucDon;
                        SelectedUnit = SelectedItem.DonViTinh;
                        SelectedMenuType = SelectedMenuGroup.LoaiThucDon;
                        IsEnabledMaMon = false;
                    }
                }
            }
        }

        private NhomThucDon _SelectedMenuGroup;

        public NhomThucDon SelectedMenuGroup
        {
            get => _SelectedMenuGroup;
            set
            {
                _SelectedMenuGroup = value;
                RaisePropertyChanged();
                if (SelectedMenuGroup != null)
                {
                    {
                        TenNhomThucDon = SelectedMenuGroup.TenNhomThucDon;
                    }
                }
            }
        }

        private LoaiThucDon _selectedMenuType;

        public LoaiThucDon SelectedMenuType
        {
            get
            {
                return _selectedMenuType;
            }
            set
            {
                _selectedMenuType = value;
                RaisePropertyChanged("SelectedMenuType");
                if (SelectedMenuType != null)
                {
                    if (SelectedMenuType.MaLoaiThucDon == "LTDDU")
                    {
                        TenHienThi_LoaiThucDon = "Loại đồ uống ( * )";
                        TenHienThi_ThucDon = "Tên đồ uống ( * )";
                        MaHienThi_ThucDon = "Mã đồ uống ( * )";
                    }
                    else if (SelectedMenuType.MaLoaiThucDon == "LTDMA")
                    {
                        TenHienThi_LoaiThucDon = "Loại món ăn ( * )";
                        TenHienThi_ThucDon = "Tên món ăn ( * )";
                        MaHienThi_ThucDon = "Mã món ăn ( * )";
                    }
                }
            }
        }

        private DonViTinh _SelectedUnit;

        public DonViTinh SelectedUnit
        {
            get => _SelectedUnit;
            set
            {
                _SelectedUnit = value;
                RaisePropertyChanged();
                if (SelectedUnit != null)
                {
                    {
                        TenDonViTinh = SelectedUnit.TenDonViTinh;
                    }
                }
            }
        }

        private string _tenHienThi_ThucDon;

        public string TenHienThi_ThucDon
        {
            get
            {
                return _tenHienThi_ThucDon;
            }
            set
            {
                if (_tenHienThi_ThucDon != value)
                {
                    _tenHienThi_ThucDon = value;
                    RaisePropertyChanged("TenHienThi_ThucDon");
                }
            }
        }

        private string _maHienThi_ThucDon;

        public string MaHienThi_ThucDon
        {
            get
            {
                return _maHienThi_ThucDon;
            }
            set
            {
                if (_maHienThi_ThucDon != value)
                {
                    _maHienThi_ThucDon = value;
                    RaisePropertyChanged("MaHienThi_ThucDon");
                }
            }
        }

        private string _tenHienThi_LoaiThucDon;

        public string TenHienThi_LoaiThucDon
        {
            get
            {
                return _tenHienThi_LoaiThucDon;
            }
            set
            {
                if (_tenHienThi_LoaiThucDon != value)
                {
                    _tenHienThi_LoaiThucDon = value;
                    RaisePropertyChanged("TenHienThi_LoaiThucDon");
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
                    _daXoa = value;
                    RaisePropertyChanged("DaXoa");
                }
            }
        }

        private ObservableCollection<ThucDon> _List;
        public ObservableCollection<ThucDon> List { get => _List; set { _List = value; RaisePropertyChanged("List"); } }

        private ObservableCollection<LoaiThucDon> _menuTypeList;
        public ObservableCollection<LoaiThucDon> MenuTypeList { get => _menuTypeList; set { _menuTypeList = value; RaisePropertyChanged("MenuTypeList"); } }

        private ObservableCollection<NhomThucDon> _MenuGroupList;
        public ObservableCollection<NhomThucDon> MenuGroupList { get => _MenuGroupList; set { _MenuGroupList = value; RaisePropertyChanged("MenuGroupList"); } }

        private ObservableCollection<DonViTinh> _UnitList;
        public ObservableCollection<DonViTinh> UnitList { get => _UnitList; set { _UnitList = value; RaisePropertyChanged("UnitList"); } }

        public string fileName_Image { get; set; }

        #endregion Properties

        #region Command Properties

        private ICommand addMenuCommand;
        public ICommand AddMenuCommand { get => addMenuCommand; set { addMenuCommand = value; RaisePropertyChanged(); } }

        private ICommand updateMenuCommand;
        public ICommand UpdateMenuCommand { get => updateMenuCommand; set { updateMenuCommand = value; RaisePropertyChanged(); } }

        private ICommand deleteMenuCommand;
        public ICommand DeleteMenuCommand { get => deleteMenuCommand; set { deleteMenuCommand = value; RaisePropertyChanged(); } }

        private ICommand createNewMenuCommand;
        public ICommand CreateNewMenuCommand { get => createNewMenuCommand; set { createNewMenuCommand = value; RaisePropertyChanged(); } }

        private ICommand openExplorerCommand;
        public ICommand OpenExplorerCommand { get => openExplorerCommand; set { openExplorerCommand = value; RaisePropertyChanged(); } }

        private ICommand deleteImageCommand;
        public ICommand DeleteImageCommand { get => deleteImageCommand; set { deleteImageCommand = value; RaisePropertyChanged(); } }

        #endregion Command Properties

        #region Constructor

        public MenuViewModel()
        {

            IsEnabledMaMon = true;

            MaHienThi_ThucDon = "Mã món ( * )";

            TenHienThi_ThucDon = "Tên món ( * )";

            TenHienThi_LoaiThucDon = "Loại thực đơn ( * )";

            LoadAll();

            AddMenuCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(MaMon) || string.IsNullOrEmpty(TenMon) || SelectedUnit == null || SelectedMenuType == null || SelectedMenuGroup == null)

                {
                    return false;
                }

                if (!isSymbolAndNumber(MaMon))
                {
                    return false;
                }
                if (DataProvider.Instance.Database.ThucDons.Where(x => x.MaMon == MaMon).Count() != 0)
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                AddMenu_Execute();
            });

            UpdateMenuCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(MaMon) || string.IsNullOrEmpty(TenMon) || SelectedUnit == null || SelectedMenuType == null || SelectedMenuGroup == null)

                {
                    return false;
                }

                if (!isSymbolAndNumber(MaMon))
                {
                    return false;
                }
                //var list = DataProvider.Instance.Database.ThucDons.Where(x => x.TenMon == TenMon && x.MaMon == MaMon && x.MaNhomThucDon == SelectedMenuGroup.MaNhomThucDon && x.MaDonViTinh == SelectedUnit.MaDonViTinh && x.GiaBan == (decimal)GiaBan && x.GhiChu == GhiChu).Count();
                //if (list != 0)
                //{
                //    return false;
                //}
                return true;
            }, (p) =>
            {
                UpdateMenu_Execute();
            });

            DeleteMenuCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null || SelectedMenuType == null || SelectedUnit == null || SelectedMenuGroup == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                DeleteMenu_Execute();
            });

            CreateNewMenuCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null && SelectedMenuType == null && SelectedUnit == null && SelectedMenuGroup == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                ClearTextBox();
            });

            OpenExplorerCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(MaMon) || string.IsNullOrEmpty(TenMon) || GiaBan == 0)

                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                var dlg = new OpenFileDialog();

                dlg.DefaultExt = ".png";

                dlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";

                if (dlg.ShowDialog() == true)
                {
                    fileName_Image = dlg.FileName;

                    if (fileName_Image == null)
                    {
                        return;
                    }
                    try
                    {
                        using (FileStream fs = new FileStream(fileName_Image, FileMode.Open, FileAccess.Read))
                        {
                            string convert;

                            convert = ImageToByte(fs);

                            byte[] imgStr = Convert.FromBase64String(convert);

                            ImageSource = ByteToImage(imgStr);

                            return;
                        }
                    }
                    catch (IOException ex)
                    {
                        throw ex;
                    }
                }
            });

            DeleteImageCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem.HinhDaiDien == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                var res = DataProvider.Instance.Database.ThucDons.SingleOrDefault(x => x.MaMon == SelectedItem.MaMon);
                if (res != null)
                {
                    if (res.HinhDaiDien != null && ImageSource != null)
                    {
                        res.HinhDaiDien = null;
                        ImageSource = null;
                    }
                    DataProvider.Instance.Database.SaveChanges();
                    ClearTextBox();
                }
            });
        }

        private void LoadAll()
        {
            //Get list and display on listview
            GetListOfMenu();

            //Get list and display on combobox of menu group
            GetListOfMenuGroup();

            //Get list and display on comboxbox of menu type
            GetListOfMenuType();

            //Get list and dis
            GetListOfUnit();
        }

        #endregion Constructor

        #region Methods

        private bool GetListOfMenuGroup()
        {

            MenuGroupList = new ObservableCollection<NhomThucDon>(DataProvider.Instance.Database.NhomThucDons.Where(x => x.DaXoa == DaXoa).ToList());
            return true;
        }

        private bool GetListOfMenuType()
        {

            MenuTypeList = new ObservableCollection<LoaiThucDon>(DataProvider.Instance.Database.LoaiThucDons.Where(x => x.DaXoa == DaXoa).ToList());
            return true;
        }

        private bool GetListOfUnit()
        {
            UnitList = new ObservableCollection<DonViTinh>(DataProvider.Instance.Database.DonViTinhs.Where(x => x.DaXoa == DaXoa).ToList());
            return true;

        }

        private void GetListOfMenu()
        {
            List = new ObservableCollection<ThucDon>(DataProvider.Instance.Database.ThucDons.Where(x=>x.DaXoa == DaXoa).ToList());
        }

        private void AddMenu_Execute()
        { 
            ThucDon td = new ThucDon();

            UserMessage msg = new UserMessage();
 
            try
            {
                if (td != null && fileName_Image != null)
                {

                    FileStream fs = new FileStream(fileName_Image, FileMode.Open, FileAccess.Read);
                    string tmp;
                    tmp = ImageToByte(fs);
                    td.HinhDaiDien = Convert.FromBase64String(tmp);
                }
                else
                {
                    if (fileName_Image == null)
                    {
                        //Nếu không thêm ảnh thì sẽ lấy ảnh mặc định
                        fileName_Image = @"..\Imagines\defaultimage.jpg";
                        FileStream fs = new FileStream(fileName_Image, FileMode.Open, FileAccess.Read);
                        string tmp;
                        tmp = ImageToByte(fs);
                        td.HinhDaiDien = Convert.FromBase64String(tmp);
                    }
                }
                var menu = new ThucDon()
                {
                    MaMon = MaMon,
                    TenMon = TenMon,
                    GiaBan = Convert.ToDecimal(GiaBan),
                    HinhDaiDien = td.HinhDaiDien,
                    GhiChu = GhiChu,
                    MaDonViTinh = SelectedUnit.MaDonViTinh,
                    MaNhomThucDon = SelectedMenuGroup.MaNhomThucDon,
                    DaXoa = DaXoa,
                };
                if (menu.GhiChu == "")
                {
                    menu.GhiChu = @"Không có";
                }

                DataProvider.Instance.Database.ThucDons.Add(menu);
                DataProvider.Instance.Database.SaveChanges();
                List.Add(menu);
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

        private void UpdateMenu_Execute()
        {

            ThucDon td = new ThucDon();

            UserMessage msg = new UserMessage();

            try
            {
                if (ImageSource != null)
                {
                    
                    if (!string.IsNullOrEmpty(fileName_Image))
                    {
                        FileStream fs = new FileStream(fileName_Image, FileMode.Open, FileAccess.Read);
                        string tmp = ImageToByte(fs);
                        td.HinhDaiDien = Convert.FromBase64String(tmp);
                    }   
                }
                var res = DataProvider.Instance.Database.ThucDons.SingleOrDefault(x => x.MaMon == SelectedItem.MaMon);
                if (res != null)
                {

                    res.MaMon = MaMon;
                    res.TenMon = TenMon;
                    res.GiaBan = Convert.ToDecimal(GiaBan);
                    res.GhiChu = GhiChu;
                    res.HinhDaiDien = td.HinhDaiDien;
                    res.MaNhomThucDon = SelectedMenuGroup.MaNhomThucDon;
                    res.MaDonViTinh = SelectedUnit.MaDonViTinh;
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

        private void DeleteMenu_Execute()
        {
            UserMessage msg = new UserMessage();
            if (ConfirmDialog("Bạn có chắc chắn muốn xoá thực đơn <<" + SelectedItem.TenMon + ">> không ? "))
            {
                var menu = DataProvider.Instance.Database.ThucDons.SingleOrDefault(x => x.MaMon == SelectedItem.MaMon);
                List.Remove(menu);
                menu.DaXoa = true;
                DataProvider.Instance.Database.SaveChanges();
                RaisePropertyChanged("List");
                msg.Message = "Dữ liệu đã xoá thành công";

            }

            Messenger.Default.Send<UserMessage>(msg);
            ClearTextBox();
        }

        public bool ClearTextBox()
        {
            MaMon = string.Empty;
            TenMon = string.Empty;
            GhiChu = string.Empty;
            GiaBan = 0;
            ImageSource = null;
            HinhDaiDien = null;
            SelectedUnit = null;
            SelectedItem = null;
            SelectedMenuType = null;
            SelectedMenuGroup = null;
            IsEnabledMaMon = true;
            return true;
        }

        public ImageSource ByteToImage(byte[] hinhDaiDien)
        {
            BitmapImage _bigImg = new BitmapImage();

            if (hinhDaiDien != null && hinhDaiDien.Length > 0)
            {
                MemoryStream _ms = new MemoryStream(hinhDaiDien);
                _bigImg.BeginInit();
                _bigImg.StreamSource = _ms;
                _bigImg.EndInit();
            }
            ImageSource _imgSrc = _bigImg as ImageSource;
            return _imgSrc;
        }

        public string ImageToByte(FileStream fs)
        {
            byte[] imgBytes = new byte[fs.Length];
            fs.Read(imgBytes, 0, Convert.ToInt32(fs.Length));
            string encodeData = Convert.ToBase64String(imgBytes, Base64FormattingOptions.InsertLineBreaks);

            return encodeData;
        }

        #endregion Methods
    }
}