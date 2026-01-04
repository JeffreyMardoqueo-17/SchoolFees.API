using SchoolFees.EN.Exceptions;
using SchoolFees.EN.models;

namespace SchoolFees.EN.Models
{
    public class AlumnoGrupo
    {
        public int IdAlumno { get; private set; }
        public int IdGrupo { get; private set; }
        public DateTime FechaAsignacion { get; private set; }

        // 🔥 PROPIEDADES DE NAVEGACIÓN (CLAVE)
        public Alumno Alumno { get; private set; } = null!;
        public Grupo Grupo { get; private set; } = null!;



        protected AlumnoGrupo() { }

        public AlumnoGrupo(int idAlumno, int idGrupo)
        {
            if (idAlumno <= 0)
                throw new BusinessException("Alumno inválido.");

            if (idGrupo <= 0)
                throw new BusinessException("Grupo inválido.");

            IdAlumno = idAlumno;
            IdGrupo = idGrupo;
            FechaAsignacion = DateTime.UtcNow;
        }
    }
}
