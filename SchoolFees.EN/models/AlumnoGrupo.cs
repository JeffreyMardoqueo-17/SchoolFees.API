using SchoolFees.EN.Exceptions;

namespace SchoolFees.EN.Models
{
    public class AlumnoGrupo
    {
        public int IdAlumno { get; private set; }
        public int IdGrupo { get; private set; }
        public DateTime FechaAsignacion { get; private set; }

        // Constructor para EF
        protected AlumnoGrupo() { }

        // Constructor de dominio
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
