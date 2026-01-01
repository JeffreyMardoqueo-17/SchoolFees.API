using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.EN.models
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = String.Empty;
        
        public bool Estado { get; set; }
    }
}