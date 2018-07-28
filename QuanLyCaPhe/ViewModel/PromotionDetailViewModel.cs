using GalaSoft.MvvmLight.Messaging;
using QuanLyCaPhe.Message;
using QuanLyCaPhe.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyCaPhe.ViewModel
{
    public class PromotionDetailViewModel:BaseViewModel
    {
        #region Property

        private ObservableCollection<ChiTietKhuyenMai> _List;
        public ObservableCollection<ChiTietKhuyenMai> List { get => _List; set { _List = value; RaisePropertyChanged("List"); } }

        private ObservableCollection<ChuongTrinhKhuyenMai> _PromotionList;
        public ObservableCollection<ChuongTrinhKhuyenMai> PromotionList { get => _PromotionList; set { _PromotionList = value; RaisePropertyChanged("PromotionList"); } }

        private ObservableCollection<ThucDon> _MenuProductBuyList;
        public ObservableCollection<ThucDon> MenuProductBuyList { get => _MenuProductBuyList; set { _MenuProductBuyList = value; RaisePropertyChanged("MenuProductBuyList"); } }

        private ObservableCollection<ThucDon> _MenuProductGiftList;
        public ObservableCollection<ThucDon> MenuProductGiftList { get => _MenuProductGiftList; set { _MenuProductGiftList = value; RaisePropertyChanged("MenuProductGiftList"); } }



        private string _maCTKM;

        private string _sanPhamTang;

        private string _maMon;

        private string _maChiTietKM;


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
                    DieuKien = (int)SelectedItem.DieuKien;
                    IsEnabledPromotionCode = false;
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
        private bool _isEnabledPromotionCode;
        public bool IsEnabledPromotionCode
        {
            get
            {
                return _isEnabledPromotionCode;
            }
            set
            {
                if (_isEnabledPromotionCode != value)
                {
                    _isEnabledPromotionCode = value; RaisePropertyChanged("IsEnabledPromotionCode");
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
            LoadAll();

            IsEnabledPromotionCode = true;

            AddPromotionDetailCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(MaChiTietKM) || string.IsNullOrEmpty(SanPhamTang) || string.IsNullOrEmpty(MaMon) || DieuKien == null || SelectPromotionList == null)
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
                if (string.IsNullOrEmpty(MaChiTietKM) || SelectedItem == null)
                    return false;

                var list = DataProvider.Instance.Database.ChiTietKhuyenMais.Where(x => x.MaMon == MaMon && x.SanPhamTang == SanPhamTang && x.DieuKien == DieuKien && x.MaCTKM == SelectPromotionList.MaCTKM).Count();
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
            MenuProductBuyList = new ObservableCollection<ThucDon>(DataProvider.Instance.Database.ThucDons.Where(x => x.DaXoa == DaXoa).ToList());
        }

        private void LoadMenuProductGiftList()
        {
            MenuProductGiftList = new ObservableCollection<ThucDon>(DataProvider.Instance.Database.ThucDons.Where(x => x.DaXoa == DaXoa).ToList());
        }

        #endregion Constructor

        #region Methods

        public bool ClearTextBox()
        {

            MaChiTietKM = string.Empty;
            SanPhamTang = string.Empty;
            MaMon = string.Empty;
            DieuKien = null;
            SelectedItem = null;
            SelectPromotionList = null;
            SelectedProductBuyList = null;
            SelectedProductGiftList = null;
            IsEnabledPromotionCode = true;

            return false;
        }

        private void AddPromotionDetail_Execute()
        {
            UserMessage msg = new UserMessage();
            try
            {
                var promotionDetail = new ChiTietKhuyenMai()
                {
                    MaChiTietKM = MaChiTietKM,
                    MaCTKM = SelectPromotionList.MaCTKM,
                    SanPhamTang = SelectedProductGiftList.MaMon,
                    MaMon = SelectedProductBuyList.MaMon,
                    DieuKien = DieuKien,
                    DaXoa = DaXoa,
                    
                    
                };

                DataProvider.Instance.Database.ChiTietKhuyenMais.Add(promotionDetail);
                DataProvider.Instance.Database.SaveChanges();
                List.Add(promotionDetail);
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

        private void UpdatePromotionDetail_Execute()
        {
            UserMessage msg = new UserMessage();
            try
            {
                var res = DataProvider.Instance.Database.ChiTietKhuyenMais.SingleOrDefault(x => x.MaChiTietKM == SelectedItem.MaChiTietKM);
                if (res != null)
                {
                    res.SanPhamTang = SanPhamTang;
                    res.MaMon = MaMon;
                    res.DieuKien = DieuKien;
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

        private void DeletePromotionDetail_Execute()
        {
            UserMessage msg = new UserMessage();
            try
            {
                if (ConfirmDialog("Bạn có chắc chắn muốn xoá khuyến mãi <<" + SelectedItem.MaCTKM + ">> không ? "))
                {
                    var promotionDetail = DataProvider.Instance.Database.ChiTietKhuyenMais.SingleOrDefault(x => x.MaCTKM == SelectedItem.MaCTKM);
                    List.Remove(promotionDetail);
                    promotionDetail.DaXoa = true;
                    DataProvider.Instance.Database.SaveChanges();
                    RaisePropertyChanged("List");
                    msg.Message = "Dữ liệu đã xoá thành công";

                }
            }

            catch (System.Exception ex)
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    MessageBox.Show(ex.InnerException.GetBaseException().ToString());
                }
                msg.Message = "Có vấn đề trong xoá dữ liệu";
            }

            Messenger.Default.Send<UserMessage>(msg);
            ClearTextBox();

        }
        public void LoadPromotionDetailList()
        {
            List = new ObservableCollection<ChiTietKhuyenMai>(DataProvider.Instance.Database.ChiTietKhuyenMais.Where(x=>x.DaXoa == DaXoa).ToList());
        }        

        public void LoadPromotionList()
        {
            PromotionList = new ObservableCollection<ChuongTrinhKhuyenMai>(DataProvider.Instance.Database.ChuongTrinhKhuyenMais.Where(x => x.DaXoa == DaXoa).ToList());
        }
        public void LoadAll()
        {
            LoadPromotionDetailList();

            LoadPromotionList();

            LoadMenuProductBuyList();

            LoadMenuProductGiftList();

            ClearTextBox();
        }
        #endregion Methods
    }
}
