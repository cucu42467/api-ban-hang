using DAL;
using Models;

namespace DAL.Interfaces
{
    public interface ILichSuGiaDAL
    {
        List<LichSuGia> GetByBienThe(int idBienThe);
        LichSuGia GetLatest(int idBienThe);
    }
}
