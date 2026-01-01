using Microsoft.AspNetCore.Mvc;
using SchoolFees.BL.Interfaces;
using SchoolFees.EN.models;

namespace SchoolFees.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        // GET: api/rol
        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _rolService.GetAllRolesAsync();
            return Ok(roles);
        }

        // GET: api/rol/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var rol = await _rolService.GetRoleByIdAsync(id);
            return Ok(rol);
        }

        // POST: api/rol
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] Rol rol)
        {
            var nuevoRol = await _rolService.CreateRoleAsync(rol);
            return CreatedAtAction(nameof(GetRoleById), new { id = nuevoRol.Id }, nuevoRol);
        }

        // PUT: api/rol/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] Rol rol)
        {
            rol.Id = id;
            await _rolService.UpdateRoleAsync(rol);
            return NoContent();
        }

        // DELETE: api/rol/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            await _rolService.DeleteRoleAsync(id);
            return NoContent();
        }
    }
}
