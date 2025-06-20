using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolFees.Domain.Entities
{
    /// <summary>
    /// Clase que representa una institucion educativa. o que obtenga el sistema 
    /// Su informacion basica como nombre, correo, telefono, logo, sitio web, direccion y descripcion
    /// Tambien contiene una lista de los dias de la semana en los que se imparten clases
    /// </summary>
    public class Institution
    {

        public Guid Id { get; set; } = Guid.NewGuid(); //identificador para la institucion 
        public string Name { get; set; } = string.Empty;//nombre de la institucion
        public string Email { get; set; } = string.Empty; //correo de la institucion
        public string PhoneNumber { get; set; } = string.Empty; //telefono de la institucion
        public string LogoUrl { get; set; } = string.Empty; //url del logo de la institucion
        public string SiteWebUrl { get; set; } = string.Empty; //url del sitio web de la institucion
        public string Address { get; set; } = string.Empty; //direccion de la institucion
        public string Description { get; set; } = string.Empty; //descripcion de la institucion
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; //fecha de creacion de la institucion

        public List<DayOfWeek> ClassDays { get; private set; } = new();

        //constructor por defecto
        public Institution(string name, string email, string phoneNumber, string logoUrl, string siteWebUrl, string address, string description)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            LogoUrl = logoUrl;
            SiteWebUrl = siteWebUrl;
            Address = address;
            Description = description;
        }
        /// <summary>
        /// Establece los días de la semana en que la institución imparte clases.
        /// Elimina días duplicados.
        /// </summary>
        /// <param name="days">Días de clase seleccionados (ej. lunes, miércoles, viernes).</param>
        public void SetClassDays(IEnumerable<DayOfWeek> days)
        {
            ClassDays = days.Distinct().ToList();
        }
    }
}

