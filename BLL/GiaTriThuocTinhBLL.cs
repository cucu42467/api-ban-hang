using BLL.Interfaces;
using DAL.Interfaces;
using Models;
using BLL.Interfaces;

namespace Services
{
    public class GiaTriThuocTinhBLL : IGiaTriThuocTinhBLL
    {
        private readonly IGiaTriThuocTinhDAL _giaTriDAL;

        public GiaTriThuocTinhBLL(IGiaTriThuocTinhDAL giaTriDAL)
        {
            _giaTriDAL = giaTriDAL;
        }

        public List<GiaTriThuocTinhDTO> GetByThuocTinh(int idThuocTinh, string lang)
        {
            if (idThuocTinh <= 0)
                throw new ArgumentException("Id thuộc tính không hợp lệ.");

            if (string.IsNullOrWhiteSpace(lang))
                lang = "vi";

            return _giaTriDAL.GetByThuocTinh(idThuocTinh, lang);
        }

        public GiaTriThuocTinhDTO GetById(int idGiaTri, string lang)
        {
            if (idGiaTri <= 0)
                throw new ArgumentException("Id giá trị không hợp lệ.");

            if (string.IsNullOrWhiteSpace(lang))
                lang = "vi";

            return _giaTriDAL.GetById(idGiaTri, lang);
        }

        public int Create(string maGiaTri, int idThuocTinh, string giaTri)
        {
            if (string.IsNullOrWhiteSpace(maGiaTri))
                throw new ArgumentException("Mã giá trị không được để trống.");

            if (idThuocTinh <= 0)
                throw new ArgumentException("Id thuộc tính không hợp lệ.");

            if (string.IsNullOrWhiteSpace(giaTri))
                throw new ArgumentException("Giá trị không được để trống.");

            return _giaTriDAL.Create(maGiaTri, idThuocTinh, giaTri);
        }

        public bool Update(int idGiaTri, string maGiaTri, string giaTri)
        {
            if (idGiaTri <= 0)
                throw new ArgumentException("Id giá trị không hợp lệ.");

            if (string.IsNullOrWhiteSpace(maGiaTri))
                throw new ArgumentException("Mã giá trị không được để trống.");

            if (string.IsNullOrWhiteSpace(giaTri))
                throw new ArgumentException("Giá trị không được để trống.");

            return _giaTriDAL.Update(idGiaTri, maGiaTri, giaTri);
        }

        public bool Delete(int idGiaTri)
        {
            if (idGiaTri <= 0)
                throw new ArgumentException("Id giá trị không hợp lệ.");

            return _giaTriDAL.Delete(idGiaTri);
        }
    }
}