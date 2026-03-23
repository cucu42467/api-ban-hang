using BLL.Interfaces;
using DAL.Interfaces;
using Models;
using BLL.Interfaces;

namespace Services
{
    public class HinhAnhSanPhamBLL : IHinhAnhSanPhamBLL
    {
        private readonly IHinhAnhSanPhamDAL _hinhAnhDAL;

        public HinhAnhSanPhamBLL(IHinhAnhSanPhamDAL hinhAnhDAL)
        {
            _hinhAnhDAL = hinhAnhDAL;
        }

        public List<HinhAnhSanPhamDTO> GetByBienThe(int idBienThe)
        {
            if (idBienThe <= 0)
                throw new ArgumentException("Id biến thể không hợp lệ.");

            return _hinhAnhDAL.GetByBienThe(idBienThe);
        }

        public List<HinhAnhSanPhamDTO> GetBySanPham(int idSanPham)
        {
            if (idSanPham <= 0)
                throw new ArgumentException("Id sản phẩm không hợp lệ.");

            return _hinhAnhDAL.GetBySanPham(idSanPham);
        }

        public int Create(HinhAnhSanPham hinhAnh)
        {
            if (hinhAnh == null)
                throw new ArgumentNullException(nameof(hinhAnh));

            if (string.IsNullOrWhiteSpace(hinhAnh.DuongDanAnh))
                throw new ArgumentException("Đường dẫn ảnh không được để trống.");

            if (string.IsNullOrWhiteSpace(hinhAnh.MaHinhAnh))
                throw new ArgumentException("Mã hình ảnh không được để trống.");

            return _hinhAnhDAL.Create(hinhAnh);
        }

        public bool Delete(int idHinhAnh)
        {
            if (idHinhAnh <= 0)
                throw new ArgumentException("Id hình ảnh không hợp lệ.");

            return _hinhAnhDAL.Delete(idHinhAnh);
        }

        public bool SetAnhChinh(int idHinhAnh, int idBienThe)
        {
            if (idHinhAnh <= 0)
                throw new ArgumentException("Id hình ảnh không hợp lệ.");

            if (idBienThe <= 0)
                throw new ArgumentException("Id biến thể không hợp lệ.");

            return _hinhAnhDAL.SetAnhChinh(idHinhAnh, idBienThe);
        }
    }
}