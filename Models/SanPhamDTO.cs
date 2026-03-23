using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SanPhamDTO
    {
        public int IdSanPham { get; set; }
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string MoTa { get; set; }

        public string TenDanhMuc { get; set; }

        public double GiaBan { get; set; }
        public int TonKho { get; set; }

        public string MaSKU { get; set; }
    }
}
