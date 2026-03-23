using DAL.Interfaces;
using Models;

namespace DAL
{
    public class HinhAnhSanPhamDAL : IHinhAnhSanPhamDAL
    {
        private readonly DatabaseContext _context;

        public HinhAnhSanPhamDAL(DatabaseContext context)
        {
            _context = context;
        }

        // Lấy hình ảnh theo IdBienThe
        public List<HinhAnhSanPhamDTO> GetByBienThe(int idBienThe)
        {
            return (from h in _context.HinhAnhSanPhams
                    where h.IdBienThe == idBienThe
                    orderby h.LaAnhChinh descending, h.ThuTu
                    select new HinhAnhSanPhamDTO
                    {
                        IdHinhAnh = h.IdHinhAnh,
                        MaHinhAnh = h.MaHinhAnh,
                        DuongDanAnh = h.DuongDanAnh,
                        LaAnhChinh = h.LaAnhChinh,
                        ThuTu = h.ThuTu
                    })
                   .ToList();
        }

        // Lấy hình ảnh theo IdSanPham
        public List<HinhAnhSanPhamDTO> GetBySanPham(int idSanPham)
        {
            return (from h in _context.HinhAnhSanPhams
                    where h.IdSanPham == idSanPham
                    orderby h.LaAnhChinh descending, h.ThuTu
                    select new HinhAnhSanPhamDTO
                    {
                        IdHinhAnh = h.IdHinhAnh,
                        MaHinhAnh = h.MaHinhAnh,
                        DuongDanAnh = h.DuongDanAnh,
                        LaAnhChinh = h.LaAnhChinh,
                        ThuTu = h.ThuTu
                    })
                   .ToList();
        }

        // Thêm hình ảnh
        public int Create(HinhAnhSanPham hinhAnh)
        {
            _context.HinhAnhSanPhams.Add(hinhAnh);
            _context.SaveChanges();
            return hinhAnh.IdHinhAnh;
        }

        // Xóa hình ảnh
        public bool Delete(int idHinhAnh)
        {
            var data = _context.HinhAnhSanPhams
                .FirstOrDefault(x => x.IdHinhAnh == idHinhAnh);

            if (data == null) return false;

            _context.HinhAnhSanPhams.Remove(data);
            _context.SaveChanges();

            return true;
        }

        // Đặt ảnh chính
        public bool SetAnhChinh(int idHinhAnh, int idBienThe)
        {
            using var tran = _context.Database.BeginTransaction();
            try
            {
                // Bỏ ảnh chính cũ
                var danhSach = _context.HinhAnhSanPhams
                    .Where(x => x.IdBienThe == idBienThe)
                    .ToList();

                foreach (var h in danhSach)
                    h.LaAnhChinh = false;

                // Set ảnh chính mới
                var anhMoi = danhSach.FirstOrDefault(x => x.IdHinhAnh == idHinhAnh);
                if (anhMoi == null) return false;

                anhMoi.LaAnhChinh = true;

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