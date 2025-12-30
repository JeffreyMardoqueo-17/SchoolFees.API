# 📘 Transacciones y Unit of Work
AQUI EXPLICO el uso de **transacciones** y del patrón **Unit of Work** dentro del sistema **SchoolFees**, específicamente en operaciones que involucran múltiples escrituras relacionadas en base de datos.

---

## 🎯 Problema que se resuelve

Algunas operaciones de negocio requieren ejecutar **varias acciones persistentes** que dependen entre sí.  
Por ejemplo, en el proceso de creación de un alumno:

1. Crear el alumno  
2. Generar y asignar su código institucional  
3. Activar el alumno  
4. Asignar el alumno a un grupo  

Si una de estas acciones falla y las anteriores ya fueron persistidas, el sistema puede quedar en un **estado inconsistente** (datos incompletos o inválidos).

---

## 🔐 Transacciones

Una **transacción** permite agrupar múltiples operaciones de base de datos en una sola unidad atómica.

### Características principales
- **Atomicidad**: todas las operaciones se confirman o ninguna se guarda.
- **Consistencia**: la base de datos no queda en estados intermedios inválidos.
- **Aislamiento**: otras operaciones no ven cambios parciales.
- **Durabilidad**: una vez confirmados, los cambios persisten.

### Uso recomendado
Las transacciones deben utilizarse cuando:
- Existen **dos o más escrituras** relacionadas.
- Una operación depende del resultado exitoso de otra.
- El fallo parcial produciría datos inconsistentes.

---

## 🧩 Unit of Work

El patrón **Unit of Work** centraliza el control de la transacción y coordina el trabajo de múltiples repositorios bajo una misma unidad lógica.

### Responsabilidades
- Iniciar la transacción.
- Confirmar los cambios (`Commit`).
- Revertir los cambios en caso de error (`Rollback`).

### Responsabilidades que **NO** tiene
- No contiene reglas de negocio.
- No valida entidades.
- No ejecuta lógica de dominio.

Su única función es **gestionar el ciclo de vida de la transacción**.

---

## 🏗️ Separación de responsabilidades

| Capa | Responsabilidad |
|-----|-----------------|
| Business Logic (BL) | Orquestar el flujo de la operación |
| Rules | Reglas de negocio puras |
| Validators | Validaciones dependientes de base de datos |
| Repositories | Acceso a datos |
| Unit of Work | Control transaccional |

Esta separación evita acoplamientos indebidos y facilita el mantenimiento del sistema.

---

## 🔄 Flujo transaccional aplicado

En operaciones complejas, el flujo es el siguiente:

1. Iniciar transacción (`BeginTransaction`)
2. Ejecutar operaciones de negocio
3. Confirmar transacción (`Commit`) si todo fue exitoso
4. Revertir transacción (`Rollback`) si ocurre un error

Esto garantiza que el sistema nunca persista información parcial.

---

## ✅ Beneficios obtenidos

- Consistencia de datos garantizada
- Reducción de errores difíciles de rastrear
- Mejor control de fallos
- Código más predecible y escalable
- Arquitectura alineada con buenas prácticas empresariales

---

## 📌 Conclusión

El uso de transacciones y del patrón **Unit of Work** es fundamental en procesos donde múltiples operaciones de persistencia forman parte de una sola acción de negocio.  
Su implementación permite mantener la integridad del sistema, especialmente a medida que la complejidad y el volumen de datos aumentan.
