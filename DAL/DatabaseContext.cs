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

        // =============================
        // NGON NGU
        // =============================
        public DbSet<NgonNgu> NgonNgus { get; set; }

        // =============================
        // NGUOI DUNG
        // =============================
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<ThongTinNguoiDung> ThongTinNguoiDungs { get; set; }
        public DbSet<VaiTro> VaiTros { get; set; }
        public DbSet<NguoiDungVaiTro> NguoiDungVaiTros { get; set; }

        // =============================
        // QUYEN
        // =============================
        public DbSet<Quyen> Quyens { get; set; }
        public DbSet<VaiTroQuyen> VaiTroQuyens { get; set; }

        // =============================
        // TRANG THAI
        // =============================
        public DbSet<TrangThai> TrangThais { get; set; }
        public DbSet<LoaiTrangThai> LoaiTrangThais { get; set; }

        // =============================
        // DANH MUC + SAN PHAM
        // =============================
        public DbSet<DanhMuc> DanhMucs { get; set; }
        public DbSet<DanhMucLang> DanhMucLangs { get; set; }

        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<SanPhamLang> SanPhamLangs { get; set; }

        public DbSet<BienTheSanPham> BienTheSanPhams { get; set; }

        public DbSet<HinhAnhSanPham> HinhAnhSanPhams { get; set; }
        public DbSet<LichSuGia> LichSuGias { get; set; }

        // =============================
        // BEP + HOA DON
        // =============================
        public DbSet<Bep> Beps { get; set; }
        public DbSet<HoaDonBep> HoaDonBeps { get; set; }
        public DbSet<ChiTietHoaDonBep> ChiTietHoaDonBeps { get; set; }

        // =============================
        // THUOC TINH
        // =============================
        public DbSet<ThuocTinh> ThuocTinhs { get; set; }
        public DbSet<ThuocTinhLang> ThuocTinhLangs { get; set; }

        public DbSet<GiaTriThuocTinh> GiaTriThuocTinhs { get; set; }
        public DbSet<GiaTriThuocTinhLang> GiaTriThuocTinhLangs { get; set; }

        public DbSet<BienTheThuocTinh> BienTheThuocTinhs { get; set; }

        // =============================
        // NHAT KY + LOGIN
        // =============================
        public DbSet<NhatKyHeThong> NhatKyHeThongs { get; set; }
        public DbSet<LichSuDangNhap> LichSuDangNhaps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =============================
            // TABLE MAP
            // =============================

            modelBuilder.Entity<NgonNgu>().ToTable("tbl_NgonNgu");

            modelBuilder.Entity<NguoiDung>().ToTable("tbl_NguoiDung");
            modelBuilder.Entity<ThongTinNguoiDung>().ToTable("tbl_ThongTinNguoiDung");

            modelBuilder.Entity<VaiTro>().ToTable("tbl_VaiTro");
            modelBuilder.Entity<NguoiDungVaiTro>().ToTable("tbl_NguoiDungVaiTro");

            modelBuilder.Entity<Quyen>().ToTable("tbl_Quyen");
            modelBuilder.Entity<VaiTroQuyen>().ToTable("tbl_VaiTroQuyen");

            modelBuilder.Entity<TrangThai>().ToTable("tbl_TrangThai");
            modelBuilder.Entity<LoaiTrangThai>().ToTable("tbl_LoaiTrangThai");

            modelBuilder.Entity<DanhMuc>().ToTable("tbl_DanhMuc");
            modelBuilder.Entity<DanhMucLang>().ToTable("tbl_DanhMuc_Lang");

            modelBuilder.Entity<SanPham>().ToTable("tbl_SanPham");
            modelBuilder.Entity<SanPhamLang>().ToTable("tbl_SanPham_Lang");

            modelBuilder.Entity<BienTheSanPham>().ToTable("tbl_BienTheSanPham");

            modelBuilder.Entity<HinhAnhSanPham>().ToTable("tbl_HinhAnhSanPham");
            modelBuilder.Entity<LichSuGia>().ToTable("tbl_LichSuGia");

            modelBuilder.Entity<Bep>().ToTable("tbl_Bep");
            modelBuilder.Entity<HoaDonBep>().ToTable("tbl_HoaDonBep");
            modelBuilder.Entity<ChiTietHoaDonBep>().ToTable("tbl_ChiTietHoaDonBep");

            modelBuilder.Entity<ThuocTinh>().ToTable("tbl_ThuocTinh");
            modelBuilder.Entity<ThuocTinhLang>().ToTable("tbl_ThuocTinh_Lang");

            modelBuilder.Entity<GiaTriThuocTinh>().ToTable("tbl_GiaTriThuocTinh");
            modelBuilder.Entity<GiaTriThuocTinhLang>().ToTable("tbl_GiaTriThuocTinh_Lang");

            modelBuilder.Entity<BienTheThuocTinh>().ToTable("tbl_BienTheThuocTinh");

            modelBuilder.Entity<NhatKyHeThong>().ToTable("tbl_NhatKyHeThong");
            modelBuilder.Entity<LichSuDangNhap>().ToTable("tbl_LichSuDangNhap");


            // =============================
            // COMPOSITE KEYS (BẢNG LANG)
            // =============================

            modelBuilder.Entity<SanPhamLang>()
                .HasKey(x => new { x.IdSanPham, x.MaNgonNgu });

            modelBuilder.Entity<DanhMucLang>()
                .HasKey(x => new { x.IdDanhMuc, x.MaNgonNgu });

            modelBuilder.Entity<ThuocTinhLang>()
                .HasKey(x => new { x.IdThuocTinh, x.MaNgonNgu });

            modelBuilder.Entity<GiaTriThuocTinhLang>()
                .HasKey(x => new { x.IdGiaTri, x.MaNgonNgu });
        }
    }
}