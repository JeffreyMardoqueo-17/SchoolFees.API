using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolFees.API.Services.Roles;
using SchoolFees.API.Models;
using SchoolFees.API.DTOs.Roles;

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
            var roles = await _role.GetAllRoleAsync();
            var result = _mapper.Map<IEnumerable<RoleReadDto>>(roles);
            return Ok(result);
        }

        // GET: api/role/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<RoleReadDto>> GetRoleByIdAsync(int id)
        {
            var role = await _role.GetRoleByIdAsync(id);
            if (role == null)
                return NotFound(new { message = $"No se encontró ningún rol con Id {id}" });

            var result = _mapper.Map<RoleReadDto>(role);
            return Ok(result);
        }

        // POST: api/role
        [HttpPost]
        public async Task<ActionResult<RoleReadDto>> CreateRole(RoleCreateDto roleCreateDto)
        {
            var role = _mapper.Map<Role>(roleCreateDto);

            // Aquí asignarías el IdInstitucion según el usuario autenticado o null si es global
            // role.IdInstitucion = userContext.InstitucionId;

            await _role.CreateRoleAsync(role);

            var roleRead = _mapper.Map<RoleReadDto>(role);
            return CreatedAtAction(nameof(GetRoleByIdAsync), new { id = roleRead.Id }, roleRead);
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
