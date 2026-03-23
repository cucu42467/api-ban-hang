using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class NgonNgu
    {
        [Key]
        public string MaNgonNgu { get; set; }

        public string TenNgonNgu { get; set; }

        public decimal TiGiaDoiToai { get; set; }

        public string KyHieuTienTe { get; set; }

        public int LaMacDinh { get; set; }

        public DateTime? NgayCapNhat { get; set; }
    }

    public class TiGiaDTO
    {
        public string MaNgonNgu { get; set; }
        public decimal TiGiaDoiToai { get; set; }
        public DateTime NgayCapNhat { get; set; }
    }
}
