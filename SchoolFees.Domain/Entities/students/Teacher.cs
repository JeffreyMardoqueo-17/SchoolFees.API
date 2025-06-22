using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.Domain.Entities.students
{
    public class Teacher
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid UserId { get; private set; } // Identificador del usuario en el sistema de autenticación
        public string? Specialization { get; private set; } // Especialización del maestro (ej: Matemáticas, Ciencias, etc.)

        public Teacher( Guid userId, string? specialization = null)
        {
            UserId = userId;
            Specialization = specialization;
        }
    }
}