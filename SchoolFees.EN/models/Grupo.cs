using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolFees.EN.Models;

namespace SchoolFees.EN.models
{
    public class Grupo
    {
//         //CREATE TABLE Grupo (
//     Id INT IDENTITY(1,1) PRIMARY KEY,
//     IdGrado INT NOT NULL,
//     IdTurno INT NOT NULL,
//     Nombre VARCHAR(10) NOT NULL,
//     AnioEscolar INT NOT NULL,
//     Estado BIT NOT NULL DEFAULT 1,
//     CONSTRAINT FK_Grupo_Grado FOREIGN KEY (IdGrado) REFERENCES Grado(Id),
//     CONSTRAINT FK_Grupo_Turno FOREIGN KEY (IdTurno) REFERENCES Turno(Id),
//     CONSTRAINT UQ_Grupo UNIQUE (IdGrado, IdTurno, Nombre, AnioEscolar)
// );
// GO
        public int Id { get; set; }
        public int IdGrado { get; set; }
        public int IdTurno { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int AnioEscolar { get; set; }
        public bool Estado { get; set; } = true;
        public ICollection<AlumnoGrupo> Grupos { get; private set; } = new List<AlumnoGrupo>();
    }
}