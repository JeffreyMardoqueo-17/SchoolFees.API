
using SchoolFees.API.Models;
using SchoolFees.API.Services;
using SchoolFees.API.Services.Change;
using SchoolFees.API.Services.Institution;
using SchoolFees.API.Services.Roles;
using SchoolFees.API.Services.TypeDocumento;
using SchoolFees.API.Services.TypeInstitution;
using SchoolFees.API.Services.TypePago;

namespace SchoolFees.API.Services
{
    public static class ServiceRegistration
    {
        public static void AddAplicationServices(this IServiceCollection services)
        {
            //services.addScoped<Interfaz, servicio>();
            //... Servicios de clases sin llaves foraneas
            services.AddScoped<ITipoDocumento, TipoDocumentoService>();
            services.AddScoped<ITipoPago, TipoPagoService>();
            services.AddScoped<ITipeInstitution, TipoInsititucionService>();
            services.AddScoped<IRole, RoleService>();
            services.AddScoped<ITurno, TurnoService>();
            services.AddScoped<ITipeInstitution, TipoInsititucionService>();
            services.AddScoped<IInstitucion, InstitucionService>();

            
            //........... Servicio de las clases con llaves foraneas
            //services.AddScoped<>;
        }
    }
}
