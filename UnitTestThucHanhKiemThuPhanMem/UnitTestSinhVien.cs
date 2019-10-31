using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThucHanhKiemThuPhanMem.Model.DAO;
using ThucHanhKiemThuPhanMem.Model.EF;

namespace UnitTestThucHanhKiemThuPhanMem
{
    [TestClass]
    public class UnitTestSinhVien
    {
        [TestMethod]
        public void TestThemSinhV()
        {
            using(MyDbContext db= new MyDbContext())
            {
                SinhVien sv = new SinhVien();
                sv.MaSinhVien = Helper.getMSSV("CN");
                sv.TenSinhVien = "test";
                sv.NgaySinh = DateTime.Parse("12/31/1997");
                sv.GioiTinh = false;
                sv.TruongTHPT = "test";
                sv.MaNganh = "CN";
                sv.DiemChuan = db.Nganh.Find("CN").DiemChuan;
                sv.DiemThi = db.Nganh.Find("CN").DiemChuan + 1;

                SinhVienDAO dao = new SinhVienDAO();
                Assert.AreEqual(dao.Add(sv), true);
            }
        }
        [TestMethod]
        public void TestXoaSinhV()
        {
            using (MyDbContext db = new MyDbContext())
            {
                SinhVien sv = new SinhVien();
                sv.MaSinhVien = Helper.getMSSV("CN");
                sv.TenSinhVien = "test";
                sv.NgaySinh = DateTime.Parse("12/31/1997");
                sv.GioiTinh = false;
                sv.TruongTHPT = "test";
                sv.MaNganh = "CN";
                sv.DiemChuan = db.Nganh.Find("CN").DiemChuan;
                sv.DiemThi = db.Nganh.Find("CN").DiemChuan + 1;

                SinhVienDAO dao = new SinhVienDAO();
                Assert.AreEqual(dao.Add(sv), true);
            }

        }
    }
}
