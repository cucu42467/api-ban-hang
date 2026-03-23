using Models;

namespace BLL.Interfaces
{
    public interface IDanhMucBLL
    {
        List<DanhMuc> GetAll(string lang, int pageIndex, int pageSize);
        int GetTotalCount(string lang);
        DanhMucDTO GetById(int id, string lang);
        int Create(string maDanhMuc, string tenDanhMuc, string moTa);
        bool Update(DanhMuc danhMuc, string tenDanhMuc, string moTa);
        bool UpdateTrangThai(int id, int trangThai);
    }
}