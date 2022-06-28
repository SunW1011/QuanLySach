namespace QuanLySach
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(200)]
        public string Username { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(200)]
        public string Password { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(200)]
        public string HoTen { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(200)]
        public string DiaChi { get; set; }

        [StringLength(200)]
        public string LoaiTK { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(10)]
        public string SDT { get; set; }

        [StringLength(200)]
        public string Email { get; set; }
    }
}
