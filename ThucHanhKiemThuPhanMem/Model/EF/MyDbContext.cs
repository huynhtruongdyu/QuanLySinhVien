using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThucHanhKiemThuPhanMem.Model.EF
{
    public class MyDbContext :DbContext
    {
        public MyDbContext() : base()
        {
            string dbName = "ThucHanhKiemThu";
            this.Database.Connection.ConnectionString = "Data Source=.;Initial Catalog="+dbName+";Integrated Security=true";
        }
        public DbSet<TaiKhoan> TaiKhoan { get; set; }
        public DbSet<ThamSo> ThamSo { get; set; }
        public DbSet<Nganh> Nganh { get; set; }
        public DbSet<GiangVien> GiangVien { get; set; }
        public DbSet<MonHoc> MonHoc { get; set; }
        public DbSet<SinhVien> SinhVien { get; set; }
        public DbSet<Lop> Lop { get; set; }
        public DbSet<ThamGia> ThamGia { get; set; }

    }
}
