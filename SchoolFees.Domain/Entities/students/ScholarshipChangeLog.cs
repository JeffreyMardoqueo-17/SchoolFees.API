using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.Domain.Entities.students
{
    /// <summary>
    /// Clase que representa un registro de cambio de beca para un estudiante.
    /// Este registro se crea cada vez que se modifica el porcentaje de beca de un estudiante
    /// </summary>
    public class ScholarshipChangeLog
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid StudentId { get; private set; } // A quién se le cambia la beca
        public decimal OldPercentage { get; private set; }
        public decimal NewPercentage { get; private set; }
        public Guid? SponsorId { get; private set; } // Nuevo sponsor si cambia
        public DateTime ChangedAt { get; private set; } = DateTime.UtcNow;
        public Guid ChangedByUserId { get; private set; } // Quién hizo el cambio

        public string? Reason { get; private set; }

        public ScholarshipChangeLog(Guid studentId, decimal oldPercentage, decimal newPercentage, Guid? sponsorId, Guid changedByUserId, string? reason = null)
        {
            StudentId = studentId;
            OldPercentage = oldPercentage;
            NewPercentage = newPercentage;
            SponsorId = sponsorId;
            ChangedByUserId = changedByUserId;
            Reason = reason;
        }
    }

}