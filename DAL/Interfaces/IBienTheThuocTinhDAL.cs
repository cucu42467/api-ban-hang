using Models;

namespace DAL.Interfaces
{
    public interface IBienTheThuocTinhDAL
    {
        List<ThuocTinhDTO> GetByBienThe(int idBienThe, string lang);
        int Create(string maBienTheThuocTinh, int idBienThe, int idGiaTri);
        bool Delete(int id);
        bool DeleteByBienThe(int idBienThe);
    }
}