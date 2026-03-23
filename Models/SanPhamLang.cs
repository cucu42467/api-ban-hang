using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SanPhamLang
    {
        [Key, Column(Order = 0)]
        public int IdSanPham { get; set; }

        [Key, Column(Order = 1)]
        public string MaNgonNgu { get; set; }

        public string TenSanPham { get; set; }

        public string MoTa { get; set; }
    }
}
