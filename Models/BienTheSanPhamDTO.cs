namespace Models
{
    public class BienTheSanPhamDTO
    {
        // ===== Biến thể =====
        public int IdBienThe { get; set; }
        public string MaBienThe { get; set; }
        public string MaSKU { get; set; }
        public double? GiaBan { get; set; }
        public int TonKho { get; set; }
        // ===== Sản phẩm =====
        public int IdSanPham { get; set; }
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string MoTa { get; set; }
        // ===== Danh mục =====
        public string TenDanhMuc { get; set; }
        // ===== Trạng thái =====
        public int IdTrangThai { get; set; }
        public string TenTrangThai { get; set; }
        // ===== Thuộc tính biến thể (màu, size,...) =====
        public List<GiaTriThuocTinhDTO> ThuocTinhs { get; set; }        // ===== Hình ảnh =====                          ← THÊM
        public List<HinhAnhSanPhamDTO> HinhAnhs { get; set; } = new List<HinhAnhSanPhamDTO>();

    }

    public class HinhAnhSanPhamDTO                  // ← THÊM
    {
        public int IdHinhAnh { get; set; }
        public string? MaHinhAnh { get; set; }
        public string? DuongDanAnh { get; set; }
        public bool LaAnhChinh { get; set; }         // 0 = ảnh phụ | 1 = ảnh chính
        public int ThuTu { get; set; }
    }
}