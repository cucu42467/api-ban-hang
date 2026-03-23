using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SanPhamCreateDTO
    {
        public string MaSanPham { get; set; }

        public int IdDanhMuc { get; set; }

        public string TenSanPham { get; set; }

        public string MoTa { get; set; }
    }
}
