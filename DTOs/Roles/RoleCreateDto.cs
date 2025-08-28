
using System.ComponentModel.DataAnnotations;

namespace SchoolFees.API.DTOs.Roles
{
    public record class RoleCreateDto
    (
        [property: Required(ErrorMessage ="El Nombre es Requerido")]
        [property: StringLength(50, ErrorMessage ="El maximo de caacteres es de 50")]
        string Name
    );
}
