# Explicación sobre los los Archivos de Autorizacion
--- 
Se componete de estos archivos:

- **DTOs:** _Aqui pongo los DTOS de creacion o solo lectura de cada entidades que conforman la autorizacion que son: "ROLE", "Permission" y "RolePermission"_
- **Repositories:** _Aqui estan las interfaces que definen los contratos que usare despues en el service,a qui defino los metodos que necesito_ 
---


#### Patrón: DTO como `record`

**Contexto:**  
Los Data Transfer Objects (DTOs) son contratos de datos inmutables que viajan entre capas o servicios (Controllers ⇄ Application ⇄ Domain). Necesitamos que sean simples, seguros y fáciles de comparar.

**Decisión:**  
Definiremos todos los DTOs como `record class` (o `record struct` cuando el tipo sea ≤ 32 bytes y sin herencia).

**Justificación técnica:**
1. Inmutabilidad por defecto → elimina efectos colaterales en la lógica de negocio.  
2. Igualdad por valor → evita duplicados fantasma en tests y colecciones.  
3. Sintaxis concisa → reduce un 60‑80 % de código repetitivo.  
4. Métodos auto‑generados (`ToString`, `Deconstruct`, `with`) → mejor trazabilidad y mantenibilidad.  

**Ejemplo:**
```csharp
public record StudentResponse(Guid Id, string FullName, string Grade);
```
**Ejemplo de estructura request**
```csharp
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.Application.users.DTOs
{
    /// <summary>
    /// Datos necesarios para registrar un nuevo usuario.
    /// </summary>
    public record CreateUserRequest(
      [property: Required, StringLength(40)] string Name,
      [property: Required, StringLength(40)] string LastName,
      [property: Required, EmailAddress] string Email,
      [property: Phone] string PhoneNumber,
      [property: Required, MinLength(8)] string Password,
      Guid RoleId,
      Guid InstitutionId);
}
```
**Ejemplo de estructura de tipo response**
```csharp
    
/// <summary>
/// Datos que se devuelven al consultar un usuario del sistema.
/// </summary>
public record UserResponse(
    Guid     Id,
    string   Name,
    string   LastName,
    string   Email,
    string   PhoneNumber,
    Guid     RoleId,
    string   RoleName,
    Guid     InstitutionId,
    string   InstitutionName,
    DateTime CreatedAt,
    bool     IsActive);
```
---
