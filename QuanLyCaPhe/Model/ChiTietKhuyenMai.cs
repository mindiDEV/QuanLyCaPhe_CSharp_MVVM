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
    
    public partial class ChiTietKhuyenMai
    {
        public string MaChiTietKM { get; set; }
        public Nullable<float> GiamGia { get; set; }
        public string SanPhamTang { get; set; }
        public string MaCTKM { get; set; }
        public string MaMon { get; set; }
        public Nullable<int> DieuKien { get; set; }
    
        public virtual ChuongTrinhKhuyenMai ChuongTrinhKhuyenMai { get; set; }
        public virtual ThucDon ThucDon { get; set; }
    }
}