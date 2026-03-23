using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ChiTietLayHang
    {
        [Key]
        public int IdChiTiet { get; set; }

        public string MaChiTiet { get; set; }

        public int IdPhieu { get; set; }

        public int IdBienThe { get; set; }

        public int SoLuong { get; set; }
    }
}
