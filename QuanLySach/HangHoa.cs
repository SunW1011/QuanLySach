namespace QuanLySach
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HangHoa")]
    public partial class HangHoa
    {
        [Key]
        [StringLength(200)]
        public string MaHang { get; set; }

        [Required]
        [StringLength(20)]
        public string TenHang { get; set; }

        public int DonGia { get; set; }

        public int SoLuong { get; set; }

        public int? MaLoai { get; set; }

        public int ThanhTien { get; set; }

        public virtual LoaiHang LoaiHang { get; set; }
    }
}
