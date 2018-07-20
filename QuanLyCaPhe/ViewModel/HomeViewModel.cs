using Microsoft.Win32;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using QuanLyCaPhe.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
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

        private string _maHoaDon;
        private DateTime _ngayXuatHoaDon;
        private double _tongHoaDon;
        private string _maNhanVien;
        private string _maKhachHang;
        private DateTime _ngayBatDau = DateTime.Now.Date.AddDays(-1);
        private DateTime _ngayKetThuc = DateTime.Now;

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

        
        #endregion Properties

        #region Command Property
        public ICommand ReportBill { get; set; }

        public ICommand SelectedChangedNgayBatDau { get; set; }

        public ICommand SelectedChangedNgayKetThuc { get; set; }

        #endregion

        #region Constructor

        public HomeViewModel()
        {
            LoadCustomerList();

            LoadBillList();

            LoadEmployeeList();

            ReportBill = new RelayCommand<object>((p) =>
            {
                return true;
            },
             (p) =>
             {
                 string filePath = "";
                 // tạo SaveFileDialog để lưu file excel
                 SaveFileDialog dialog = new SaveFileDialog();

                 // chỉ lọc ra các file có định dạng Excel
                 dialog.Filter = "Excel | *.xlsx | Excel 2003 | *.xls";

                 // Nếu mở file và chọn nơi lưu file thành công sẽ lưu đường dẫn lại dùng
                 if (dialog.ShowDialog() == true)
                 {
                     filePath = dialog.FileName;
                 }

                 // nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm
                 if (string.IsNullOrEmpty(filePath))
                 {
                     MessageBox.Show("Đường dẫn báo cáo không hợp lệ");
                     return;
                 }

                 try
                 {
                     using (ExcelPackage x = new ExcelPackage())
                     {
                         // đặt tên người tạo file
                         x.Workbook.Properties.Author = "Duy Nguyen Developer";

                         // đặt tiêu đề cho file
                         x.Workbook.Properties.Title = "Báo cáo thống kê";

                         //Tạo một sheet để làm việc trên đó
                         x.Workbook.Worksheets.Add("HoaDon Sheet");

                         // lấy sheet vừa add ra để thao tác
                         ExcelWorksheet ws = x.Workbook.Worksheets[1];

                         // đặt tên cho sheet
                         ws.Name = "HoaDon Sheet";
                         // fontsize mặc định cho cả sheet
                         ws.Cells.Style.Font.Size = 12;
                         // font family mặc định cho cả sheet
                         ws.Cells.Style.Font.Name = "Calibri";

                         // Tạo danh sách các column header
                         string[] arrColumnHeader = 
                         {
                                                "Mã hoá đơn",
                                                "Ngày xuất hoá đơn",
                                                "Tổng hoá đơn",
                                                "Mã nhân viên",
                                                "Mã khách hàng",
                         };

                         // lấy ra số lượng cột cần dùng dựa vào số lượng header
                         var countColHeader = arrColumnHeader.Count();

                         // merge các column lại từ column 1 đến số column header
                         // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                         ws.Cells[1, 1].Value = "THỐNG KÊ THÔNG TIN HOÁ ĐƠN";
                         ws.Cells[1, 1, 1, countColHeader].Merge = true;
                         // in đậm
                         ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;
                         // căn giữa
                         ws.Cells[1, 1, 1, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                         int colIndex = 1;
                         int rowIndex = 2;

                         //tạo các header từ column header đã tạo từ bên trên
                         foreach (var item in arrColumnHeader)
                         {
                             var cell = ws.Cells[rowIndex, colIndex];

                             //set màu thành gray
                             var fill = cell.Style.Fill;
                             fill.PatternType = ExcelFillStyle.Solid;
                             fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

                             //căn chỉnh các border
                             var border = cell.Style.Border;
                             border.Bottom.Style =
                                 border.Top.Style =
                                 border.Left.Style =
                                 border.Right.Style = ExcelBorderStyle.Thin;

                             //gán giá trị
                             cell.Value = item;

                             colIndex++;
                         }

      
                         // với mỗi item trong danh sách sẽ ghi trên 1 dòng
                         foreach (var item in ListBill)
                         {
                             // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                             colIndex = 1;

                             // rowIndex tương ứng từng dòng dữ liệu
                             rowIndex++;

                             //gán giá trị cho từng cell                      
                             ws.Cells[rowIndex, colIndex++].Value = item.MaHoaDon;

                             ws.Cells[rowIndex, colIndex++].Value = item.NgayXuatHoaDon.Value.ToShortDateString();

                             ws.Cells[rowIndex, colIndex++].Value = item.TongHoaDon;

                             ws.Cells[rowIndex, colIndex++].Value = item.NhanVien.TenNhanVien;

                             ws.Cells[rowIndex, colIndex++].Value = item.KhachHang.TenKhachHang;

                         }

                         //Lưu file lại
                         Byte[] bin = x.GetAsByteArray();
                         File.WriteAllBytes(filePath, bin);
                     }
                     MessageBox.Show("Xuất excel thành công!");
                 }
                 catch (Exception EE)
                 {
                     MessageBox.Show("Có lỗi khi lưu file!",EE.Message);
                 }
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

        #endregion Constructor
        
        #region Methods

        public void LoadCustomerList()
        {
            ListCustomer = new ObservableCollection<KhachHang>(DataProvider.Instance.Database.KhachHangs);
            CountCustomer = ListCustomer.Count();
        }

        public void LoadEmployeeList()
        {
            ListEmployee = new ObservableCollection<NhanVien>(DataProvider.Instance.Database.NhanViens);
            CountEmployee = ListEmployee.Count();
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

        #endregion Methods
    }
}