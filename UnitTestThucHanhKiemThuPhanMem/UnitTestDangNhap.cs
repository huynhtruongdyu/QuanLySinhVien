using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThucHanhKiemThuPhanMem.Model;
using ThucHanhKiemThuPhanMem.Model.DAO;
using ThucHanhKiemThuPhanMem.Model.EF;

namespace UnitTestThucHanhKiemThuPhanMem
{
    [TestClass]
    public class UnitTestDangNhap
    {
        [TestMethod]
        public void TestDangNhapDung()
        {
            string username = "aaaaaa";
            string pass = "aaaaaaA1!";
            MyDbContext db = new MyDbContext();
            Assert.AreEqual(db.TaiKhoan.Any(x => x.Username == username && x.Password == pass), true);
        }

        [TestMethod]
        public void TestDangNhapSai()
        {
            string username = "aaa";
            string pass = "aaaaaaA1!";
            MyDbContext db = new MyDbContext();
            Assert.AreEqual(db.TaiKhoan.Any(x => x.Username == username && x.Password == pass), false);
        }
    }
}
