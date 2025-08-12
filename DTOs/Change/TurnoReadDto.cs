using System.ComponentModel.DataAnnotations;

namespace SchoolFees.API.DTOs.Change
{
    public record class TurnoReadDto(
        int Id,
        string Name
        );
}
