using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL
{
    public class SanPhamFullBLL : ISanPhamFullBLL
    {
        private readonly ISanPhamFullDAL _dal;

        public SanPhamFullBLL(ISanPhamFullDAL dal)
        {
            _dal = dal;
        }

        // ===== CREATE =====
        public async Task<bool> CreateFull(
            string maSanPham,
            int idDanhMuc,
            string tenSanPham,
            string moTa,
            string bienTheJson,
            List<(string fileName, bool laAnhChinh, int thuTu, int indexBienThe)> images
        )
        {
            // 🔥 Validate cơ bản
            if (string.IsNullOrEmpty(maSanPham))
                throw new Exception("Mã sản phẩm không được rỗng");

            if (string.IsNullOrEmpty(tenSanPham))
                throw new Exception("Tên sản phẩm không được rỗng");

            return await _dal.CreateFull(
                maSanPham,
                idDanhMuc,
                tenSanPham,
                moTa,
                bienTheJson,
                images
            );
        }

        // ===== UPDATE =====
        public async Task<bool> UpdateFull(
            int idSanPham,
            string maSanPham,
            int idDanhMuc,
            string tenSanPham,
            string moTa,
            string bienTheJson,
            List<(string fileName, bool laAnhChinh, int thuTu, int indexBienThe)> images,
            List<string> oldImagesToDelete
        )
        {
            if (idSanPham <= 0)
                throw new Exception("Id sản phẩm không hợp lệ");

            if (string.IsNullOrEmpty(tenSanPham))
                throw new Exception("Tên sản phẩm không được rỗng");

            return await _dal.UpdateFull(
                idSanPham,
                maSanPham,
                idDanhMuc,
                tenSanPham,
                moTa,
                bienTheJson,
                images,
                oldImagesToDelete
            );
        }
    }
}