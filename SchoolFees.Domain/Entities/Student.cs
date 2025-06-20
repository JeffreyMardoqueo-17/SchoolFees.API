using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.Domain.Entities
{
    public class Student
    {
        public Guid Id { get; set; } = Guid.NewGuid(); //identificador para la institucion 
        public Guid UserId { get; private set; }
        public Guid? CurrentGroupId { get; private set; }
        public DateTime EnrollmentDate { get; private set; }
        public bool IsActive { get; private set; } = true;

        public Student(Guid userId, DateTime enrollmentDate)
        {
            UserId = userId;
            EnrollmentDate = enrollmentDate;
        }
    }
}