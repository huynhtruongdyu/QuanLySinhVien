using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThucHanhKiemThuPhanMem.Model.EF;

namespace ThucHanhKiemThuPhanMem.Model.DAO
{
    public class SinhVienVMM
    {
        public string MaSinhVien { get; set; }
        public string TenSinhVien { get; set; }
        public DateTime NgaySinh { get; set; }
        public bool GioiTinh { get; set; }
        public string MaNganh { get; set; }
        public string TenMonHoc { get; set; }
        public string TenLop { get; set; }
        public float Diem { get; set; }
        public SinhVienVMM(SinhVien sv, string tenmon="z", string tenlop="z", float diem = 0)
        {
            this.MaSinhVien = sv.MaSinhVien;
            this.TenSinhVien = sv.TenSinhVien;
            this.NgaySinh = sv.NgaySinh;
            this.GioiTinh = sv.GioiTinh;
            this.MaNganh = sv.MaNganh;
            this.TenMonHoc = tenmon;
            this.TenLop = tenlop;
            this.Diem = diem;
        }
    }
}
