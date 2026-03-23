using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class LoaiTrangThai
    {
        [Key]
        public int IdLoaiTrangThai { get; set; }

        public string MaLoaiTrangThai { get; set; }

        public string TenLoaiTrangThai { get; set; }

        public string MoTa { get; set; }
    }
}
