using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.EN.models
{
    /// <summary>
//     /// 
// CREATE TABLE Grado (
//     Id INT IDENTITY(1,1) PRIMARY KEY,
//     Nombre VARCHAR(20) NOT NULL,
//     Nivel VARCHAR(30) NOT NULL,
//     Estado BIT NOT NULL DEFAULT 1
// );
// GO
    /// </summary>
    public class Grado
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = String.Empty;
        public string Nivel { get; set; } = String.Empty;
        public bool Estado { get; set; }
    }
}