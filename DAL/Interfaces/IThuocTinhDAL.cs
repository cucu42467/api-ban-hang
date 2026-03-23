using Models;

namespace DAL.Interfaces
{
    public interface IThuocTinhDAL
    {
        List<ThuocTinhDTO> GetAll(string lang);
        ThuocTinhDTO GetById(int idThuocTinh, string lang);
        int Create(string maThuocTinh, string tenThuocTinh);
        bool Update(int idThuocTinh, string maThuocTinh, string tenThuocTinh);
    }
}