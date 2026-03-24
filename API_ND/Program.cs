using BLL;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services;
using Microsoft.Extensions.FileProviders;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// --- 1. CẤU HÌNH CORS (GIỮ NGUYÊN) ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Render cần lắng nghe trên 0.0.0.0 và Port 10000
builder.WebHost.UseUrls("http://0.0.0.0:10000");

// --- 2. CẤU HÌNH ĐƯỜNG DẪN DATABASE & ẢNH ---
// Sử dụng ContentRootPath để tự động nhận diện thư mục gốc của App dù ở Local hay Render
string rootPath = builder.Environment.ContentRootPath;
string dbPath = Path.Combine(rootPath, "SQL.db");
string anhPath = Path.Combine(rootPath, "Anh");

// Tạo thư mục Anh nếu chưa có để tránh lỗi vật lý
if (!Directory.Exists(anhPath)) Directory.CreateDirectory(anhPath);

Console.WriteLine($"[DEPLOY CHECK] Database Path: {dbPath}");

// Lấy ConnectionString từ appsettings.json
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"))
);

// --- 3. ĐĂNG KÝ SERVICES (DI) ---
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

var app = builder.Build();

// --- 4. SỬA SWAGGER ĐỂ CHẠY Ở MỌI MÔI TRƯỜNG ---
// Xóa hoặc comment dòng check Environment.IsDevelopment()
// Để dù là Production (Render) thì vẫn vào được giao diện Swagger để test
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty; // Truy cập trực tiếp bằng https://api-ban-hang-3.onrender.com/
});

// --- 5. MIDDLEWARES ---
// app.UseHttpsRedirection(); // Trên Render thường dùng HTTP nội bộ, có thể tắt nếu lỗi Redirect

app.UseCors("AllowAll");

// Cấu hình phục vụ file tĩnh (Ảnh)
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(anhPath),
    RequestPath = "/Anh" // Truy cập ảnh qua: domain.com/Anh/ten-file.jpg
});

app.UseAuthorization();
app.MapControllers();

app.Run();