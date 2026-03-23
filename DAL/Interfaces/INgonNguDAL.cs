using Models;

namespace DAL.Interfaces
{
    public interface INgonNguDAL
    {
        List<NgonNgu> GetAll();
        NgonNgu GetByMa(string maNgonNgu);
        bool Create(NgonNgu ngonNgu);
        bool Update(NgonNgu ngonNgu);
        bool Delete(string maNgonNgu);
        bool SetMacDinh(string maNgonNgu);
        NgonNgu GetMacDinh();

        public bool UpdateTiGia(string maNgonNgu, decimal tiGia, DateTime ngay);

    }
}