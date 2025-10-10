using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolFees.API.Services.Roles;
using SchoolFees.API.Models;
using SchoolFees.API.DTOs.Roles;
using SchoolFees.API.Helpers;

namespace SchoolFees.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRole _role;
        private readonly IMapper _mapper;

        public RoleController(IRole role, IMapper mapper)
        {
            _role = role;
            _mapper = mapper;
        }

        // GET: api/role
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleReadDto>>> GetAllRole()
        {
            var rolesResult = await _role.GetAllRoleAsync(); // esto devuelve Result<IEnumerable<Role>>

            // aqui se  mapea el contenido interno (la Data)
            var result = _mapper.Map<IEnumerable<RoleReadDto>>(rolesResult.Data);

            return Ok(result);
        }


        // GET: api/role/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Result<RoleReadDto>>> GetRoleByIdAsync(int id)
        {
            var role = await _role.GetRoleByIdAsync(id);
            if (role == null || role.Data == null)
                return NotFound(Result<RoleReadDto>.Fail($"No se encontró ningún rol con Id {id}"));

            var result = _mapper.Map<Result<RoleReadDto>>(role);
            return Ok(result);
        }

        // POST: api/role
        [HttpPost]
        public async Task<ActionResult<Result<RoleReadDto>>> CreateRole(RoleCreateDto roleCreateDto)
        {
            var role = _mapper.Map<Role>(roleCreateDto);

            // Intentar crear el rol
            var creationResult = await _role.CreateRoleAsync(role);

            // Si falla, devolver BadRequest con mensaje
            if (!creationResult.Success)
                return BadRequest(Result<RoleReadDto>.Fail(creationResult.Message ?? "Error al crear el rol"));

            // Mapear el DTO
            var dto = _mapper.Map<RoleReadDto>(creationResult.Data);

            var result = Result<RoleReadDto>.Ok(dto);

            // Retornar Created con URL
            return Created($"/api/role/{dto.Id}", result);
        }


        // PUT: api/role/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult<RoleReadDto>> UpdateRole(int id, RoleUpdateDto roleUpdateDto)
        {
            if (id != roleUpdateDto.Id)
                return BadRequest(new { message = "El ID en la URL no coincide con el ID en el cuerpo de la solicitud." });

            var existingRoleResult = await _role.GetRoleByIdAsync(id);
            if (!existingRoleResult.Success || existingRoleResult.Data == null)
                return NotFound(new { message = $"No se encontró ningún rol con Id {id}" });

            var roleToUpdate = _mapper.Map(roleUpdateDto, existingRoleResult.Data);

            var updateResult = await _role.UpdateRoleAsync(roleToUpdate);
            if (!updateResult.Success)
                return BadRequest(new { message = updateResult.Message });

            var roleRead = _mapper.Map<RoleReadDto>(updateResult.Data);
            return Ok(roleRead);
        }

        // DELETE: api/role/{id}
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteRole(int id)
        {
            var existingRoleResult = await _role.GetRoleByIdAsync(id);
            if (!existingRoleResult.Success || existingRoleResult.Data == null)
                return NotFound(new { message = $"No se encontró ningún rol con Id {id}" });

            var deleteResult = await _role.DeleteRoleAsync(existingRoleResult.Data);
            if (!deleteResult.Success)
                return BadRequest(new { message = deleteResult.Message });

            return NoContent();
        }
        // GET: api/role/paged?pageNumber=1&pageSize=10&orderBy=name&orderDirection=asc
        [HttpGet("paged")]
        public async Task<ActionResult<Result<PagedResult<RoleReadDto>>>> GetRolesPagedAsync(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? orderBy = "Id",
            [FromQuery] string? orderDirection = "asc")
        {
            try
            {
                // 🔹 Llamamos al servicio con orden dinámico
                var pagedResult = await _role.GetRolesPagedAsync(pageNumber, pageSize, orderBy, orderDirection);

                if (!pagedResult.Items.Any())
                    return NotFound(Result<PagedResult<RoleReadDto>>.Fail("No se encontraron roles para la página solicitada."));

                // 🔹 Mapeamos las entidades a DTOs
                var mappedRoles = _mapper.Map<IEnumerable<RoleReadDto>>(pagedResult.Items);

                // 🔹 Reconstruimos el PagedResult con los DTOs y parámetros de orden
                var result = new PagedResult<RoleReadDto>
                {
                    Items = mappedRoles,
                    TotalCount = pagedResult.TotalCount,
                    PageSize = pagedResult.PageSize,
                    CurrentPage = pagedResult.CurrentPage,
                    OrderBy = pagedResult.OrderBy,
                    OrderDirection = pagedResult.OrderDirection
                };

                // 🔹 Retornamos en Result genérico
                return Ok(Result<PagedResult<RoleReadDto>>.Ok(result));
            }
            catch (Exception ex)
            {
                // 🔹 Manejo de errores profesional
                return StatusCode(500, Result<PagedResult<RoleReadDto>>.Fail($"Error interno al obtener roles paginados: {ex.Message}"));
            }
        }

    }
}
