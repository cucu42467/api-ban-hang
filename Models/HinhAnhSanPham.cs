using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class HinhAnhSanPham
    {
        [Key]
        public int IdHinhAnh { get; set; }

        public string MaHinhAnh { get; set; }

        public int? IdSanPham { get; set; }

        public int? IdBienThe { get; set; }

        public string DuongDanAnh { get; set; }

        public bool LaAnhChinh { get; set; }

        public int ThuTu { get; set; }
    }
}
