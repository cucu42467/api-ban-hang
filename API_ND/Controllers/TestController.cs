using Microsoft.AspNetCore.Mvc;
using DAL;
using System.Linq;

namespace API_ND.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public TestController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("db")]
        public IActionResult TestDatabase()
        {
            try
            {
                var soSanPham = _context.SanPhams.Count();

                return Ok(new
                {
                    message = "Kết nối database thành công",
                    soSanPham = soSanPham
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Lỗi kết nối database",
                    error = ex.Message
                });
            }
        }
    }
}