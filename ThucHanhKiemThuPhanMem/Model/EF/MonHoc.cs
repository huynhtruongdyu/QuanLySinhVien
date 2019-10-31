using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThucHanhKiemThuPhanMem.Model.EF
{
    public class MonHoc
    {
        [Key]
        public string MaMonHoc { get; set; }

        [Required]
        public string TenMon { get; set; }

        public string MaNganh { get; set; }

        public virtual Nganh Nganh { get; set; }
    }
}
