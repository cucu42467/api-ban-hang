using BLL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Services
{
    public class TiGiaBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _services;

        public TiGiaBackgroundService(IServiceProvider services)
        {
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                try
                {
                    using var scope = _services.CreateScope();
                    var bll = scope.ServiceProvider.GetRequiredService<INgonNguBLL>();
                    await bll.CapNhatTuDong();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[TiGiaBackgroundService] Lỗi: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromHours(6), ct);
            }
        }
    }
}