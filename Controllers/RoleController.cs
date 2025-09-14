using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolFees.API.Services;
using SchoolFees.API.Services.Roles;
using SchoolFees.API.Models;

namespace SchoolFees.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        //lo del servicio 
        private readonly IRole _role;
        private readonly IMapper _mapper;
        public RoleController(IRole role, IMapper mapper)
        {
            _role = role;
            _mapper = mapper; 
        }
        //get_ api/role
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetAllRole()
        {
            var roles = await _role.GetAllRoleAsync();
            var result = _mapper.Map<IEnumerable<Role>>(roles);

            return Ok(result);
        }
        //get: api/role/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Role>> GetRoleByIdAsync (int id)
        {
            var role = await _role.GetRoleByIdAsync(id);
            if (role == null)
                return NotFound(new { message = $"No se encontro ningun rol con este Id" });

            var result = _mapper.Map<Role>(role);
            return Ok(result);
        }


    }
}
