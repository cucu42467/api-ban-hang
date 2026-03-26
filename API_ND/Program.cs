using BLL;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services;
using Microsoft.Extensions.FileProviders;
using System.IO;
using API_ND.Hubs; // 1. Đảm bảo bạn đã tạo folder Hubs và file ProductHub.cs

var builder = WebApplication.CreateBuilder(args);

// --- 1. CẤU HÌNH DATABASE (MYSQL) ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseMySql(connectionString,
        new MySqlServerVersion(new Version(8, 0, 21)),
        mySqlOptions => mySqlOptions.EnableRetryOnFailure(
            maxRetryCount: 10,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null)
    )
);

// --- 2. CẤU HÌNH SIGNALR ---
builder.Services.AddSignalR(); // Đăng ký dịch vụ SignalR

// --- 3. CẤU HÌNH CORS (CẬP NHẬT CHO SIGNALR) ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        // Khi dùng SignalR với .withAutomaticReconnect() ở FE, 
        // tốt nhất nên cho phép Credentials và chỉ định Origin cụ thể nếu cần.
        // Nhưng để đồ án chạy thông suốt, mình để AllowAnyHeader/Method.
        policy.SetIsOriginAllowed(_ => true) // Cho phép tất cả các nguồn (Origin)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials(); // BẮT BUỘC phải có dòng này cho SignalR
    });
});

// --- 4. ĐĂNG KÝ SERVICES (DI) ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISanPhamDAL, SanPhamDAL>();
builder.Services.AddScoped<ISanPhamBLL, SanPhamBLL>();
builder.Services.AddScoped<INgonNguDAL, NgonNguDAL>();
builder.Services.AddScoped<INgonNguBLL, NgonNguBLL>();
builder.Services.AddScoped<IBienTheSanPhamDAL, BienTheSanPhamDAL>();
builder.Services.AddScoped<IBienTheSanPhamBLL, BienTheSanPhamBLL>();
builder.Services.AddHttpClient<IExchangeRateService, ExchangeRateService>();
builder.Services.AddScoped<IDanhMucBLL, DanhMucBLL>();
builder.Services.AddScoped<IDanhMucDAL, DanhMucDAL>();
builder.Services.AddScoped<ILichSuGiaBLL, LichSuGiaBLL>();
builder.Services.AddScoped<ILichSuGiaDAL, LichSuGiaDAL>();
builder.Services.AddScoped<ISanPhamFullDAL, SanPhamFullDAL>();
builder.Services.AddScoped<ISanPhamFullBLL, SanPhamFullBLL>();
builder.Services.AddScoped<IThuocTinhBLL, ThuocTinhBLL>();
builder.Services.AddScoped<IThuocTinhDAL, ThuocTinhDAL>();


var app = builder.Build();

// --- 5. CẤU HÌNH SWAGGER ---
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Van Phong Pham HA V1");
    c.RoutePrefix = string.Empty;
});

// --- 6. XỬ LÝ FILE TĨNH (THƯ MỤC ANH) ---
string anhPath = Path.GetFullPath(Path.Combine(app.Environment.ContentRootPath, "Anh"));

if (!Directory.Exists(anhPath))
{
    Directory.CreateDirectory(anhPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(anhPath),
    RequestPath = "/Anh",
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=604800");
    }
});

app.UseStaticFiles();

// --- 7. MIDDLEWARES & ROUTING ---
app.UseCors("AllowAll"); // Phải đặt trước HttpsRedirection và Map
// app.UseHttpsRedirection(); // Nếu chạy trên Render, Render đã lo HTTPS, có thể tắt dòng này để tránh lỗi Port
app.UseAuthorization();

app.MapControllers();

// --- 8. MAP SIGNALR HUB ---
// Địa chỉ kết nối sẽ là: https://api-ban-hang-4.onrender.com/productHub
app.MapHub<ProductHub>("/productHub");

app.Run();