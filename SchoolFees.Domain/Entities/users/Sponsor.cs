using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.Domain.Entities
{
    /// <summary>
    /// Representa un patrocinador de becas o donaciones.
    /// Puede ser una persona, organización o entidad que apoya a los estudiantes.
    /// </summary>
    public class Sponsor
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; } // Ej: "Iglesia Central", "Fundación Luz", "Anónimo"

        public string? Description { get; private set; } // Comentarios opcionales

        // Constructor
        public Sponsor(string name, string? description = null)
        {
            Name = name;
            Description = description;
        }
    }

}