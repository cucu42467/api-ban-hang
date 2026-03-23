using BLL.Interfaces;
using DAL.Interfaces;
using Models;
using static DAL.SanPhamDAL;

namespace BLL
{
    public class SanPhamBLL : ISanPhamBLL
    {
        private readonly ISanPhamDAL _sanPhamDAL;

        public SanPhamBLL(ISanPhamDAL sanPhamDAL)
        {
            _sanPhamDAL = sanPhamDAL;
        }

        // Lấy danh sách sản phẩm
        public PagedResult<SanPhamDTO> GetAll(string lang, int pageIndex, int pageSize)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;

            var data = _sanPhamDAL.GetAll(lang, pageIndex, pageSize);
            var total = _sanPhamDAL.GetTotalCount(lang);

            return new PagedResult<SanPhamDTO>
            {
                Data = data,
                TotalCount = total,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        }

        // Lấy chi tiết sản phẩm
        public SanPhamDTO GetById(int id, string lang)
        {
            if (id <= 0)
                return null;

            if (string.IsNullOrEmpty(lang))
                lang = "vi";

            return _sanPhamDAL.GetById(id, lang);
        }

        // Thêm sản phẩm
        public int CreateSanPham(string maSanPham, int idDanhMuc, string tenSanPham, string moTa)
        {
            if (string.IsNullOrEmpty(maSanPham))
                throw new Exception("Mã sản phẩm không được rỗng");

            if (string.IsNullOrEmpty(tenSanPham))
                throw new Exception("Tên sản phẩm không được rỗng");

            return _sanPhamDAL.CreateSanPham(maSanPham, idDanhMuc, tenSanPham, moTa);
        }

        // Cập nhật sản phẩm
        public bool Update(SanPham sanPham, string tenSanPham, string moTa)
        {
            if (sanPham == null)
                return false;

            if (sanPham.IdSanPham <= 0)
                return false;

            return _sanPhamDAL.Update(sanPham, tenSanPham, moTa);
        }

        // Cập nhật trạng thái
        public bool UpdateTrangThai(int id, int trangThai)
        {
            if (id <= 0)
                return false;

            return _sanPhamDAL.UpdateTrangThai(id, trangThai);
        }
    }
}