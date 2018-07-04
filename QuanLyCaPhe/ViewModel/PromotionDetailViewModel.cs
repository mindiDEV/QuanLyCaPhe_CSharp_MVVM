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
        

        private string _maCTKM;

        private string _sanPhamTang;

        private string _maMon;

        private string _maChiTietKM;

        private double _giamGia;

        private int _dieuKien;

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
        public double GiamGia
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

        public int DieuKien
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
                    SanPhamTang = SelectedItem.SanPhamTang;
                    MaMon = SelectedItem.MaMon;
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

            

            AddPromotionDetailCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(MaChiTietKM) || string.IsNullOrEmpty(SanPhamTang) || string.IsNullOrEmpty(MaMon))
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
                if (string.IsNullOrEmpty(MaCTKM))
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

        private bool ClearTextBox()
        {
            
            return false;
        }

        private void AddPromotionDetail_Execute()
        {
            var promotionDetail = new ChiTietKhuyenMai() { MaChiTietKM = MaChiTietKM, MaCTKM = MaCTKM, SanPhamTang = SanPhamTang, MaMon = MaMon, GiamGia = (float)GiamGia, DieuKien = DieuKien  };
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
                    

                DataProvider.Instance.Database.SaveChanges();
                ClearTextBox();
            }
        }

        private void DeletePromotionDetail_Execute()
        {
            WarningDialogs("Dữ liệu không thể xoá. Xin vui lòng thử lại vào dịp khác!!!");
            ClearTextBox();

        }
        private void LoadPromotionDetailList()
        {
            List = new ObservableCollection<ChiTietKhuyenMai>(DataProvider.Instance.Database.ChiTietKhuyenMais);
        }        

        private void LoadPromotionList()
        {
            PromotionList = new ObservableCollection<ChuongTrinhKhuyenMai>(DataProvider.Instance.Database.ChuongTrinhKhuyenMais);
        }
        #endregion Methods
    }
}
