using System;
using System.Collections.Generic;
using SchoolFees.Domain.Entities.Payments;

namespace SchoolFees.Domain.Entities.students
{
    public class ScholarshipPolicy
    {
        public decimal Percentage { get; private set; }
        public Guid? SponsorId { get; private set; }

        private readonly HashSet<PaymentType> _coveredPaymentTypes = new();
        public IReadOnlyCollection<PaymentType> CoveredPaymentTypes => _coveredPaymentTypes;

        public ScholarshipPolicy(decimal percentage, IEnumerable<PaymentType> coveredTypes, Guid? sponsorId = null)
        {
            if (percentage < 0 || percentage > 100)
                throw new ArgumentOutOfRangeException(nameof(percentage), "El porcentaje debe estar entre 0 y 100.");

            Percentage = percentage;
            SponsorId = sponsorId;
            _coveredPaymentTypes = new HashSet<PaymentType>(coveredTypes);
        }

        public bool Covers(PaymentType type) => _coveredPaymentTypes.Contains(type);
    }
}
