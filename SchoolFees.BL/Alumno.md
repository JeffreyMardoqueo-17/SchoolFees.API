# 📘 Reglas de Negocio – Alumno

Este documento describe las **reglas de negocio** aplicadas a la entidad **Alumno** dentro del sistema **SchoolFees**.  
Estas reglas son validadas en la **capa de Business Logic (BL)** y no dependen del frontend.

---

## 🧠 Reglas de Negocio - CREATE // UPDATE

### 1 El alumno se crea activo
Todo alumno, al momento de ser registrado, debe iniciar en estado **Activo**.

```csharp
alumno.Estado = true;
```
### 2 Campos obligatorios
Los campos **Nombre**, **Apellido**, **Fecha de nacimiento** son campos que llena el usuario son completamente oblilgatorios. 

### 3 El Alumno debe de pertencer a un Grado 
Al crear el Alumno debe de pertenecer a un **Grado** al crearse 

``` csharp

IdGrado = 1
```
### 4 El Grado debe de existir
Se debe de berificar que el Id del Grado verdadermaene exista 
````csharp
// REGLA 4: El grado debe existir
            var gradoExiste = await _gradoRepository.GetByIdGradoAsync(alumno.GradoId);
````

> - En esta parte y accedo a la interfaz de grado no directamente con la clase DAL 