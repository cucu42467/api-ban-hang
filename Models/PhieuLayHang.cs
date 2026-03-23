using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PhieuLayHang
    {
        [Key]
        public int IdPhieu { get; set; }

        public string MaPhieu { get; set; }

        public string NgayLay { get; set; }

        public int IdNguoiTao { get; set; }

        public int? IdTrangThai { get; set; }

        public string GhiChu { get; set; }
    }
}
