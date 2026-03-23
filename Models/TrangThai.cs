using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TrangThai
    {
        [Key]
        public int IdTrangThai { get; set; }

        public string MaTrangThai { get; set; }

        public string TenTrangThai { get; set; }

        public string MoTa { get; set; }

        public int? IdLoaiTrangThai { get; set; }
    }
}
