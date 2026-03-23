using Microsoft.AspNetCore.SignalR;
using YourProject.Interfaces;

namespace Hub
{
    public class SanPhamHubService : ISanPhamHubService
    {
        private readonly IHubContext<SanPhamHub> _hubContext;

        public SanPhamHubService(IHubContext<SanPhamHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyBienTheUpdated(int idBienThe)
        {
            await _hubContext.Clients.All.SendAsync("BienTheUpdated", idBienThe);
        }

        public async Task NotifySanPhamUpdated(int idSanPham)
        {
            await _hubContext.Clients.All.SendAsync("SanPhamUpdated", idSanPham);
        }
    }
}