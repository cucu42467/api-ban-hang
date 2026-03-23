using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DanhMuc
    {
        [Key]
        public int IdDanhMuc { get; set; }

        public string MaDanhMuc { get; set; }

        public int? IdTrangThai { get; set; }

        public string TenDanhMuc { get; set; }
    }

    public class DanhMucDTO
    {
        [Key]
        public int IdDanhMuc { get; set; }

        public string MaDanhMuc { get; set; }

        public int? IdTrangThai { get; set; }

        public string TenDanhMuc { get; set; }

        public string TenTrangThai { get; set; }
    }
}
