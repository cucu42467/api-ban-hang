using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class GiaTriThuocTinh
    {
        [Key]
        public int IdGiaTri { get; set; }

        public string MaGiaTri { get; set; }

        public int IdThuocTinh { get; set; }
    }
}
