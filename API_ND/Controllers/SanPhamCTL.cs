using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using Models;

namespace API_ND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamCTL : ControllerBase
    {
        private readonly ISanPhamBLL _sanPhamBLL;

        public SanPhamCTL(ISanPhamBLL sanPhamBLL)
        {
            _sanPhamBLL = sanPhamBLL;
        }

        // ===============================
        // Lấy danh sách sản phẩm
        // GET: api/sanpham?lang=vi
        // ===============================
        [HttpGet]
        [HttpGet]
        public IActionResult GetAll(string lang, int pageIndex = 1, int pageSize = 10)
        {
            var result = _sanPhamBLL.GetAll(lang, pageIndex, pageSize);
            return Ok(result);
        }

        // ===============================
        // Lấy sản phẩm theo id
        // GET: api/sanpham/5?lang=vi
        // ===============================
        [HttpGet("{id}")]
        public IActionResult GetById(int id, string lang = "vi")
        {
            var data = _sanPhamBLL.GetById(id, lang);

            if (data == null)
                return NotFound("Không tìm thấy sản phẩm");

            return Ok(data);
        }

        // ===============================
        // Thêm sản phẩm
        // POST: api/sanpham
        // ===============================
        [HttpPost]
        public IActionResult Create([FromBody] SanPhamCreateDTO req)
        {
            try
            {
                var id = _sanPhamBLL.CreateSanPham(
                    req.MaSanPham,
                    req.IdDanhMuc,
                    req.TenSanPham,
                    req.MoTa
                );

                return Ok(new
                {
                    message = "Thêm sản phẩm thành công",
                    idSanPham = id
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ===============================
        // Cập nhật sản phẩm
        // PUT: api/sanpham
        // ===============================
        [HttpPut]
        public IActionResult Update([FromBody] SanPhamUpdateDTO req)
        {
            var sanPham = new SanPham
            {
                IdSanPham = req.IdSanPham,
                MaSanPham = req.MaSanPham,
                IdDanhMuc = req.IdDanhMuc,
                IdTrangThai = req.IdTrangThai
            };

            var result = _sanPhamBLL.Update(
                sanPham,
                req.TenSanPham,
                req.MoTa
            );

            if (!result)
                return NotFound("Không tìm thấy sản phẩm");

            return Ok("Cập nhật thành công");
        }

        // ===============================
        // Cập nhật trạng thái
        // DELETE: api/sanpham/5
        // ===============================
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _sanPhamBLL.UpdateTrangThai(id, 3);

            if (!result)
                return NotFound("Không tìm thấy sản phẩm");

            return Ok("Xóa sản phẩm thành công");
        }
    }
}