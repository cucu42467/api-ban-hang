using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class NhatKyHeThong
    {
        [Key]
        public int IdNhatKy { get; set; }

        public string MaNhatKy { get; set; }

        public int IdNguoiDung { get; set; }

        public string HanhDong { get; set; }

        public string ThoiGian { get; set; }
    }
}
