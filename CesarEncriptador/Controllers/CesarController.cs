using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CesarEncriptador.Services;

namespace CesarEncriptador.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CesarController : ControllerBase
    {
        private readonly CesarService _cesarService;
        public CesarController()
        {
            _cesarService = new CesarService();
        }

        [HttpPost("encriptar")]
        public IActionResult Encriptar([FromBody] CesarRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Mensaje))
                return BadRequest(new { error = "El campo 'mensaje' es requerido y debe ser una cadena de texto" });
            var resultado = _cesarService.Cifrar(request.Mensaje, request.Desplazamiento);
            return Ok(new { mensaje_encriptado = resultado });
        }

        [HttpPost("desencriptar")]
        public IActionResult Desencriptar([FromBody] CesarRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Mensaje))
                return BadRequest(new { error = "El campo 'mensaje' es requerido y debe ser una cadena de texto" });
            var resultado = _cesarService.Descifrar(request.Mensaje, request.Desplazamiento);
            return Ok(new { mensaje_desencriptado = resultado });
        }

        [HttpGet("desencriptar")]
        public IActionResult DesencriptarGet([FromQuery] string mensaje, [FromQuery] int desplazamiento)
        {
            if (string.IsNullOrWhiteSpace(mensaje))
                return BadRequest(new { error = "El campo 'mensaje' es requerido y debe ser una cadena de texto" });
            var resultado = _cesarService.Descifrar(mensaje, desplazamiento);
            return Ok(new { mensaje_desencriptado = resultado });
        }
    }

    public class CesarRequest
    {
        public required string Mensaje { get; set; }
        public int Desplazamiento { get; set; }
    }
} 