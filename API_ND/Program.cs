using BLL;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services;
using Microsoft.Extensions.FileProviders;
using System.IO;

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

// --- 2. CẤU HÌNH CORS (CHO PHÉP APP REACT NATIVE KẾT NỐI) ---
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

// --- 4. CẤU HÌNH SWAGGER ---
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Van Phong Pham HA V1");
    c.RoutePrefix = string.Empty;
});

// --- 5. XỬ LÝ FILE TĨNH (THƯ MỤC ANH) ---
// Sử dụng GetFullPath để tránh sai lệch đường dẫn trên Linux
string anhPath = Path.GetFullPath(Path.Combine(app.Environment.ContentRootPath, "Anh"));

// Log để debug trên Render (Hoàng Anh xem trong mục Logs của Render nhé)
Console.WriteLine($"[DEBUG] Root Path: {app.Environment.ContentRootPath}");
Console.WriteLine($"[DEBUG] Anh Path: {anhPath}");

if (!Directory.Exists(anhPath))
{
    Console.WriteLine("[WARNING] Thu muc 'Anh' khong ton tai. Dang tao moi...");
    Directory.CreateDirectory(anhPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(anhPath),
    RequestPath = "/Anh",
    OnPrepareResponse = ctx =>
    {
        // Thêm Header để trình duyệt/App cache ảnh, đỡ tốn băng thông Render
        ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=604800");
    }
});

// Hỗ trợ file tĩnh mặc định (nếu có dùng wwwroot)
app.UseStaticFiles();

// --- 6. MIDDLEWARES & ROUTING ---
app.UseHttpsRedirection(); // Ép dùng HTTPS cho an toàn (Render hỗ trợ sẵn)
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();