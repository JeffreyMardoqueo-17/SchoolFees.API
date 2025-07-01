# 📘 Contract Overview

## 1. `IChangePasswordRepository`

- **Responsabilidad:**  
  Gestiona la verificación y actualización de la contraseña de un usuario en la base de datos.
- **Métodos clave:**  
  - ``Task<bool> VerifyCurrentPasswordAsync(Guid userId, string currentPassword)``  
    Verifica si la contraseña actual del usuario es correcta.  
  - `Task SetNewPasswordHashAsync(Guid userId, string newPasswordHash)`  
    Guarda el nuevo hash de contraseña.
- **Uso típico:**  
  1. El servicio de aplicación llama `VerifyCurrentPasswordAsync`.  
  2. Si es válida, llama `SetNewPasswordHashAsync` para actualizar el hash.

---

## 2. `IUserLoginRepository`

- **Responsabilidad:**  
  Define las operaciones de inicio y cierre de sesión.
- **Métodos clave:**  
  - `Task<UserSession> UserLoginAsync(LoginRequest loginRequest)`  
    Procesa las credenciales e inicia sesión.  
  - `Task UserLogoutAsync(string userId)`  
    Cierra sesión (por ejemplo, invalida tokens).
- **Uso típico:**  
  Autenticación con JWT/refresh tokens y control de sesiones.

---

## 3. `IUserNotificationService`

- **Responsabilidad:**  
  Envía notificaciones al usuario sobre eventos críticos.
- **Método clave:**  
  - `Task SendPasswordChangedAsync(Guid userId)`  
    Notifica (correo, SMS, etc.) que la contraseña fue cambiada.
- **Uso típico:**  
  Se invoca inmediatamente después de actualizar la contraseña.

---

## 4. `IUserReadRepository`

- **Responsabilidad:**  
  Consultas de solo lectura sobre usuarios.
- **Métodos clave:**  
  - `Task<IEnumerable<UserDto>> GetUserAllAsync()`  
  - `Task<UserDto?> GetUserByIdAsync(Guid id)`  
  - `Task<UserDto?> GetUserByEmailAsync(string email)`  
  - `Task<IEnumerable<UserDto>> GetUsersByInstitutionIdAsync(Guid institutionId)`  
  - `Task<IEnumerable<UserDto>> GetUsersByRoleIdAsync(Guid roleId)`
- **Uso típico:**  
  Panel de administración, validaciones internas y filtros por email, rol o institución.

---

## 5. `IUserWriteRepository`

- **Responsabilidad:**  
  Operaciones de escritura (alta y baja de usuarios).
- **Métodos clave:**  
  - `Task<Guid> CreateUserAsync(CreateUserRequest userRequest)`  
    Registra un nuevo usuario.  
  - `Task DeleteUserAsync(Guid id)`  
    Elimina (o marca como inactivo) un usuario.
- **Uso típico:**  
  Registro de usuarios y administración de cuentas.

---

## ✅ Buenas prácticas

- **SRP:** Cada interfaz tiene una sola responsabilidad.  
- **Nombres expresivos:** Métodos describen claramente la intención.  
- **Métodos asincrónicos:** `Task<T>` para operaciones no bloqueantes.  
- **Testabilidad:** Contratos fáciles de mockear en pruebas unitarias.
