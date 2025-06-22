using System;
using System.Collections.Generic;
using SchoolFees.Domain.Entities.Payments;

namespace SchoolFees.Domain.Entities.students
{
    public class Student
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid UserId { get; private set; }
        public Guid GuardianId { get; private set; } // Encargado padre o tutor
        public Guid CurrentGroupId { get; private set; } // Obligatorio asignar grupo al crear
        public DateTime EnrollmentDate { get; private set; }
        public bool IsActive { get; private set; } = true;

        public ScholarshipPolicy? ScholarshipPolicy { get; private set; }

        public bool HasScholarship => ScholarshipPolicy != null && ScholarshipPolicy.Percentage > 0;

        // Constructor asegura que se asigne grupo y encargado
        public Student(Guid userId, Guid guardianId, Guid groupId, DateTime enrollmentDate)
        {
            if (guardianId == Guid.Empty)
                throw new ArgumentException("El estudiante debe tener un encargado asignado.", nameof(guardianId));
            if (groupId == Guid.Empty)
                throw new ArgumentException("El estudiante debe estar asignado a un grupo.", nameof(groupId));

            UserId = userId;
            GuardianId = guardianId;
            CurrentGroupId = groupId;
            EnrollmentDate = enrollmentDate;
        }

        public void AssignToGroup(Guid groupId)
        {
            if (!IsActive)
                throw new InvalidOperationException("No se puede asignar grupo a un estudiante inactivo.");

            if (groupId == Guid.Empty)
                throw new ArgumentException("El Id del grupo no puede estar vacío.", nameof(groupId));

            if (CurrentGroupId != Guid.Empty)
                throw new InvalidOperationException("El estudiante ya está asignado a un grupo.");

            CurrentGroupId = groupId;
        }


        public void ChangeGuardian(Guid newGuardianId)
        {
            if (newGuardianId == Guid.Empty)
                throw new ArgumentException("El Id del encargado no puede estar vacío.", nameof(newGuardianId));

            if (newGuardianId == GuardianId)
                throw new InvalidOperationException("El encargado asignado es el mismo.");

            GuardianId = newGuardianId;
        }

        public void AssignScholarship(decimal percentage, IEnumerable<PaymentType> coveredTypes, Guid? sponsorId = null)
        {
            if (percentage < 0 || percentage > 100)
                throw new ArgumentOutOfRangeException(nameof(percentage), "El porcentaje debe estar entre 0 y 100.");

            ScholarshipPolicy = new ScholarshipPolicy(percentage, coveredTypes, sponsorId);
        }

        public void RemoveScholarship()
        {
            ScholarshipPolicy = null;
        }

        public void Deactivate()
        {
            if (!IsActive)
                throw new InvalidOperationException("El estudiante ya está inactivo.");

            IsActive = false;
        }
    }
}
