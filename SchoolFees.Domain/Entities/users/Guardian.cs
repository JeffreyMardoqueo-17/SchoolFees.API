using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.Domain.Entities
{
    public class Guardian
    {
        public Guid Id { get; private set; } = Guid.NewGuid(); //este es el id del Encargado
        public Guid UserId { get; private set; } //identificador del usuario en el sistema de autenticacion
        private readonly List<Guid> _studentIds = new(); //lista de identificadores de estudiantes asociados a este encargado
        public IReadOnlyCollection<Guid> StudentIds => _studentIds; //propiedad de solo lectura para obtener los identificadores de estudiantes

        public Guardian(Guid userId)
        {
            UserId = userId;
        }

        public void AddStudent(Guid studentId)
        {
            if (_studentIds.Contains(studentId))
                return;

            _studentIds.Add(studentId);
        }
    }
}