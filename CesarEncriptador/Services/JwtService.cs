using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using CesarEncriptador.Models;

namespace CesarEncriptador.Services
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;
        private readonly string _secretKey = "TuClaveSecretaSuperSegura123!@#$%^&*()";
        private readonly List<User> _users = new List<User>
        {
            new User { Username = "admin", Password = "admin", CreatedAt = DateTime.UtcNow },
            new User { Username = "user1", Password = "password1", CreatedAt = DateTime.UtcNow },
            new User { Username = "test", Password = "test123", CreatedAt = DateTime.UtcNow }
        };

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public RegisterResponse Register(RegisterRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return new RegisterResponse { Success = false, Message = "Usuario y Password son requeridos" };
            }

            if (_users.Any(u => u.Username.Equals(request.Username, StringComparison.Ordinal)))
            {
                return new RegisterResponse { Success = false, Message = "El usuario ya existe" };
            }

            _users.Add(new User { Username = request.Username, Password = request.Password, CreatedAt = DateTime.UtcNow });
            return new RegisterResponse { Success = true, Message = "Usuario registrado exitosamente" };
        }

        public LoginResponse? Authenticate(LoginRequest request)
        {
            var user = _users.FirstOrDefault(u => 
                u.Username.Equals(request.Username, StringComparison.OrdinalIgnoreCase) && 
                u.Password == request.Password);

            if (user == null)
                return null;

            var token = GenerateJwtToken(user.Username);
            var expiration = DateTime.UtcNow.AddMinutes(
                int.Parse(_configuration["JwtSettings:ExpirationMinutes"] ?? "60"));

            return new LoginResponse
            {
                Token = token,
                Username = user.Username,
                Expiration = expiration
            };
        }

        public ValidateTokenResponse ValidateToken(ValidateTokenRequest request)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_secretKey);

                tokenHandler.ValidateToken(request.Token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var username = jwtToken.Claims.First(x => x.Type == "usuario").Value;

                if (username.Equals(request.Username, StringComparison.OrdinalIgnoreCase))
                {
                    return new ValidateTokenResponse
                    {
                        IsValid = true,
                        Message = "Token válido para el usuario: " + username,
                        Username = username
                    };
                }
                else
                {
                    return new ValidateTokenResponse
                    {
                        IsValid = false,
                        Message = "El token no corresponde al usuario especificado",
                        Username = string.Empty
                    };
                }
            }
            catch
            {
                return new ValidateTokenResponse
                {
                    IsValid = false,
                    Message = "Token inválido o expirado",
                    Username = string.Empty
                };
            }
        }

        public List<User> GetAllUsers()
        {
            return _users.Select(u => new User 
            { 
                Username = u.Username, 
                Password = "***", // No mostrar contraseñas
                CreatedAt = u.CreatedAt 
            }).ToList();
        }

        private string GenerateJwtToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("usuario", username),
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = "CESARENCRIPTADOR",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
} 