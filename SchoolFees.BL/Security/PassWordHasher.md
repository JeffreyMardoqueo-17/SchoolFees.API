# 🔐 Documentación – PasswordHasher (Argon2id)

## 📌 Propósito

Este módulo implementa el **hashing seguro de contraseñas** para el sistema *SchoolFees* usando **Argon2id**, el algoritmo recomendado actualmente para proteger credenciales sensibles en sistemas modernos, especialmente aquellos relacionados con **dinero, pagos y control administrativo**.

Este componente **NO cifra** contraseñas.  
Las contraseñas **no se recuperan jamás**.  
Solo se **verifican**.

Si algún día este sistema puede “desencriptar” una contraseña, el diseño es incorrecto.

---

## 🧠 Decisión arquitectónica

- La lógica vive en la **Business Layer**
- No depende de infraestructura ni de base de datos
- Es **determinista**, **stateless** y **auditable**
- Centraliza la política de seguridad del sistema

Esto permite:
- Cambiar parámetros sin tocar controllers ni repositorios
- Mantener reglas de seguridad coherentes
- Auditar fácilmente decisiones criptográficas

---

## 🧱 Qué hace este módulo

El `PasswordHasher` provee **dos operaciones y solo dos**:

### 1️⃣ Hash de contraseña
Genera:
- Un **hash irreversible**
- Un **salt único y aleatorio**

Ambos se almacenan en la base de datos.

### 2️⃣ Verificación de contraseña
- Recalcula el hash usando el mismo salt
- Compara de forma segura
- Nunca revela información sensible

---

## ⚙️ Qué necesita para funcionar

### Dependencias

- `.NET 6+`
- Paquete NuGet:
  - `Konscious.Security.Cryptography.Argon2`

### Requisitos del sistema

- CPU compatible con paralelismo
- Memoria suficiente (mínimo ~64 MB por operación de hash)
- Entorno de servidor (no recomendado para dispositivos muy limitados)

---

## 🔐 Parámetros de seguridad

El algoritmo usa los siguientes parámetros fijos:

| Parámetro | Valor | Motivo |
|---------|------|-------|
| Salt | 128 bits | Evita rainbow tables |
| Hash | 256 bits | Resistencia criptográfica |
| Memoria | 64 MB | Defensa contra GPU/ASIC |
| Iteraciones | 4 | Dificulta ataques por fuerza bruta |
| Paralelismo | 2 | Aprovecha CPUs modernas |

Estos valores están pensados para **sistemas administrativos y financieros**, no para apps triviales.

---

## 🧬 ¿Por qué Argon2id?

### Razones técnicas

- Ganador del **Password Hashing Competition (PHC)**
- Diseñado específicamente para proteger contraseñas
- Resistente a:
  - Fuerza bruta
  - GPU attacks
  - ASIC attacks
  - Timing attacks

### ¿Por qué `Argon2id` y no otros?

| Algoritmo | Problema |
|---------|---------|
| SHA256 | Demasiado rápido (inseguro) |
| PBKDF2 | Obsoleto para sistemas críticos |
| BCrypt | Memoria limitada |
| Argon2i | Menor protección contra ataques híbridos |
| **Argon2id** | ✔ Mejor equilibrio seguridad / rendimiento |

**Argon2id** combina lo mejor de `Argon2i` y `Argon2d`.

---

## 🛡️ Medidas de seguridad implementadas

### ✔ Salt único por contraseña
Evita hashes iguales para contraseñas iguales.

### ✔ Comparación en tiempo constante
Previene **timing attacks**.

### ✔ No existe recuperación de contraseña
El sistema solo verifica, nunca revela.

### ✔ Parámetros centralizados
Evita configuraciones inconsistentes en el sistema.

---

## 🚫 Qué NO hace este módulo

- ❌ No guarda contraseñas
- ❌ No desencripta
- ❌ No genera tokens
- ❌ No gestiona sesiones
- ❌ No valida complejidad de contraseñas

Eso pertenece a **otras capas y reglas**.

---

## 🧩 Integración con el sistema

Este módulo se usa en:

- Creación de administradores
- Autenticación (login)
- Cambio de contraseña
- Reset de credenciales

Siempre desde la **Business Layer**, nunca desde controllers.

---

## 🧠 Principio clave

> “Si alguien obtiene la base de datos,  
> **no obtiene las contraseñas**.”

Este es el mínimo aceptable para un sistema serio.

---

## 🔮 Evolución futura (planeada)

- Versionado de parámetros de hash
- Rehash automático en login
- Configuración dinámica por entorno
- Auditoría criptográfica

---

