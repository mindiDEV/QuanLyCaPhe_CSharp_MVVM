using QuanLyCaPhe.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace QuanLyCaPhe.ViewModel
{
    public class MenuTypeViewModel : BaseViewModel
    {
        #region Property

        private ObservableCollection<LoaiThucDon> _List;
        public ObservableCollection<LoaiThucDon> List { get => _List; set { _List = value; } }

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

            List = new ObservableCollection<LoaiThucDon>(DataProvider.Instance.Database.LoaiThucDons);

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
                  var res = DataProvider.Instance.Database.LoaiThucDons.SingleOrDefault(x => x.MaLoaiThucDon == SelectedItem.MaLoaiThucDon);
                  if (res != null)
                  {
                      IsEnabledMenuTypeCode = false;
                      res.TenLoaiThucDon = TenLoaiThucDon;
                      DataProvider.Instance.Database.SaveChanges();
                      ClearTextBox();
                  }
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

        private void DeleteMenuType_Execute()
        {
            if (ConfirmDialog("Bạn có chắc chắn muốn xoá loại thực đơn <<" + SelectedItem.TenLoaiThucDon + ">> không ? "))
            {
                var menuType = DataProvider.Instance.Database.LoaiThucDons.SingleOrDefault(x => x.MaLoaiThucDon == SelectedItem.MaLoaiThucDon);
                List.Remove(menuType);
                DataProvider.Instance.Database.LoaiThucDons.Remove(menuType);
                DataProvider.Instance.Database.SaveChanges();
                RaisePropertyChanged("List");
                ClearTextBox();
            }
            else
            {
                ClearTextBox();
            }
        }

        private void AddMenuType_Execute()
        {
            var menuType = new LoaiThucDon() { MaLoaiThucDon = MaLoaiThucDon, TenLoaiThucDon = TenLoaiThucDon };
            DataProvider.Instance.Database.LoaiThucDons.Add(menuType);
            DataProvider.Instance.Database.SaveChanges();
            List.Add(menuType);
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