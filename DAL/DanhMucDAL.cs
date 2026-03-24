using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DAL
{
    public class DanhMucDAL : IDanhMucDAL
    {
        private readonly DatabaseContext _context;

        public DanhMucDAL(DatabaseContext context)
        {
            _context = context;
        }

        private string Translate(string text, string lang)
        {
            return text + "_" + lang;
        }

        // Lấy danh sách danh mục
        public List<DanhMuc> GetAll(string lang, int pageIndex, int pageSize)
        {
            var query = from dm in _context.DanhMucs
                        join dml in _context.DanhMucLangs
                            on dm.IdDanhMuc equals dml.IdDanhMuc
                        where dml.MaNgonNgu == lang
                           && dm.IdTrangThai != 3
                        select new DanhMuc
                        {
                            IdDanhMuc = dm.IdDanhMuc,
                            MaDanhMuc = dm.MaDanhMuc,
                            TenDanhMuc = dml.TenDanhMuc,
                            IdTrangThai = dm.IdTrangThai
                        };

            return query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int GetTotalCount(string lang)
        {
            return (from dm in _context.DanhMucs
                    join dml in _context.DanhMucLangs
                        on dm.IdDanhMuc equals dml.IdDanhMuc
                    where dml.MaNgonNgu == lang
                       && dm.IdTrangThai != 3
                    select dm.IdDanhMuc)
                   .Count();
        }

        // Lấy danh mục theo Id
        public DanhMucDTO GetById(int id, string lang)
        {
            return (from dm in _context.DanhMucs
                    join dml in _context.DanhMucLangs
                        on dm.IdDanhMuc equals dml.IdDanhMuc
                    join tt in _context.TrangThais
                        on dm.IdTrangThai equals tt.IdTrangThai
                    where dm.IdDanhMuc == id
                       && dml.MaNgonNgu == lang
                    select new DanhMucDTO
                    {
                        IdDanhMuc = dm.IdDanhMuc,
                        MaDanhMuc = dm.MaDanhMuc,
                        TenDanhMuc = dml.TenDanhMuc,
                        IdTrangThai = dm.IdTrangThai,
                        TenTrangThai = tt.TenTrangThai
                    })
                   .FirstOrDefault();
        }

        // Thêm danh mục
        public int Create(string maDanhMuc, string tenDanhMuc, string moTa)
        {
            var strategy = _context.Database.CreateExecutionStrategy();

            return strategy.Execute(() =>
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    var danhMuc = new DanhMuc
                    {
                        MaDanhMuc = maDanhMuc,
                        IdTrangThai = 1
                    };

                    _context.DanhMucs.Add(danhMuc);
                    _context.SaveChanges();

                    int idDanhMuc = danhMuc.IdDanhMuc;
                    var languages = _context.NgonNgus.ToList();

                    foreach (var lang in languages)
                    {
                        string ten = tenDanhMuc;
                        string mota = moTa;

                        if (lang.MaNgonNgu != "vi")
                        {
                            ten = Translate(tenDanhMuc, lang.MaNgonNgu);
                            mota = Translate(moTa, lang.MaNgonNgu);
                        }

                        _context.DanhMucLangs.Add(new DanhMucLang
                        {
                            IdDanhMuc = idDanhMuc,
                            MaNgonNgu = lang.MaNgonNgu,
                            TenDanhMuc = ten,
                            MoTa = mota
                        });
                    }

                    _context.SaveChanges();
                    tran.Commit();
                    return idDanhMuc;
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            });
        }

        // Cập nhật danh mục
        public bool Update(DanhMuc danhMuc, string tenDanhMuc, string moTa)
        {
            var strategy = _context.Database.CreateExecutionStrategy();

            return strategy.Execute(() =>
            {
                using var tran = _context.Database.BeginTransaction();
                try
                {
                    var data = _context.DanhMucs
                        .FirstOrDefault(x => x.IdDanhMuc == danhMuc.IdDanhMuc);

                    if (data == null) return false;

                    data.MaDanhMuc = danhMuc.MaDanhMuc;
                    data.IdTrangThai = danhMuc.IdTrangThai;

                    var languages = _context.NgonNgus.ToList();
                    foreach (var lang in languages)
                    {
                        var dmLang = _context.DanhMucLangs
                            .FirstOrDefault(x => x.IdDanhMuc == danhMuc.IdDanhMuc
                                              && x.MaNgonNgu == lang.MaNgonNgu);

                        if (dmLang != null)
                        {
                            dmLang.TenDanhMuc = tenDanhMuc;
                            dmLang.MoTa = moTa;
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
            });
        }

        // Cập nhật trạng thái (soft delete)
        public bool UpdateTrangThai(int id, int trangThai)
        {
            var danhMuc = _context.DanhMucs
                .FirstOrDefault(x => x.IdDanhMuc == id);

            if (danhMuc == null)
                return false;

            var trangThaiExist = _context.TrangThais
                .Any(x => x.IdTrangThai == trangThai);

            if (!trangThaiExist)
                return false;

            danhMuc.IdTrangThai = trangThai;

            _context.SaveChanges();

            return true;
        }
    }
}