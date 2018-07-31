﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyCaPhe.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class QuanLyCaPheEntities1 : DbContext
    {
        public QuanLyCaPheEntities1()
            : base("name=QuanLyCaPheEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Ban> Bans { get; set; }
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DbSet<ChiTietKhuyenMai> ChiTietKhuyenMais { get; set; }
        public virtual DbSet<ChiTietQuyenHan> ChiTietQuyenHans { get; set; }
        public virtual DbSet<ChuongTrinhKhuyenMai> ChuongTrinhKhuyenMais { get; set; }
        public virtual DbSet<DonViTinh> DonViTinhs { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<LoaiKhachHang> LoaiKhachHangs { get; set; }
        public virtual DbSet<LoaiThucDon> LoaiThucDons { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<NhatKyDangNhap> NhatKyDangNhaps { get; set; }
        public virtual DbSet<NhomThucDon> NhomThucDons { get; set; }
        public virtual DbSet<QuanHeNguoiDung_QuyenHan> QuanHeNguoiDung_QuyenHan { get; set; }
        public virtual DbSet<QuyenHan> QuyenHans { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<ThucDon> ThucDons { get; set; }
    
        public virtual ObjectResult<InPhieuThanhToan_Result> InPhieuThanhToan(string maHoaDon)
        {
            var maHoaDonParameter = maHoaDon != null ?
                new ObjectParameter("MaHoaDon", maHoaDon) :
                new ObjectParameter("MaHoaDon", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<InPhieuThanhToan_Result>("InPhieuThanhToan", maHoaDonParameter);
        }
    
        public virtual ObjectResult<TimKiemThucDon_Result> TimKiemThucDon(string tenMon)
        {
            var tenMonParameter = tenMon != null ?
                new ObjectParameter("TenMon", tenMon) :
                new ObjectParameter("TenMon", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TimKiemThucDon_Result>("TimKiemThucDon", tenMonParameter);
        }
    
        public virtual ObjectResult<InBaoCaoHoaDon_Result> InBaoCaoHoaDon(Nullable<System.DateTime> ngayBatDau, Nullable<System.DateTime> ngayKetThuc)
        {
            var ngayBatDauParameter = ngayBatDau.HasValue ?
                new ObjectParameter("NgayBatDau", ngayBatDau) :
                new ObjectParameter("NgayBatDau", typeof(System.DateTime));
    
            var ngayKetThucParameter = ngayKetThuc.HasValue ?
                new ObjectParameter("NgayKetThuc", ngayKetThuc) :
                new ObjectParameter("NgayKetThuc", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<InBaoCaoHoaDon_Result>("InBaoCaoHoaDon", ngayBatDauParameter, ngayKetThucParameter);
        }
    
        public virtual ObjectResult<InBaoCaoNhanVien_Result> InBaoCaoNhanVien(string trangThaiLamViec)
        {
            var trangThaiLamViecParameter = trangThaiLamViec != null ?
                new ObjectParameter("TrangThaiLamViec", trangThaiLamViec) :
                new ObjectParameter("TrangThaiLamViec", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<InBaoCaoNhanVien_Result>("InBaoCaoNhanVien", trangThaiLamViecParameter);
        }
    
        public virtual ObjectResult<InBaoCaoKhachHang_Result> InBaoCaoKhachHang(string tenLoaiKhachHang)
        {
            var tenLoaiKhachHangParameter = tenLoaiKhachHang != null ?
                new ObjectParameter("TenLoaiKhachHang", tenLoaiKhachHang) :
                new ObjectParameter("TenLoaiKhachHang", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<InBaoCaoKhachHang_Result>("InBaoCaoKhachHang", tenLoaiKhachHangParameter);
        }
    }
}
