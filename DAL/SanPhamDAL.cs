using DAL.Interfaces;
using Models;

namespace DAL
{
    public class SanPhamDAL : ISanPhamDAL
    {
        private readonly DatabaseContext _context;

        public SanPhamDAL(DatabaseContext context)
        {
            _context = context;
        }

        private string Translate(string text, string lang)
        {
            return text + "_" + lang;
        }

        // Lấy danh sách sản phẩm
        public List<SanPhamDTO> GetAll(string lang, int pageIndex, int pageSize)
        {
            var query = (from sp in _context.SanPhams
                         join spl in _context.SanPhamLangs
                         on sp.IdSanPham equals spl.IdSanPham
                         join dm in _context.DanhMucs
                         on sp.IdDanhMuc equals dm.IdDanhMuc
                         join dml in _context.DanhMucLangs
                         on dm.IdDanhMuc equals dml.IdDanhMuc
                         where spl.MaNgonNgu == lang
                         && dml.MaNgonNgu == lang
                         && sp.IdTrangThai != 3
                         let btFirst = _context.BienTheSanPhams
                                         .Where(bt => bt.IdSanPham == sp.IdSanPham)
                                         .FirstOrDefault()
                         select new SanPhamDTO
                         {
                             IdSanPham = sp.IdSanPham,
                             MaSanPham = sp.MaSanPham,
                             TenSanPham = spl.TenSanPham,
                             MoTa = spl.MoTa,
                             TenDanhMuc = dml.TenDanhMuc,
                             GiaBan = btFirst != null ? btFirst.GiaBan : 0,
                             TonKho = btFirst != null ? btFirst.TonKho : 0,
                             MaSKU = btFirst != null ? btFirst.MaSKU : null
                         });

            return query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int GetTotalCount(string lang)
        {
            return (from sp in _context.SanPhams
                    join spl in _context.SanPhamLangs on sp.IdSanPham equals spl.IdSanPham
                    where spl.MaNgonNgu == lang
                    && sp.IdTrangThai != 3
                    select sp.IdSanPham)
                   .Count();
        }

        // Lấy sản phẩm theo Id
        public SanPhamDTO GetById(int id, string lang)
        {
            var data = (from sp in _context.SanPhams

                        join spl in _context.SanPhamLangs
                        on sp.IdSanPham equals spl.IdSanPham

                        join bt in _context.BienTheSanPhams
                        on sp.IdSanPham equals bt.IdSanPham

                        join dm in _context.DanhMucs
                        on sp.IdDanhMuc equals dm.IdDanhMuc

                        join dml in _context.DanhMucLangs
                        on dm.IdDanhMuc equals dml.IdDanhMuc

                        where sp.IdSanPham == id
                        && spl.MaNgonNgu == lang
                        && dml.MaNgonNgu == lang

                        select new SanPhamDTO
                        {
                            IdSanPham = sp.IdSanPham,
                            MaSanPham = sp.MaSanPham,
                            TenSanPham = spl.TenSanPham,
                            MoTa = spl.MoTa,
                            TenDanhMuc = dml.TenDanhMuc,
                            GiaBan = bt.GiaBan,
                            TonKho = bt.TonKho,
                            MaSKU = bt.MaSKU
                        })
                        .FirstOrDefault();

            return data;
        }

        // Thêm sản phẩm
        public int CreateSanPham(string maSanPham, int idDanhMuc, string tenSanPham, string moTa)
        {
            using var tran = _context.Database.BeginTransaction();

            try
            {
                var sanPham = new SanPham
                {
                    MaSanPham = maSanPham,
                    IdDanhMuc = idDanhMuc,
                    IdTrangThai = 1
                };

                _context.SanPhams.Add(sanPham);
                _context.SaveChanges();

                int idSanPham = sanPham.IdSanPham;

                // lấy tất cả ngôn ngữ
                var languages = _context.NgonNgus.ToList();

                foreach (var lang in languages)
                {
                    string ten = tenSanPham;
                    string mota = moTa;

                    if (lang.MaNgonNgu != "vi")
                    {
                        ten = Translate(tenSanPham, lang.MaNgonNgu);
                        mota = Translate(moTa, lang.MaNgonNgu);
                    }

                    var spLang = new SanPhamLang
                    {
                        IdSanPham = idSanPham,
                        MaNgonNgu = lang.MaNgonNgu,
                        TenSanPham = ten,
                        MoTa = mota
                    };

                    _context.SanPhamLangs.Add(spLang);
                }

                _context.SaveChanges();

                tran.Commit();

                return idSanPham;
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }

        // Cập nhật thông tin sản phẩm
        public bool Update(SanPham sanPham, string tenSanPham, string moTa)
        {
            using var tran = _context.Database.BeginTransaction();

            try
            {
                // cập nhật bảng SanPham
                var data = _context.SanPhams
                    .FirstOrDefault(x => x.IdSanPham == sanPham.IdSanPham);

                if (data == null)
                    return false;

                data.MaSanPham = sanPham.MaSanPham;
                data.IdDanhMuc = sanPham.IdDanhMuc;
                data.IdTrangThai = sanPham.IdTrangThai;

                // cập nhật bảng SanPham_Lang
                var languages = _context.NgonNgus.ToList();

                foreach (var lang in languages)
                {
                    var spLang = _context.SanPhamLangs
                        .FirstOrDefault(x => x.IdSanPham == sanPham.IdSanPham
                                          && x.MaNgonNgu == lang.MaNgonNgu);

                    if (spLang != null)
                    {
                        spLang.TenSanPham = tenSanPham;
                        spLang.MoTa = moTa;
                    }
                }

                _context.SaveChanges();
                tran.Commit();

                return true;
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }
        // Cập nhật trạng thái (soft delete)
        public bool UpdateTrangThai(int id, int trangThai)
        {
            var sanPham = _context.SanPhams
                .FirstOrDefault(x => x.IdSanPham == id);

            if (sanPham == null)
                return false;

            var trangThaiExist = _context.TrangThais
                .Any(x => x.IdTrangThai == trangThai);

            if (!trangThaiExist)
                return false;

            sanPham.IdTrangThai = trangThai;

            _context.SaveChanges();

            return true;
        }

        public class PagedResult<T>
        {
            public List<T> Data { get; set; }
            public int TotalCount { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        }
    }
}