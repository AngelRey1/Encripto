using System;

namespace CesarEncriptador.Models
{
    public class MessageLog
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string MensajeOriginal { get; set; } = string.Empty;
        public string MensajeProcesado { get; set; } = string.Empty;
        public int Desplazamiento { get; set; }
        public string Tipo { get; set; } = string.Empty; // "encriptar" o "desencriptar"
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
    }
} 