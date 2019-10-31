using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThucHanhKiemThuPhanMem.Model.EF;

namespace ThucHanhKiemThuPhanMem.Model.DAO
{
    public static class Helper
    {
        static MyDbContext db;
        public static bool InitializedNganh()
        {
            db = new MyDbContext();
            try
            {
                Nganh CN = new Nganh()
                {
                    MaNganh = "CN",
                    TenNganh = "Công nghệ thông tin",
                    DiemChuan = 20
                };
                Nganh QTKD = new Nganh()
                {
                    MaNganh = "QT",
                    TenNganh = "Quản trị kinh doanh",
                    DiemChuan = 19
                };
                Nganh NN = new Nganh()
                {
                    MaNganh = "NN",
                    TenNganh = "Ngoại ngữ",
                    DiemChuan = 19
                };
                Nganh DL = new Nganh()
                {
                    MaNganh = "DL",
                    TenNganh = "Du lịch",
                    DiemChuan = 19
                };

                db.Nganh.Add(CN);
                db.Nganh.Add(QTKD);
                db.Nganh.Add(NN);
                db.Nganh.Add(DL);
                db.SaveChanges();
                return true;
            } catch(Exception e) { return false; }
        }
        public static bool InitializedGiangVien()
        {
            db = new MyDbContext();
            try
            {
                for (int i = 1; i < 13; i++)
                {
                    GiangVien gv = new GiangVien();
                    gv.MaGiangVien = "GV_"+i.ToString();
                    gv.TenGiangVien = "Giảng Vien "+i.ToString();
                    db.GiangVien.Add(gv);
                }
                

                db.SaveChanges();
                return true;
            }
            catch (Exception e) { return false; }
        }
        public static bool InitializedThamSo()
        {
            db = new MyDbContext();
            try
            {
                ThamSo ts1 = new ThamSo();
                ts1.TenThamSo = "Sỉ số lớp tối đa";
                ts1.GiaTri = 40.ToString();

                ThamSo ts2 = new ThamSo();
                ts2.TenThamSo = "Số môn tối đa mỗi ngành";
                ts2.GiaTri = 3.ToString();

                ThamSo ts3 = new ThamSo();
                ts3.TenThamSo = "MSSV";
                ts3.GiaTri = 1.ToString();

                ThamSo ts4 = new ThamSo();
                ts4.TenThamSo = "Lop";
                ts4.GiaTri = 1.ToString();

                db.ThamSo.Add(ts1);
                db.ThamSo.Add(ts2);
                db.ThamSo.Add(ts3);
                db.ThamSo.Add(ts4);

                db.SaveChanges();
                return true;
            }
            catch (Exception e) { return false; }
        }
        public static bool IntitialzedSinhVien()
        {
            db = new MyDbContext();
            List<Nganh> nganhs = db.Nganh.ToList();
            try
            {
                for (int i = 0; i < nganhs.Count; i++)
                {
                    for (int j = 1; j < 50; j++)
                    {
                        SinhVien sv = new SinhVien();
                        sv.MaSinhVien = getMSSV(nganhs[i].MaNganh);
                        sv.TenSinhVien = "Tui là sinh viên " + j;
                        if (j % 2 == 0)
                            sv.GioiTinh = false;
                        else
                            sv.GioiTinh = true;
                        sv.NgaySinh = DateTime.Parse(DateTime.Now.Date.ToShortDateString());
                        sv.TruongTHPT = "Trường TPHT " + i;
                        sv.MaNganh = db.Nganh.ToList()[i].MaNganh;
                        sv.DiemChuan = db.Nganh.ToList()[i].DiemChuan;
                        sv.DiemThi = 21;
                        db.SinhVien.Add(sv);
                        db.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception e) { return false; }
        }
        public static bool IntitialzedMonHoc()
        {
            db = new MyDbContext();
            try
            {
                List<Nganh> nganhs = db.Nganh.ToList();
                for (int i = 0; i < db.Nganh.Count(); i++)
                {
                    for (int j = 1; j < 4; j++)
                    {
                        MonHoc mh = new MonHoc();
                        mh.MaMonHoc = "MH" + nganhs[i].MaNganh + j;
                        mh.TenMon = "Môn học " + (j+1);
                        mh.MaNganh = nganhs[i].MaNganh;
                        db.MonHoc.Add(mh);
                        db.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception e) { return false; }
        }


        public static string getMSSV(string maNganh)
        {
            db = new MyDbContext();
            ThamSo ts = db.ThamSo.Find(3);
            int stt = int.Parse(db.ThamSo.Find(3).GiaTri);
            DateTime now = DateTime.Now;
            string khoa = now.Year.ToString().Substring(2);

            string sttAfterChanged = (stt+1).ToString();
            string sttResult = "";
            for (int i = 1; i <= 6- sttAfterChanged.Length; i++)
            {
                sttResult += 0;
            }
            sttResult += sttAfterChanged;
            ts.GiaTri = sttResult;
            db.SaveChanges();

            return maNganh + khoa + db.ThamSo.Find(3).GiaTri;

        }

    }
}
