using QuanLyCaPhe.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System;

namespace QuanLyCaPhe.ViewModel
{
    public class PromotionViewModel : BaseViewModel
    {
        #region Property

        private ObservableCollection<ChuongTrinhKhuyenMai> _List;
        public ObservableCollection<ChuongTrinhKhuyenMai> List { get => _List; set { _List = value; } }

        private string _tenCTKM;

        private string _motaCTKM;

        private string _maCTKM;

        private DateTime _ngayBDKM = DateTime.Now;

        private DateTime _ngayKTKM = DateTime.Now;

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

        public string MaCTKM { get => _maCTKM; set { if (_maCTKM != value) _maCTKM = value; RaisePropertyChanged("MaCTKM"); } }

        public string MoTaCTKM { get => _motaCTKM; set { if (_motaCTKM != value) _motaCTKM = value; RaisePropertyChanged("MoTaCTKM"); } }

        public DateTime NgayBDKM
        {
            get
            {
                return _ngayBDKM;
            }
            set
            {
                if (_ngayBDKM != value)
                {
                    _ngayBDKM = value; RaisePropertyChanged("NgayBDKM");
                }
            }
        }
        public DateTime NgayKTKM
        {
            get
            {
                return _ngayKTKM;
            }
            set
            {
                if (_ngayKTKM != value)
                {
                    _ngayKTKM = value; RaisePropertyChanged("NgayKTKM");
                }
            }
        }


        private ChuongTrinhKhuyenMai _SelectedItem;

        public ChuongTrinhKhuyenMai SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                RaisePropertyChanged();
                if (SelectedItem != null)
                {
                    MaCTKM = SelectedItem.MaCTKM;
                    TenCTKM = SelectedItem.TenCTKM;
                    MoTaCTKM = SelectedItem.MoTaCTKM;
                    NgayBDKM = (DateTime)SelectedItem.NgayBDKM;
                    NgayKTKM = (DateTime)SelectedItem.NgayKTKM;
                }
            }
        }

        #endregion Property

        #region Command Property

        private ICommand addPromotionCommand;
        public ICommand AddPromotionCommand { get => addPromotionCommand; set { addPromotionCommand = value; RaisePropertyChanged(); } }

        private ICommand updatePromotionCommand;
        public ICommand UpdatePromotionCommand { get => updatePromotionCommand; set { updatePromotionCommand = value; RaisePropertyChanged(); } }

        private ICommand _deletePromotionCommand;

        public ICommand DeletePromotionCommand { get => _deletePromotionCommand; set { _deletePromotionCommand = value; RaisePropertyChanged(); } }

        private ICommand _createNewPromotionCommand;
        public ICommand CreateNewPromotionCommand { get => _createNewPromotionCommand; set { _createNewPromotionCommand = value; RaisePropertyChanged(); } }



        #endregion Command Property

        #region Constructor

        public PromotionViewModel()
        {
            MaCTKM = "KM";

            List = new ObservableCollection<ChuongTrinhKhuyenMai>(DataProvider.Instance.Database.ChuongTrinhKhuyenMais);

            AddPromotionCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(MaCTKM) || string.IsNullOrEmpty(TenCTKM))
                {
                    return false;
                }

                var list = DataProvider.Instance.Database.ChuongTrinhKhuyenMais.Where(x => x.TenCTKM == TenCTKM && x.MaCTKM == MaCTKM).Count();
                if (list != 0)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                AddPromotion_Execute();
            });

            UpdatePromotionCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(MaCTKM) && SelectedItem == null)
                    return false;

                if (!isSymbolAndNumber(TenCTKM))
                {
                    return false;
                }

                var list = DataProvider.Instance.Database.ChuongTrinhKhuyenMais.Where(x => x.MaCTKM == MaCTKM && x.TenCTKM == TenCTKM).Count();
                if (list != 0)
                {
                    return false;
                }
                return true;
            },
              (p) =>
              {
                  UpdatePromotion_Execute();
              });

            DeletePromotionCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            }, (p) =>
            {
                DeletePromotion_Execute();
            });

            CreateNewPromotionCommand = new RelayCommand<object>((p) =>
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
            if (MaCTKM != null && TenCTKM != null && MoTaCTKM != null)
            {
                MaCTKM = "DVT";
                TenCTKM = string.Empty;
                MoTaCTKM = string.Empty;
                NgayBDKM = DateTime.Now;
                NgayKTKM = DateTime.Now;
                return true;
            }
            return false;
        }

        private void AddPromotion_Execute()
        {
            var Promotion = new ChuongTrinhKhuyenMai() { MaCTKM = MaCTKM, TenCTKM = TenCTKM,MoTaCTKM = MoTaCTKM,NgayBDKM = NgayKTKM,NgayKTKM = NgayKTKM};
            DataProvider.Instance.Database.ChuongTrinhKhuyenMais.Add(Promotion);
            DataProvider.Instance.Database.SaveChanges();
            List.Add(Promotion);
            ClearTextBox();
        }

        private void UpdatePromotion_Execute()
        {
            var res = DataProvider.Instance.Database.ChuongTrinhKhuyenMais.SingleOrDefault(x => x.MaCTKM == SelectedItem.MaCTKM);
            if (res != null)
            {
                res.MaCTKM = MaCTKM;
                res.TenCTKM = TenCTKM;
                res.MoTaCTKM = MoTaCTKM;
                res.NgayBDKM = NgayBDKM;
                res.NgayKTKM = NgayKTKM;
               
                DataProvider.Instance.Database.SaveChanges();
                ClearTextBox();
            }
        }

        private void DeletePromotion_Execute()
        {
            WarningDialogs("Dữ liệu không thể xoá. Xin vui lòng thử lại vào dịp khác!!!");
            ClearTextBox();

        }

        #endregion Methods
    }
}