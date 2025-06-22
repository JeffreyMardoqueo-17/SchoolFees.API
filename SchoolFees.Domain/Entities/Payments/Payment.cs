using System;

namespace SchoolFees.Domain.Entities.Payments
{
    /// <summary>
    /// Representa un pago realizado por un estudiante.
    /// /// Incluye información sobre el monto, método de pago, tipo de pago,
    /// y datos del estudiante para facturación y auditoría.
    /// </summary>
    public class Payment
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid StudentId { get; private set; }
        public Guid PaymentOrderId { get; private set; }
        public DateTime PaidAt { get; private set; }
        public PaymentMethod Method { get; private set; }
        public Guid? PaidByUserId { get; private set; }
        public string? ReferenceCode { get; private set; }
        public string? Description { get; private set; }

        // Snapshot del estudiante
        public string StudentName { get; private set; }
        public string StudentGrade { get; private set; }
        public string? StudentGroupName { get; private set; }
        public bool HasScholarship { get; private set; }
        public decimal ScholarshipPercentage { get; private set; }
        public Guid? SponsorId { get; private set; }

        // NUEVO: lista de conceptos pagados
        public List<PaymentItem> Items { get; private set; } = new();

        public decimal TotalAmountPaid => Items.Sum(i => i.DiscountedAmount);

        public Payment(
            Guid studentId,
            Guid paymentOrderId,
            PaymentMethod method,
            List<PaymentItem> items,
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
            if (items == null || !items.Any())
                throw new ArgumentException("El pago debe contener al menos un concepto.");

            StudentId = studentId;
            PaymentOrderId = paymentOrderId;
            PaidAt = DateTime.UtcNow;
            Method = method;
            Items = items;
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
