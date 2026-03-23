using BLL.Interfaces;
using DAL.Interfaces;
using Models;
using BLL.Interfaces;

namespace BLL
{
    public class BienTheThuocTinhBLL : IBienTheThuocTinhBLL
    {
        private readonly IBienTheThuocTinhDAL _bienTheThuocTinhDAL;

        public BienTheThuocTinhBLL(IBienTheThuocTinhDAL bienTheThuocTinhDAL)
        {
            _bienTheThuocTinhDAL = bienTheThuocTinhDAL;
        }

        public List<ThuocTinhDTO> GetByBienThe(int idBienThe, string lang)
        {
            if (idBienThe <= 0)
                throw new ArgumentException("Id biến thể không hợp lệ.");

            if (string.IsNullOrWhiteSpace(lang))
                lang = "vi";

            return _bienTheThuocTinhDAL.GetByBienThe(idBienThe, lang);
        }

        public int Create(string maBienTheThuocTinh, int idBienThe, int idGiaTri)
        {
            if (string.IsNullOrWhiteSpace(maBienTheThuocTinh))
                throw new ArgumentException("Mã biến thể thuộc tính không được để trống.");

            if (idBienThe <= 0)
                throw new ArgumentException("Id biến thể không hợp lệ.");

            if (idGiaTri <= 0)
                throw new ArgumentException("Id giá trị không hợp lệ.");

            return _bienTheThuocTinhDAL.Create(maBienTheThuocTinh, idBienThe, idGiaTri);
        }

        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id không hợp lệ.");

            return _bienTheThuocTinhDAL.Delete(id);
        }

        public bool DeleteByBienThe(int idBienThe)
        {
            if (idBienThe <= 0)
                throw new ArgumentException("Id biến thể không hợp lệ.");

            return _bienTheThuocTinhDAL.DeleteByBienThe(idBienThe);
        }
    }
}