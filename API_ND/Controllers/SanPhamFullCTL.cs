using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using Models;
using Newtonsoft.Json;

namespace API_ND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamFullController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly ISanPhamFullBLL _bll;

        public SanPhamFullController(IWebHostEnvironment env, ISanPhamFullBLL bll)
        {
            _env = env;
            _bll = bll;
        }

        [HttpPut("update-full")]
        public async Task<IActionResult> UpdateFull(
            [FromForm] int idSanPham,
            [FromForm] string maSanPham,
            [FromForm] int idDanhMuc,
            [FromForm] string tenSanPham,
            [FromForm] string moTa,
            [FromForm] string bienTheJson,
            [FromForm] string oldImagesJson,
            List<IFormFile> files
        )
        {
            var rootPath = Directory.GetParent(_env.ContentRootPath).FullName;
            var folderPath = Path.Combine(rootPath, "Anh");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var savedFiles = new List<string>();

            try
            {
                // ===== parse dữ liệu =====
                var bienThes = JsonConvert.DeserializeObject<List<BienTheSanPhamDTO>>(bienTheJson);
                var oldImagesToDelete = JsonConvert.DeserializeObject<List<string>>(oldImagesJson);

                var images = new List<(string, bool, int, int)>();

                int fileIndex = 0;

                // ===== xử lý upload file =====
                for (int i = 0; i < bienThes.Count; i++)
                {
                    var bt = bienThes[i];

                    if (bt.HinhAnhs != null)
                    {
                        foreach (var img in bt.HinhAnhs)
                        {
                            // ❗ chỉ xử lý ảnh mới (chưa có tên file)
                            if (string.IsNullOrEmpty(img.DuongDanAnh))
                            {
                                var file = files[fileIndex++];

                                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                                var filePath = Path.Combine(folderPath, fileName);

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    await file.CopyToAsync(stream);
                                }

                                savedFiles.Add(fileName);

                                images.Add((fileName, img.LaAnhChinh, img.ThuTu, i));
                            }
                        }
                    }
                }

                // ===== gọi BLL =====
                var result = await _bll.UpdateFull(
                    idSanPham,
                    maSanPham,
                    idDanhMuc,
                    tenSanPham,
                    moTa,
                    bienTheJson,
                    images,
                    oldImagesToDelete
                );

                if (!result)
                    throw new Exception("Cập nhật thất bại");

                // ===== xóa file cũ =====
                foreach (var fileName in oldImagesToDelete)
                {
                    var path = Path.Combine(folderPath, fileName);
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                }

                return Ok("Cập nhật thành công");
            }
            catch (Exception ex)
            {
                // ❗ rollback file mới nếu lỗi
                foreach (var fileName in savedFiles)
                {
                    var path = Path.Combine(folderPath, fileName);
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                }

                return BadRequest("Lỗi: " + ex.Message);
            }
        }

        [HttpPost("create-full")]
        public async Task<IActionResult> CreateFull(
    [FromForm] string maSanPham,
    [FromForm] int idDanhMuc,
    [FromForm] string tenSanPham,
    [FromForm] string moTa,
    [FromForm] string bienTheJson,
    List<IFormFile> files
)
        {
            var rootPath = Directory.GetParent(_env.ContentRootPath).FullName;
            var folderPath = Path.Combine(rootPath, "Anh");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var savedFiles = new List<string>();

            try
            {
                // ===== parse JSON =====
                var bienThes = JsonConvert.DeserializeObject<List<BienTheSanPhamDTO>>(bienTheJson);

                var images = new List<(string, bool, int, int)>();

                int fileIndex = 0;

                // ===== xử lý upload =====
                for (int i = 0; i < bienThes.Count; i++)
                {
                    var bt = bienThes[i];

                    if (bt.HinhAnhs != null)
                    {
                        foreach (var img in bt.HinhAnhs)
                        {
                            var file = files[fileIndex++];

                            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                            var filePath = Path.Combine(folderPath, fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            savedFiles.Add(fileName);

                            images.Add((fileName, img.LaAnhChinh, img.ThuTu, i));
                        }
                    }
                }

                // ===== gọi BLL =====
                var result = await _bll.CreateFull(
                    maSanPham,
                    idDanhMuc,
                    tenSanPham,
                    moTa,
                    bienTheJson,
                    images
                );

                if (!result)
                    throw new Exception("Tạo thất bại");

                return Ok("Tạo thành công");
            }
            catch (Exception ex)
            {
                // ❗ rollback file nếu lỗi
                foreach (var fileName in savedFiles)
                {
                    var path = Path.Combine(folderPath, fileName);
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                }

                return BadRequest("Lỗi: " + ex.Message);
            }
        }
    }
}