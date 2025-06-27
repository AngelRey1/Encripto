using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CesarEncriptador.Models
{
    public class User
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    public class RegisterRequest
    {
        [Required(ErrorMessage = "El campo Username es requerido")]
        public string Username { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "El campo Password es requerido")]
        public string Password { get; set; } = string.Empty;
    }

    public class LoginRequest
    {
        [Required(ErrorMessage = "El campo Username es requerido")]
        public string Username { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "El campo Password es requerido")]
        public string Password { get; set; } = string.Empty;
    }

    public class LoginResponse
    {
        [Description("Token JWT generado")]
        public string Token { get; set; } = string.Empty;
        
        [Description("Nombre del usuario autenticado")]
        public string Username { get; set; } = string.Empty;
        
        [Description("Mensaje de validación")]
        public string Message { get; set; } = string.Empty;

        public DateTime Expiration { get; set; }
    }

    public class ValidateTokenRequest
    {
        [Required(ErrorMessage = "El campo Username es requerido")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo Token es requerido")]
        public string Token { get; set; } = string.Empty;
    }

    public class ValidateTokenResponse
    {
        [Description("Indica si el token es válido")]
        public bool IsValid { get; set; }
        
        [Description("Nombre del usuario del token")]
        public string Username { get; set; } = string.Empty;
        
        [Description("Mensaje de validación")]
        public string Message { get; set; } = string.Empty;
    }

    public class RegisterResponse
    {
        [Description("Indica si el registro fue exitoso")]
        public bool Success { get; set; }
        
        [Description("Mensaje del resultado del registro")]
        public string Message { get; set; } = string.Empty;
    }
} 