using Microsoft.AspNetCore.SignalR;

namespace API_ND.Hubs
{
    // Đây là cái "Trạm phát sóng"
    public class ProductHub : Hub
    {
        // Khi một máy khách kết nối thành công
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"==> Thiet bi moi ket noi: {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }

        // Khi một máy khách ngắt kết nối
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine($"==> Thiet bi ngat ket noi: {Context.ConnectionId}");
            await base.OnDisconnectedAsync(exception);
        }
    }
}