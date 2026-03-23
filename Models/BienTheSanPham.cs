using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BienTheSanPham
    {
        [Key]
        public int IdBienThe { get; set; }

        public string MaBienThe { get; set; }

        public int IdSanPham { get; set; }

        public double GiaBan { get; set; }

        public int TonKho { get; set; }

        public string MaSKU { get; set; }

        public int? IdTrangThai { get; set; }
    }
}
