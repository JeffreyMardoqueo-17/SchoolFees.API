using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.EN.models
{
    public class Rol
    { public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Estado { get; set; }

        public ICollection<AdministradorRol> Administradores { get; set; }
            = new List<AdministradorRol>();
    }
}