using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Models;
using BLL.Interfaces;

namespace Services
{
    public class LichSuGiaBLL : ILichSuGiaBLL
    {
        private readonly ILichSuGiaDAL _lichSuGiaDAL;

        public LichSuGiaBLL(ILichSuGiaDAL lichSuGiaDAL)
        {
            _lichSuGiaDAL = lichSuGiaDAL;
        }

        public List<LichSuGia> GetByBienThe(int idBienThe)
        {
            if (idBienThe <= 0)
                throw new ArgumentException("Id biến thể không hợp lệ.");

            return _lichSuGiaDAL.GetByBienThe(idBienThe);
        }

        public LichSuGia GetLatest(int idBienThe)
        {
            if (idBienThe <= 0)
                throw new ArgumentException("Id biến thể không hợp lệ.");

            return _lichSuGiaDAL.GetLatest(idBienThe);
        }
    }
}