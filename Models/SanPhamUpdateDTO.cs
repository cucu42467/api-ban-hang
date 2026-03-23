using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SanPhamUpdateDTO
    {
        public int IdSanPham { get; set; }

        public string MaSanPham { get; set; }

        public int IdDanhMuc { get; set; }

        public int IdTrangThai { get; set; }

        public string TenSanPham { get; set; }

        public string MoTa { get; set; }
    }
}
