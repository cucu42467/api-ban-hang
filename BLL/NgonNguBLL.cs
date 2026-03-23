using BLL.Interfaces;
using DAL.Interfaces;
using Models;

namespace BLL
{
    public class NgonNguBLL : INgonNguBLL
    {
        private readonly INgonNguDAL _ngonNguDAL;
        private readonly IExchangeRateService _exchangeService; // ← dùng interface


        public NgonNguBLL(INgonNguDAL ngonNguDAL, IExchangeRateService exchangeService) // ← dùng interface
        {
            _ngonNguDAL = ngonNguDAL;
            _exchangeService = exchangeService;
        }

        // Lấy danh sách ngôn ngữ
        public List<NgonNgu> GetAll()
        {
            return _ngonNguDAL.GetAll();
        }

        // Lấy ngôn ngữ theo mã
        public NgonNgu GetByMa(string maNgonNgu)
        {
            if (string.IsNullOrWhiteSpace(maNgonNgu))
                throw new Exception("Mã ngôn ngữ không được để trống");

            var data = _ngonNguDAL.GetByMa(maNgonNgu);
            if (data == null)
                throw new Exception("Không tìm thấy ngôn ngữ");

            return data;
        }

        // Thêm ngôn ngữ
        public bool Create(NgonNgu ngonNgu)
        {
            if (string.IsNullOrWhiteSpace(ngonNgu.MaNgonNgu))
                throw new Exception("Mã ngôn ngữ không được để trống");

            if (string.IsNullOrWhiteSpace(ngonNgu.TenNgonNgu))
                throw new Exception("Tên ngôn ngữ không được để trống");

            var existing = _ngonNguDAL.GetByMa(ngonNgu.MaNgonNgu);
            if (existing != null)
                throw new Exception("Mã ngôn ngữ đã tồn tại");

            return _ngonNguDAL.Create(ngonNgu);
        }

        // Cập nhật ngôn ngữ
        public bool Update(NgonNgu ngonNgu)
        {
            if (string.IsNullOrWhiteSpace(ngonNgu.MaNgonNgu))
                throw new Exception("Mã ngôn ngữ không được để trống");

            if (string.IsNullOrWhiteSpace(ngonNgu.TenNgonNgu))
                throw new Exception("Tên ngôn ngữ không được để trống");

            var result = _ngonNguDAL.Update(ngonNgu);
            if (!result)
                throw new Exception("Không tìm thấy ngôn ngữ");

            return result;
        }

        // Xóa ngôn ngữ
        public bool Delete(string maNgonNgu)
        {
            if (string.IsNullOrWhiteSpace(maNgonNgu))
                throw new Exception("Mã ngôn ngữ không được để trống");

            var result = _ngonNguDAL.Delete(maNgonNgu);
            if (!result)
                throw new Exception("Không tìm thấy ngôn ngữ");

            return result;
        }

        // Đặt ngôn ngữ mặc định
        public bool SetMacDinh(string maNgonNgu)
        {
            if (string.IsNullOrWhiteSpace(maNgonNgu))
                throw new Exception("Mã ngôn ngữ không được để trống");

            var result = _ngonNguDAL.SetMacDinh(maNgonNgu);
            if (!result)
                throw new Exception("Không tìm thấy ngôn ngữ");

            return result;
        }

        // Lấy ngôn ngữ mặc định
        public NgonNgu GetMacDinh()
        {
            var data = _ngonNguDAL.GetMacDinh();
            if (data == null)
                throw new Exception("Chưa có ngôn ngữ mặc định");

            return data;
        }

        public async Task CapNhatTuDong()
        {
            var rates = await _exchangeService.LayTiGia();
            foreach (var (ma, tiGia) in rates)
                _ngonNguDAL.UpdateTiGia(ma, tiGia, DateTime.UtcNow);
        }

        public bool CapNhatThuCong(string maNgonNgu, decimal tiGia)
            => _ngonNguDAL.UpdateTiGia(maNgonNgu, tiGia, DateTime.UtcNow);
    }
}