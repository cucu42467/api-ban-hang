using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class VaiTro
    {
        [Key]
        public int IdVaiTro { get; set; }

        public string MaVaiTro { get; set; }

        public string TenVaiTro { get; set; }

        public string MoTa { get; set; }
    }
}
