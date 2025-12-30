# 📘 Reglas de Negocio – Alumno

Este documento describe las **reglas de negocio** aplicadas a la entidad **Alumno** dentro del sistema **SchoolFees**.  
Estas reglas son validadas en la **capa de Business Logic (BL)** y no dependen del frontend.

---

## 🧠 Reglas de Negocio - CREATE // UPDATE

### 1 El alumno se crea Inactivo
Todo alumno, al momento de ser registrado, debe iniciar en estado **Inactivo**, porque despes de que se cree, se creara sin codigo
y no esta funcional asi, hasta que ya haya creado el codigo us estad pasar a **Activo**.

```csharp
alumno.Estado = false; ```
### 2 Campos obligatorios
Los campos **Nombre**, **Apellido**, **Fecha de nacimiento** son campos que llena el usuario son completamente oblilgatorios. 

### 3 El Alumno debe de pertencer a un Grado 
Al crear el Alumno debe de pertenecer a un **Grado** al crearse 

``` csharp

IdGrado = 1 ```
### 4 El Grado debe de existir
Se debe de berificar que el Id del Grado verdadermaene exist.
````csharp
// REGLA 4: El grado debe existir
            var gradoExiste = await _gradoRepository.GetByIdGradoAsync(alumno.GradoId);
````

> - En esta parte y accedo a la interfaz de grado no directamente con la clase DAL.

### 5 Detección de posible alumno duplicado (Advertencia)

El sistema no impide la creación de un alumno cuando existen otros registros con los mismos Nombres, Apellidos y Fecha de nacimiento, ya que se reconoce que pueden existir personas distintas con datos coincidentes.

Sin embargo, como medida preventiva, el sistema detecta y notifica cuando se identifica una posible coincidencia, permitiendo:

- Advertir al usuario

- Evitar registros duplicados por error

- Mantener la flexibilidad del dominio

> ⚠ Esta validación no lanza excepción ni bloquea el proceso de creación.
```csharp
// REGLA 5: Detectar posible duplicado (no bloqueante)
var posibleDuplicado = await _alumnoRepository
    .ExistePosibleDuplicadoAsync(
        alumno.Nombres,
        alumno.Apellidos,
        alumno.FechaNacimiento);

```

## OTROS
