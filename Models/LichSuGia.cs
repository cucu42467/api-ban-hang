using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("tbl_LichSuGia")]
    public class LichSuGia
    {
        [Key]
        public int IdLichSu { get; set; }
        public string MaLichSu { get; set; }
        public int IdBienThe { get; set; }
        public double? GiaCu { get; set; }
        public double? GiaMoi { get; set; }
        public DateTime NgayThayDoi { get; set; }
    }

    public class LichSuGiaDTO
    {
        public int IdLichSu { get; set; }
        public string MaLichSu { get; set; }
        public int IdBienThe { get; set; }
        public double? GiaCu { get; set; }
        public double? GiaMoi { get; set; }
        public DateTime NgayThayDoi { get; set; }
    }
}