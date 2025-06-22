using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.Domain.Entities.Payments
{
    public enum PaymentMethod
    {
        Cash = 1,
        CreditCard = 2,
        BankTransfer = 3
        // aqui se pueden agregar más métodos de pago si es necesario como PayPal, etc.
    }
}