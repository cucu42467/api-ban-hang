namespace DAL.Interfaces
{
    public interface ISanPhamFullDAL
    {
        Task<bool> CreateFull(
            string maSanPham,
            int idDanhMuc,
            string tenSanPham,
            string moTa,
            string bienTheJson,
            List<(string fileName, bool laAnhChinh, int thuTu, int indexBienThe)> images
        );

        Task<bool> UpdateFull(
            int idSanPham,
            string maSanPham,
            int idDanhMuc,
            string tenSanPham,
            string moTa,
            string bienTheJson,
            List<(string fileName, bool laAnhChinh, int thuTu, int indexBienThe)> images,
            List<string> oldImagesToDelete
        );
    }
}