using BLL.Interfaces;
using DAL.Interfaces;
using Models;

namespace BLL
{
    public class DanhMucBLL : IDanhMucBLL
    {
        private readonly IDanhMucDAL _danhMucDAL;

        public DanhMucBLL(IDanhMucDAL danhMucDAL)
        {
            _danhMucDAL = danhMucDAL;
        }

        public List<DanhMuc> GetAll(string lang, int pageIndex, int pageSize)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;

            return _danhMucDAL.GetAll(lang, pageIndex, pageSize);
        }

        public int GetTotalCount(string lang)
        {
            return _danhMucDAL.GetTotalCount(lang);
        }

        public DanhMucDTO GetById(int id, string lang)
        {
            if (id <= 0)
                throw new ArgumentException("Id danh mục không hợp lệ.");

            return _danhMucDAL.GetById(id, lang);
        }

        public int Create(string maDanhMuc, string tenDanhMuc, string moTa)
        {
            if (string.IsNullOrWhiteSpace(maDanhMuc))
                throw new ArgumentException("Mã danh mục không được để trống.");

            if (string.IsNullOrWhiteSpace(tenDanhMuc))
                throw new ArgumentException("Tên danh mục không được để trống.");

            return _danhMucDAL.Create(maDanhMuc, tenDanhMuc, moTa);
        }

        public bool Update(DanhMuc danhMuc, string tenDanhMuc, string moTa)
        {
            if (danhMuc == null)
                throw new ArgumentNullException(nameof(danhMuc));

            if (string.IsNullOrWhiteSpace(tenDanhMuc))
                throw new ArgumentException("Tên danh mục không được để trống.");

            return _danhMucDAL.Update(danhMuc, tenDanhMuc, moTa);
        }

        public bool UpdateTrangThai(int id, int trangThai)
        {
            if (id <= 0)
                throw new ArgumentException("Id danh mục không hợp lệ.");

            return _danhMucDAL.UpdateTrangThai(id, trangThai);
        }
    }
}