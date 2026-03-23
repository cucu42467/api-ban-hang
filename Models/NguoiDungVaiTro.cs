using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class NguoiDungVaiTro
    {
        [Key]
        public int Id { get; set; }

        public int IdNguoiDung { get; set; }

        public int IdVaiTro { get; set; }
    }
}
