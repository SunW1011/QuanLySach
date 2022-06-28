namespace QuanLySach
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        [Key]
        [StringLength(50)]
        public string MaHD { get; set; }

        [Required]
        [StringLength(50)]
        public string TenKhachHang { get; set; }

        public DateTime NgayLap { get; set; }

        public DateTime NgayGiao { get; set; }

        [Required]
        [StringLength(100)]
        public string NhanVien { get; set; }

        [Required]
        [StringLength(50)]
        public string TinhTrang { get; set; }

        [StringLength(20)]
        public string TenHang { get; set; }

        public int SoLuong { get; set; }
    }
}
