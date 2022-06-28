using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace QuanLySach
{
    public partial class HHContextDB : DbContext
    {
        public HHContextDB()
            : base("name=HHContextDB")
        {
        }

        public virtual DbSet<HangHoa> HangHoas { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<LoaiHang> LoaiHangs { get; set; }
        public virtual DbSet<LoginNV> LoginNVs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HangHoa>()
                .Property(e => e.TenHang)
                .IsFixedLength();

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.TenHang)
                .IsFixedLength();
        }
    }
}
