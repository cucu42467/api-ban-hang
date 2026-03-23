using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IThuocTinhBLL
    {
        List<ThuocTinhDTO> GetAll(string lang);
        ThuocTinhDTO GetById(int idThuocTinh, string lang);
        int Create(string maThuocTinh, string tenThuocTinh);
        bool Update(int idThuocTinh, string maThuocTinh, string tenThuocTinh);
    }
}
