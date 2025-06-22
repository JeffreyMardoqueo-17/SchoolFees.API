using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.Domain.Entities.Payments
{
    public class PaymentOrder
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid StudentId { get; private set; }
        public DateTime DueDate { get; private set; }
        public decimal OriginalAmount { get; private set; } // Monto sin beca
        public decimal DiscountedAmount { get; private set; } // Monto después de aplicar la beca
        public List<Payment> Payments { get; private set; } = new();

        public Student? Student { get; private set; }

        // Calculado
        public decimal TotalPaid => Payments.Sum(p => p.AmountPaid);
        public decimal AmountDue => DiscountedAmount - TotalPaid;
        public bool IsPaid => AmountDue <= 0;

        // Constructor
        public PaymentOrder(Student student, decimal originalAmount, DateTime dueDate)
        {
            if (originalAmount <= 0)
                throw new ArgumentException("El monto debe ser mayor a 0.");

            StudentId = student.Id;
            DueDate = dueDate;
            OriginalAmount = originalAmount;
            DiscountedAmount = originalAmount * (1 - (student.ScholarshipPercentage / 100m));
        }

        public void AddPayment(Payment payment)
        {
            if (payment.StudentId != StudentId)
                throw new InvalidOperationException("El pago no corresponde a este estudiante.");

            if (IsPaid)
                throw new InvalidOperationException("Esta orden ya fue pagada.");

            Payments.Add(payment);
        }
    }

}