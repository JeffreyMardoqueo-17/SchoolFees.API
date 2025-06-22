using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.Domain.Entities.Payments
{
    public class ScholarshipPolicy
    {
        private readonly HashSet<PaymentType> _eligibleTypes;

        public ScholarshipPolicy(IEnumerable<PaymentType> types)
        {
            _eligibleTypes = new HashSet<PaymentType>(types);
        }

        public bool AppliesTo(PaymentType type) => _eligibleTypes.Contains(type);
    }

}