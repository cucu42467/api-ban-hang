using Microsoft.AspNetCore.Mvc;
using Models;
using BLL.Interfaces;

namespace API_ND.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DanhMucController : ControllerBase
    {
        private readonly IDanhMucBLL _danhMucBLL;

        public DanhMucController(IDanhMucBLL danhMucBLL)
        {
            _danhMucBLL = danhMucBLL;
        }

        // GET: api/danhmuc?lang=vi&pageIndex=1&pageSize=10
        [HttpGet]
        public IActionResult GetAll(
            [FromQuery] string lang = "vi",
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                var data = _danhMucBLL.GetAll(lang, pageIndex, pageSize);
                var total = _danhMucBLL.GetTotalCount(lang);

                return Ok(new
                {
                    TotalCount = total,
                    TotalPages = (int)Math.Ceiling((double)total / pageSize),
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        // GET: api/danhmuc/5?lang=vi
        [HttpGet("{id}")]
        public IActionResult GetById(int id, [FromQuery] string lang = "vi")
        {
            try
            {
                var data = _danhMucBLL.GetById(id, lang);

                if (data == null)
                    return NotFound(new { Message = "Không tìm thấy danh mục." });

                return Ok(data);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        // POST: api/danhmuc
        [HttpPost]
        public IActionResult Create([FromBody] DanhMucRequest request)
        {
            try
            {
                int id = _danhMucBLL.Create(request.MaDanhMuc, request.TenDanhMuc, request.MoTa);

                return Ok(new { Message = "Tạo danh mục thành công.", IdDanhMuc = id });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        // PUT: api/danhmuc/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] DanhMucRequest request)
        {
            try
            {
                var danhMuc = new DanhMuc
                {
                    IdDanhMuc = id,
                    MaDanhMuc = request.MaDanhMuc,
                    IdTrangThai = request.IdTrangThai
                };

                var result = _danhMucBLL.Update(danhMuc, request.TenDanhMuc, request.MoTa);

                if (!result)
                    return NotFound(new { Message = "Không tìm thấy danh mục." });

                return Ok(new { Message = "Cập nhật danh mục thành công." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        // PATCH: api/danhmuc/5/trangthai?trangThai=3
        [HttpPatch("{id}/trangthai")]
        public IActionResult UpdateTrangThai(int id, [FromQuery] int trangThai)
        {
            try
            {
                var result = _danhMucBLL.UpdateTrangThai(id, trangThai);

                if (!result)
                    return NotFound(new { Message = "Không tìm thấy danh mục hoặc trạng thái không hợp lệ." });

                return Ok(new { Message = "Cập nhật trạng thái thành công." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }
    }

    public class DanhMucRequest
    {
        public string MaDanhMuc { get; set; }
        public string TenDanhMuc { get; set; }
        public string MoTa { get; set; }
        public int? IdTrangThai { get; set; }
    }
}