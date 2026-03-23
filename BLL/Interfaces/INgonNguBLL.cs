using Models;

namespace BLL.Interfaces
{
    public interface INgonNguBLL
    {
        List<NgonNgu> GetAll();
        NgonNgu GetByMa(string maNgonNgu);
        bool Create(NgonNgu ngonNgu);
        bool Update(NgonNgu ngonNgu);
        bool Delete(string maNgonNgu);
        bool SetMacDinh(string maNgonNgu);
        NgonNgu GetMacDinh();
        public bool CapNhatThuCong(string maNgonNgu, decimal tiGia);
        Task CapNhatTuDong();
    }
}