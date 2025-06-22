using System;

namespace SchoolFees.Domain.Entities.Payments
{
    /// <summary>
    /// Representa un pago realizado por un estudiante.
    /// /// Incluye información sobre el monto, método de pago, tipo de pago,
    /// y datos del estudiante para facturación y auditoría.
    public class Payment
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        // Referencias
        public Guid StudentId { get; private set; }
        public Guid PaymentOrderId { get; private set; }

        // Montos y fechas
        public decimal AmountPaid { get; private set; }
        public DateTime PaidAt { get; private set; }

        // Métodos y tipos de pago
        public PaymentMethod Method { get; private set; }
        public PaymentType Type { get; private set; }

        // Información adicional y trazabilidad
        public string? Description { get; private set; }
        public Guid? PaidByUserId { get; private set; }
        public string? ReferenceCode { get; private set; }

        // Snapshot de datos críticos del alumno para facturación y auditoría
        public string StudentName { get; private set; }
        public string StudentGrade { get; private set; }
        public string? StudentGroupName { get; private set; }

        // Datos de beca snapshot
        public bool HasScholarship { get; private set; }
        public decimal ScholarshipPercentage { get; private set; }
        public Guid? SponsorId { get; private set; }

        // Constructor principal
        public Payment(
            Guid studentId,
            Guid paymentOrderId,
            decimal amountPaid,
            PaymentMethod method,
            PaymentType type,
            string studentName,
            string studentGrade,
            string? studentGroupName,
            bool hasScholarship,
            decimal scholarshipPercentage,
            Guid? sponsorId,
            Guid? paidByUserId = null,
            string? description = null,
            string? referenceCode = null)
        {
            if (amountPaid <= 0)
                throw new ArgumentException("El monto debe ser mayor a 0.");

            if (string.IsNullOrWhiteSpace(studentName))
                throw new ArgumentException("El nombre del estudiante es obligatorio.");

            if (string.IsNullOrWhiteSpace(studentGrade))
                throw new ArgumentException("El grado del estudiante es obligatorio.");

            StudentId = studentId;
            PaymentOrderId = paymentOrderId;
            AmountPaid = amountPaid;
            PaidAt = DateTime.UtcNow;
            Method = method;
            Type = type;
            StudentName = studentName;
            StudentGrade = studentGrade;
            StudentGroupName = studentGroupName;
            HasScholarship = hasScholarship;
            ScholarshipPercentage = scholarshipPercentage;
            SponsorId = sponsorId;
            PaidByUserId = paidByUserId;
            Description = description;
            ReferenceCode = referenceCode;
        }
    }
}
