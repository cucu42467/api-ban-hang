using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BienTheThuocTinh
    {
        [Key]
        public int Id { get; set; }

        public string MaBienTheThuocTinh { get; set; }

        public int IdBienThe { get; set; }

        public int IdGiaTri { get; set; }
    }

    public class BienTheThuocTinhDTO
    {
        public string TenThuocTinh { get; set; }
        public string GiaTri { get; set; }
    }
}
