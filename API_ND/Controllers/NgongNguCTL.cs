using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using Models;

namespace API_ND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NgonNguCTL : ControllerBase
    {
        private readonly INgonNguBLL _ngonNguBLL;

        public NgonNguCTL(INgonNguBLL ngonNguBLL)
        {
            _ngonNguBLL = ngonNguBLL;
        }

        // ===============================
        // Lấy danh sách ngôn ngữ
        // GET: api/ngonngu
        // ===============================
        [HttpGet]
        public IActionResult GetAll()
        {
            var data = _ngonNguBLL.GetAll();
            return Ok(data);
        }

        // ===============================
        // Lấy ngôn ngữ theo mã
        // GET: api/ngonngu/vi
        // ===============================
        [HttpGet("{maNgonNgu}")]
        public IActionResult GetByMa(string maNgonNgu)
        {
            try
            {
                var data = _ngonNguBLL.GetByMa(maNgonNgu);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // ===============================
        // Lấy ngôn ngữ mặc định
        // GET: api/ngonngu/macdinh
        // ===============================
        [HttpGet("macdinh")]
        public IActionResult GetMacDinh()
        {
            try
            {
                var data = _ngonNguBLL.GetMacDinh();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // ===============================
        // Thêm ngôn ngữ
        // POST: api/ngonngu
        // ===============================
        [HttpPost]
        public IActionResult Create([FromBody] NgonNgu ngonNgu)
        {
            try
            {
                var result = _ngonNguBLL.Create(ngonNgu);
                return Ok(new
                {
                    message = "Thêm ngôn ngữ thành công",
                    maNgonNgu = ngonNgu.MaNgonNgu
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ===============================
        // Cập nhật ngôn ngữ
        // PUT: api/ngonngu
        // ===============================
        [HttpPut]
        public IActionResult Update([FromBody] NgonNgu ngonNgu)
        {
            try
            {
                var result = _ngonNguBLL.Update(ngonNgu);
                return Ok("Cập nhật ngôn ngữ thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ===============================
        // Đặt ngôn ngữ mặc định
        // PUT: api/ngonngu/macdinh/vi
        // ===============================
        [HttpPut("macdinh/{maNgonNgu}")]
        public IActionResult SetMacDinh(string maNgonNgu)
        {
            try
            {
                var result = _ngonNguBLL.SetMacDinh(maNgonNgu);
                return Ok("Đặt ngôn ngữ mặc định thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ===============================
        // Xóa ngôn ngữ
        // DELETE: api/ngonngu/vi
        // ===============================
        [HttpDelete("{maNgonNgu}")]
        public IActionResult Delete(string maNgonNgu)
        {
            try
            {
                var result = _ngonNguBLL.Delete(maNgonNgu);
                return Ok("Xóa ngôn ngữ thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{maNgonNgu}")]
        public IActionResult CapNhatThuCong(string maNgonNgu, [FromBody] decimal tiGia)
        {
            var ok = _ngonNguBLL.CapNhatThuCong(maNgonNgu, tiGia);
            return ok ? Ok() : NotFound();
        }
    }
}