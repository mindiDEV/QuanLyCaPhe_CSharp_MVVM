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
    
    public partial class ChuongTrinhKhuyenMai
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChuongTrinhKhuyenMai()
        {
            this.ChiTietKhuyenMais = new HashSet<ChiTietKhuyenMai>();
        }
    
        public string MaCTKM { get; set; }
        public string TenCTKM { get; set; }
        public string MoTaCTKM { get; set; }
        public Nullable<System.DateTime> NgayBDKM { get; set; }
        public Nullable<System.DateTime> NgayKTKM { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietKhuyenMai> ChiTietKhuyenMais { get; set; }
    }
}