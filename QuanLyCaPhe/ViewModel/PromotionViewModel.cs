using QuanLyCaPhe.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System;
using QuanLyCaPhe.Message;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;

namespace QuanLyCaPhe.ViewModel
{
    public class PromotionViewModel : BaseViewModel
    {
        #region Property

        private ObservableCollection<ChuongTrinhKhuyenMai> _List;
        public ObservableCollection<ChuongTrinhKhuyenMai> List { get => _List; set { _List = value; RaisePropertyChanged("List"); } }

        private string _tenCTKM;

        private string _motaCTKM;

        private string _maCTKM;

        private DateTime? _ngayBDKM = DateTime.Now.AddDays(-1);

        private DateTime? _ngayKTKM = DateTime.Now;

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

        public DateTime? NgayBDKM
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
        public DateTime? NgayKTKM
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

        private bool _isEnabledPromotionCode;
        public bool IsEnabledPromotionCode
        {
            get => _isEnabledPromotionCode;
            set
            {
                if (_isEnabledPromotionCode != value)
                {
                    _isEnabledPromotionCode = value;
                    RaisePropertyChanged("IsEnabledPromotionCode");
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
                    IsEnabledPromotionCode = false;
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

            IsEnabledPromotionCode = true;

            MaCTKM = "KM";

            LoadPromotionList();

            AddPromotionCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(MaCTKM) || string.IsNullOrEmpty(TenCTKM) || string.IsNullOrEmpty(MoTaCTKM) || NgayBDKM == null || NgayKTKM == null)
                {
                    return false;
                }

                if(!isSymbol(MaCTKM) || !isSymbol(TenCTKM) || !isSymbol(MoTaCTKM))
                {
                    return false;
                }

                if (DataProvider.Instance.Database.ChuongTrinhKhuyenMais.Where(x=>x.MaCTKM == MaCTKM).Count() != 0)
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
                if ((string.IsNullOrEmpty(MaCTKM) && string.IsNullOrEmpty(TenCTKM) && string.IsNullOrEmpty(MoTaCTKM)) || SelectedItem == null)
                    return false;

                if (!isSymbolAndNumber(TenCTKM))
                {
                    return false;
                }

                if (DataProvider.Instance.Database.ChuongTrinhKhuyenMais.Where(x => x.TenCTKM == TenCTKM && x.MoTaCTKM == MoTaCTKM && x.NgayBDKM == (DateTime)NgayBDKM && x.NgayKTKM == (DateTime)NgayKTKM).Count() != 0)
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

        public void LoadPromotionList()
        {
            List = new ObservableCollection<ChuongTrinhKhuyenMai>(DataProvider.Instance.Database.ChuongTrinhKhuyenMais.Where(x=>x.DaXoa == DaXoa).ToList());
        }



        #endregion Constructor

        #region Methods

        public bool ClearTextBox()
        {

            MaCTKM = "KM";
            TenCTKM = string.Empty;
            MoTaCTKM = string.Empty;
            NgayBDKM = DateTime.Now.AddDays(-1);
            NgayKTKM = DateTime.Now;
            SelectedItem = null;
            IsEnabledPromotionCode = true;
            return true;

        }

        private void AddPromotion_Execute()
        {
            UserMessage msg = new UserMessage();
            try
            {
                var Promotion = new ChuongTrinhKhuyenMai()
                {
                    MaCTKM = MaCTKM,
                    TenCTKM = TenCTKM,
                    MoTaCTKM = MoTaCTKM,
                    NgayBDKM = NgayBDKM,
                    NgayKTKM = NgayKTKM,
                    DaXoa = DaXoa,
                };
                DataProvider.Instance.Database.ChuongTrinhKhuyenMais.Add(Promotion);
                DataProvider.Instance.Database.SaveChanges();
                List.Add(Promotion);
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

        private void UpdatePromotion_Execute()
        {
            UserMessage msg = new UserMessage();
            try
            {
                var res = DataProvider.Instance.Database.ChuongTrinhKhuyenMais.SingleOrDefault(x => x.MaCTKM == SelectedItem.MaCTKM);
                if (res != null)
                {
                    res.TenCTKM = TenCTKM;
                    res.MoTaCTKM = MoTaCTKM;
                    res.NgayBDKM = NgayBDKM;
                    res.NgayKTKM = NgayKTKM;
                    DataProvider.Instance.Database.SaveChanges();
                    msg.Message = "Cập nhật dữ liệu thành công";
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

        private void DeletePromotion_Execute()
        {
            UserMessage msg = new UserMessage();
            try
            {
                if (ConfirmDialog("Bạn có chắc chắn muốn xoá chương trình khuyến mãi <<" + SelectedItem.TenCTKM + ">> không ? "))
                {
                    var promotion = DataProvider.Instance.Database.ChuongTrinhKhuyenMais.SingleOrDefault(x => x.MaCTKM == SelectedItem.MaCTKM);
                    List.Remove(promotion);
                    promotion.DaXoa = true;
                    DataProvider.Instance.Database.SaveChanges();
                    RaisePropertyChanged("List");
                    msg.Message = "Xoá dữ liệu thành công";
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

        #endregion Methods
    }
}