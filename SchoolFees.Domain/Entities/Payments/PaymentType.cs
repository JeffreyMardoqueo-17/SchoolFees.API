using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.Domain.Entities.Payments
{
    public enum PaymentType
    {
        
        Enrollment = 1,
        Tuition = 2,
        Services = 3,
        Books = 4,
        Uniform = 5
    }
}