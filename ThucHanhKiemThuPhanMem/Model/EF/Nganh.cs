using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThucHanhKiemThuPhanMem.Model.EF
{
    public class Nganh
    {
        [Key]
        public string MaNganh { get; set; }

        [Required]
        public string TenNganh { get; set; }
        public float DiemChuan { get; set; }

        public ICollection<MonHoc> MonHocs { get; set; }
    }
}
