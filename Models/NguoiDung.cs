using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class NguoiDung
    {
        [Key]
        public int IdNguoiDung { get; set; }

        public string MaNguoiDung { get; set; }

        public string TenDangNhap { get; set; }

        public string MatKhau { get; set; }

        public string Email { get; set; }

        public string SoDienThoai { get; set; }

        public int? IdTrangThai { get; set; }

        public DateTime? LanDangNhapCuoi { get; set; }

        public DateTime NgayTao { get; set; }
    }
}
