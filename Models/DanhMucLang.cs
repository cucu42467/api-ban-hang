using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DanhMucLang
    {
        [Key, Column(Order = 0)]
        public int IdDanhMuc { get; set; }

        [Key, Column(Order = 1)]
        public string MaNgonNgu { get; set; }

        public string TenDanhMuc { get; set; }

        public string MoTa { get; set; }
    }
}
