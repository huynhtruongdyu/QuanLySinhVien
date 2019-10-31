using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThucHanhKiemThuPhanMem.Model.EF;

namespace ThucHanhKiemThuPhanMem.Model.DAO
{
    public class SinhVienDAO
    {
        MyDbContext db;
        List<SinhVien> sv;
        List<SinhVien> svNganh;
        public SinhVienDAO()
        {
            db = new MyDbContext();
        }
        public List<SinhVien> GetSinhViens()
        {
            if (sv == null)
            {
                db = new MyDbContext();
                sv = db.SinhVien.OrderBy(x => x.TenSinhVien).ToList();
                return sv;
            }
            return sv;
        }
        public List<SinhVien> GetSinhViens(string maNganh)
        {
            if (svNganh == null)
            {
                db = new MyDbContext();
                svNganh = db.SinhVien.Where(x=>x.MaNganh==maNganh).ToList();
                return svNganh;
            }
            return svNganh;
        }

        public bool Add(SinhVien sv)
        {
            db = new MyDbContext();
            if (db.SinhVien.Any(x => x.MaSinhVien == sv.MaSinhVien))
                return false;
            else if (String.IsNullOrEmpty(sv.MaSinhVien))
                return false;
            else if (String.IsNullOrEmpty(sv.TenSinhVien))
                return false;
            else if (DateTime.Now.Year - sv.NgaySinh.Year < 19)
                return false;
            else if (String.IsNullOrEmpty(sv.TruongTHPT))
                return false;
            else if (String.IsNullOrEmpty(sv.MaNganh))
                return false;
            else if (String.IsNullOrEmpty(sv.DiemChuan.ToString()))
                return false;
            else if (String.IsNullOrEmpty(sv.DiemThi.ToString()))
                return false;
            else if (sv.DiemThi < sv.DiemChuan || sv.DiemThi > 30)
                return false;
            else
            {
                db.SinhVien.Add(sv);
                db.SaveChanges();
                return true;
            }
        }
        public bool Update(SinhVien sv)
        {
            if (sv != null)
            {
                if (String.IsNullOrEmpty(sv.MaSinhVien))
                    return false;
                else if (String.IsNullOrEmpty(sv.TenSinhVien))
                    return false;
                else if (DateTime.Now.Year - sv.NgaySinh.Year < 19)
                    return false;
                else if (String.IsNullOrEmpty(sv.TruongTHPT))
                    return false;
                else if (String.IsNullOrEmpty(sv.MaNganh))
                    return false;
                else if (String.IsNullOrEmpty(sv.DiemChuan.ToString()))
                    return false;
                else if (String.IsNullOrEmpty(sv.DiemThi.ToString()))
                    return false;
                else if (sv.DiemThi < sv.DiemChuan || sv.DiemThi > 30)
                    return false;
                else
                {
                    SinhVien newSV = db.SinhVien.Find(sv.MaSinhVien);
                    newSV.TenSinhVien = sv.TenSinhVien;
                    newSV.GioiTinh = sv.GioiTinh;
                    newSV.NgaySinh = sv.NgaySinh;
                    newSV.TruongTHPT = sv.TruongTHPT;
                    newSV.MaNganh = sv.MaNganh;
                    newSV.DiemChuan = sv.DiemChuan;
                    newSV.DiemThi = sv.DiemThi;
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        public bool Remove(string mssv)
        {
            SinhVien sv = db.SinhVien.Find(mssv);
            if(sv!=null)
            {
                List<ThamGia> listThamGia = db.ThamGia.Where(x => x.MaSinhVien == mssv).ToList();
                if (listThamGia.Count != 0)
                {
                    foreach (ThamGia thamGia in listThamGia)
                    {
                        db.ThamGia.Remove(thamGia);
                        db.SaveChanges();
                    }
                }
                db.SinhVien.Remove(sv);
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
