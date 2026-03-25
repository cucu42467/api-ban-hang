using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using Microsoft.AspNetCore.SignalR; // 1. Thêm namespace SignalR
using API_ND.Hubs; // 2. Thêm namespace Hub của bạn

namespace API_ND.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BienTheSanPhamCTL : ControllerBase
    {
        private readonly IBienTheSanPhamBLL _bienTheSanPhamBLL;
        private readonly IHubContext<ProductHub> _hubContext; // 3. Khai báo HubContext

        public BienTheSanPhamCTL(IBienTheSanPhamBLL bienTheSanPhamBLL, IHubContext<ProductHub> hubContext)
        {
            _bienTheSanPhamBLL = bienTheSanPhamBLL;
            _hubContext = hubContext; // 4. Tiêm Hub vào Controller
        }

        // GET: api/BienTheSanPham?lang=vi
        [HttpGet]
        public IActionResult GetAll([FromQuery] string lang = "vi")
        {
            try
            {
                var data = _bienTheSanPhamBLL.GetAll(lang);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET: api/BienTheSanPham/5?lang=vi
        [HttpGet("{id}")]
        public IActionResult GetById(int id, [FromQuery] string lang = "vi")
        {
            try
            {
                var data = _bienTheSanPhamBLL.GetById(id, lang);
                return Ok(data);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // GET: api/BienTheSanPham/SanPham/5?lang=vi
        [HttpGet("SanPham/{idSanPham}")]
        public IActionResult GetByIdSanPham(int idSanPham, [FromQuery] string lang = "vi")
        {
            try
            {
                var data = _bienTheSanPhamBLL.GetByIdSanPham(idSanPham, lang);
                return Ok(data);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // POST: api/BienTheSanPham
        [HttpPost]
        public IActionResult Create([FromBody] BienTheSanPhamCreateRequest request)
        {
            try
            {
                int id = _bienTheSanPhamBLL.Create(
                    request.MaBienThe,
                    request.IdSanPham,
                    request.GiaBan,
                    request.TonKho,
                    request.MaSKU,
                    request.IdTrangThai
                );

                return Ok(new { message = "Thêm biến thể thành công.", id });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // PUT: api/BienTheSanPham/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BienTheSanPham request)
        {
            try
            {
                if (id != request.IdBienThe)
                    return BadRequest(new { message = "Id không khớp." });

                bool result = _bienTheSanPhamBLL.Update(request);

                if (result)
                {
                    // Phát tín hiệu cập nhật toàn bộ thông tin biến thể
                    await _hubContext.Clients.All.SendAsync("ReceiveProductUpdate", request.IdBienThe, request);
                }

                return Ok(new { message = "Cập nhật biến thể thành công.", result });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // PATCH: api/BienTheSanPham/5/GiaBan
        [HttpPatch("{id}/GiaBan")]
        public async Task<IActionResult> UpdateGiaBan(int id, [FromBody] UpdateGiaBanRequest request)
        {
            try
            {
                bool result = _bienTheSanPhamBLL.UpdateGiaBan(id, request.GiaMoi);

                if (result)
                {
                    // Sửa "ReceivePriceUpdate" thành "PriceChanged" để khớp với index.tsx
                    // Gửi kèm object có chứa MaSanPham (nếu có thể) hoặc ID để FE xử lý
                    await _hubContext.Clients.All.SendAsync("PriceChanged", new
                    {
                        idBienThe = id,
                        newPrice = request.GiaMoi
                    });
                }


                return Ok(new { message = "Cập nhật giá bán thành công.", result });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // PATCH: api/BienTheSanPham/5/TonKho
        [HttpPatch("{id}/TonKho")]
        public async Task<IActionResult> UpdateTonKho(int id, [FromBody] UpdateTonKhoRequest request)
        {
            try
            {
                bool result = _bienTheSanPhamBLL.UpdateTonKho(id, request.SoLuongThayDoi);

                if (result)
                {
                    // Sửa "ReceiveStockUpdate" thành "StockChanged"
                    await _hubContext.Clients.All.SendAsync("StockChanged", new
                    {
                        idBienThe = id
                    });
                }

                return Ok(new { message = "Cập nhật tồn kho thành công.", result });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // PATCH: api/BienTheSanPham/5/TrangThai
        [HttpPatch("{id}/TrangThai")]
        public async Task<IActionResult> UpdateTrangThai(int id, [FromBody] UpdateTrangThaiRequest request)
        {
            try
            {
                bool result = _bienTheSanPhamBLL.UpdateTrangThai(id, request.TrangThai);

                if (result)
                {
                    await _hubContext.Clients.All.SendAsync("ReceiveStatusUpdate", id, request.TrangThai);
                }

                return Ok(new { message = "Cập nhật trạng thái thành công.", result });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET: api/BienTheSanPham/5/LichSuGia
        [HttpGet("{id}/LichSuGia")]
        public IActionResult GetLichSuGia(int id)
        {
            try
            {
                var data = _bienTheSanPhamBLL.GetLichSuGia(id);
                return Ok(data);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }

    // ==================== REQUEST MODELS ====================
    public class BienTheSanPhamCreateRequest
    {
        public string MaBienThe { get; set; }
        public int IdSanPham { get; set; }
        public double GiaBan { get; set; }
        public int TonKho { get; set; }
        public string MaSKU { get; set; }
        public int IdTrangThai { get; set; }
    }

    public class UpdateGiaBanRequest
    {
        public double GiaMoi { get; set; }
    }

    public class UpdateTonKhoRequest
    {
        public int SoLuongThayDoi { get; set; }
    }

    public class UpdateTrangThaiRequest
    {
        public int TrangThai { get; set; }
    }
}