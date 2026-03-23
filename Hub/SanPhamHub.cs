using Microsoft.AspNetCore.SignalR;

namespace Hub
{
    public class SanPhamHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"Client kết nối: {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine($"Client ngắt kết nối: {Context.ConnectionId}");
            await base.OnDisconnectedAsync(exception);
        }
    }
}