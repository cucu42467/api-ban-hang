using BLL;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services;

var builder = WebApplication.CreateBuilder(args);

// --- C?U HÌNH CHO PHÉP TRUY C?P T? BÊN NGOÀI (CORS) ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()   // Cho phép t?t c? các ngu?n (IP khác nhau)
              .AllowAnyMethod()   // Cho phép t?t c? các ph??ng th?c (GET, POST, PUT, DELETE)
              .AllowAnyHeader();  // Cho phép t?t c? các Header
    });
});

builder.WebHost.UseUrls("http://0.0.0.0:10000");


// 1. L?y ???ng d?n c?a th? m?c API_ND
string apiPath = builder.Environment.ContentRootPath;

// 2. Lùi l?i 3 c?p: API_ND -> BE -> BE -> G?c (App mua bán ?? n?i b?)
// Sau ?ó m?i ?i vào DATA/SQL.db
string dbPath = Path.Combine(builder.Environment.ContentRootPath, "SQL.db");// ? THÊM DÒNG NÀY
string anhPath = Path.Combine(builder.Environment.ContentRootPath, "Anh");

// In ra ?? b?n ki?m tra xem nó có hi?n ?úng: D:\Phát tri?n\App mua bán ?? n?i b?\DATA\SQL.db không
Console.WriteLine("---------------------------------------------------------");
Console.WriteLine($"[DATABASE CHECK] ???ng d?n th?c t?: {dbPath}");
Console.WriteLine("---------------------------------------------------------");

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"))
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ISanPhamDAL, SanPhamDAL>();
builder.Services.AddScoped<ISanPhamBLL, SanPhamBLL>();
builder.Services.AddScoped<INgonNguDAL, NgonNguDAL>();
builder.Services.AddScoped<INgonNguBLL, NgonNguBLL>();
builder.Services.AddScoped<IBienTheSanPhamDAL, BienTheSanPhamDAL>();
builder.Services.AddScoped<IBienTheSanPhamBLL,BienTheSanPhamBLL>();
builder.Services.AddHttpClient<IExchangeRateService, ExchangeRateService>();
builder.Services.AddScoped<IDanhMucBLL,DanhMucBLL>();
builder.Services.AddScoped<IDanhMucDAL,DanhMucDAL>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

// ? THÊM ?O?N NÀY
if (!Directory.Exists(anhPath))
    Directory.CreateDirectory(anhPath);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(anhPath),
    RequestPath = ""
});

app.UseAuthorization();

app.MapControllers();

app.Run();
