namespace QuanLySach
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sach")]
    public partial class Sach
    {
        [Key]
        [StringLength(200)]
        public string MaSach { get; set; }

        [Required]
        [StringLength(20)]
        public string TenSach { get; set; }

        public int DonGia { get; set; }

        public int SoLuong { get; set; }

        public int? MaLoai { get; set; }

        public int ThanhTien { get; set; }

        public virtual LoaiSach LoaiSach { get; set; }
    }
}
