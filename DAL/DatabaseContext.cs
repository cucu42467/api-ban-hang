using Microsoft.EntityFrameworkCore;
using Models;

namespace DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        // --- DbSet giữ nguyên ---
        public DbSet<NgonNgu> NgonNgus { get; set; }
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<ThongTinNguoiDung> ThongTinNguoiDungs { get; set; }
        public DbSet<VaiTro> VaiTros { get; set; }
        public DbSet<NguoiDungVaiTro> NguoiDungVaiTros { get; set; }
        public DbSet<Quyen> Quyens { get; set; }
        public DbSet<VaiTroQuyen> VaiTroQuyens { get; set; }
        public DbSet<TrangThai> TrangThais { get; set; }
        public DbSet<LoaiTrangThai> LoaiTrangThais { get; set; }
        public DbSet<DanhMuc> DanhMucs { get; set; }
        public DbSet<DanhMucLang> DanhMucLangs { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<SanPhamLang> SanPhamLangs { get; set; }
        public DbSet<BienTheSanPham> BienTheSanPhams { get; set; }
        public DbSet<HinhAnhSanPham> HinhAnhSanPhams { get; set; }
        public DbSet<LichSuGia> LichSuGias { get; set; }
        public DbSet<Bep> Beps { get; set; }
        public DbSet<HoaDonBep> HoaDonBeps { get; set; }
        public DbSet<ChiTietHoaDonBep> ChiTietHoaDonBeps { get; set; }
        public DbSet<ThuocTinh> ThuocTinhs { get; set; }
        public DbSet<ThuocTinhLang> ThuocTinhLangs { get; set; }
        public DbSet<GiaTriThuocTinh> GiaTriThuocTinhs { get; set; }
        public DbSet<GiaTriThuocTinhLang> GiaTriThuocTinhLangs { get; set; }
        public DbSet<BienTheThuocTinh> BienTheThuocTinhs { get; set; }
        public DbSet<NhatKyHeThong> NhatKyHeThongs { get; set; }
        public DbSet<LichSuDangNhap> LichSuDangNhaps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =============================
            // TABLE MAP - CHUYỂN HẾT SANG CHỮ THƯỜNG (LOWERCASE)
            // =============================

            modelBuilder.Entity<NgonNgu>().ToTable("tbl_ngonngu");
            modelBuilder.Entity<NguoiDung>().ToTable("tbl_nguoidung");
            modelBuilder.Entity<ThongTinNguoiDung>().ToTable("tbl_thongtinnguoidung");
            modelBuilder.Entity<VaiTro>().ToTable("tbl_vaitro");
            modelBuilder.Entity<NguoiDungVaiTro>().ToTable("tbl_nguoidungvaitro");
            modelBuilder.Entity<Quyen>().ToTable("tbl_quyen");
            modelBuilder.Entity<VaiTroQuyen>().ToTable("tbl_vaitroquyen");
            modelBuilder.Entity<TrangThai>().ToTable("tbl_trangthai");
            modelBuilder.Entity<LoaiTrangThai>().ToTable("tbl_loaitrangthai");
            modelBuilder.Entity<DanhMuc>().ToTable("tbl_danhmuc");
            modelBuilder.Entity<DanhMucLang>().ToTable("tbl_danhmuc_lang");
            modelBuilder.Entity<SanPham>().ToTable("tbl_sanpham");
            modelBuilder.Entity<SanPhamLang>().ToTable("tbl_sanpham_lang");
            modelBuilder.Entity<BienTheSanPham>().ToTable("tbl_bienthesanpham");
            modelBuilder.Entity<HinhAnhSanPham>().ToTable("tbl_hinhanhsanpham");
            modelBuilder.Entity<LichSuGia>().ToTable("tbl_lichsugia");
            modelBuilder.Entity<Bep>().ToTable("tbl_bep");
            modelBuilder.Entity<HoaDonBep>().ToTable("tbl_hoadonbep");
            modelBuilder.Entity<ChiTietHoaDonBep>().ToTable("tbl_chitiethoadonbep");
            modelBuilder.Entity<ThuocTinh>().ToTable("tbl_thuoctinh");
            modelBuilder.Entity<ThuocTinhLang>().ToTable("tbl_thuoctinh_lang");
            modelBuilder.Entity<GiaTriThuocTinh>().ToTable("tbl_giatrithuoctinh");
            modelBuilder.Entity<GiaTriThuocTinhLang>().ToTable("tbl_giatrithuoctinh_lang");
            modelBuilder.Entity<BienTheThuocTinh>().ToTable("tbl_bienthethuoctinh");
            modelBuilder.Entity<NhatKyHeThong>().ToTable("tbl_nhatkyhethong");
            modelBuilder.Entity<LichSuDangNhap>().ToTable("tbl_lichsudangnhap");

            // =============================
            // COMPOSITE KEYS (Giữ nguyên)
            // =============================
            modelBuilder.Entity<SanPhamLang>().HasKey(x => new { x.IdSanPham, x.MaNgonNgu });
            modelBuilder.Entity<DanhMucLang>().HasKey(x => new { x.IdDanhMuc, x.MaNgonNgu });
            modelBuilder.Entity<ThuocTinhLang>().HasKey(x => new { x.IdThuocTinh, x.MaNgonNgu });
            modelBuilder.Entity<GiaTriThuocTinhLang>().HasKey(x => new { x.IdGiaTri, x.MaNgonNgu });
        }
    }
}