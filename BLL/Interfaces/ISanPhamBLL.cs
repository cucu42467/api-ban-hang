using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DAL.SanPhamDAL;

namespace BLL.Interfaces
{
    public interface ISanPhamBLL
    {
        public PagedResult<SanPhamDTO> GetAll(string lang, int pageIndex, int pageSize);

        SanPhamDTO GetById(int id, string lang);

        int CreateSanPham(string maSanPham, int idDanhMuc, string tenSanPham, string moTa);

        bool Update(SanPham sanPham, string tenSanPham, string moTa);

        bool UpdateTrangThai(int id, int trangThai);
    }
}
