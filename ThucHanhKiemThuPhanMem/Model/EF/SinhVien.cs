using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThucHanhKiemThuPhanMem.Model.EF
{
    public class SinhVien
    {
        [Key]
        public string MaSinhVien { get; set; }
        [Required]
        public string TenSinhVien { get; set; }
        public DateTime NgaySinh { get; set; }
        public bool GioiTinh { get; set; }
        public string TruongTHPT { get; set; }
        public string MaNganh { get; set; }
        public double DiemChuan { get; set; }
        public double DiemThi { get; set; }


        public virtual Nganh Nganh { get; set; }

        public ICollection<ThamGia> ThamGias { get; set; }

    }
}
