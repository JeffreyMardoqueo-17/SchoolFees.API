using SchoolFees.Domain.Entities.Payments;
using System;
using System.Collections.Generic;

namespace SchoolFees.Domain.Entities.students
{
    public class Student
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid UserId { get; private set; }
        public Guid? CurrentGroupId { get; private set; }
        public DateTime EnrollmentDate { get; private set; }
        public bool IsActive { get; private set; } = true;

        // === NUEVA PROP DE BECA ===
        public ScholarshipPolicy? ScholarshipPolicy { get; private set; }

        public bool HasScholarship => ScholarshipPolicy != null && ScholarshipPolicy.Percentage > 0;

        public Student(Guid userId, DateTime enrollmentDate)
        {
            UserId = userId;
            EnrollmentDate = enrollmentDate;
        }

        public void AssignToGroup(Guid groupId)
        {
            if (CurrentGroupId != null)
                throw new InvalidOperationException("El estudiante ya está asignado a un grupo.");

            CurrentGroupId = groupId;
        }

        public void Deactivate()
        {
            if (!IsActive)
                throw new InvalidOperationException("El estudiante ya está inactivo.");

            IsActive = false;
        }

        // Nueva forma de asignar beca
        public void AssignScholarship(decimal percentage, IEnumerable<PaymentType> coveredTypes, Guid? sponsorId = null)
        {
            ScholarshipPolicy = new ScholarshipPolicy(percentage, coveredTypes, sponsorId);
        }

        public void RemoveScholarship()
        {
            ScholarshipPolicy = null;
        }
    }
}
