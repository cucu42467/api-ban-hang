using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class VaiTroQuyen
    {
        [Key]
        public int Id { get; set; }

        public int IdVaiTro { get; set; }

        public int IdQuyen { get; set; }
    }
}
