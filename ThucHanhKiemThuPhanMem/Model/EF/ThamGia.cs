using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThucHanhKiemThuPhanMem.Model.EF
{
    public class ThamGia
    {
        [Key]
        [Column(Order = 1)]
        public string MaLop { get; set; }
        [Key]
        [Column(Order = 2)]
        public string MaSinhVien { get; set; }

        public float Diem { get; set; }

        public virtual Lop Lop { get; set; }
        public virtual TaiKhoan SinhVien { get; set; }
    }
}

