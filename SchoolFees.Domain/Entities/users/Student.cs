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
        public Guid Id { get; set; } = Guid.NewGuid(); // ID del estudiante
        public Guid UserId { get; private set; } // ID del usuario autenticado
        public Guid? CurrentGroupId { get; private set; } // Grupo actual
        public DateTime EnrollmentDate { get; private set; }
        public bool IsActive { get; private set; } = true;

        // === NUEVOS CAMPOS DE BECA ===
        public bool HasScholarship => ScholarshipPercentage > 0;
        public decimal ScholarshipPercentage { get; private set; } = 0; // 0 - 100
        public Guid? SponsorId { get; private set; } // Persona o entidad que otorga la beca

        // Constructor
        public Student(Guid userId, DateTime enrollmentDate)
        {
            UserId = userId;
            EnrollmentDate = enrollmentDate;
        }

        // Asignar a grupo
        public void AssignToGroup(Guid grupId)
        {
            if (CurrentGroupId != null)
                throw new InvalidOperationException("El estudiante ya está asignado a un grupo.");
            CurrentGroupId = grupId;
        }

        // Desactivar
        public void Deactivate()
        {
            if (!IsActive)
                throw new InvalidOperationException("El estudiante ya está inactivo.");
            IsActive = false;
        }

        // === METODOS DE BECA ===

        public void AssignScholarship(decimal percentage, Guid? sponsorId = null)
        {
            if (percentage < 0 || percentage > 100)
                throw new ArgumentOutOfRangeException(nameof(percentage), "El porcentaje debe estar entre 0 y 100.");

            ScholarshipPercentage = percentage;
            SponsorId = sponsorId;
        }

        public void RemoveScholarship()
        {
            ScholarshipPercentage = 0;
            SponsorId = null;
        }
    }
}