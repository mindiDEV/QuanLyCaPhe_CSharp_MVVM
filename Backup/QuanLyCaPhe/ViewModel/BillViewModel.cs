using DevExpress.XtraReports.UI;
using QuanLyCaPhe.ClassSupport;
using QuanLyCaPhe.Model;
using QuanLyCaPhe.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyCaPhe.ViewModel
{
    public class BillViewModel : BaseViewModel
    {
        #region Properties

        private ObservableCollection<HoaDon_DTO> _List;
        public ObservableCollection<HoaDon_DTO> List { get => _List; set { _List = value; RaisePropertyChanged(); } }

        private ObservableCollection<LoaiThucDon> _MenuTypeList;
        public ObservableCollection<LoaiThucDon> MenuTypeList { get => _MenuTypeList; set { _MenuTypeList = value; RaisePropertyChanged(); } }

        private ObservableCollection<ThucDon> _MenuList;
        public ObservableCollection<ThucDon> MenuList { get => _MenuList; set { _MenuList = value; RaisePropertyChanged(); } }

        private ObservableCollection<ChiTietHoaDon> _BillDetaiList;
        public ObservableCollection<ChiTietHoaDon> BillDetaiList { get => _BillDetaiList; set { _BillDetaiList = value; RaisePropertyChanged(); } }

        private ObservableCollection<LoaiThucDon> _LoaiMonAn;
        public ObservableCollection<LoaiThucDon> LoaiMonAn { get => _LoaiMonAn; set { _LoaiMonAn = value; RaisePropertyChanged(); } }

        private ObservableCollection<LoaiKhachHang> _CustomerTypeList;
        public ObservableCollection<LoaiKhachHang> CustomerTypeList { get => _CustomerTypeList; set { _CustomerTypeList = value; RaisePropertyChanged(); } }

        private ObservableCollection<KhachHang> _CustomerNameList;
        public ObservableCollection<KhachHang> CustomerNameList { get => _CustomerNameList; set { _CustomerNameList = value; RaisePropertyChanged(); } }

        private ObservableCollection<KhuyenMai_DTO> _PromotionDetailList;
        public ObservableCollection<KhuyenMai_DTO> PromotionDetailList { get => _PromotionDetailList; set { _PromotionDetailList = value; RaisePropertyChanged(); } }

        private ObservableCollection<KhuyenMai_DTO> _PromotionNameList;
        public ObservableCollection<KhuyenMai_DTO> PromotionNameList { get => _PromotionNameList; set { _PromotionNameList = value; RaisePropertyChanged(); } }

        private ObservableCollection<KhuyenMai_DTO> _FavoriteMenuList;
        public ObservableCollection<KhuyenMai_DTO> FavoriteMenuList { get => _FavoriteMenuList; set { _FavoriteMenuList = value; RaisePropertyChanged(); } }

        private ObservableCollection<KhuyenMai_DTO> _listKhuyenMaiGiamGia;
        public ObservableCollection<KhuyenMai_DTO> ListKhuyenMaiGiamGia { get => _listKhuyenMaiGiamGia; set { _listKhuyenMaiGiamGia = value; RaisePropertyChanged(); } }



        private string _tenLoaiKhachHang;

        private string _tenMon;

        private byte[] _hinhDaiDien;

        private double _giaBan;

        private int _maKhachHang;

        private string _tenLoaiThucDon;

        private string _maLoaiThucDon;

        private string _maMon;

        private double? _thanhToan;

        private bool _ComboBoxEnabled;
        public bool ComboBoxEnabled
        {
            get
            {
                return _ComboBoxEnabled;
            }
            set
            {
                _ComboBoxEnabled = value;
                RaisePropertyChanged("ComboBoxEnabled");
            }
        }
        
        public string TenLoaiKhachHang
        {
            get
                  => _tenLoaiKhachHang;
            set
            {
                if (_tenLoaiKhachHang != value)
                {
                    _tenLoaiKhachHang = value;
                    RaisePropertyChanged("TenLoaiKhachHang");
                }
            }
        }

        private string _InputSearchText;

        public string InputSearchText
        {
            get
                  => _InputSearchText;
            set
            {
                if (_InputSearchText != value)
                {
                    _InputSearchText = value;
                    RaisePropertyChanged("InputSearchText");
                }
            }
        }

        private double? _thanhTien;

        public double? ThanhTien
        {
            get => _thanhTien;
            set
            {
                if (_thanhTien != value)
                {
                    _thanhTien = value;
                    RaisePropertyChanged("ThanhTien");
                }
            }
        }

        public double? ThanhToan
        {
            get => _thanhToan;
            set
            {
                if (_thanhToan != value)
                {
                    _thanhToan = value;
                    RaisePropertyChanged("ThanhToan");
                }
            }
        }

        private string _sanPhamTang;

        public string SanPhamTang
        {
            get => _sanPhamTang;
            set
            {
                if (_sanPhamTang != value)
                {
                    _sanPhamTang = value;
                    RaisePropertyChanged("SanPhamTang");
                }
            }
        }

        private double? _tongTien;

        public double? TongTien
        {
            get
            {
                return _tongTien;
            }
            set
            {
                _tongTien = value;
                RaisePropertyChanged("TongTien");
            }
        }

        private string _khuyenMai;

        public string KhuyenMai
        {
            get
            {
                return _khuyenMai;
            }
            set
            {
                _khuyenMai = value;
                RaisePropertyChanged();
            }
        }

        private double? _tienKhachTra = 0;

        public double? TienKhachTra
        {
            get
            {
                return _tienKhachTra;
            }
            set
            {
                if(_tienKhachTra!=value)
                {
                    _tienKhachTra = value;
                    RaisePropertyChanged("TienKhachTra");
                }
                


            }
        }

        private double? _tienThua;

        public double? TienThua
        {
            get
            {
                return _tienThua;
            }
            set
            {
                _tienThua = value;
                RaisePropertyChanged("TienThua");
            }
        }

        public double GiaBan
        {
            get => _giaBan;
            set
            {
                if (_giaBan != value)
                {
                    _giaBan = value;
                    RaisePropertyChanged("GiaBan");
                }
            }
        }

        private bool _daXoa = false;
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


        public string TenMon
        {
            get => _tenMon;
            set
            {
                if (_tenMon != value)
                {
                    _tenMon = value;
                    RaisePropertyChanged("TenMon");
                }
            }
        }

        public string MaMon
        {
            get => _maMon;
            set
            {
                if (_maMon != value)
                {
                    _maMon = value;
                    RaisePropertyChanged("MaMon");
                }
            }
        }

        public byte[] HinhDaiDien
        {
            get => _hinhDaiDien;
            set
            {
                if (_hinhDaiDien != value)
                {
                    _hinhDaiDien = value;
                    RaisePropertyChanged("HinhDaiDien");
                }
            }
        }

        public string TenLoaiThucDon
        {
            get => _tenLoaiThucDon;
            set
            {
                if (_tenLoaiThucDon != value)
                {
                    _tenLoaiThucDon = value;
                    RaisePropertyChanged("TenLoaiThucDon");
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

        public string MaLoaiThucDon
        {
            get => _maLoaiThucDon;
            set
            {
                if (_maLoaiThucDon != value)
                {
                    _maLoaiThucDon = value;
                    RaisePropertyChanged("MaLoaiThucDon");
                }
            }
        }

        private string _tenCTKM;

        public string TenCTKM
        {
            get => _tenCTKM;
            set
            {
                if (_tenCTKM != value)
                {
                    _tenCTKM = value;
                    RaisePropertyChanged("TenCTKM");
                }
            }
        }

        private string _maCTKM;

        public string MaCTKM
        {
            get => _maCTKM;
            set
            {
                if (_maCTKM != value)
                {
                    _maCTKM = value;
                    RaisePropertyChanged("MaCTKM");
                }
            }
        }

        private double _giamGia;

        public double GiamGia
        {
            get => _giamGia;
            set
            {
                if (_giamGia != value)
                {
                    _giamGia = value;
                    RaisePropertyChanged("GiamGia");
                }
            }
        }

        private int _SelectedIndex;

        public int SelectedIntex
        {
            get => _SelectedIndex;
            set
            {
                _SelectedIndex = value;
                RaisePropertyChanged();
            }
        }

        private bool _isEnabledCustomerType = false;
        public bool IsEnabledCustomerType
        {
            get => _isEnabledCustomerType;
            set
            {
                _isEnabledCustomerType = value;
                RaisePropertyChanged();
            }
        }

        private bool _isEnabledCustomerName = false;
        public bool IsEnabledCustomerName
        {
            get => _isEnabledCustomerName;
            set
            {
                _isEnabledCustomerName = value;
                RaisePropertyChanged();
            }
        }

        private bool _isEnabledDiscountByQRCode = false;
        public bool IsEnabledDiscountByQRCode
        {
            get => _isEnabledDiscountByQRCode;
            set
            {
                _isEnabledDiscountByQRCode = value;
                RaisePropertyChanged();
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

                KhuyenMaiPhanTram(SelectedCustomerType);
            }
        }

        private KhachHang _SelectedCustomerName;

        public KhachHang SelectedCustomerName
        {
            get => _SelectedCustomerName;
            set
            {
                _SelectedCustomerName = value;
                RaisePropertyChanged();
                if (_SelectedCustomerName != null)
                {
                    MaKhachHang = SelectedCustomerName.MaKhachHang;
                }
            }
        }

        private int _SelectedIndexCustomerType;

        public int SelectedIndexCustomerType
        {
            get => _SelectedIndexCustomerType;
            set
            {
                _SelectedIndexCustomerType = value;
                RaisePropertyChanged();
            }
        }

        private int _SelectedIndexCustomerName;

        public int SelectedIndexCustomerName
        {
            get => _SelectedIndexCustomerName;
            set
            {
                _SelectedIndexCustomerName = value;
                RaisePropertyChanged();
            }
        }

        private ChuongTrinhKhuyenMai _SelectedPromotionName;

        public ChuongTrinhKhuyenMai SelectedPromotionName
        {
            get => _SelectedPromotionName;
            set
            {
                _SelectedPromotionName = value;
                RaisePropertyChanged();
            }
        }

        private string TextBoxSearch;


        #endregion Properties

        #region Command Properties

        private ICommand _btnMenuClick;
        public ICommand btnMenuClick { get => _btnMenuClick; set => _btnMenuClick = value; }

        private ICommand _removeMenuCommand;
        public ICommand RemoveMenuCommand { get => _removeMenuCommand; set => _removeMenuCommand = value; }

        private ICommand _payCommand;
        public ICommand PayCommand { get => _payCommand; set => _payCommand = value; }

        private ICommand _reduceMenuCommand;
        public ICommand ReduceMenuCommand { get => _reduceMenuCommand; set => _reduceMenuCommand = value; }

        private ICommand _cancelMenuCommand;
        public ICommand CancelMenuCommand { get => _cancelMenuCommand; set => _cancelMenuCommand = value; }

        private ICommand _FoodListCommand;
        public ICommand FoodListCommand { get => _FoodListCommand; set => _FoodListCommand = value; }

        private ICommand _DrinkListCommand;
        public ICommand DrinkListCommand { get => _DrinkListCommand; set => _DrinkListCommand = value; }

        private ICommand _TextChangedMoneyCustomerCommand;
        public ICommand TextChangedMoneyCustomerCommand { get => _TextChangedMoneyCustomerCommand; set => _TextChangedMoneyCustomerCommand = value; }

        private ICommand _IncreaseNumberCommand;
        public ICommand IncreaseNumberCommand { get => _IncreaseNumberCommand; set => _IncreaseNumberCommand = value; }

        private ICommand _GetMenuCode_IncreaseNumberCommand;
        public ICommand GetMenuCode_IncreaseNumberCommand { get => _GetMenuCode_IncreaseNumberCommand; set => _GetMenuCode_IncreaseNumberCommand = value; }

        private ICommand _TextPayCommand;
        public ICommand TextPayCommand { get => _TextPayCommand; set => _TextPayCommand = value; }

        private ICommand _TextTotalCommand;
        public ICommand TextTotalCommand { get => _TextTotalCommand; set => _TextTotalCommand = value; }

        private ICommand _TextChangedSearchCommand;
        public ICommand TextChangedSearchCommand { get => _TextChangedSearchCommand; set => _TextChangedSearchCommand = value; }

        private ICommand _FavoriteMenuCommand;
        public ICommand FavoriteMenuCommand { get => _FavoriteMenuCommand; set => _FavoriteMenuCommand = value; }

        private ICommand _DiscountByQRCodeCommand;
        public ICommand DiscountByQRCodeCommand { get => _DiscountByQRCodeCommand; set => _DiscountByQRCodeCommand = value; }


        #endregion Command Properties

        #region Constructor

        public BillViewModel()
        {
            GetListPromotionName();

            GetListCustomerType();

            GetDrinkListWhenStartup();

            ListKhuyenMaiGiamGia = new ObservableCollection<KhuyenMai_DTO>();


            //Init list of bill
            List = new ObservableCollection<HoaDon_DTO>();

            PromotionDetailList = new ObservableCollection<KhuyenMai_DTO>();


            FoodListCommand = new RelayCommand<object>((p) =>
            {
                return true;
            },
            (p) =>
            {
                MenuList.Clear();
                MenuList = new ObservableCollection<ThucDon>(DataProvider.Instance.Database.ThucDons.Where(x => x.NhomThucDon.LoaiThucDon.MaLoaiThucDon == "LTDMA"));
            });

            DrinkListCommand = new RelayCommand<object>((p) =>
            {
                return true;
            },
            (p) =>
            {
                MenuList.Clear();
                MenuList = new ObservableCollection<ThucDon>(DataProvider.Instance.Database.ThucDons.Where(x => x.NhomThucDon.LoaiThucDon.MaLoaiThucDon == "LTDDU"));
            });

            btnMenuClick = new RelayCommand<object>((p) =>
            {                     
                    
                return true;
            },
            (p) =>
            {
                ThucDon thucDon = p as ThucDon;
                HoaDon_DTO bill_DTO = new HoaDon_DTO();
                if (List.Where(x => x.MaMon == thucDon.MaMon).Count() == 0)
                {
                    KhuyenMai_DTO khuyenMai_DTO = new KhuyenMai_DTO();
                    
                    bill_DTO.MaMon = thucDon.MaMon;
                    bill_DTO.TenMon = thucDon.TenMon;
                    bill_DTO.GiaBan = Convert.ToDouble(thucDon.GiaBan);
                    bill_DTO.SoLuong = 1;
                    bill_DTO.ThanhTien = Convert.ToDouble(thucDon.GiaBan);
                    List.Add(bill_DTO);  

                    TotalDiscountByQRCode();           

                }
                //Khi số lượng món ăn lớn hơn 1
                else
                {
                   bill_DTO = List.Where(x => x.MaMon == thucDon.MaMon).SingleOrDefault();

                    if (List.Where(x => x.MaMon == bill_DTO.MaMon).Count() > 0)
                    {

                        bill_DTO.SoLuong++;

                        bill_DTO.ThanhTien = bill_DTO.SoLuong * bill_DTO.GiaBan;

                    }
                }             

                KhuyenMaiPhanTram(bill_DTO);

                KhuyenMaiSanPham(bill_DTO);
                
                ThanhToanCuoiCung();

                if (!string.IsNullOrEmpty(TienKhachTra + ""))
                {
                    TienThua = TienKhachTra - ThanhToan;
                }

                EnabledCustomerVIP(SelectedCustomerType);

                EnabledButtonDiscountByQRCode();

            });

            //Xoá món ăn khỏi list
            RemoveMenuCommand = new RelayCommand<object>((p) =>
            {
                return true;
            },
            (p) =>
            {
                var hoaDon = p as HoaDon_DTO;
                //Kiểm tra xem có mã món đó trong danh sách order hay không

                if (ListKhuyenMaiGiamGia.Count > 0)
                {
                    if (ListKhuyenMaiGiamGia.Where(x => x.MaMon == hoaDon.MaMon).SingleOrDefault() != null)
                    {
                        var tmp = ListKhuyenMaiGiamGia.Where(x => x.MaMon == hoaDon.MaMon).SingleOrDefault();
                        ListKhuyenMaiGiamGia.Remove(tmp);

                    }
        
                }

                //Nếu có sản phẩm 
                if (List.Where(x => x.MaMon == hoaDon.MaMon).SingleOrDefault() != null)
                {
                    //Nếu như sản phẩm mua có tặng sản phẩm khuyến mãi
                    if (DataProvider.Instance.Database.ChiTietKhuyenMais.Where(x => x.MaMon == hoaDon.MaMon).ToList().Count > 0)
                    {

                        //Nếu có sản phẩm tặng
                        if (DataProvider.Instance.Database.ChiTietKhuyenMais.Where(x => x.MaMon == hoaDon.MaMon).SingleOrDefault().SanPhamTang != null)
                        {
                            //Lấy ra mã sản phẩm tặng
                            var maSanPhamTang = DataProvider.Instance.Database.ChiTietKhuyenMais.Where(x => x.MaMon == hoaDon.MaMon).SingleOrDefault().SanPhamTang;
                            KhuyenMai_DTO khuyenMai = PromotionDetailList.Where(x => x.MaMon == maSanPhamTang).SingleOrDefault();
                            if (khuyenMai != null)
                            {
                                if (List.Where(x => x.MaMon == hoaDon.MaMon).SingleOrDefault().SoLuong >= khuyenMai.SoLuong)
                                {
                                    List.Remove(hoaDon);
                                    PromotionDetailList.Remove(khuyenMai);
                                   
                                }
                            }

                        }

                    }
                    //Nếu sản phẩm mua không có sản phẩm khuyến mãi
                    else
                    {
                        var sanPhamMua = List.Where(x => x.MaMon == hoaDon.MaMon).SingleOrDefault();
                        List.Remove(sanPhamMua);

                        

                    }
                }
                List.Remove(hoaDon);

                EnabledCustomerVIP(SelectedCustomerType);

                EnabledButtonDiscountByQRCode();

                ThanhToanCuoiCung();

                if (List.Count() == 0)
                {
                    ClearValue();
                }


            });

            //Giảm số lượng món ăn
            ReduceMenuCommand = new RelayCommand<object>((p) =>
            {
                return true;
            },
            (p) =>
            {
                var thucDon = p as HoaDon_DTO;

                HoaDon_DTO bill_DTO = List.Where(x => x.MaMon == thucDon.MaMon).SingleOrDefault();

                if (bill_DTO.SoLuong >= 2)
                {
                    bill_DTO.SoLuong--;

                    bill_DTO.ThanhTien = bill_DTO.SoLuong * bill_DTO.GiaBan;

                    //Tổng tiền khi thay đổi số lượng món ăn hoặc đồ uống
                    KhuyenMaiPhanTram(bill_DTO);

                    KhuyenMaiSanPham(bill_DTO);

                    ThanhToanCuoiCung();  

                }

            });

            //Thanh toán
            PayCommand = new RelayCommand<object>((p) =>
            {
                if (Convert.ToDouble(TienKhachTra) < ThanhToan || SelectedCustomerName == null||TongTien==0||string.IsNullOrEmpty(TongTien+"")) 
                {
                    return false;
                }
                
                return true;
            }, (p) =>
             {
                
                 HoaDon hd = new HoaDon();
                 string _maHoaDon = EncodeIDBill(hd.MaHoaDon);
                 hd.MaHoaDon = _maHoaDon;
                 hd.NgayXuatHoaDon = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                 hd.MaNhanVien = LoginViewModel.MaNhanVien;
                 if (SelectedCustomerType.MaLoaiKhachHang == "LKHTH" || SelectedCustomerType.MaLoaiKhachHang == "LKHVIP" || SelectedCustomerType.MaLoaiKhachHang == "LKHVIPP")
                 {
                     hd.MaKhachHang = SelectedCustomerName.MaKhachHang;
                 } 
                
                 hd.TienKhachTra = Convert.ToDecimal(TienKhachTra);
                 hd.GiamGia = Convert.ToDecimal(GiamGia);
                 hd.TienThua = Convert.ToDecimal(TienThua);
                 hd.TongHoaDon = Convert.ToDecimal(ThanhToan);

                 DataProvider.Instance.Database.HoaDons.Add(hd);
                 DataProvider.Instance.Database.SaveChanges();
                 BillDetaiList = new ObservableCollection<ChiTietHoaDon>();
                 foreach (var item in List)
                 {
                     ChiTietHoaDon cthd = new ChiTietHoaDon();
                     cthd.MaHoaDon = hd.MaHoaDon;
                     cthd.MaMon = item.MaMon;
                     
                     cthd.SoLuong = item.SoLuong;
                     cthd.TongCong = Convert.ToDecimal(item.ThanhTien);
                     BillDetaiList.Add(cthd);
                     ClearValue();
                 }
                 DataProvider.Instance.Database.ChiTietHoaDons.AddRange(BillDetaiList);
                 DataProvider.Instance.Database.SaveChanges();
                 HoaDonReport report = new HoaDonReport();
                 report.Parameters["MaHoaDon"].Value = hd.MaHoaDon;
                 report.ShowPreviewDialog();


             });

            //Huỷ bỏ hoá đơn
            CancelMenuCommand = new RelayCommand<object>((p) =>
            {
                if (List.Count==0)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {

                ClearValue();

            });

            TextChangedMoneyCustomerCommand = new RelayCommand<TextBox>((p) =>
            {
                return true;
            }, (p) =>
            {
                Regex regex = new Regex("[^0-9]+");
                bool handle = regex.IsMatch(p.Text);
                if (handle)
                {
                    StringBuilder dd = new StringBuilder();
                    int i = -1;
                    int cursor = -1;
                    foreach (char item in p.Text)
                    {
                        i++;
                        if (char.IsDigit(item))
                            dd.Append(item);
                        else if (cursor == -1)
                            cursor = i;
                    }
                    p.Text = dd.ToString();

                    if (i == -1)
                        p.SelectionStart = p.Text.Length;
                    else
                        p.SelectionStart = cursor;
                }
                else
                {
                    try
                    {
                       
                        TienThua = Convert.ToDouble(p.Text) - ThanhToan;
                    }
                    catch
                    {
                        p.Text = null;
                        return;
                    }
                }
            });

            IncreaseNumberCommand = new RelayCommand<TextBox>((p) =>
            {
                return true;
            }, (p) =>
            {
                if (IsNumber(p.Text) != -1)
                {
                    p.Text = p.Text.Remove(IsNumber(p.Text), 1);
                    if (p.Text == "")
                    {
                        p.Text = "1";
                    }
                }
                else if (p.Text != "" && p.Text != "0")
                {
                    _countIncrease = int.Parse(p.Text);
                }
                else
                {
                    p.Text = "1";

                }
                var getDataContextOfMenu = p.DataContext;

                IncreaseNumber_Handmade((getDataContextOfMenu as HoaDon_DTO).MaMon, _countIncrease);
            });

            TextPayCommand = new RelayCommand<TextBox>((p) =>
            {
                return true;
            },
            (p) =>
            {
                try
                {
                    string strTemp = p.Text;
                    if (String.IsNullOrEmpty(strTemp))
                    {
                        return;
                    }

                    double _tienThanhToan = double.Parse(p.Text.Trim(','));
                    p.Text = _tienThanhToan.ToString("0,00.## VND");

                }
                catch
                {

                }
            });

            TextTotalCommand = new RelayCommand<TextBox>((p) =>
            {
                return true;
            },
            (p) =>
            {
                try
                {
                    string strTemp = p.Text;
                    if (String.IsNullOrEmpty(strTemp))
                    {
                        return;
                    }

                    double _tongTien = double.Parse(p.Text.Trim(','));
                    p.Text = _tongTien.ToString("0,00.## VND");

                }
                catch
                {

                }
            });


            TextChangedSearchCommand = new RelayCommand<TextBox>((p) =>
            {
                TextBoxSearch = p.Text;
                return true;
            },
            (p) =>
            {
                p.TextChanged += new TextChangedEventHandler(AutoCompleteSearch);

            });

            FavoriteMenuCommand = new RelayCommand<object>((p) =>
            {

                return true;
            },
            (p) =>
            {
                HienThiSanPhamYeuThich();

            });

            
            DiscountByQRCodeCommand = new RelayCommand<object>((p) =>
            {
                
                return true;
            },
            (p) =>
            {

                ShowScanQRCodeView();


            });
        }


        #endregion Constructor

        #region Methods

        private int _countIncrease = 0;
        private void AutoCompleteSearch(object sender, TextChangedEventArgs e)
        {

            e.Handled = true;
            string query = (sender as TextBox).Text;
            ObservableCollection<ThucDon> filter = new ObservableCollection<ThucDon>(DataProvider.Instance.Database.ThucDons.Where(x => x.TenMon.Contains(query)).ToList());
            foreach (var item in filter)
            {
                if (item.Equals(query))
                {
                    TextBoxSearch = query;
                }

            }
            MenuList = filter;
            if (query == string.Empty)
            {
                filter.Clear();
                MenuList = new ObservableCollection<ThucDon>(DataProvider.Instance.Database.ThucDons.Where(x => x.NhomThucDon.LoaiThucDon.MaLoaiThucDon == "LTDDU"));
            }

        }

        private void KhuyenMaiSanPham(HoaDon_DTO hoaDon)
        {
            
            ThucDon SanPhamTang;
            ChiTietKhuyenMai chiTietKM;
            KhuyenMai_DTO khuyenMai_DTO;
            //Lấy danh sách các chương trình khuyến mãi nằm trong ngày bắt đầu và kết thúc
            List<ChuongTrinhKhuyenMai> ListChuongTrinhKM = new List<ChuongTrinhKhuyenMai>(DataProvider.Instance.Database.ChuongTrinhKhuyenMais.Where(x => x.NgayBDKM <= DateTime.Now && x.NgayKTKM >= DateTime.Now));
            foreach (var itemChuongTrinh in ListChuongTrinhKM)
            {
                //Lấy mã của từng chương trình khuyến mãi
                List<ChiTietKhuyenMai> ListChiTietKM = new List<ChiTietKhuyenMai>(DataProvider.Instance.Database.ChiTietKhuyenMais.Where(x => x.MaCTKM == itemChuongTrinh.MaCTKM));

                foreach (var itemChiTietKM in ListChiTietKM)
                {
                    //Lấy mã sản phẩm mua có tặng kèm sản phẩm khuyến mãi
                    chiTietKM = DataProvider.Instance.Database.ChiTietKhuyenMais.Where(x => x.MaMon == hoaDon.MaMon).SingleOrDefault();
                    if (chiTietKM != null)

                    {
                        //Kiểm tra điều kiện tặng
                        if (chiTietKM.DieuKien <= hoaDon.SoLuong)
                        {

                            KhuyenMai_DTO isCheck = PromotionDetailList.Where(x => x.MaMon == chiTietKM.SanPhamTang).SingleOrDefault();
                            //Sản phẩm tặng 1 sản phẩm
                            if (isCheck == null)
                            {
                                khuyenMai_DTO = new KhuyenMai_DTO();

                                SanPhamTang = DataProvider.Instance.Database.ThucDons.Where(x => x.MaMon == chiTietKM.SanPhamTang).SingleOrDefault();

                                if(SanPhamTang==null)
                                {
                                    return;
                                }

                                int? _soLuong = hoaDon.SoLuong / chiTietKM.DieuKien;

                                khuyenMai_DTO = ConvertThucDon_KhuyenMai(SanPhamTang, chiTietKM.MaMon, (int)_soLuong);   
                                
                                PromotionDetailList.Add(khuyenMai_DTO);

                                double sumThanhTien = 0;

                                foreach (var item in PromotionDetailList)
                                {
                                    
                                    sumThanhTien += item.ThanhTien;
                                }

                                GiamGia = sumThanhTien;
                                
                            }
                            else
                            {

                                //Nhiều sản phẩm tặng
                                SanPhamTang = DataProvider.Instance.Database.ThucDons.Where(x => x.MaMon == chiTietKM.SanPhamTang).SingleOrDefault();

                                int? _soLuong = hoaDon.SoLuong / chiTietKM.DieuKien;

                                khuyenMai_DTO = ConvertThucDon_KhuyenMai(SanPhamTang, chiTietKM.MaMon, (int)_soLuong);

                                PromotionDetailList.Where(x => x.MaMon == chiTietKM.SanPhamTang).SingleOrDefault().SoLuong = (int)_soLuong;

                                PromotionDetailList.Where(x => x.MaMon == chiTietKM.SanPhamTang).SingleOrDefault().ThanhTien = khuyenMai_DTO.ThanhTien;

                                double sumThanhTien = 0;

                                foreach (var item in PromotionDetailList)
                                {
                                    sumThanhTien += item.ThanhTien;
                                }

                                GiamGia = sumThanhTien;

                            }
                        }
                        else
                        {
                            //Xoá sản phẩm theo button reduce
                            KhuyenMai_DTO isCheck = PromotionDetailList.Where(x => x.MaMon == chiTietKM.SanPhamTang).SingleOrDefault();
                            if (isCheck != null)
                            {
                                KhuyenMai_DTO _sanPhamXoa = PromotionDetailList.Where(x => x.SanPhamTang == isCheck.SanPhamTang).SingleOrDefault();
                                PromotionDetailList.Remove(_sanPhamXoa);
                            }

                        }


                    }

                }
            }
        }
       
        private void KhuyenMaiPhanTram(HoaDon_DTO hoaDon)
        {
            ChiTietKhuyenMai _chiTietKM = new ChiTietKhuyenMai();
            
            List<ChuongTrinhKhuyenMai> danhSachCTKM = new List<ChuongTrinhKhuyenMai>(DataProvider.Instance.Database.ChuongTrinhKhuyenMais.Where(x => x.NgayBDKM <= DateTime.Now && x.NgayKTKM >= DateTime.Now).ToList());
            foreach(var itemCTKM in danhSachCTKM)
            {
                List<ChiTietKhuyenMai> danhSachChiTietKM = new List<ChiTietKhuyenMai>(DataProvider.Instance.Database.ChiTietKhuyenMais).ToList();
                foreach(var itemChiTietKM in danhSachChiTietKM)
                {
                    if(itemChiTietKM.MaMon == hoaDon.MaMon)
                    {
                        //_flag = true;
                        _chiTietKM = itemChiTietKM;
                        break;
                       
                    }
                }
                if(_chiTietKM !=null)
                {
                    if(_chiTietKM.DieuKien <= hoaDon.SoLuong)
                    {
                        int _tienGiam = (int)_chiTietKM.GiamGia;
                        _tienGiam = Convert.ToInt32(_tienGiam * hoaDon.SoLuong);
                       // hoaDon.ThanhTien = hoaDon.ThanhTien - _tienGiam;
                       
                        if(ListKhuyenMaiGiamGia.Where(x=>x.MaMon == hoaDon.MaMon).SingleOrDefault() == null)
                        {
                            KhuyenMai_DTO khuyenMai_DTO = new KhuyenMai_DTO();
                            khuyenMai_DTO.MaMon = hoaDon.MaMon;
                            khuyenMai_DTO.SoLuong = hoaDon.SoLuong;
                            khuyenMai_DTO.ThanhTien = hoaDon.ThanhTien;
                            khuyenMai_DTO.GiamGia = _tienGiam;
                            ListKhuyenMaiGiamGia.Add(khuyenMai_DTO);

                        }
                        else
                        {
                            ListKhuyenMaiGiamGia.Where(x => x.MaMon == hoaDon.MaMon).SingleOrDefault().SoLuong = hoaDon.SoLuong;
                        }
                    }
                }


            }
        }

        private void KhuyenMaiPhanTram(LoaiKhachHang selectedCustomerType)
        {
            double sum = 0;
            if (SelectedCustomerType != null)
            {
                if (SelectedCustomerType.MaLoaiKhachHang == "LKHTH")
                {
                    CustomerNameList = new ObservableCollection<KhachHang>(DataProvider.Instance.Database.KhachHangs.Where(x => x.MaLoaiKhachHang == SelectedCustomerType.MaLoaiKhachHang));
                    TongHoaDon();
                    ThanhToanCuoiCung();
                }
                else if (SelectedCustomerType.MaLoaiKhachHang == "LKHVIP")
                {
                    foreach (var item in List)
                    {
                        sum += item.ThanhTien;
                    }
                    TongTien = sum + TongTienKhuyenMai();

                    ThanhToan = sum * 0.95;

                    GiamGia = Convert.ToDouble(TongTien - ThanhToan);

                    CustomerNameList = new ObservableCollection<KhachHang>(DataProvider.Instance.Database.KhachHangs.Where(x => x.MaLoaiKhachHang == SelectedCustomerType.MaLoaiKhachHang));
                }
                else if (SelectedCustomerType.MaLoaiKhachHang == "LKHVIPP")
                {
                    foreach (var item in List)
                    {
                        sum += item.ThanhTien;
                    }
                    TongTien = sum + TongTienKhuyenMai();

                    ThanhToan = sum * 0.9;

                    GiamGia = Convert.ToDouble(TongTien - ThanhToan);

                    CustomerNameList = new ObservableCollection<KhachHang>(DataProvider.Instance.Database.KhachHangs.Where(x => x.MaLoaiKhachHang == SelectedCustomerType.MaLoaiKhachHang));
                }
            }
        }

        private void EnabledButtonDiscountByQRCode()
        {
            if (List.Count > 0)
            {
                IsEnabledDiscountByQRCode = true;
            }
            else
            {
                IsEnabledDiscountByQRCode = false;
            }
        }

        private void DisabledButtonDiscountByQrCode()
        {
            IsEnabledDiscountByQRCode = false;
        }

        private void EnabledCustomerVIP(LoaiKhachHang selectedCustomerType = null)
        {
            if (List.Count > 0)
            {
                IsEnabledCustomerName = IsEnabledCustomerType = true;
                KhuyenMaiPhanTram(SelectedCustomerType);

            }
            else
            {
                IsEnabledCustomerName = IsEnabledCustomerType = false;
                SelectedIndexCustomerType = 0;
                SelectedIndexCustomerName = 0;

            }
        }

        private void DisabledCustomerVip(LoaiKhachHang selectedCustomerType = null)
        {
            IsEnabledCustomerName = IsEnabledCustomerType = false;
            SelectedIndexCustomerName = SelectedIndexCustomerType = 0;
        }

        private KhuyenMai_DTO ConvertThucDon_KhuyenMai(ThucDon _maSanPhamMua, string _maSanPhamTang, int soLuong = 1)
        {
            KhuyenMai_DTO khuyenMai_DTO = new KhuyenMai_DTO();
            khuyenMai_DTO.MaMon = _maSanPhamMua.MaMon;
            khuyenMai_DTO.TenMon = DataProvider.Instance.Database.ThucDons.Where(x => x.MaMon == _maSanPhamTang).SingleOrDefault().TenMon;
            khuyenMai_DTO.SanPhamTang = DataProvider.Instance.Database.ThucDons.Where(x => x.MaMon == _maSanPhamMua.MaMon).SingleOrDefault().TenMon;
            khuyenMai_DTO.ThanhTien = (double)_maSanPhamMua.GiaBan * soLuong;
            khuyenMai_DTO.SoLuong = soLuong;
            return khuyenMai_DTO;

        }

        private KhuyenMai_DTO ConvertThucDon_KhuyenMai(ThucDon _maSanPhamMua, int soLuong = 1)
        {
            KhuyenMai_DTO khuyenMai_DTO = new KhuyenMai_DTO();
            khuyenMai_DTO.MaMon = _maSanPhamMua.MaMon;
            khuyenMai_DTO.TenMon = _maSanPhamMua.TenMon;
            khuyenMai_DTO.HinhDaiDien = _maSanPhamMua.HinhDaiDien;
            khuyenMai_DTO.ThanhTien = (double)_maSanPhamMua.GiaBan;
            khuyenMai_DTO.SoLuong = soLuong;
            return khuyenMai_DTO;

        }

        private ThucDon ConvertKhuyenMai_ThucDon(KhuyenMai_DTO khuyenMai)
        {
            ThucDon thucDon = new ThucDon();
            thucDon.MaMon = khuyenMai.MaMon;
            thucDon.TenMon = khuyenMai.TenMon;
            thucDon.HinhDaiDien = khuyenMai.HinhDaiDien;
            thucDon.GiaBan = Convert.ToDecimal(khuyenMai.ThanhTien);
            return thucDon;


        }

        public int IsNumber(string pValue)
        {
            int _count = 0;
            foreach (Char c in pValue)
            {
                if (!Char.IsDigit(c))
                    return _count;
                _count++;
            }
            return -1;
        }

        private void IncreaseNumber_Handmade(string _menuCode, int _count)
        {
            if (_menuCode != null)
            {
                HoaDon_DTO hoaDon_DTO = List.Where(n => n.MaMon == _menuCode).SingleOrDefault();
                hoaDon_DTO.SoLuong = _count;
                hoaDon_DTO.ThanhTien = hoaDon_DTO.SoLuong * hoaDon_DTO.GiaBan;

                KhuyenMaiSanPham(hoaDon_DTO);

                KhuyenMaiPhanTram(hoaDon_DTO);

                EnabledCustomerVIP(SelectedCustomerType);
            }
        }

        public void GetDrinkListWhenStartup()
        {

            MenuList = new ObservableCollection<ThucDon>(DataProvider.Instance.Database.ThucDons.Where(x => x.NhomThucDon.LoaiThucDon.MaLoaiThucDon == "LTDDU" && x.DaXoa == DaXoa).ToList());
        }

        public void ClearValue()
        {
            if (List != null || PromotionDetailList != null )
            {
                
                TongTien = ThanhToan = TienKhachTra = TienThua = null;

                GiamGia = 0;
            }

            ListKhuyenMaiGiamGia = new ObservableCollection<KhuyenMai_DTO>();

            List = new ObservableCollection<HoaDon_DTO>();

            PromotionDetailList = new ObservableCollection<KhuyenMai_DTO>();

            EnabledCustomerVIP();

            EnabledButtonDiscountByQRCode();

            TongTien = ThanhToan = TienKhachTra = TienThua = null;

            GiamGia = 0;

        }

        //Tính tiền
        private double? TongTienKhuyenMai()
        {
            double sum = 0;

            double sumPromotion = 0;

            if (ListKhuyenMaiGiamGia.Count > 0)
            {
                foreach (var itemKM in ListKhuyenMaiGiamGia)
                {
                    sum += itemKM.SoLuong * (int)itemKM.GiamGia;
                }
            }
            if (PromotionDetailList.Count > 0)
            {
                foreach (var itemKM in PromotionDetailList)
                {
                    sumPromotion += itemKM.ThanhTien;
                }

            }


            GiamGia = sum + sumPromotion;

            return GiamGia;
        }
        private double? TongHoaDon()
        {
            double? sum = 0;
            foreach (var item in List)
            {
                sum += item.ThanhTien;
            }
            foreach (var itemKM in PromotionDetailList)
            {
                sum += itemKM.ThanhTien;
            }

            TongTien = sum;


            return sum;
        }
        private double? ThanhToanCuoiCung()
        {
            double? sum = TongHoaDon() - TongTienKhuyenMai();

            ThanhToan = sum;

            if (SelectedCustomerType != null)
            {
                if (SelectedCustomerType.MaLoaiKhachHang == "LKHTH")
                {
                    ThanhToan = sum;
                }
                else if (SelectedCustomerType.MaLoaiKhachHang == "LKHVIP")
                {

                    ThanhToan = sum * 0.95;


                }
                else if (SelectedCustomerType.MaLoaiKhachHang == "LKHVIPP")
                {
                   
                    ThanhToan = sum * 0.9;


                }
            }

            if (!string.IsNullOrEmpty(TienKhachTra + ""))
            {
                TienThua = TienKhachTra - ThanhToan;        

            }
                     
            return ThanhToan;
        }
        public void GetListPromotionName()
        {
            KhuyenMai_DTO khuyenMai_DTO;
            
            try
            {
                List<ChuongTrinhKhuyenMai> getCTKM = new List<ChuongTrinhKhuyenMai>(DataProvider.Instance.Database.ChuongTrinhKhuyenMais.Where(x => x.DaXoa == DaXoa));

                List<ChuongTrinhKhuyenMai> ListChuongTrinhKM = new List<ChuongTrinhKhuyenMai>(getCTKM.Where(x => x.NgayBDKM.Value.Date <= DateTime.Now.Date && x.NgayKTKM.Value.Date >= DateTime.Now.Date));

                PromotionNameList = new ObservableCollection<KhuyenMai_DTO>();

                foreach (var itemChuongTrinh in ListChuongTrinhKM)
                {

                    List<ChiTietKhuyenMai> ListChiTietKM = new List<ChiTietKhuyenMai>(DataProvider.Instance.Database.ChiTietKhuyenMais.Where(x => x.MaCTKM == itemChuongTrinh.MaCTKM && x.DaXoa == DaXoa));

                    foreach (var itemChiTietKM in ListChiTietKM)
                    {
                        List<ThucDon> ListThucDon = new List<ThucDon>(DataProvider.Instance.Database.ThucDons.Where(x => x.MaMon == itemChiTietKM.MaMon && x.DaXoa == DaXoa));

                        foreach (var itemThucDOn in ListThucDon)
                        {
                            khuyenMai_DTO = new KhuyenMai_DTO();

                            khuyenMai_DTO.TenCTKM = itemChuongTrinh.TenCTKM;

                            khuyenMai_DTO.NgayBDKM = (DateTime)itemChuongTrinh.NgayBDKM;

                            khuyenMai_DTO.NgayKTKM = (DateTime)itemChuongTrinh.NgayKTKM;

                            if (string.IsNullOrEmpty(itemChiTietKM.SanPhamTang))
                            {
                                khuyenMai_DTO.SanPhamTang = "Không có";
                            }
                            else
                            {
                                khuyenMai_DTO.SanPhamTang = DataProvider.Instance.Database.ThucDons.Where(x => x.MaMon == itemChiTietKM.SanPhamTang).SingleOrDefault().TenMon;

                            }
                            if (itemChiTietKM.GiamGia != 0)
                            {
                                khuyenMai_DTO.GiamGia = (int)itemChiTietKM.GiamGia;
                            }
                            else
                            {
                                khuyenMai_DTO.GiamGia = 0;
                            }
                            khuyenMai_DTO.MaMon = DataProvider.Instance.Database.ThucDons.Where(x => x.MaMon == itemChiTietKM.MaMon).SingleOrDefault().TenMon; ;

                            PromotionNameList.Add(khuyenMai_DTO);
                        }

                    }
                }
            }
            catch
            {
                return;
                
            }

            
        }
        private string EncodeIDBill(string maHoaDon)
        {
            maHoaDon = "HD" + "-" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + "-"
                + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();
            return maHoaDon;
        }
        public void GetListCustomerType()
        {
            try
            {
                CustomerTypeList = new ObservableCollection<LoaiKhachHang>(DataProvider.Instance.Database.LoaiKhachHangs.Where(x => x.DaXoa == DaXoa).ToList());
            }
            catch
            {
                return;

            }

        }

        private void HienThiSanPhamYeuThich()
        {
            FavoriteMenuList = new ObservableCollection<KhuyenMai_DTO>();
            List<HoaDon> DanhSachHoaDon = DataProvider.Instance.Database.HoaDons.Where(x => x.NgayXuatHoaDon.Value.Year == DateTime.Now.Year).ToList();

            //Lấy các món đã mua trong danh sách hoá đơn
            foreach (var itemDSHD in DanhSachHoaDon)
            {
                List<ChiTietHoaDon> DanhSachCTHD = DataProvider.Instance.Database.ChiTietHoaDons.Where(x => x.MaHoaDon == itemDSHD.MaHoaDon).ToList();
                //Lấy data trong chi tiết hoá đơn
                foreach (var itemDSCTHD in DanhSachCTHD)
                {
                    //Nếu chưa có món nào được yêu thích thì thêm vào
                    if (FavoriteMenuList.Where(x => x.MaMon == itemDSCTHD.MaMon).SingleOrDefault() == null)
                    {
                        ThucDon thucDon = DataProvider.Instance.Database.ThucDons.Where(x => x.MaMon == itemDSCTHD.MaMon).SingleOrDefault();

                        FavoriteMenuList.Add(ConvertThucDon_KhuyenMai(thucDon, (int)itemDSCTHD.SoLuong));

                    }
                    else
                    {
                        FavoriteMenuList.Where(x => x.MaMon == itemDSCTHD.MaMon).SingleOrDefault().SoLuong += (int)itemDSCTHD.SoLuong;
                    }
                }

            }

            //Sắp xếp theo thứ tự tăng dần
            FavoriteMenuList = new ObservableCollection<KhuyenMai_DTO>(FavoriteMenuList.OrderByDescending(x => x.SoLuong).ToList());
            int _count = 0;
            MenuList.Clear();
            foreach (var itemFavoriteMenu in FavoriteMenuList)
            {
                //Thay đổi số lượng các món yêu thích ở đây
                MenuList.Add(ConvertKhuyenMai_ThucDon(itemFavoriteMenu));
                _count++;
                if (_count > 8)
                {

                    break;
                }
            }
        }
        private void ShowScanQRCodeView()
        {
            ScanQRCodeDiscountView scanQRCodeView = new ScanQRCodeDiscountView();

            scanQRCodeView.ShowDialog();

            var scanQRCodeViewModel = scanQRCodeView.DataContext as ScanQRCodeDiscountViewModel;

            if(scanQRCodeViewModel.IsCheckQrCode)
            {

               
                //Lấy các sản phẩm đã mua
                try
                {
                    ThanhToan = TotalDiscountByQRCode();

                    DisabledCustomerVip();

                    DisabledButtonDiscountByQrCode();
                }
                catch
                {
                    return;
                }
            }   
        }
        private double TotalDiscountByQRCode(double sum = 0)
        {
            
            foreach (var itemTongTien in List)
            {
                sum += itemTongTien.ThanhTien * 0.9;
                
            }
            
            return sum;
        }

        #endregion Methods
    }
}