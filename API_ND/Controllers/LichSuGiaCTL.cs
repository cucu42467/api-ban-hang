using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using Models;

namespace API_ND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichSuGiaCTL : ControllerBase
    {
        private readonly ILichSuGiaBLL _lichSuGiaBLL;

        public LichSuGiaCTL(ILichSuGiaBLL lichSuGiaBLL)
        {
            _lichSuGiaBLL = lichSuGiaBLL;
        }

        // GET: api/LichSuGiaCTL/bienthe/5
        [HttpGet("bienthe/{idBienThe}")]
        public IActionResult GetByBienThe(int idBienThe)
        {
            try
            {
                var data = _lichSuGiaBLL.GetByBienThe(idBienThe);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/LichSuGiaCTL/latest/5
        [HttpGet("latest/{idBienThe}")]
        public IActionResult GetLatest(int idBienThe)
        {
            try
            {
                var data = _lichSuGiaBLL.GetLatest(idBienThe);

                if (data == null)
                    return NotFound(new { message = "Không tìm thấy lịch sử giá." });

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}