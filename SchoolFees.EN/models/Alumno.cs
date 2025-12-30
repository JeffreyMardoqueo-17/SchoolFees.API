using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.EN.models
{

    //        CREATE TABLE Alumno (
    //        Id INT IDENTITY(1,1) PRIMARY KEY,
    //        Nombres VARCHAR(50) NOT NULL,
    //        Apellidos VARCHAR(50) NOT NULL,
    //        FechaNacimiento DATE,
    //        CodigoAlumno VARCHAR(20)  NULL UNIQUE,
    //        Estado BIT NOT NULL DEFAULT 1,
    //        FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    //        CreadoPor INT NULL,
    //        ModificadoPor INT NULL,
    //        FechaModificacion DATETIME NULL
    //    );
    public class Alumno
    {
        public int Id { get; set; }

        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }

        public string? CodigoAlumno { get; set; }

        public bool Estado { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int? CreadoPor { get; set; }
        public int? ModificadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }

}