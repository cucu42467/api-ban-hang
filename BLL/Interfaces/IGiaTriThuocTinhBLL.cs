using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IGiaTriThuocTinhBLL
    {
        List<GiaTriThuocTinhDTO> GetByThuocTinh(int idThuocTinh, string lang);
        GiaTriThuocTinhDTO GetById(int idGiaTri, string lang);
        int Create(string maGiaTri, int idThuocTinh, string giaTri);
        bool Update(int idGiaTri, string maGiaTri, string giaTri);
        bool Delete(int idGiaTri);
    }
}
