using DevExpress.XtraReports.UI;
using Microsoft.Win32;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using QuanLyCaPhe.ClassSupport;
using QuanLyCaPhe.Model;
using QuanLyCaPhe.Report;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace QuanLyCaPhe.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        #region Properties

        private ObservableCollection<KhachHang> _listCustomer;
        public ObservableCollection<KhachHang> ListCustomer { get => _listCustomer; set { _listCustomer = value; RaisePropertyChanged("ListCustomer"); } }

        private ObservableCollection<NhanVien> _listEmployee;
        public ObservableCollection<NhanVien> ListEmployee { get => _listEmployee; set { _listEmployee = value; RaisePropertyChanged("ListEmployee"); } }

        public ObservableCollection<HoaDon> _listBill;
        public ObservableCollection<HoaDon> ListBill { get => _listBill; set { _listBill = value; RaisePropertyChanged("ListBill"); } }

        public ObservableCollection<ChuongTrinhKhuyenMai> _listPromotion;
        public ObservableCollection<ChuongTrinhKhuyenMai> ListPromotion { get => _listPromotion; set { _listPromotion = value; RaisePropertyChanged("ListPromotion"); } }

        private ObservableCollection<LoaiKhachHang> _CustomerTypeList;
        public ObservableCollection<LoaiKhachHang> CustomerTypeList { get => _CustomerTypeList; set { _CustomerTypeList = value; RaisePropertyChanged("CustomerTypeList"); } }

        private string _maHoaDon;
        private DateTime _ngayXuatHoaDon;
        private double _tongHoaDon;
        private string _maNhanVien;
        private string _maKhachHang;
        private DateTime _ngayBatDau = DateTime.Now.Date.AddDays(-1);
        private DateTime _ngayKetThuc = DateTime.Now;
        private string _trangThaiLamViec = "Đang làm";

        private string _tenLoaiKhachHang;

        

        private bool _daXoa;
        public bool DaXoa
        {
            get { return _daXoa; }
            set { _daXoa = value; RaisePropertyChanged("DaXoa"); }
        }
        public string TenLoaiKhachHang
        {
            get { return _tenLoaiKhachHang; }
            set { _tenLoaiKhachHang = value; RaisePropertyChanged("TenLoaiKhachHang"); }
        }
   

        private LoaiKhachHang _selectedCustomerType;
        public LoaiKhachHang SelectedCustomerType
        {
            get
            {
                return _selectedCustomerType;
            }
            set
            {
                if (_selectedCustomerType != value)
                {
                    _selectedCustomerType = value;
                    RaisePropertyChanged("SelectedCustomerType");
                    if (SelectedCustomerType != null)
                    {
                        
                        TenLoaiKhachHang = SelectedCustomerType.TenLoaiKhachHang;
                        if (TenLoaiKhachHang == "Thường")
                        {
                            ListCustomer = new ObservableCollection<KhachHang>(DataProvider.Instance.Database.KhachHangs.Where(x => x.MaLoaiKhachHang == "LKHTH" && x.DaXoa == DaXoa).ToList());
                        }
                        else if (TenLoaiKhachHang == "VIP")
                        {
                            ListCustomer = new ObservableCollection<KhachHang>(DataProvider.Instance.Database.KhachHangs.Where(x => x.MaLoaiKhachHang == "LKHVIP" && x.DaXoa == DaXoa).ToList());
                        }
                        else if (TenLoaiKhachHang == "VIPP")
                        {
                            ListCustomer = new ObservableCollection<KhachHang>(DataProvider.Instance.Database.KhachHangs.Where(x => x.MaLoaiKhachHang == "LKHVIPP" && x.DaXoa == DaXoa).ToList());
                        }
                        CountCustomer = ListCustomer.Count();
                    }
                }
            }
        }



        public string TrangThaiLamViec
        {
            get
            {
                return _trangThaiLamViec;
            }
            set
            {
                if (_trangThaiLamViec != value)
                {
                    _trangThaiLamViec = value;
                    RaisePropertyChanged("TrangThaiLamViec");
                }
            }
        }
        public string MaHoaDon
        {
            get
            {
                return _maHoaDon;
            }
            set
            {
                if (_maHoaDon != value)
                {
                    _maHoaDon = value;
                    RaisePropertyChanged("MaHoaDon");
                }
            }
        }

        public DateTime NgayXuatHoaDon
        {
            get
            {
                return _ngayXuatHoaDon;
            }
            set
            {
                if (_ngayXuatHoaDon != value)
                {
                    _ngayXuatHoaDon = value;
                    RaisePropertyChanged("NgayXuatHoaDon");
                }
            }
        }

        public DateTime NgayBatDau
        {
            get
            {
                return _ngayBatDau;
            }
            set
            {
                if (_ngayBatDau != value)
                {
                    _ngayBatDau = value;
                    RaisePropertyChanged("NgayBatDau");
                }
            }
        }
        public DateTime NgayKetThuc
        {
            get
            {
                return _ngayKetThuc;
            }
            set
            {
                if (_ngayKetThuc != value)
                {
                    _ngayKetThuc = value;
                    RaisePropertyChanged("NgayKetThuc");
                }
            }
        }
        public double TongHoaDon
        {
            get
            {
                return _tongHoaDon;
            }
            set
            {
                if (_tongHoaDon != value)
                {
                    _tongHoaDon = value;
                    RaisePropertyChanged("TongHoaDon");
                }
            }
        }



        public string MaNhanVien
        {
            get
            {
                return _maNhanVien;
            }
            set
            {
                if (_maNhanVien != value)
                {
                    _maNhanVien = value;
                    RaisePropertyChanged("MaNhanVien");
                }
            }
        }

        public string MaKhachHang
        {
            get
            {
                return _maKhachHang;
            }
            set
            {
                if (_maKhachHang != value)
                {
                    _maKhachHang = value;
                    RaisePropertyChanged("MaKhachHang");
                }
            }
        }

        private int _countCustomer;

        public int CountCustomer
        {
            get
            {
                return _countCustomer;
            }
            set
            {
                if (_countCustomer != value)
                {
                    _countCustomer = value;
                    RaisePropertyChanged("CountCustomer");
                }
            }
        }

        private int _countEmployee;

        public int CountEmployee
        {
            get
            {
                return _countEmployee;
            }
            set
            {
                if (_countEmployee != value)
                {
                    _countEmployee = value;
                    RaisePropertyChanged("CountEmployee");
                }
            }
        }

        private int _countBill;

        public int CountBill
        {
            get
            {
                return _countBill;
            }
            set
            {
                if (_countBill != value)
                {
                    _countBill = value;
                    RaisePropertyChanged("CountBill");
                }
            }
        }

        private int _countPromotion;

        public int CountPromotion
        {
            get
            {
                return _countPromotion;
            }
            set
            {
                if (_countPromotion != value)
                {
                    _countPromotion = value;
                    RaisePropertyChanged("CountPromotion");
                }
            }
        }


        private string _maLoaiKhachHang;

        public string MaLoaiKhachHang
        {
            get
            {
                return _maLoaiKhachHang;
            }
            set
            {
                if (_maLoaiKhachHang != value)
                {
                    _maLoaiKhachHang = value;
                    RaisePropertyChanged("MaLoaiKhachHang");
                }
            }
        }




        #endregion Properties

        #region Command Property
        public ICommand ReportBill { get; set; }

        public ICommand ReportPromotion { get; set; }

        public ICommand ReportCustomer { get; set; }
        public ICommand ReportEmployee { get; set; }

        

        public ICommand SelectedChangedNgayBatDau { get; set; }

        public ICommand SelectedChangedNgayKetThuc { get; set; }

        #endregion

        #region Constructor

        public HomeViewModel()
        {
            LoadCustomerList();

            LoadBillList();

            LoadEmployeeList();

            LoadCustomerTypeList();

            LoadPromotionList();

            ReportEmployee = new RelayCommand<object>((p) =>
            {
                return true;
            },
            (p) =>
            {
                InBaoCaoNhanVien reportNhanVien = new InBaoCaoNhanVien();
                reportNhanVien.Parameters["TrangThaiLamViec"].Value = _trangThaiLamViec;
                reportNhanVien.ShowPreviewDialog();
            });

            ReportCustomer = new RelayCommand<object>((p) => { return true; }, (p) => 
            {
                try
                {
                    InBaoCaoKhachHang reportKhachHang = new InBaoCaoKhachHang();
                    reportKhachHang.Parameters["LoaiKhachHang"].Value = SelectedCustomerType.TenLoaiKhachHang;
                    reportKhachHang.ShowPreviewDialog();
                }
                catch
                {
                    MessageBox.Show("Chưa chọn loại khách hàng!!");
                    return;
                }
              
            });
            


            ReportBill = new RelayCommand<object>((p) =>
            {
                return true;
            },
             (p) =>
             {
                 
                 InBaoCaoHoaDon reportHoaDon = new InBaoCaoHoaDon();
                 reportHoaDon.Parameters["_ngayBatDau"].Value = _ngayBatDau;
                 reportHoaDon.Parameters["_ngayKetThuc"].Value = _ngayKetThuc;
                 reportHoaDon.ShowPreviewDialog();

             });

            ReportPromotion = new RelayCommand<object>((p) =>
            {
                return true;
            },
             (p) =>
             {
                 
                 Export("HOADON SHEET", "THỐNG KÊ CHƯƠNG TRÌNH KHUYẾN MÃI");
             });

            SelectedChangedNgayBatDau = new RelayCommand<object>((p) =>
            {
                return true;
            },
             (p) =>
             {
                 LoadBillList(_ngayBatDau, _ngayKetThuc);


             });

            SelectedChangedNgayKetThuc = new RelayCommand<object>((p) =>
            {
                return true;
            },
             (p) =>
             {
                 LoadBillList(_ngayBatDau,_ngayKetThuc);

             });
        }

        private void LoadCustomerTypeList()
        {
            CustomerTypeList = new ObservableCollection<LoaiKhachHang>(DataProvider.Instance.Database.LoaiKhachHangs);
        }

        #endregion Constructor

        #region Methods

        public void LoadCustomerList()
        {
            ListCustomer = new ObservableCollection<KhachHang>(DataProvider.Instance.Database.KhachHangs.Where(x=>x.DaXoa == DaXoa).ToList());    

            CountCustomer = ListCustomer.Count();
        }

        public void LoadEmployeeList()
        {
            ListEmployee = new ObservableCollection<NhanVien>(DataProvider.Instance.Database.NhanViens);
            CountEmployee = ListEmployee.Count();
        }
        public void LoadPromotionList()
        {
            ListPromotion = new ObservableCollection<ChuongTrinhKhuyenMai>(DataProvider.Instance.Database.ChuongTrinhKhuyenMais);
            CountPromotion = ListPromotion.Count();
        }



        public void LoadBillList(DateTime? ngayBatDau =null, DateTime? ngayKetThuc=null)
        {
            ngayBatDau = _ngayBatDau;
            ngayKetThuc = _ngayKetThuc;
            ListBill = new ObservableCollection<HoaDon>(DataProvider.Instance.Database.HoaDons.Where(x=>
            ((x.NgayXuatHoaDon.Value.Month > ngayBatDau.Value.Month)||
            (x.NgayXuatHoaDon.Value.Month == ngayBatDau.Value.Month && x.NgayXuatHoaDon.Value.Day >= ngayBatDau.Value.Day)) && 
            ((x.NgayXuatHoaDon.Value.Month < ngayKetThuc.Value.Month) || 
            (x.NgayXuatHoaDon.Value.Month == ngayKetThuc.Value.Month) && x.NgayXuatHoaDon.Value.Day <= ngayKetThuc.Value.Day)).ToList());

            CountBill = ListBill.Count();
        }

        public void Export(string sheetName, string title)
        {

            //Tạo các đối tượng Excel

            Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();

            Microsoft.Office.Interop.Excel.Workbooks oBooks;

            Microsoft.Office.Interop.Excel.Sheets oSheets;

            Microsoft.Office.Interop.Excel.Workbook oBook;

            Microsoft.Office.Interop.Excel.Worksheet oSheet;

            //Tạo mới một Excel WorkBook 

            oExcel.Visible = true;

            oExcel.DisplayAlerts = false;

            oExcel.Application.SheetsInNewWorkbook = 1;

            oBooks = oExcel.Workbooks;

            oBook = (Microsoft.Office.Interop.Excel.Workbook)(oExcel.Workbooks.Add(Type.Missing));

            oSheets = oBook.Worksheets;

            oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oSheets.get_Item(1);

            oSheet.Name = sheetName;

            // Tạo phần đầu nếu muốn

            Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "C1");

            head.MergeCells = true;

            head.Value2 = title;

            head.Font.Bold = true;

            head.Font.Name = "Tahoma";

            head.Font.Size = "20";

            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Tạo tiêu đề cột 

            Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");

            cl1.Value2 = "Mã CTKM";

            cl1.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");

            cl2.Value2 = "Tên CTKM";

            cl2.ColumnWidth = 60.0;

            Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");

            cl3.Value2 = "Mô tả CTKM";

            cl3.ColumnWidth = 60.0;

            Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");

            cl4.Value2 = "Ngày BDKM";

            cl4.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");

            cl5.Value2 = "Ngày KTKM";

            cl5.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "E3");

            rowHead.Font.Bold = true;

            // Kẻ viền

            rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

            // Thiết lập màu nền

            rowHead.Interior.ColorIndex = 15;

            rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            int colIndex = 1;

            int rowIndex = 1;

            foreach (var item in ListPromotion)
            {
                colIndex = 1;

                // rowIndex tương ứng từng dòng dữ liệu
                rowIndex++;

                cl1.Cells[rowIndex, colIndex].Value = item.MaCTKM;
                cl2.Cells[rowIndex, colIndex].Value = item.TenCTKM;
                cl3.Cells[rowIndex, colIndex].Value = item.MoTaCTKM;
                cl4.Cells[rowIndex, colIndex].Value = item.NgayBDKM.Value.ToShortDateString();
                cl5.Cells[rowIndex, colIndex].Value = item.NgayKTKM.Value.ToShortDateString();
                
            }

        }
    }

    #endregion Methods
}