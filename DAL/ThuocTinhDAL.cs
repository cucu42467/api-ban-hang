using DAL.Interfaces;
using Models;

namespace DAL
{
    public class ThuocTinhDAL : IThuocTinhDAL
    {
        private readonly DatabaseContext _context;

        public ThuocTinhDAL(DatabaseContext context)
        {
            _context = context;
        }

        public List<ThuocTinhDTO> GetAll(string lang)
        {
            // Query 1: lấy ThuocTinh — không join, chỉ filter ThuocTinhLang
            var dsThuocTinh = _context.ThuocTinhs.ToList();
            var dsThuocTinhLang = _context.ThuocTinhLangs
                .Where(x => x.MaNgonNgu == lang)
                .ToList();

            // Query 2: lấy GiaTriThuocTinh
            var dsGiaTri = _context.GiaTriThuocTinhs.ToList();
            var dsGiaTriLang = _context.GiaTriThuocTinhLangs
                .Where(x => x.MaNgonNgu == lang)
                .ToList();

            // Map thủ công — không có ambiguity
            var result = new List<ThuocTinhDTO>();

            foreach (var ta in dsThuocTinh)
            {
                var taLang = dsThuocTinhLang
                    .FirstOrDefault(x => x.IdThuocTinh == ta.IdThuocTinh);

                if (taLang == null) continue;

                var dto = new ThuocTinhDTO
                {
                    IdThuocTinh = ta.IdThuocTinh,
                    MaThuocTinh = ta.MaThuocTinh,
                    TenThuocTinh = taLang.TenThuocTinh,
                    DanhSachGiaTri = new List<GiaTriThuocTinhDTO>()
                };

                var dsGiaTriCuaTa = dsGiaTri
                    .Where(g => g.IdThuocTinh == ta.IdThuocTinh)
                    .ToList();

                foreach (var gtri in dsGiaTriCuaTa)
                {
                    var gtriLang = dsGiaTriLang
                        .FirstOrDefault(x => x.IdGiaTri == gtri.IdGiaTri);

                    if (gtriLang == null) continue;

                    dto.DanhSachGiaTri.Add(new GiaTriThuocTinhDTO
                    {
                        IdGiaTri = gtri.IdGiaTri,
                        MaGiaTri = gtri.MaGiaTri,
                        IdThuocTinh = gtri.IdThuocTinh,
                        TenThuocTinh = taLang.TenThuocTinh,
                        GiaTri = gtriLang.GiaTri
                    });
                }

                result.Add(dto);
            }

            return result;
        }

        public ThuocTinhDTO GetById(int idThuocTinh, string lang)
        {
            var ta = _context.ThuocTinhs
                .FirstOrDefault(x => x.IdThuocTinh == idThuocTinh);

            if (ta == null) return null;

            var taLang = _context.ThuocTinhLangs
                .FirstOrDefault(x => x.IdThuocTinh == idThuocTinh
                                  && x.MaNgonNgu == lang);

            if (taLang == null) return null;

            var dto = new ThuocTinhDTO
            {
                IdThuocTinh = ta.IdThuocTinh,
                MaThuocTinh = ta.MaThuocTinh,
                TenThuocTinh = taLang.TenThuocTinh,
                DanhSachGiaTri = new List<GiaTriThuocTinhDTO>()
            };

            var dsGiaTri = _context.GiaTriThuocTinhs
                .Where(x => x.IdThuocTinh == idThuocTinh)
                .ToList();

            var dsGiaTriLang = _context.GiaTriThuocTinhLangs
                .Where(x => x.MaNgonNgu == lang)
                .ToList();

            foreach (var gtri in dsGiaTri)
            {
                var gtriLang = dsGiaTriLang
                    .FirstOrDefault(x => x.IdGiaTri == gtri.IdGiaTri);

                if (gtriLang == null) continue;

                dto.DanhSachGiaTri.Add(new GiaTriThuocTinhDTO
                {
                    IdGiaTri = gtri.IdGiaTri,
                    MaGiaTri = gtri.MaGiaTri,
                    IdThuocTinh = gtri.IdThuocTinh,
                    TenThuocTinh = taLang.TenThuocTinh,
                    GiaTri = gtriLang.GiaTri
                });
            }

            return dto;
        }

        public int Create(string maThuocTinh, string tenThuocTinh)
        {
            using var tran = _context.Database.BeginTransaction();
            try
            {
                var thuocTinh = new ThuocTinh
                {
                    MaThuocTinh = maThuocTinh
                };

                _context.ThuocTinhs.Add(thuocTinh);
                _context.SaveChanges();

                int id = thuocTinh.IdThuocTinh;

                var languages = _context.NgonNgus.ToList();

                foreach (var lang in languages)
                {
                    _context.ThuocTinhLangs.Add(new ThuocTinhLang
                    {
                        IdThuocTinh = id,
                        MaNgonNgu = lang.MaNgonNgu,
                        TenThuocTinh = lang.MaNgonNgu == "vi"
                            ? tenThuocTinh
                            : tenThuocTinh + "_" + lang.MaNgonNgu
                    });
                }

                _context.SaveChanges();
                tran.Commit();

                return id;
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }

        public bool Update(int idThuocTinh, string maThuocTinh, string tenThuocTinh)
        {
            using var tran = _context.Database.BeginTransaction();
            try
            {
                var data = _context.ThuocTinhs
                    .FirstOrDefault(x => x.IdThuocTinh == idThuocTinh);

                if (data == null) return false;

                data.MaThuocTinh = maThuocTinh;

                var langVi = _context.ThuocTinhLangs
                    .FirstOrDefault(x => x.IdThuocTinh == idThuocTinh
                                      && x.MaNgonNgu == "vi");

                if (langVi != null)
                    langVi.TenThuocTinh = tenThuocTinh;

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
    }
}