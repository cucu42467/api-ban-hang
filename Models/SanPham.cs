using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SanPham
    {
        [Key]
        public int IdSanPham { get; set; }

        public string MaSanPham { get; set; }

        public int? IdDanhMuc { get; set; }

        public int? IdTrangThai { get; set; }

        public DateTime NgayTao { get; set; }
    }
}
