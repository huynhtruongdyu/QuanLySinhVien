using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThucHanhKiemThuPhanMem.Model.EF
{
    public class Lop
    {
        [Key]
        public string MaLop { get; set; }
        public string TenLop { get; set; }
        public string MaMonHoc { get; set; }
        public string MaGiangVien { get; set; }

        public virtual GiangVien GiangVien { get; set; }
        public virtual MonHoc MonHoc { get; set; }

        public ICollection<ThamGia> ThamGias { get; set; }

    }
}
