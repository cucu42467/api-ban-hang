using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Quyen
    {
        [Key]
        public int IdQuyen { get; set; }

        public string MaQuyen { get; set; }

        public string TenQuyen { get; set; }

        public string MoTa { get; set; }
    }
}
