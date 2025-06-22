using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.Domain.Entities.Payments
{
    public class PaymentItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public PaymentType Type { get; private set; } // Ej: Matrícula, colegiatura
    public decimal OriginalAmount { get; private set; }
    public decimal DiscountedAmount { get; private set; }
    public bool ScholarshipApplied { get; private set; }

    // Constructor
    public PaymentItem(PaymentType type, decimal originalAmount, decimal scholarshipPercentage, bool isScholarshipApplicable)
    {
        Type = type;
        OriginalAmount = originalAmount;

        if (isScholarshipApplicable && scholarshipPercentage > 0)
        {
            DiscountedAmount = originalAmount * (1 - (scholarshipPercentage / 100m));
            ScholarshipApplied = true;
        }
        else
        {
            DiscountedAmount = originalAmount;
            ScholarshipApplied = false;
        }
    }
}

}