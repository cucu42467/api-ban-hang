using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ThongTinNguoiDung
    {
        [Key]
        public int Id { get; set; }

        public int IdNguoiDung { get; set; }

        public string HoTen { get; set; }

        public string NgaySinh { get; set; }

        public string GioiTinh { get; set; }

        public string DiaChi { get; set; }

        public string AnhDaiDien { get; set; }
    }
}
