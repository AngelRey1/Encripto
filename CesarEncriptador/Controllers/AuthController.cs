using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CesarEncriptador.Services;
using CesarEncriptador.Models;

namespace CesarEncriptador.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromQuery] string usuario, [FromQuery] string password)
        {
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(password))
            {
                return BadRequest(new { error = "Usuario y Password son requeridos" });
            }

            var request = new LoginRequest { Username = usuario, Password = password };
            var response = _jwtService.Authenticate(request);
            if (response == null)
            {
                return Unauthorized(new { error = "Credenciales inválidas" });
            }

            return Ok(new { token = response.Token });
        }

        [HttpGet("verify")]
        [Authorize]
        public IActionResult VerifyToken()
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized(new { error = "Token inválido o expirado" });
            }

            var claims = User.Claims.Select(c => new { type = c.Type, value = c.Value }).ToList();

            return Ok(new { 
                valid = true, 
                message = "Token autorizado y válido.",
                usuario = username,
                claims = claims
            });
        }
    }
} 