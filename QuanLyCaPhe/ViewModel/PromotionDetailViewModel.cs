using QuanLyCaPhe.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyCaPhe.ViewModel
{
    public class PromotionDetailViewModel:BaseViewModel
    {
        #region Property

        private ObservableCollection<ChiTietKhuyenMai> _List;
        public ObservableCollection<ChiTietKhuyenMai> List { get => _List; set { _List = value; } }

        private ObservableCollection<ChuongTrinhKhuyenMai> _PromotionList;
        public ObservableCollection<ChuongTrinhKhuyenMai> PromotionList { get => _PromotionList; set { _PromotionList = value; } }

        private ObservableCollection<ThucDon> _MenuProductBuyList;
        public ObservableCollection<ThucDon> MenuProductBuyList { get => _MenuProductBuyList; set { _MenuProductBuyList = value; } }

        private ObservableCollection<ThucDon> _MenuProductGiftList;
        public ObservableCollection<ThucDon> MenuProductGiftList { get => _MenuProductGiftList; set { _MenuProductGiftList = value; } }



        private string _maCTKM;

        private string _sanPhamTang;

        private string _maMon;

        private string _maChiTietKM;

        private double? _giamGia;

        private int? _dieuKien;

        public string MaChiTietKM
        {
            get
            {
                return _maChiTietKM;
            }
            set
            {
                if (_maChiTietKM != value)
                {
                    _maChiTietKM = value; RaisePropertyChanged("MaChiTietKM");
                }
            }
        }

        public string MaCTKM
        {
            get
            {
                return _maCTKM;
            }
            set
            {
                if (_maCTKM != value)
                {
                    _maCTKM = value; RaisePropertyChanged("MaCTKM");
                }
            }
        }

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
                    _maMon = value; RaisePropertyChanged("MaMon");
                }
            }
        }

        private string _tenCTKM;
        public string TenCTKM
        {
            get
            {
                return _tenCTKM;
            }
            set
            {
                if (_tenCTKM != value)
                {
                    _tenCTKM = value; RaisePropertyChanged("TenCTKM");
                }
            }
        }

        public string TenMon
        {
            get
            {
                return _sanPhamTang;
            }
            set
            {
                if (_sanPhamTang != value)
                {
                    _sanPhamTang = value; RaisePropertyChanged("TenMon");
                }
            }
        }


        public string SanPhamTang
        {
            get
            {
                return _sanPhamTang;
            }
            set
            {
                if (_sanPhamTang != value)
                {
                    _sanPhamTang = value; RaisePropertyChanged("SanPhamTang");
                }
            }
        }
        public double? GiamGia
        {
            get
            {
                return _giamGia;
            }
            set
            {
                if (_giamGia != value)
                {
                    _giamGia = value; RaisePropertyChanged("GiamGia");
                }
            }
        }

        public int? DieuKien
        {
            get
            {
                return _dieuKien;
            }
            set
            {
                if (_dieuKien != value)
                {
                    _dieuKien = value; RaisePropertyChanged("DieuKien");
                }
            }
        }



        private ChiTietKhuyenMai _SelectedItem;

        public ChiTietKhuyenMai SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                RaisePropertyChanged();
                if (SelectedItem != null)
                {
                    MaChiTietKM = SelectedItem.MaChiTietKM;
                    SelectPromotionList = SelectedItem.ChuongTrinhKhuyenMai;
                    SelectedProductBuyList = SelectedItem.ThucDon;
                    SelectedProductGiftList = DataProvider.Instance.Database.ThucDons.Where(x => x.MaMon == SelectedItem.SanPhamTang).FirstOrDefault();
                    GiamGia = (double)SelectedItem.GiamGia;
                    DieuKien = (int)SelectedItem.DieuKien;
                }
            }
        }


        private ChuongTrinhKhuyenMai _SelectPromotionList;

        public ChuongTrinhKhuyenMai SelectPromotionList
        {
            get => _SelectPromotionList;
            set
            {
                _SelectPromotionList = value;
                RaisePropertyChanged();
                if (SelectPromotionList != null)
                {
                    TenCTKM = SelectPromotionList.TenCTKM;
                }
            }
        }

        private ThucDon _SelectedProductBuyList;

        public ThucDon SelectedProductBuyList
        {
            get => _SelectedProductBuyList;
            set
            {
                _SelectedProductBuyList = value;
                RaisePropertyChanged();
                if(SelectedProductBuyList != null)
                {
                    MaMon = SelectedProductBuyList.MaMon;
                }
            }
        }

        private ThucDon _SelectedProductGiftList;

        public ThucDon SelectedProductGiftList
        {
            get => _SelectedProductGiftList;
            set
            {
                _SelectedProductGiftList = value;
                RaisePropertyChanged();
                if (SelectedProductGiftList != null)
                {
                    SanPhamTang = SelectedProductGiftList.MaMon;
                }
            }
        }



        #endregion Property

        #region Command Property

        private ICommand addPromotionDetailCommand;
        public ICommand AddPromotionDetailCommand { get => addPromotionDetailCommand; set { addPromotionDetailCommand = value; RaisePropertyChanged(); } }

        private ICommand updatePromotionDetailCommand;
        public ICommand UpdatePromotionDetailCommand { get => updatePromotionDetailCommand; set { updatePromotionDetailCommand = value; RaisePropertyChanged(); } }

        private ICommand _deletePromotionDetailCommand;
        public ICommand DeletePromotionDetailCommand { get => _deletePromotionDetailCommand; set { _deletePromotionDetailCommand = value; RaisePropertyChanged(); } }

        private ICommand _createNewPromotionDetailCommand;
        public ICommand CreateNewPromotionDetailCommand { get => _createNewPromotionDetailCommand; set { _createNewPromotionDetailCommand = value; RaisePropertyChanged(); } }



        #endregion Command Property

        #region Constructor

        public PromotionDetailViewModel()
        {
            LoadPromotionDetailList();

            LoadPromotionList();

            LoadMenuProductBuyList();

            LoadMenuProductGiftList();


            AddPromotionDetailCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(MaChiTietKM) || string.IsNullOrEmpty(SanPhamTang) || string.IsNullOrEmpty(MaMon) || SelectPromotionList == null)
                {
                    return false;
                }

                var list = DataProvider.Instance.Database.ChiTietKhuyenMais.Where(x => x.MaChiTietKM == MaChiTietKM).Count();
                if (list != 0)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                AddPromotionDetail_Execute();
            });

            UpdatePromotionDetailCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(MaChiTietKM) && SelectedItem == null)
                    return false;

                

                var list = DataProvider.Instance.Database.ChiTietKhuyenMais.Where(x => x.MaChiTietKM == MaChiTietKM).Count();
                if (list != 0)
                {
                    return false;
                }
                return true;
            },
              (p) =>
              {
                  UpdatePromotionDetail_Execute();
              });

            DeletePromotionDetailCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            }, (p) =>
            {
                DeletePromotionDetail_Execute();
            });

            CreateNewPromotionDetailCommand = new RelayCommand<object>((p) =>
            {
               
                return true;
            }, (p) =>
            {
                ClearTextBox();
            });
        }

        private void LoadMenuProductBuyList()
        {
            MenuProductBuyList = new ObservableCollection<ThucDon>(DataProvider.Instance.Database.ThucDons);
        }

        private void LoadMenuProductGiftList()
        {
            MenuProductGiftList = new ObservableCollection<ThucDon>(DataProvider.Instance.Database.ThucDons);
        }

        #endregion Constructor

        #region Methods

        public bool ClearTextBox()
        {
            if(MaChiTietKM !=null)
            {
                MaChiTietKM = string.Empty;
                GiamGia = null;
                SanPhamTang = string.Empty;
                MaMon = string.Empty;
                DieuKien = null;
                SelectedItem = null;
                SelectPromotionList = null;
                SelectedProductBuyList = null;
                SelectedProductGiftList = null;

            }
            return false;
        }

        private void AddPromotionDetail_Execute()
        {
            var promotionDetail = new ChiTietKhuyenMai()
            {
                MaChiTietKM = MaChiTietKM,
                MaCTKM = SelectPromotionList.MaCTKM,
                SanPhamTang = SelectedProductGiftList.MaMon,
                MaMon = SelectedProductBuyList.MaMon,
                GiamGia = (float)GiamGia,
                DieuKien = DieuKien
            };
            DataProvider.Instance.Database.ChiTietKhuyenMais.Add(promotionDetail);
            DataProvider.Instance.Database.SaveChanges();
            List.Add(promotionDetail);
            ClearTextBox();
        }

        private void UpdatePromotionDetail_Execute()
        {
            var res = DataProvider.Instance.Database.ChiTietKhuyenMais.SingleOrDefault(x => x.MaChiTietKM == SelectedItem.MaChiTietKM);
            if (res != null)
            {
                res.GiamGia = (int)GiamGia;
                res.SanPhamTang = SanPhamTang;
                res.MaMon = MaMon;
                res.DieuKien = DieuKien;
                DataProvider.Instance.Database.SaveChanges();
                ClearTextBox();
            }
        }

        private void DeletePromotionDetail_Execute()
        {
            WarningDialogs("Dữ liệu không thể xoá. Xin vui lòng thử lại vào dịp khác!!!");
            ClearTextBox();

        }
        public void LoadPromotionDetailList()
        {
            List = new ObservableCollection<ChiTietKhuyenMai>(DataProvider.Instance.Database.ChiTietKhuyenMais);
        }        

        public void LoadPromotionList()
        {
            PromotionList = new ObservableCollection<ChuongTrinhKhuyenMai>(DataProvider.Instance.Database.ChuongTrinhKhuyenMais);
        }
        #endregion Methods
    }
}
