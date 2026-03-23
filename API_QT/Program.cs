using DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- CẤU HÌNH CHO PHÉP TRUY CẬP TỪ BÊN NGOÀI (CORS) ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()   // Cho phép tất cả các nguồn (IP khác nhau)
              .AllowAnyMethod()   // Cho phép tất cả các phương thức (GET, POST, PUT, DELETE)
              .AllowAnyHeader();  // Cho phép tất cả các Header
    });
});


// 1. Lấy đường dẫn của thư mục API_ND
string apiPath = builder.Environment.ContentRootPath;

// 2. Lùi lại 3 cấp: API_ND -> BE -> BE -> Gốc (App mua bán đồ nội bộ)
// Sau đó mới đi vào DATA/SQL.db
string dbPath = Path.GetFullPath(Path.Combine(apiPath, "..", "..", "..", "DATA", "SQL.db"));

// In ra để bạn kiểm tra xem nó có hiện đúng: D:\Phát triển\App mua bán đồ nội bộ\DATA\SQL.db không
Console.WriteLine("---------------------------------------------------------");
Console.WriteLine($"[DATABASE CHECK] Đường dẫn thực tế: {dbPath}");
Console.WriteLine("---------------------------------------------------------");

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlite($"Data Source={dbPath}")
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
