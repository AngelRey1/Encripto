using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CesarEncriptador.Services;
using CesarEncriptador.Models;
using Npgsql;

namespace CesarEncriptador.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CesarController : ControllerBase
    {
        private readonly CesarService _cesarService;
        private readonly string _connectionString = "Host=aws-0-us-east-2.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.jcvewjfglogbvfcybmcg;Password=J4tniel45@.;Pooling=true;";

        public CesarController()
        {
            _cesarService = new CesarService();
        }

        [HttpPost("encriptar")]
        public async Task<IActionResult> Encriptar([FromBody] CesarRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Mensaje))
                return BadRequest(new { error = "El campo 'mensaje' es requerido y debe ser una cadena de texto" });
            var resultado = _cesarService.Cifrar(request.Mensaje, request.Desplazamiento);
            var log = new MessageLog
            {
                Username = User.Identity?.Name,
                MensajeOriginal = request.Mensaje,
                MensajeProcesado = resultado,
                Desplazamiento = request.Desplazamiento,
                Tipo = "encriptar",
                Fecha = DateTime.UtcNow
            };
            await _cesarService.GuardarMensajeAsync(log, _connectionString);
            return Ok(new { mensaje_encriptado = resultado });
        }

        [HttpPost("desencriptar")]
        public async Task<IActionResult> Desencriptar([FromBody] CesarRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Mensaje))
                return BadRequest(new { error = "El campo 'mensaje' es requerido y debe ser una cadena de texto" });
            var resultado = _cesarService.Descifrar(request.Mensaje, request.Desplazamiento);
            var log = new MessageLog
            {
                Username = User.Identity?.Name,
                MensajeOriginal = request.Mensaje,
                MensajeProcesado = resultado,
                Desplazamiento = request.Desplazamiento,
                Tipo = "desencriptar",
                Fecha = DateTime.UtcNow
            };
            await _cesarService.GuardarMensajeAsync(log, _connectionString);
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

        [HttpGet("encriptados")]
        public async Task<IActionResult> GetEncriptados([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var mensajes = new List<MessageLog>();
            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            var cmd = new NpgsqlCommand(@"SELECT id, username, mensajeoriginal, mensajeprocesado, desplazamiento, fecha FROM encryptedmessages ORDER BY fecha DESC OFFSET @offset LIMIT @limit", conn);
            cmd.Parameters.AddWithValue("@offset", (page - 1) * pageSize);
            cmd.Parameters.AddWithValue("@limit", pageSize);
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                mensajes.Add(new MessageLog
                {
                    Id = reader.GetInt32(0),
                    Username = reader.IsDBNull(1) ? null : reader.GetString(1),
                    MensajeOriginal = reader.GetString(2),
                    MensajeProcesado = reader.GetString(3),
                    Desplazamiento = reader.GetInt32(4),
                    Fecha = reader.GetDateTime(5),
                    Tipo = "encriptar"
                });
            }
            return Ok(mensajes);
        }

        [HttpGet("desencriptados")]
        public async Task<IActionResult> GetDesencriptados([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var mensajes = new List<MessageLog>();
            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            var cmd = new NpgsqlCommand(@"SELECT id, username, mensajeoriginal, mensajeprocesado, desplazamiento, fecha FROM decryptedmessages ORDER BY fecha DESC OFFSET @offset LIMIT @limit", conn);
            cmd.Parameters.AddWithValue("@offset", (page - 1) * pageSize);
            cmd.Parameters.AddWithValue("@limit", pageSize);
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                mensajes.Add(new MessageLog
                {
                    Id = reader.GetInt32(0),
                    Username = reader.IsDBNull(1) ? null : reader.GetString(1),
                    MensajeOriginal = reader.GetString(2),
                    MensajeProcesado = reader.GetString(3),
                    Desplazamiento = reader.GetInt32(4),
                    Fecha = reader.GetDateTime(5),
                    Tipo = "desencriptar"
                });
            }
            return Ok(mensajes);
        }
    }

    public class CesarRequest
    {
        public required string Mensaje { get; set; }
        public int Desplazamiento { get; set; }
    }
} 