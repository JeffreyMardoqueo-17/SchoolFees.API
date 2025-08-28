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
        public async Task<ActionResult<IEnumerable<Role>>> GetAllRole()
        {
            var roles = await _role.GetAllRoleAsync();
            var result = _mapper.Map<IEnumerable<Role>>(roles);

            return Ok(result);
        }

        
    }
}
