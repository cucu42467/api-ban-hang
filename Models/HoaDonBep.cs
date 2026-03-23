using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class HoaDonBep
    {
        [Key]
        public int IdHoaDon { get; set; }

        public string MaHoaDon { get; set; }

        public int IdBep { get; set; }

        public DateTime NgayXuat { get; set; }

        public double TongTien { get; set; }

        public int? IdTrangThai { get; set; }
    }
}
