# 📜 REGLAS DE NEGOCIO  

## MÓDULO: ADMINISTRACIÓN (ADMINISTRADOR)

---

## 🧱 A. Reglas de estructura (base del sistema)

### **A1. Roles administrativos definidos**

El sistema debe tener roles administrativos fijos:

- **SUPER_ADMIN** (rol crítico)
- Tesorero
- Secretario

Los roles existen como catálogo (`Rol`) y no se crean dinámicamente.

---

### **A2. Relación Administrador – Rol**

- Un administrador **puede tener uno o varios roles**.
- No es obligatorio tener más de un rol.
- Las combinaciones de roles son válidas (ejemplo: SUPER_ADMIN + Tesorero).

---

### **A3. Regla crítica del sistema**

🚨 El sistema **NUNCA** puede quedar sin al menos **1 SUPER_ADMIN activo**.

Esta regla aplica para:

- Desactivación
- Bloqueo
- Quitar roles
- Cualquier acción administrativa

---

### **A4. Roles no críticos**

- El sistema **puede** quedar sin Tesorero.
- El sistema **puede** quedar sin Secretario.

---

### **A5. Cupos por rol**

- Máximo **2 administradores activos por rol**.
- Para **SUPER_ADMIN**:
  - Mínimo: **1**
  - Máximo: **2** (configurable)

---

### **A6. Administradores mínimos**

- El sistema debe tener **al menos 1 administrador activo**.
- No existe un mínimo obligatorio de administradores por cantidad total.

---

## 👤 B. Reglas de creación de administrador

**B1.** El correo electrónico debe ser **único en todo el sistema**, sin importar el estado.

**B2.** Solo un **SUPER_ADMIN activo** puede crear administradores.

**B3.** Todo administrador debe crearse con **al menos un rol asignado**.

**B4.** No se puede asignar un rol si el cupo de ese rol está completo.

**B5.** Todo administrador debe crearse con:

- Contraseña hasheada
- Salt único
- Está prohibido almacenar contraseñas en texto plano

**B6.** Todo administrador nuevo inicia con:

- `Estado = activo`
- `IntentosFallidos = 0`
- `BloqueadoHasta = null`

**B7.** Toda creación debe registrar auditoría:

- `CreadoPor`
- `FechaCreacion`

---

## 🔐 C. Reglas de autenticación (login)

**C1.** Solo administradores **activos** pueden iniciar sesión.

**C2.** Un administrador **bloqueado** no puede iniciar sesión, aunque la contraseña sea correcta.

**C3.** El sistema permite un máximo de **N intentos fallidos consecutivos**.

**C4.** Al superar el máximo de intentos:

- El administrador se bloquea automáticamente
- El bloqueo es temporal

**C5.** Un login exitoso debe:

- Resetear intentos fallidos
- Registrar último login
- Registrar IP

**C6.** Los mensajes de error en login deben ser **genéricos**.
Nunca revelar si el correo existe o no.

---

## 🔁 D. Reglas de cambio de contraseña

**D1.** Para cambiar la contraseña se debe validar la contraseña actual.

**D2.** Cada cambio de contraseña debe generar un **nuevo salt**.

**D3.** Después de cambiar la contraseña:

- Se resetean los intentos fallidos
- El administrador queda desbloqueado

**D4.** El cambio de contraseña debe registrar auditoría.

---

## 🚫 E. Reglas de activación / desactivación

**E1.** La desactivación de un administrador es **lógica**, nunca física.

**E2.** Un administrador desactivado:

- No puede iniciar sesión
- No cuenta como administrador activo

**E3.** ❗ No se puede desactivar al **último SUPER_ADMIN activo**.

**E4.** Un administrador puede desactivarse aunque sea:

- El único Tesorero
- El único Secretario

**E5.** Al desactivar un administrador:

- Se liberan automáticamente todos sus roles

**E6.** Toda activación o desactivación debe registrar auditoría.

---

## 🎭 F. Reglas de asignación de roles

**F1.** Solo un **SUPER_ADMIN activo** puede:

- Asignar roles
- Quitar roles

**F2.** No se puede quitar el rol **SUPER_ADMIN** si:

- Es el último SUPER_ADMIN activo del sistema

**F3.** Un administrador desactivado:

- No puede recibir roles

---

## 🧾 G. Reglas de auditoría y control

**G1.** Toda acción crítica debe registrar:

- Quién la realizó
- Fecha
- Acción ejecutada

**G2.** Nunca se elimina físicamente un administrador.

**G3.** El sistema debe permitir reconstruir el historial administrativo.

---

## 🧠 H. Reglas de responsabilidad (arquitectura)

**H1.** Todas las reglas viven **exclusivamente en la Business Layer**.

**H2.** El Repository **no valida reglas de negocio**.

**H3.** El Controller **no decide reglas de negocio**.

---

## 🔒 I. Constantes del sistema

- `MAX_INTENTOS_FALLIDOS`
- `TIEMPO_BLOQUEO`
- `MAX_ADMINS_POR_ROL`
- `ROL_SUPER_ADMIN_ID`

---

## 🧠 Punto final

Estas reglas **NO son documentación decorativa**.  
Deben implementarse en:

- `AdministradorRules`
- `AdministradorService`

Si una regla no se cumple en la Business Layer,  
**el sistema está mal diseñado**.
