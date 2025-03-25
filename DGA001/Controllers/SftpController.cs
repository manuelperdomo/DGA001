using DGA001.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DGA001.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SftpController : ControllerBase
    {
        private readonly SftpService _sftpService;

        public SftpController()
        {
            _sftpService = new SftpService();
        }

        [HttpGet("descargar")]
        public IActionResult DescargarArchivos()
        {
            _sftpService.DescargarArchivos();
            return Ok("Descarga iniciada.");
        }
    }
}
