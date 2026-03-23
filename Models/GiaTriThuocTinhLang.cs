using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class GiaTriThuocTinhLang
    {
        [Key, Column(Order = 0)]
        public int IdGiaTri { get; set; }

        [Key, Column(Order = 1)]
        public string MaNgonNgu { get; set; }

        public string GiaTri { get; set; }
    }
}
