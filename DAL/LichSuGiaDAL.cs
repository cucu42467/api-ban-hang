using DAL.Interfaces;
using Models;

namespace DAL
{
    public class LichSuGiaDAL : ILichSuGiaDAL
    {
        private readonly DatabaseContext _context;

        public LichSuGiaDAL(DatabaseContext context)
        {
            _context = context;
        }

        // Lấy lịch sử giá theo IdBienThe
        public List<LichSuGia> GetByBienThe(int idBienThe)
        {
            return (from l in _context.LichSuGias
                    where l.IdBienThe == idBienThe
                    orderby l.NgayThayDoi descending
                    select new LichSuGia
                    {
                        IdLichSu = l.IdLichSu,
                        MaLichSu = l.MaLichSu,
                        IdBienThe = l.IdBienThe,
                        GiaCu = l.GiaCu,
                        GiaMoi = l.GiaMoi,
                        NgayThayDoi = l.NgayThayDoi
                    })
                   .ToList();
        }

        // Lấy giá mới nhất của biến thể
        public LichSuGia GetLatest(int idBienThe)
        {
            return (from l in _context.LichSuGias
                    where l.IdBienThe == idBienThe
                    orderby l.NgayThayDoi descending
                    select new LichSuGia
                    {
                        IdLichSu = l.IdLichSu,
                        MaLichSu = l.MaLichSu,
                        IdBienThe = l.IdBienThe,
                        GiaCu = l.GiaCu,
                        GiaMoi = l.GiaMoi,
                        NgayThayDoi = l.NgayThayDoi
                    })
                   .FirstOrDefault();
        }
    }
}