using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.Domain.Entities
{
    /// <summary>
    /// Represents a student in the school system.
    /// Estudiante usa el id del usario para relacionarse con el sistema de autenticacion.
    /// </summary>
    public class Student
    {
        public Guid Id { get; set; } = Guid.NewGuid(); //identificador para la institucion 
        public Guid UserId { get; private set; } //identificador del usuario en el sistema de autenticacion
        public Guid? CurrentGroupId { get; private set; } //identificador del grupo al que pertenece el estudiante, puede ser nulo si no esta asignado a ningun grupo
        public DateTime EnrollmentDate { get; private set; } //fecha de inscripcion del estudiante
        public bool IsActive { get; private set; } = true; //indica si el estudiante esta activo o inactivo

        //constructor 
        public Student(Guid userId, DateTime enrollmentDate)
        {
            UserId = userId;
            EnrollmentDate = enrollmentDate;
        }
        //metodo para asignar un grupo al estudiante
        public void AssignToGroup(Guid grupId)
        {
            if (CurrentGroupId != null)
                throw new InvalidOperationException("El estudiante ya está asignado a un grupo.");
            CurrentGroupId = grupId;
        }

        //metodo desactivar a un alumno
        public void Deactivate()
        {
            if (!IsActive)
                throw new InvalidOperationException("El estudiante ya esta inactivo.");
            IsActive = false;
        }
    }
}