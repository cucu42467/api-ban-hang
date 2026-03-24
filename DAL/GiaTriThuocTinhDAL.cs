using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DAL
{
    public class GiaTriThuocTinhDAL : IGiaTriThuocTinhDAL
    {
        private readonly DatabaseContext _context;

        public GiaTriThuocTinhDAL(DatabaseContext context)
        {
            _context = context;
        }

        public List<GiaTriThuocTinhDTO> GetByThuocTinh(int idThuocTinh, string lang)
        {
            var query = from gtri in _context.GiaTriThuocTinhs
                        join gtriLang in _context.GiaTriThuocTinhLangs
                            on gtri.IdGiaTri equals gtriLang.IdGiaTri
                        join taLang in _context.ThuocTinhLangs
                            on gtri.IdThuocTinh equals taLang.IdThuocTinh
                        where gtri.IdThuocTinh == idThuocTinh
                           && gtriLang.MaNgonNgu == lang
                           && taLang.MaNgonNgu == lang
                        select new
                        {
                            gtri.IdGiaTri,
                            gtri.MaGiaTri,
                            gtri.IdThuocTinh,
                            taLang.TenThuocTinh,
                            gtriLang.GiaTri
                        };

            return query.ToList().Select(x => new GiaTriThuocTinhDTO
            {
                IdGiaTri = x.IdGiaTri,
                MaGiaTri = x.MaGiaTri,
                IdThuocTinh = x.IdThuocTinh,
                TenThuocTinh = x.TenThuocTinh,
                GiaTri = x.GiaTri
            }).ToList();
        }

        public GiaTriThuocTinhDTO GetById(int idGiaTri, string lang)
        {
            var query = from gtri in _context.GiaTriThuocTinhs
                        join gtriLang in _context.GiaTriThuocTinhLangs
                            on gtri.IdGiaTri equals gtriLang.IdGiaTri
                        join taLang in _context.ThuocTinhLangs
                            on gtri.IdThuocTinh equals taLang.IdThuocTinh
                        where gtri.IdGiaTri == idGiaTri
                           && gtriLang.MaNgonNgu == lang
                           && taLang.MaNgonNgu == lang
                        select new
                        {
                            gtri.IdGiaTri,
                            gtri.MaGiaTri,
                            gtri.IdThuocTinh,
                            taLang.TenThuocTinh,
                            gtriLang.GiaTri
                        };

            var item = query.FirstOrDefault();
            if (item == null) return null;

            return new GiaTriThuocTinhDTO
            {
                IdGiaTri = item.IdGiaTri,
                MaGiaTri = item.MaGiaTri,
                IdThuocTinh = item.IdThuocTinh,
                TenThuocTinh = item.TenThuocTinh,
                GiaTri = item.GiaTri
            };
        }

        public int Create(string maGiaTri, int idThuocTinh, string giaTri)
        {
            var strategy = _context.Database.CreateExecutionStrategy();

            return strategy.Execute(() =>
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    var giaTriMoi = new GiaTriThuocTinh
                    {
                        MaGiaTri = maGiaTri,
                        IdThuocTinh = idThuocTinh
                    };

                    _context.GiaTriThuocTinhs.Add(giaTriMoi);
                    _context.SaveChanges();

                    int id = giaTriMoi.IdGiaTri;
                    var languages = _context.NgonNgus.ToList();

                    foreach (var lang in languages)
                    {
                        _context.GiaTriThuocTinhLangs.Add(new GiaTriThuocTinhLang
                        {
                            IdGiaTri = id,
                            MaNgonNgu = lang.MaNgonNgu,
                            GiaTri = lang.MaNgonNgu == "vi"
                                ? giaTri
                                : giaTri + "_" + lang.MaNgonNgu
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
            });
        }

        public bool Update(int idGiaTri, string maGiaTri, string giaTri)
        {
            var strategy = _context.Database.CreateExecutionStrategy();

            return strategy.Execute(() =>
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    var data = _context.GiaTriThuocTinhs
                        .FirstOrDefault(x => x.IdGiaTri == idGiaTri);

                    if (data == null) return false;

                    data.MaGiaTri = maGiaTri;

                    var langVi = _context.GiaTriThuocTinhLangs
                        .FirstOrDefault(x => x.IdGiaTri == idGiaTri
                                          && x.MaNgonNgu == "vi");

                    if (langVi != null)
                        langVi.GiaTri = giaTri;

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

        public bool Delete(int idGiaTri)
        {
            var strategy = _context.Database.CreateExecutionStrategy();

            return strategy.Execute(() =>
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    // 1. Xóa trong bảng Langs
                    var langs = _context.GiaTriThuocTinhLangs
                        .Where(x => x.IdGiaTri == idGiaTri)
                        .ToList();
                    _context.GiaTriThuocTinhLangs.RemoveRange(langs);

                    // 2. Xóa trong bảng Mappings (Liên kết biến thể)
                    var mappings = _context.BienTheThuocTinhs
                        .Where(x => x.IdGiaTri == idGiaTri)
                        .ToList();
                    _context.BienTheThuocTinhs.RemoveRange(mappings);

                    // 3. Xóa bảng chính
                    var data = _context.GiaTriThuocTinhs
                        .FirstOrDefault(x => x.IdGiaTri == idGiaTri);

                    if (data == null) return false;

                    _context.GiaTriThuocTinhs.Remove(data);

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
    }
}