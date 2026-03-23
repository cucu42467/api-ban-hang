using Models;

namespace DAL.Interfaces
{
    public interface IHinhAnhSanPhamDAL
    {
        List<HinhAnhSanPhamDTO> GetByBienThe(int idBienThe);
        List<HinhAnhSanPhamDTO> GetBySanPham(int idSanPham);
        int Create(HinhAnhSanPham hinhAnh);
        bool Delete(int idHinhAnh);
        bool SetAnhChinh(int idHinhAnh, int idBienThe);
    }
}