using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ISanPhamDAL
    {
        List<SanPhamDTO> GetAll(string lang, int pageIndex, int pageSize);
        int GetTotalCount(string lang);
        public SanPhamDTO GetById(int id, string lang);

        public int CreateSanPham(string maSanPham, int idDanhMuc, string tenSanPham, string moTa);

        public bool Update(SanPham sanPham, string tenSanPham, string moTa);

        bool UpdateTrangThai(int id, int trangThai);
    }
}
