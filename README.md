# 🔐 CesarEncriptador API

[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)
[![GitHub](https://img.shields.io/badge/GitHub-AngelRey1%2FEncripto-blue.svg)](https://github.com/AngelRey1/Encripto)

API REST moderna para cifrado y descifrado de texto utilizando el algoritmo de César con autenticación JWT. Desarrollada con ASP.NET Core 8 y siguiendo las mejores prácticas de desarrollo.

## ✨ Características Principales

- 🔐 **Algoritmo de César**: Implementación completa del cifrado clásico
- 🛡️ **Autenticación JWT**: Sistema de autenticación seguro y escalable
- 🌐 **API REST**: Endpoints RESTful con documentación automática
- 📚 **Swagger UI**: Documentación interactiva de la API
- 🔄 **CORS habilitado**: Acceso desde cualquier origen
- ⚡ **.NET 8**: Desarrollado con la última versión de .NET
- 🏗️ **Arquitectura limpia**: Separación clara de responsabilidades

## 🚀 Inicio Rápido

### Prerrequisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) o [VS Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)

### Instalación

1. **Clona el repositorio**
   ```bash
   git clone https://github.com/AngelRey1/Encripto.git
   cd Encripto
   ```

2. **Restaura las dependencias**
   ```bash
   dotnet restore
   ```

3. **Ejecuta la aplicación**
   ```bash
   cd CesarEncriptador
   dotnet run
   ```

4. **Accede a la documentación**
   - 🌐 **Swagger UI**: http://localhost:5205/
   - 🔗 **API Base**: http://localhost:5205/api

## 📖 Documentación de la API

### 🔑 Autenticación

#### POST /api/login
Autentica un usuario y devuelve un token JWT válido.

**Parámetros de query:**
| Parámetro | Tipo | Requerido | Descripción |
|-----------|------|-----------|-------------|
| `usuario` | string | ✅ | Nombre de usuario |
| `password` | string | ✅ | Contraseña del usuario |

**Ejemplo de uso:**
```bash
curl -X POST "http://localhost:5205/api/login?usuario=admin&password=admin" \
     -H "accept: */*"
```

**Respuesta exitosa (200):**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c3VhcmlvIjoiYWRtaW4iLCJleHAiOjE3NTA5ODYyMTl9..."
}
```

**Respuesta de error (401):**
```json
{
  "error": "Credenciales inválidas"
}
```

#### GET /api/verify
Verifica la validez de un token JWT y devuelve información del usuario.

**Headers requeridos:**
| Header | Valor |
|--------|-------|
| `Authorization` | `Bearer {token}` |

**Ejemplo de uso:**
```bash
curl -X GET "http://localhost:5205/api/verify" \
     -H "accept: */*" \
     -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
```

**Respuesta exitosa (200):**
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

### 🔒 Cifrado y Descifrado

#### POST /Cesar/cifrar
Cifra un texto utilizando el algoritmo de César.

**Headers requeridos:**
| Header | Valor |
|--------|-------|
| `Authorization` | `Bearer {token}` |
| `Content-Type` | `application/json` |

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

#### POST /Cesar/descifrar
Descifra un texto utilizando el algoritmo de César.

**Headers requeridos:**
| Header | Valor |
|--------|-------|
| `Authorization` | `Bearer {token}` |
| `Content-Type` | `application/json` |

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

## 👥 Usuarios Predefinidos

| Usuario | Contraseña | Descripción |
|---------|------------|-------------|
| `admin` | `admin` | Usuario administrador |
| `user1` | `password1` | Usuario de prueba 1 |
| `test` | `test123` | Usuario de prueba 2 |

## 🏗️ Arquitectura del Proyecto

```
CesarEncriptador/
├── 📁 Controllers/           # Controladores de la API
│   ├── AuthController.cs     # Autenticación JWT
│   └── CesarController.cs    # Cifrado/Descifrado
├── 📁 Models/                # Modelos de datos
│   └── User.cs              # Modelo de usuario
├── 📁 Services/              # Lógica de negocio
│   ├── JwtService.cs        # Servicio JWT
│   └── CesarService.cs      # Servicio de cifrado
├── 📁 Properties/            # Configuración del proyecto
├── 📁 wwwroot/              # Archivos estáticos (vacío)
├── 📄 Program.cs            # Punto de entrada
├── 📄 appsettings.json      # Configuración
└── 📄 CesarEncriptador.csproj # Archivo de proyecto
```

## ⚙️ Configuración

### Variables de Entorno

| Variable | Valor por defecto | Descripción |
|----------|-------------------|-------------|
| `ASPNETCORE_ENVIRONMENT` | `Development` | Entorno de ejecución |
| `JwtSettings:ExpirationMinutes` | `60` | Tiempo de expiración del token |

### Puertos

| Entorno | URL |
|---------|-----|
| **Desarrollo** | http://localhost:5205 |
| **Producción** | Configurable en `launchSettings.json` |

## 🚀 Despliegue

### Desarrollo Local
```bash
cd CesarEncriptador
dotnet run
```

### Docker (Opcional)
```bash
# Construir imagen
docker build -t cesarencriptador .

# Ejecutar contenedor
docker run -p 5205:5205 cesarencriptador
```

### Producción
```bash
# Publicar la aplicación
dotnet publish -c Release -o ./publish

# Ejecutar en producción
dotnet ./publish/CesarEncriptador.dll
```

## 📝 Uso con Swagger

1. **Abre Swagger UI**: http://localhost:5205/
2. **Autoriza tu sesión**: Haz clic en "Authorize" y pega tu token JWT
3. **Prueba los endpoints**: Usa la interfaz interactiva para probar la API

## 🔒 Seguridad

### JWT (JSON Web Tokens)
- **Algoritmo**: HMAC SHA256
- **Expiración**: 1 hora por defecto
- **Claims**: Usuario, expiración, issuer

### CORS (Cross-Origin Resource Sharing)
- **Configuración**: Permite cualquier origen
- **Métodos**: GET, POST, PUT, DELETE
- **Headers**: Cualquier header

### Validación
- **Parámetros**: Validación en todos los endpoints
- **Autenticación**: Requerida para endpoints protegidos
- **Autorización**: Basada en JWT

## 🧪 Pruebas

### Ejemplo de flujo completo

1. **Autenticación**:
   ```bash
   curl -X POST "http://localhost:5205/api/login?usuario=admin&password=admin"
   ```

2. **Verificación del token**:
   ```bash
   curl -X GET "http://localhost:5205/api/verify" \
        -H "Authorization: Bearer {token}"
   ```

3. **Cifrado de texto**:
   ```bash
   curl -X POST "http://localhost:5205/Cesar/cifrar" \
        -H "Authorization: Bearer {token}" \
        -H "Content-Type: application/json" \
        -d '{"texto": "Hola mundo", "desplazamiento": 3}'
   ```

4. **Descifrado de texto**:
   ```bash
   curl -X POST "http://localhost:5205/Cesar/descifrar" \
        -H "Authorization: Bearer {token}" \
        -H "Content-Type: application/json" \
        -d '{"texto": "krod pxqgr", "desplazamiento": 3}'
   ```

## 🤝 Contribuir

¡Las contribuciones son bienvenidas! Por favor, sigue estos pasos:

1. **Fork** el proyecto
2. **Crea una rama** para tu feature (`git checkout -b feature/AmazingFeature`)
3. **Commit** tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. **Push** a la rama (`git push origin feature/AmazingFeature`)
5. **Abre un Pull Request**

### Guías de contribución
- Mantén el código limpio y bien documentado
- Sigue las convenciones de nomenclatura de C#
- Agrega pruebas para nuevas funcionalidades
- Actualiza la documentación según sea necesario

## 📄 Licencia

Este proyecto está bajo la **Licencia MIT**. Ver el archivo [LICENSE](LICENSE) para más detalles.

## 👨‍💻 Autor

**Angel Rey**
- 🌐 **GitHub**: [@AngelRey1](https://github.com/AngelRey1)
- 📧 **Email**: [Tu email aquí]

## 🙏 Agradecimientos

- [.NET 8](https://dotnet.microsoft.com/) - Framework de desarrollo
- [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/) - Framework web
- [JWT.NET](https://github.com/jwt-dotnet/jwt) - Implementación JWT
- [Swagger/OpenAPI](https://swagger.io/) - Documentación de API
- [Microsoft](https://microsoft.com/) - Herramientas y documentación

## 📊 Estadísticas del Proyecto

- **Lenguaje**: 100% C#
- **Framework**: ASP.NET Core 8
- **Arquitectura**: API REST
- **Autenticación**: JWT
- **Documentación**: Swagger/OpenAPI

---

⭐ **Si este proyecto te resulta útil, ¡dale una estrella en GitHub!** 