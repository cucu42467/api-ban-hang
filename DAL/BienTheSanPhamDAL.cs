using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DAL
{
    public class BienTheSanPhamDAL : IBienTheSanPhamDAL
    {
        private readonly DatabaseContext _context;

        public BienTheSanPhamDAL(DatabaseContext context)
        {
            _context = context;
        }

        // Lấy danh sách biến thể sản phẩm
        public List<BienTheSanPhamDTO> GetAll(string lang)
        {
            var data = (from bt in _context.BienTheSanPhams

                        join sp in _context.SanPhams
                        on bt.IdSanPham equals sp.IdSanPham

                        join spl in _context.SanPhamLangs
                        on sp.IdSanPham equals spl.IdSanPham

                        join tt in _context.TrangThais
                        on bt.IdTrangThai equals tt.IdTrangThai

                        where spl.MaNgonNgu == lang
                        && bt.IdTrangThai != 3

                        select new BienTheSanPhamDTO
                        {
                            IdBienThe = bt.IdBienThe,
                            MaBienThe = bt.MaBienThe,
                            IdSanPham = bt.IdSanPham,
                            TenSanPham = spl.TenSanPham,
                            GiaBan = bt.GiaBan,
                            TonKho = bt.TonKho,
                            MaSKU = bt.MaSKU,
                            TenTrangThai = tt.TenTrangThai
                        })
                        .ToList();

            return data;
        }

        // Lấy biến thể theo IdBienThe
        public BienTheSanPhamDTO GetById(int id, string lang)
        {
            // Bước 1: lấy thông tin biến thể (giống cũ)
            var data = (from bt in _context.BienTheSanPhams
                        join sp in _context.SanPhams
                            on bt.IdSanPham equals sp.IdSanPham
                        join spl in _context.SanPhamLangs
                            on sp.IdSanPham equals spl.IdSanPham
                        join tt in _context.TrangThais
                            on bt.IdTrangThai equals tt.IdTrangThai
                        where bt.IdBienThe == id
                           && spl.MaNgonNgu == lang
                        select new BienTheSanPhamDTO
                        {
                            IdBienThe = bt.IdBienThe,
                            MaBienThe = bt.MaBienThe,
                            IdSanPham = bt.IdSanPham,
                            TenSanPham = spl.TenSanPham,
                            GiaBan = bt.GiaBan,
                            TonKho = bt.TonKho,
                            MaSKU = bt.MaSKU,
                            TenTrangThai = tt.TenTrangThai,
                        })
                        .FirstOrDefault();

            if (data == null) return null;

            // Bước 2: lấy tất cả ảnh của biến thể này, sắp xếp theo ThuTu
            data.HinhAnhs = _context.HinhAnhSanPhams
                .Where(h => h.IdBienThe == id)
                .OrderBy(h => h.LaAnhChinh ? 0 : 1) // ảnh chính lên đầu
                .ThenBy(h => h.ThuTu)
                .Select(h => new HinhAnhSanPhamDTO
                {
                    IdHinhAnh = h.IdHinhAnh,
                    MaHinhAnh = h.MaHinhAnh,
                    DuongDanAnh = h.DuongDanAnh,
                    LaAnhChinh = h.LaAnhChinh,
                    ThuTu = h.ThuTu,
                })
                .ToList();

            return data;
        }

        // Lấy danh sách biến thể theo IdSanPham
        public List<BienTheSanPhamDTO> GetByIdSanPham(int idSanPham, string lang)
        {
            var data = (from bt in _context.BienTheSanPhams
                        join sp in _context.SanPhams
                        on bt.IdSanPham equals sp.IdSanPham
                        join spl in _context.SanPhamLangs
                        on sp.IdSanPham equals spl.IdSanPham
                        join tt in _context.TrangThais
                        on bt.IdTrangThai equals tt.IdTrangThai
                        where bt.IdSanPham == idSanPham
                        && spl.MaNgonNgu == lang
                        select new BienTheSanPhamDTO
                        {
                            IdBienThe = bt.IdBienThe,
                            MaBienThe = bt.MaBienThe,
                            IdSanPham = bt.IdSanPham,
                            TenSanPham = spl.TenSanPham,
                            GiaBan = bt.GiaBan,
                            TonKho = bt.TonKho,
                            MaSKU = bt.MaSKU,
                            TenTrangThai = tt.TenTrangThai,

                            // ── Hình ảnh ── (đã có)
                            HinhAnhs = _context.HinhAnhSanPhams
                                .Where(h => h.IdBienThe == bt.IdBienThe)
                                .Select(h => new HinhAnhSanPhamDTO
                                {
                                    IdHinhAnh = h.IdHinhAnh,
                                    MaHinhAnh = h.MaHinhAnh,
                                    DuongDanAnh = h.DuongDanAnh,
                                    LaAnhChinh = h.LaAnhChinh,
                                    ThuTu = h.ThuTu
                                })
                                .OrderByDescending(h => h.LaAnhChinh)
                                .ThenBy(h => h.ThuTu)
                                .ToList(),

                            // ── Thuộc tính biến thể ── (THÊM MỚI)
                            ThuocTinhs = (from btt in _context.BienTheThuocTinhs
                                          join gtri in _context.GiaTriThuocTinhs
                                              on btt.IdGiaTri equals gtri.IdGiaTri
                                          join thuoc in _context.ThuocTinhs
                                              on gtri.IdThuocTinh equals thuoc.IdThuocTinh
                                          join thuocLang in _context.ThuocTinhLangs
                                              on thuoc.IdThuocTinh equals thuocLang.IdThuocTinh
                                          join gtriLang in _context.GiaTriThuocTinhLangs
                                              on gtri.IdGiaTri equals gtriLang.IdGiaTri
                                          where btt.IdBienThe == bt.IdBienThe
                                                && thuocLang.MaNgonNgu == lang
                                                && gtriLang.MaNgonNgu == lang
                                          select new GiaTriThuocTinhDTO
                                          {
                                              IdGiaTri = gtri.IdGiaTri,
                                              IdThuocTinh = thuoc.IdThuocTinh,
                                              TenThuocTinh = thuocLang.TenThuocTinh,
                                              GiaTri = gtriLang.GiaTri
                                          })
                                        .ToList()
                        })
                        .ToList();
            return data;
        }
        // Thêm biến thể sản phẩm
        public int Create(string maBienThe, int idSanPham, double giaBan, int tonKho, string maSKU, int idTrangThai)
        {
            // Tạo chiến lược thực thi
            var strategy = _context.Database.CreateExecutionStrategy();

            return strategy.Execute(() =>
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    var bienThe = new BienTheSanPham
                    {
                        MaBienThe = maBienThe,
                        IdSanPham = idSanPham,
                        GiaBan = giaBan,
                        TonKho = tonKho,
                        MaSKU = maSKU,
                        IdTrangThai = idTrangThai
                    };

                    _context.BienTheSanPhams.Add(bienThe);
                    _context.SaveChanges();
                    tran.Commit();

                    return bienThe.IdBienThe;
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            });
        }

        // Cập nhật biến thể sản phẩm
        public bool Update(BienTheSanPham bienThe)
        {
            var strategy = _context.Database.CreateExecutionStrategy();

            return strategy.Execute(() =>
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    var data = _context.BienTheSanPhams.FirstOrDefault(x => x.IdBienThe == bienThe.IdBienThe);
                    if (data == null) return false;

                    data.MaBienThe = bienThe.MaBienThe;
                    data.IdSanPham = bienThe.IdSanPham;
                    data.GiaBan = bienThe.GiaBan;
                    data.TonKho = bienThe.TonKho;
                    data.MaSKU = bienThe.MaSKU;
                    data.IdTrangThai = bienThe.IdTrangThai;

                    _context.SaveChanges();
                    tran.Commit();
                    return true;
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            });
        }

        // Cập nhật giá bán (có ghi lịch sử giá)
        public bool UpdateGiaBan(int idBienThe, double giaMoi, string maLichSu)
        {
            var strategy = _context.Database.CreateExecutionStrategy();

            return strategy.Execute(() =>
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    var bienThe = _context.BienTheSanPhams.FirstOrDefault(x => x.IdBienThe == idBienThe);
                    if (bienThe == null) return false;

                    var lichSu = new LichSuGia
                    {
                        MaLichSu = maLichSu,
                        IdBienThe = idBienThe,
                        GiaCu = bienThe.GiaBan,
                        GiaMoi = giaMoi
                    };
                    _context.LichSuGias.Add(lichSu);

                    bienThe.GiaBan = giaMoi;
                    _context.SaveChanges();
                    tran.Commit();
                    return true;
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            });
        }
        // Cập nhật tồn kho (cộng/trừ số lượng)
        public bool UpdateTonKho(int idBienThe, int soLuongThayDoi)
        {
            var bienThe = _context.BienTheSanPhams
                .FirstOrDefault(x => x.IdBienThe == idBienThe);

            if (bienThe == null)
                return false;

            bienThe.TonKho += soLuongThayDoi;

            _context.SaveChanges();

            return true;
        }

        // Cập nhật trạng thái (soft delete)
        public bool UpdateTrangThai(int id, int trangThai)
        {
            var bienThe = _context.BienTheSanPhams
                .FirstOrDefault(x => x.IdBienThe == id);

            if (bienThe == null)
                return false;

            var trangThaiExist = _context.TrangThais
                .Any(x => x.IdTrangThai == trangThai);

            if (!trangThaiExist)
                return false;

            bienThe.IdTrangThai = trangThai;

            _context.SaveChanges();

            return true;
        }

        // Lấy lịch sử giá của biến thể
        public List<LichSuGia> GetLichSuGia(int idBienThe)
        {
            var data = _context.LichSuGias
                .Where(x => x.IdBienThe == idBienThe)
                .OrderByDescending(x => x.NgayThayDoi)
                .ToList();

            return data;
        }
    }
}