namespace QuanLySach
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoginNV")]
    public partial class LoginNV
    {
        public int ID { get; set; }

        [StringLength(200)]
        public string Username { get; set; }

        public DateTime TimeLogin { get; set; }

        public DateTime TimeLogout { get; set; }
    }
}
