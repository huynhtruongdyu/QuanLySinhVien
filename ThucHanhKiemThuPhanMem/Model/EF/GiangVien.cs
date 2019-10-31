using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThucHanhKiemThuPhanMem.Model.EF
{
    public class GiangVien
    {
        [Key]
        public string MaGiangVien { get; set; }

        [Required]
        public string TenGiangVien { get; set; }

        public ICollection<Lop> Lops { get; set; }
    }
}
