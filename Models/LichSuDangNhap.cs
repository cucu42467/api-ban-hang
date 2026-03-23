using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class LichSuDangNhap
    {
        [Key]
        public int Id { get; set; }

        public int IdNguoiDung { get; set; }

        public string ThoiGian { get; set; }

        public string DiaChiIP { get; set; }

        public string ThietBi { get; set; }
    }
}
