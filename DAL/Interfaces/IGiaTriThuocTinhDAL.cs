using Models;

namespace DAL.Interfaces
{
    public interface IGiaTriThuocTinhDAL
    {
        List<GiaTriThuocTinhDTO> GetByThuocTinh(int idThuocTinh, string lang);
        GiaTriThuocTinhDTO GetById(int idGiaTri, string lang);
        int Create(string maGiaTri, int idThuocTinh, string giaTri);
        bool Update(int idGiaTri, string maGiaTri, string giaTri);
        bool Delete(int idGiaTri);
    }
}