using DAL.Interfaces;
using Models;

namespace DAL
{
    public class BienTheThuocTinhDAL : IBienTheThuocTinhDAL
    {
        private readonly DatabaseContext _context;

        public BienTheThuocTinhDAL(DatabaseContext context)
        {
            _context = context;
        }

        // Lấy tất cả thuộc tính của 1 biến thể
        public List<ThuocTinhDTO> GetByBienThe(int idBienThe, string lang)
        {
            return (from btt in _context.BienTheThuocTinhs
                    join gtri in _context.GiaTriThuocTinhs
                        on btt.IdGiaTri equals gtri.IdGiaTri
                    join gtrил in _context.GiaTriThuocTinhLangs
                        on gtri.IdGiaTri equals gtrил.IdGiaTri
                    join ta in _context.ThuocTinhs
                        on gtri.IdThuocTinh equals ta.IdThuocTinh
                    join tal in _context.ThuocTinhLangs
                        on ta.IdThuocTinh equals tal.IdThuocTinh
                    where btt.IdBienThe == idBienThe
                       && gtrил.MaNgonNgu == lang
                       && tal.MaNgonNgu == lang
                    select new ThuocTinhDTO
                    {
                        IdThuocTinh = ta.IdThuocTinh,
                        MaThuocTinh = ta.MaThuocTinh,
                        TenThuocTinh = tal.TenThuocTinh,
                        DanhSachGiaTri = new List<GiaTriThuocTinhDTO>
                        {
                            new GiaTriThuocTinhDTO
                            {
                                IdGiaTri = gtri.IdGiaTri,
                                MaGiaTri = gtri.MaGiaTri,
                                IdThuocTinh = gtri.IdThuocTinh,
                                TenThuocTinh = tal.TenThuocTinh,
                                GiaTri = gtrил.GiaTri
                            }
                        }
                    })
                   .ToList();
        }

        // Gán thuộc tính cho biến thể
        public int Create(string maBienTheThuocTinh, int idBienThe, int idGiaTri)
        {
            var data = new BienTheThuocTinh
            {
                MaBienTheThuocTinh = maBienTheThuocTinh,
                IdBienThe = idBienThe,
                IdGiaTri = idGiaTri
            };

            _context.BienTheThuocTinhs.Add(data);
            _context.SaveChanges();

            return data.Id;
        }

        // Xóa 1 thuộc tính khỏi biến thể
        public bool Delete(int id)
        {
            var data = _context.BienTheThuocTinhs
                .FirstOrDefault(x => x.Id == id);

            if (data == null) return false;

            _context.BienTheThuocTinhs.Remove(data);
            _context.SaveChanges();

            return true;
        }

        // Xóa toàn bộ thuộc tính của 1 biến thể (dùng khi cập nhật lại)
        public bool DeleteByBienThe(int idBienThe)
        {
            var danhSach = _context.BienTheThuocTinhs
                .Where(x => x.IdBienThe == idBienThe)
                .ToList();

            if (!danhSach.Any()) return false;

            _context.BienTheThuocTinhs.RemoveRange(danhSach);
            _context.SaveChanges();

            return true;
        }
    }
}