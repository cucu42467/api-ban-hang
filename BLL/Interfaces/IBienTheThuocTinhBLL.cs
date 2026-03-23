using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IBienTheThuocTinhBLL
    {
        List<ThuocTinhDTO> GetByBienThe(int idBienThe, string lang);
        int Create(string maBienTheThuocTinh, int idBienThe, int idGiaTri);
        bool Delete(int id);
        bool DeleteByBienThe(int idBienThe);
    }
}
