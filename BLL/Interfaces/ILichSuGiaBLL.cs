using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ILichSuGiaBLL
    {
        List<LichSuGia> GetByBienThe(int idBienThe);
        LichSuGia GetLatest(int idBienThe);
    }
}
