using BLL;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Pomelo.EntityFrameworkCore.MySql;

var builder = WebApplication.CreateBuilder(args);

// --- 1. CẤU HÌNH DATABASE (ĐÃ CHUYỂN SANG MYSQL) ---
// Lấy ConnectionString "DefaultConnection" từ appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseMySql(connectionString,
        new MySqlServerVersion(new Version(8, 0, 21)),
        mySqlOptions => mySqlOptions.EnableRetryOnFailure(
            maxRetryCount: 10,        // Thử lại tối đa 10 lần
            maxRetryDelay: TimeSpan.FromSeconds(30), // Mỗi lần cách nhau tối đa 30s
            errorNumbersToAdd: null)
    )
);

// --- 2. CẤU HÌNH CORS ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// --- 3. ĐĂNG KÝ SERVICES (DI) ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Đăng ký các lớp nghiệp vụ của bạn
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

// --- 4. CẤU HÌNH SWAGGER (MỞ CHO TẤT CẢ MÔI TRƯỜNG) ---
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Mua Ban Noi Bo V1");
    c.RoutePrefix = string.Empty; // Truy cập trực tiếp qua domain (vinh-site1.atmpages.com)
});

// --- 5. XỬ LÝ FILE TĨNH (ẢNH) ---
// MonsterASP dùng Windows, ta tạo folder Anh trong thư mục gốc của app
string anhPath = Path.Combine(app.Environment.ContentRootPath, "Anh");
if (!Directory.Exists(anhPath)) Directory.CreateDirectory(anhPath);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(anhPath),
    RequestPath = "/Anh"
});

// --- 6. MIDDLEWARES & ROUTING ---
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();