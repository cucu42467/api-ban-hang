using BLL.Interfaces;
using DAL.Interfaces;
using Models;
using BLL.Interfaces;

namespace Services
{
    public class ThuocTinhBLL : IThuocTinhBLL
    {
        private readonly IThuocTinhDAL _thuocTinhDAL;

        public ThuocTinhBLL(IThuocTinhDAL thuocTinhDAL)
        {
            _thuocTinhDAL = thuocTinhDAL;
        }

        public List<ThuocTinhDTO> GetAll(string lang)
        {
            if (string.IsNullOrWhiteSpace(lang))
                lang = "vi";

            return _thuocTinhDAL.GetAll(lang);
        }

        public ThuocTinhDTO GetById(int idThuocTinh, string lang)
        {
            if (idThuocTinh <= 0)
                throw new ArgumentException("Id thuộc tính không hợp lệ.");

            if (string.IsNullOrWhiteSpace(lang))
                lang = "vi";

            return _thuocTinhDAL.GetById(idThuocTinh, lang);
        }

        public int Create(string maThuocTinh, string tenThuocTinh)
        {
            if (string.IsNullOrWhiteSpace(maThuocTinh))
                throw new ArgumentException("Mã thuộc tính không được để trống.");

            if (string.IsNullOrWhiteSpace(tenThuocTinh))
                throw new ArgumentException("Tên thuộc tính không được để trống.");

            return _thuocTinhDAL.Create(maThuocTinh, tenThuocTinh);
        }

        public bool Update(int idThuocTinh, string maThuocTinh, string tenThuocTinh)
        {
            if (idThuocTinh <= 0)
                throw new ArgumentException("Id thuộc tính không hợp lệ.");

            if (string.IsNullOrWhiteSpace(maThuocTinh))
                throw new ArgumentException("Mã thuộc tính không được để trống.");

            if (string.IsNullOrWhiteSpace(tenThuocTinh))
                throw new ArgumentException("Tên thuộc tính không được để trống.");

            return _thuocTinhDAL.Update(idThuocTinh, maThuocTinh, tenThuocTinh);
        }
    }
}