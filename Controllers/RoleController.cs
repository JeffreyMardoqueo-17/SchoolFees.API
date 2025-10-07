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
    }
}
