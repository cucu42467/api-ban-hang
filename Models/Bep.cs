using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Bep
    {
        [Key]
        public int IdBep { get; set; }

        public string MaBep { get; set; }

        public string TenBep { get; set; }

        public string MoTa { get; set; }

        public int? IdTrangThai { get; set; }
    }
}
