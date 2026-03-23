using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourProject.Interfaces
{
    public interface ISanPhamHubService
        
    {
        Task NotifyBienTheUpdated(int idBienThe);
        Task NotifySanPhamUpdated(int idSanPham);
    }
}
