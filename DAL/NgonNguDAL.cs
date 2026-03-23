using DAL.Interfaces;
using Models;

namespace DAL
{
    public class NgonNguDAL : INgonNguDAL
    {
        private readonly DatabaseContext _context;

        public NgonNguDAL(DatabaseContext context)
        {
            _context = context;
        }

        // Lấy danh sách ngôn ngữ
        public List<NgonNgu> GetAll()
        {
            return _context.NgonNgus.ToList();
        }

        // Lấy ngôn ngữ theo mã
        public NgonNgu GetByMa(string maNgonNgu)
        {
            return _context.NgonNgus
                .FirstOrDefault(x => x.MaNgonNgu == maNgonNgu);
        }

        // Thêm ngôn ngữ
        public bool Create(NgonNgu ngonNgu)
        {
            try
            {
                ngonNgu.NgayCapNhat = DateTime.Now;
                _context.NgonNgus.Add(ngonNgu);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Cập nhật ngôn ngữ
        public bool Update(NgonNgu ngonNgu)
        {
            var data = _context.NgonNgus
                .FirstOrDefault(x => x.MaNgonNgu == ngonNgu.MaNgonNgu);

            if (data == null)
                return false;

            data.TenNgonNgu = ngonNgu.TenNgonNgu;
            data.TiGiaDoiToai = ngonNgu.TiGiaDoiToai;
            data.KyHieuTienTe = ngonNgu.KyHieuTienTe;
            data.LaMacDinh = ngonNgu.LaMacDinh;
            data.NgayCapNhat = DateTime.Now;

            _context.SaveChanges();
            return true;
        }

        // Xóa ngôn ngữ
        public bool Delete(string maNgonNgu)
        {
            var data = _context.NgonNgus
                .FirstOrDefault(x => x.MaNgonNgu == maNgonNgu);

            if (data == null)
                return false;

            _context.NgonNgus.Remove(data);
            _context.SaveChanges();
            return true;
        }

        // Đặt ngôn ngữ mặc định
        public bool SetMacDinh(string maNgonNgu)
        {
            var tatCa = _context.NgonNgus.ToList();

            var ngonNgu = tatCa.FirstOrDefault(x => x.MaNgonNgu == maNgonNgu);
            if (ngonNgu == null)
                return false;

            // Bỏ mặc định tất cả
            foreach (var item in tatCa)
                item.LaMacDinh = 0;

            // Đặt mặc định cho ngôn ngữ được chọn
            ngonNgu.LaMacDinh = 1;
            ngonNgu.NgayCapNhat = DateTime.Now;

            _context.SaveChanges();
            return true;
        }

        // Lấy ngôn ngữ mặc định
        public NgonNgu GetMacDinh()
        {
            return _context.NgonNgus
                .FirstOrDefault(x => x.LaMacDinh == 1);
        }

        public bool UpdateTiGia(string maNgonNgu, decimal tiGia, DateTime ngay)
        {
            var nn = _context.NgonNgus.FirstOrDefault(x => x.MaNgonNgu == maNgonNgu);
            if (nn == null) return false;
            nn.TiGiaDoiToai = tiGia;
            nn.NgayCapNhat = ngay;
            _context.SaveChanges();
            return true;
        }
    }
}