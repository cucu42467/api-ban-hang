using Models;

namespace BLL.Interfaces
{
    public interface IBienTheSanPhamBLL
    {
        // ==================== GET ====================
        List<BienTheSanPhamDTO> GetAll(string lang = "vi");

        BienTheSanPhamDTO GetById(int id, string lang = "vi");

        List<BienTheSanPhamDTO> GetByIdSanPham(int idSanPham, string lang = "vi");

        // ==================== CREATE ====================
        int Create(string maBienThe, int idSanPham, double giaBan,
                   int tonKho, string maSKU, int idTrangThai);

        // ==================== UPDATE ====================
        bool Update(BienTheSanPham bienThe);

        bool UpdateGiaBan(int idBienThe, double giaMoi);

        bool UpdateTonKho(int idBienThe, int soLuongThayDoi);

        bool UpdateTrangThai(int id, int trangThai);

        // ==================== LICH SU GIA ====================
        List<LichSuGia> GetLichSuGia(int idBienThe);
    }
}