using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IExchangeRateService
    {
        Task<Dictionary<string, decimal>> LayTiGia();
    }
}
