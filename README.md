# CesarEncriptador API

API REST para cifrado y descifrado usando el algoritmo de César con autenticación JWT.

## 🚀 Características

- **Algoritmo de César**: Cifrado y descifrado de texto
- **Autenticación JWT**: Sistema de autenticación seguro
- **API REST**: Endpoints RESTful con documentación Swagger
- **CORS habilitado**: Acceso desde cualquier origen
- **.NET 8**: Desarrollado con la última versión de .NET

## 📋 Requisitos

- .NET 8.0 SDK
- Visual Studio 2022 o VS Code
- Git

## 🛠️ Instalación

1. **Clona el repositorio**:
   ```bash
   git clone https://github.com/tu-usuario/CesarEncriptador.git
   cd CesarEncriptador
   ```

2. **Restaura las dependencias**:
   ```bash
   dotnet restore
   ```

3. **Ejecuta la aplicación**:
   ```bash
   cd CesarEncriptador
   dotnet run
   ```

4. **Abre en el navegador**:
   - Swagger UI: http://localhost:5205/
   - API Base: http://localhost:5205/api

## 🔐 Endpoints de Autenticación

### POST /api/login
Autentica un usuario y devuelve un token JWT.

**Parámetros de query:**
- `usuario` (string, requerido): Nombre de usuario
- `password` (string, requerido): Contraseña

**Ejemplo:**
```bash
curl -X 'POST' \
  'http://localhost:5205/api/login?usuario=admin&password=admin' \
  -H 'accept: */*'
```

**Respuesta:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

### GET /api/verify
Verifica la validez de un token JWT.

**Headers:**
- `Authorization: Bearer {token}`

**Ejemplo:**
```bash
curl -X 'GET' \
  'http://localhost:5205/api/verify' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...'
```

**Respuesta:**
```json
{
  "valid": true,
  "message": "Token autorizado y válido.",
  "usuario": "admin",
  "claims": [
    {
      "type": "usuario",
      "value": "admin"
    },
    {
      "type": "exp",
      "value": "1750986219"
    },
    {
      "type": "iss",
      "value": "CESARENCRIPTADOR"
    }
  ]
}
```

## 🔐 Usuarios Predefinidos

- **admin** / **admin**
- **user1** / **password1**
- **test** / **test123**

## 🔒 Endpoints de Cifrado (Protegidos)

### POST /Cesar/cifrar
Cifra un texto usando el algoritmo de César.

**Headers:**
- `Authorization: Bearer {token}`

**Body:**
```json
{
  "texto": "Hola mundo",
  "desplazamiento": 3
}
```

**Respuesta:**
```json
{
  "textoOriginal": "Hola mundo",
  "textoCifrado": "krod pxqgr",
  "desplazamiento": 3
}
```

### POST /Cesar/descifrar
Descifra un texto usando el algoritmo de César.

**Headers:**
- `Authorization: Bearer {token}`

**Body:**
```json
{
  "texto": "krod pxqgr",
  "desplazamiento": 3
}
```

**Respuesta:**
```json
{
  "textoCifrado": "krod pxqgr",
  "textoDescifrado": "hola mundo",
  "desplazamiento": 3
}
```

## 🏗️ Estructura del Proyecto

```
CesarEncriptador/
├── Controllers/
│   ├── AuthController.cs      # Autenticación JWT
│   └── CesarController.cs     # Cifrado/Descifrado
├── Models/
│   └── User.cs               # Modelo de usuario
├── Services/
│   ├── JwtService.cs         # Servicio JWT
│   └── CesarService.cs       # Servicio de cifrado
├── wwwroot/
│   └── index.html            # Interfaz web
└── Program.cs                # Configuración de la aplicación
```

## 🔧 Configuración

### Variables de Entorno
- `ASPNETCORE_ENVIRONMENT`: Development/Production
- `JwtSettings:ExpirationMinutes`: Tiempo de expiración del token (default: 60)

### Puertos
- **Desarrollo**: http://localhost:5205
- **Producción**: Configurable en `launchSettings.json`

## 🚀 Despliegue

### Local
```bash
dotnet run
```

### Docker (opcional)
```bash
docker build -t cesarencriptador .
docker run -p 5205:5205 cesarencriptador
```

## 📝 Uso con Swagger

1. Abre http://localhost:5205/ en tu navegador
2. Haz clic en "Authorize" y pega tu token JWT
3. Prueba los endpoints directamente desde la interfaz

## 🔒 Seguridad

- **JWT**: Tokens con expiración de 1 hora
- **CORS**: Configurado para permitir cualquier origen
- **Validación**: Parámetros validados en todos los endpoints
- **Claims**: Información del usuario en el token

## 🤝 Contribuir

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo `LICENSE` para más detalles.

## 👨‍💻 Autor

**Tu Nombre**
- GitHub: [@tu-usuario](https://github.com/tu-usuario)

## 🙏 Agradecimientos

- .NET 8
- ASP.NET Core
- JWT.NET
- Swagger/OpenAPI 