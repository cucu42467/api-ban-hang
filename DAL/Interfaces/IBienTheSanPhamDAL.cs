using Models;

namespace DAL.Interfaces
{
    public interface IBienTheSanPhamDAL
    {
        // ==================== GET ====================
        List<BienTheSanPhamDTO> GetAll(string lang);

        BienTheSanPhamDTO GetById(int id, string lang);

        List<BienTheSanPhamDTO> GetByIdSanPham(int idSanPham, string lang);

        // ==================== CREATE ====================
        int Create(string maBienThe, int idSanPham, double giaBan,
                   int tonKho, string maSKU, int idTrangThai);

        // ==================== UPDATE ====================
        bool Update(BienTheSanPham bienThe);

        bool UpdateGiaBan(int idBienThe, double giaMoi, string maLichSu);

        bool UpdateTonKho(int idBienThe, int soLuongThayDoi);

        bool UpdateTrangThai(int id, int trangThai);

        // ==================== LICH SU GIA ====================
        List<LichSuGia> GetLichSuGia(int idBienThe);
    }
}