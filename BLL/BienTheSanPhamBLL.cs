using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Models;

namespace BLL
{
    public class BienTheSanPhamBLL : IBienTheSanPhamBLL
    {
        private readonly IBienTheSanPhamDAL _bienTheSanPhamDAL;

        public BienTheSanPhamBLL(IBienTheSanPhamDAL bienTheSanPhamDAL)
        {
            _bienTheSanPhamDAL = bienTheSanPhamDAL;
        }

        // ==================== GET ALL ====================
        public List<BienTheSanPhamDTO> GetAll(string lang = "vi")
        {
            return _bienTheSanPhamDAL.GetAll(lang);
        }

        // ==================== GET BY ID ====================
        public BienTheSanPhamDTO GetById(int id, string lang = "vi")
        {
            if (id <= 0)
                throw new ArgumentException("Id biến thể không hợp lệ.");

            var bienThe = _bienTheSanPhamDAL.GetById(id, lang);

            if (bienThe == null)
                throw new Exception($"Không tìm thấy biến thể với Id = {id}.");

            return bienThe;
        }

        // ==================== GET BY ID SAN PHAM ====================
        public List<BienTheSanPhamDTO> GetByIdSanPham(int idSanPham, string lang = "vi")
        {
            if (idSanPham <= 0)
                throw new ArgumentException("Id sản phẩm không hợp lệ.");

            return _bienTheSanPhamDAL.GetByIdSanPham(idSanPham, lang);
        }

        // ==================== CREATE ====================
        public int Create(string maBienThe, int idSanPham, double giaBan,
                          int tonKho, string maSKU, int idTrangThai)
        {
            // Validate
            if (string.IsNullOrWhiteSpace(maBienThe))
                throw new ArgumentException("Mã biến thể không được để trống.");

            if (string.IsNullOrWhiteSpace(maSKU))
                throw new ArgumentException("Mã SKU không được để trống.");

            if (idSanPham <= 0)
                throw new ArgumentException("Id sản phẩm không hợp lệ.");

            if (giaBan < 0)
                throw new ArgumentException("Giá bán không được âm.");

            if (tonKho < 0)
                throw new ArgumentException("Tồn kho không được âm.");

            return _bienTheSanPhamDAL.Create(maBienThe, idSanPham, giaBan,
                                             tonKho, maSKU, idTrangThai);
        }

        // ==================== UPDATE ====================
        public bool Update(BienTheSanPham bienThe)
        {
            if (bienThe == null)
                throw new ArgumentNullException("Thông tin biến thể không được null.");

            if (bienThe.IdBienThe <= 0)
                throw new ArgumentException("Id biến thể không hợp lệ.");

            if (string.IsNullOrWhiteSpace(bienThe.MaBienThe))
                throw new ArgumentException("Mã biến thể không được để trống.");

            if (string.IsNullOrWhiteSpace(bienThe.MaSKU))
                throw new ArgumentException("Mã SKU không được để trống.");

            if (bienThe.GiaBan < 0)
                throw new ArgumentException("Giá bán không được âm.");

            if (bienThe.TonKho < 0)
                throw new ArgumentException("Tồn kho không được âm.");

            return _bienTheSanPhamDAL.Update(bienThe);
        }

        // ==================== UPDATE GIA BAN ====================
        public bool UpdateGiaBan(int idBienThe, double giaMoi)
        {
            if (idBienThe <= 0)
                throw new ArgumentException("Id biến thể không hợp lệ.");

            if (giaMoi < 0)
                throw new ArgumentException("Giá bán mới không được âm.");

            // Tự sinh mã lịch sử giá
            string maLichSu = "LSG_" + DateTime.Now.ToString("yyyyMMddHHmmss")
                                      + "_" + idBienThe;

            return _bienTheSanPhamDAL.UpdateGiaBan(idBienThe, giaMoi, maLichSu);
        }

        // ==================== UPDATE TON KHO ====================
        public bool UpdateTonKho(int idBienThe, int soLuongThayDoi)
        {
            if (idBienThe <= 0)
                throw new ArgumentException("Id biến thể không hợp lệ.");

            if (soLuongThayDoi == 0)
                throw new ArgumentException("Số lượng thay đổi không được bằng 0.");

            // Kiểm tra tồn kho không được xuống dưới 0
            var bienThe = _bienTheSanPhamDAL.GetById(idBienThe, "vi");

            if (bienThe == null)
                throw new Exception($"Không tìm thấy biến thể với Id = {idBienThe}.");

            if (bienThe.TonKho + soLuongThayDoi < 0)
                throw new InvalidOperationException(
                    $"Tồn kho không đủ. Hiện tại: {bienThe.TonKho}, " +
                    $"yêu cầu giảm: {Math.Abs(soLuongThayDoi)}.");

            return _bienTheSanPhamDAL.UpdateTonKho(idBienThe, soLuongThayDoi);
        }

        // ==================== UPDATE TRANG THAI ====================
        public bool UpdateTrangThai(int id, int trangThai)
        {
            if (id <= 0)
                throw new ArgumentException("Id biến thể không hợp lệ.");

            if (trangThai <= 0)
                throw new ArgumentException("Trạng thái không hợp lệ.");

            return _bienTheSanPhamDAL.UpdateTrangThai(id, trangThai);
        }

        // ==================== GET LICH SU GIA ====================
        public List<LichSuGia> GetLichSuGia(int idBienThe)
        {
            if (idBienThe <= 0)
                throw new ArgumentException("Id biến thể không hợp lệ.");

            return _bienTheSanPhamDAL.GetLichSuGia(idBienThe);
        }
    }
}