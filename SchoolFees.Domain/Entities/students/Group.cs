using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.Domain.Entities.students
{
    /// <summary>
    /// Clase que representa un grupo de estudiantes en la escuela.
    public class Group
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Grader GraderLevel { get; private set; } // Ej: "1ro"
        public string Name { get; private set; } // Ej: "Grupo A"
        public Guid TeacherId { get; private set; } // Maestro encargado

        public Group(Grader garderLevel, string name, Guid teacherId)
        {
            // GradeLevel = gradeLevel ?? throw new ArgumentNullException(nameof(gradeLevel));
            GraderLevel = garderLevel;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            TeacherId = teacherId;
        }
    }

}