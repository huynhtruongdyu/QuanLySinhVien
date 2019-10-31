using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThucHanhKiemThuPhanMem.Model.EF;

namespace ThucHanhKiemThuPhanMem.Model.DAO
{
    public class LopDAO
    {
        MyDbContext db;
        List<Lop> lops;
        public LopDAO()
        {
            db = new MyDbContext();
        }
        public List<Lop> GetLops()
        {
            
            if (lops == null)
            {
                db = new MyDbContext();
                lops = db.Lop.ToList();
                return lops;
            }
            
            return lops;
        }
        public List<Lop> GetLops(string mamonhoc)
        {
            List<Lop> list = new List<Lop>() ;
            db = new MyDbContext();
            var obj = db.Lop.Where(x => x.MaMonHoc == mamonhoc);
            foreach (var item in obj)
            {
                Lop lop = item as Lop;
                list.Add(lop);
            }
            return list;
        }
        public bool Add(Lop lop)
        {
            db = new MyDbContext();
            if (db.Lop.Any(x => x.MaLop == lop.MaLop))
                return false;
            else if (String.IsNullOrEmpty(lop.TenLop) || lop.TenLop.Trim() == "Nhập tên lớp")
                return false;
            else if (String.IsNullOrEmpty(lop.MaGiangVien))
                return false;
            else if (String.IsNullOrEmpty(lop.MaMonHoc))
                return false;
            else
            {
                db.Lop.Add(lop);
                db.SaveChanges();
                return true;
            }
        }
        public bool Remove(string maLop)
        {
            Lop lop = db.Lop.Find(maLop);
            if (lop != null)
            {
                List<ThamGia> thamGias = db.ThamGia.Where(x => x.MaLop == maLop).ToList();
                if (thamGias.Count != 0)
                {
                    foreach (ThamGia thamGia in thamGias)
                    {
                        db.ThamGia.Remove(thamGia);
                        db.SaveChanges();
                    }
                }
                db.Lop.Remove(lop);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Update(Lop lop)
        {
            if(lop!=null)
            {
                if (String.IsNullOrEmpty(lop.MaLop))
                    return false;
                else if (String.IsNullOrEmpty(lop.TenLop))
                    return false;
                else if (String.IsNullOrEmpty(lop.MaGiangVien))
                    return false;
                else if (String.IsNullOrEmpty(lop.MaMonHoc))
                    return false;
                else
                {
                    MyDbContext db = new MyDbContext();
                    Lop newLop = db.Lop.Find(lop.MaLop);
                    newLop.MaLop = lop.MaLop;
                    newLop.TenLop = lop.TenLop;
                    newLop.MaGiangVien = lop.MaGiangVien;
                    newLop.MaMonHoc = lop.MaMonHoc;
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
