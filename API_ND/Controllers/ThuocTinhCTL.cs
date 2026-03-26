using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using Models;

namespace API_ND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThuocTinhCTL : ControllerBase
    {
        private readonly IThuocTinhBLL _thuocTinhBLL;

        public ThuocTinhCTL(IThuocTinhBLL thuocTinhBLL)
        {
            _thuocTinhBLL = thuocTinhBLL;
        }

        // =========================
        // GET: api/ThuocTinh?lang=vi
        // =========================
        [HttpGet]
        public IActionResult GetAll([FromQuery] string lang = "vi")
        {
            try
            {
                var data = _thuocTinhBLL.GetAll(lang);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // =========================
        // GET: api/ThuocTinh/5?lang=vi
        // =========================
        [HttpGet("{id}")]
        public IActionResult GetById(int id, [FromQuery] string lang = "vi")
        {
            try
            {
                var data = _thuocTinhBLL.GetById(id, lang);

                if (data == null)
                    return NotFound(new { message = "Không tìm thấy thuộc tính." });

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // =========================
        // POST: api/ThuocTinh
        // =========================
        [HttpPost]
        public IActionResult Create([FromBody] ThuocTinhCreateRequest request)
        {
            try
            {
                var id = _thuocTinhBLL.Create(request.MaThuocTinh, request.TenThuocTinh);

                return Ok(new
                {
                    message = "Tạo thuộc tính thành công",
                    IdThuocTinh = id
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // =========================
        // PUT: api/ThuocTinh/5
        // =========================
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ThuocTinhUpdateRequest request)
        {
            try
            {
                var result = _thuocTinhBLL.Update(id, request.MaThuocTinh, request.TenThuocTinh);

                if (!result)
                    return NotFound(new { message = "Không tìm thấy thuộc tính để cập nhật." });

                return Ok(new { message = "Cập nhật thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

    // =========================
    // Request Models
    // =========================

    public class ThuocTinhCreateRequest
    {
        public string MaThuocTinh { get; set; }
        public string TenThuocTinh { get; set; }
    }

    public class ThuocTinhUpdateRequest
    {
        public string MaThuocTinh { get; set; }
        public string TenThuocTinh { get; set; }
    }
}