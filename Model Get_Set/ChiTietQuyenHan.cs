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
    
    public partial class ChiTietQuyenHan
    {
        public string MaChiTietQuyenHan { get; set; }
        public string MaHanhDong { get; set; }
        public string TenHanhDong { get; set; }
        public string MaQuyenHan { get; set; }
    
        public virtual QuyenHan QuyenHan { get; set; }
    }
}
