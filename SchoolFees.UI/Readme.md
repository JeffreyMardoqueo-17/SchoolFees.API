#### Para la configuración de la cadena de conexion a la BD

En este caso uso llos secretos de usaurios que se inicializan.

 ```bash
    dotnet user-secrets init
 ```

para agregar la cadena de conexion, despues que ya haya inicializado los secretos de usuarios uso el siguiente comando:

```bash
    dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=(el servidor);Database=(Nombre de la BD);Trusted_Connection=True;TrustServerCertificate=True;"
```

Esto puede variar dependiendo si el SQL lo tengo con usuario o contraeña, en este caso lo tengo con Autenticacion de windows

#### ultimo paso para dejarlo listo

Para dejarlo listo debo de agregar una ultima cosa en el program:

```csharp
// DbContext
builder.Services.AddDbContext< SchoolFeesDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
```

# Manejo Centralizado de Errores

Este proyecto implementa un manejo centralizado de errores mediante una excepción de dominio (`BusinessException`) y un middleware global (`ExceptionMiddleware`), siguiendo buenas prácticas de separación de responsabilidades.

---

## 1. BusinessException (Capa de Dominio)

### Propósito

`BusinessException` representa errores de negocio que ocurren en la capa BL (Business Logic).  
Permite comunicar **la causa del error** y **el código HTTP adecuado**, sin acoplar el dominio a ASP.NET.

### Implementación

```csharp
namespace SchoolFees.EN.Exceptions
{
    public class BusinessException : Exception
    {
        public int StatusCode { get; }

        public BusinessException(string message, int statusCode = 400)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
```

#### uso tipico en la BL

```csharp
throw new BusinessException("El rol no existe.", 404);
throw new BusinessException("El nombre del rol ya está registrado.", 409);
throw new BusinessException("Datos inválidos.", 400);
```

**Ventajas**:

- 1. El dominio no depende de HTTP ni de controllers.
- 1. Cada error de negocio define explícitamente su semántica HTTP.
- 1. Evita lógica de control de errores en los controllers.

#### 2. ExceptionMiddleware (Capa UI / API)

**Propósito**
El middleware captura todas las excepciones no manejadas durante el ciclo de la request y las traduce a respuestas HTTP consistentes.

- **BusinessException** → respuesta controlada según StatusCode

- **Exception genérica** → error 500 (Internal Server Error)

**Implementación:**

````csharp
using Microsoft.AspNetCore.Http;
using SchoolFees.EN.Exceptions;

namespace SchoolFees.UI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessException ex)
            {
                context.Response.StatusCode = ex.StatusCode;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new
                {
                    error = ex.Message
                });
            }
            catch (Exception)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new
                {
                    error = "Error interno del servidor"
                });
            }
        }
    }
}

````

#### 3. Registro del Middleware

El middleware debe registrarse en el pipeline HTTP antes de mapear los controllers.

````csharp

Program.cs
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();
````

#### 4. Resultado en Controllers

Los controllers no necesitan try/catch para errores de negocio.

**Ejemplo**:

````csharp
[HttpGet("{id}")]
public async Task<IActionResult> GetRoleById(int id)
{
    var rol = await _rolService.GetRoleByIdAsync(id);
    return Ok(rol);
}
````

Si la BL lanza una BusinessException, el middleware se encarga de devolver la respuesta HTTP adecuada.

#### 5. Beneficios del Enfoque

1. Manejo de errores centralizado
2. Controllers limpios y enfocados en HTTP
3. BL independiente de ASP.NET
4. Respuestas HTTP consistentes
5. Escalabilidad y mantenibilidad

#### 6. Flujo Resumido

 **_Request_**
→ Controller  
→ Business Logic (BL)  
→ `BusinessException`  
→ `ExceptionMiddleware`  
→ HTTP Response
