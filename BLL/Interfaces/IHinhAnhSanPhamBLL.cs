using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IHinhAnhSanPhamBLL
    {
        List<HinhAnhSanPhamDTO> GetByBienThe(int idBienThe);
        List<HinhAnhSanPhamDTO> GetBySanPham(int idSanPham);
        int Create(HinhAnhSanPham hinhAnh);
        bool Delete(int idHinhAnh);
        bool SetAnhChinh(int idHinhAnh, int idBienThe);
    }
}
