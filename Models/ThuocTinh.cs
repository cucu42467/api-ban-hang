using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("tbl_ThuocTinh")]
    public class ThuocTinh
    {
        [Key]
        public int IdThuocTinh { get; set; }
        public string MaThuocTinh { get; set; }
    }

    [Table("tbl_ThuocTinh_Lang")]
    public class ThuocTinhLang
    {
        public int IdThuocTinh { get; set; }
        public string MaNgonNgu { get; set; }
        public string TenThuocTinh { get; set; }
    }

    // ---- DTOs ----
    public class ThuocTinhDTO
    {
        public int IdThuocTinh { get; set; }
        public string MaThuocTinh { get; set; }
        public string TenThuocTinh { get; set; }
        public List<GiaTriThuocTinhDTO> DanhSachGiaTri { get; set; }
    }

    public class GiaTriThuocTinhDTO
    {
        public int IdGiaTri { get; set; }
        public string MaGiaTri { get; set; }
        public int IdThuocTinh { get; set; }
        public string TenThuocTinh { get; set; }

        public string GiaTri { get; set; }
    }
}