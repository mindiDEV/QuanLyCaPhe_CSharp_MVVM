//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class ThucDon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ThucDon()
        {
            this.ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
            this.ChiTietKhuyenMais = new HashSet<ChiTietKhuyenMai>();
        }
    
        public string MaMon { get; set; }
        public string TenMon { get; set; }
        public Nullable<decimal> GiaBan { get; set; }
        public byte[] HinhDaiDien { get; set; }
        public string GhiChu { get; set; }
        public string MaDonViTinh { get; set; }
        public string MaNhomThucDon { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietKhuyenMai> ChiTietKhuyenMais { get; set; }
        public virtual DonViTinh DonViTinh { get; set; }
        public virtual NhomThucDon NhomThucDon { get; set; }
    }
}