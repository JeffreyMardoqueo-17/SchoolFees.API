using Microsoft.AspNetCore.Mvc;
using SchoolFees.BL.Interfaces;
using SchoolFees.BL.Security;
using SchoolFees.EN.models;
using SchoolFees.UI.DTOs.Admin;

namespace SchoolFees.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthControllerController : ControllerBase
    {
        private readonly IAdministradorService _administradorService;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthControllerController(IAdministradorService administradorService, JwtTokenGenerator jwtTokenGenerator)
        {
            _administradorService = administradorService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        // ============================
        // 📌 GET: api/administrador
        // ============================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var administradores = await _administradorService.GetAllAsync();

            var response = administradores.Select(a => new AdministradorResponseDto
            {
                Id = a.Id,
                Nombres = a.Nombres,
                Apellidos = a.Apellidos,
                Correo = a.Correo,
                Estado = a.Estado,
                FechaCreacion = a.FechaCreacion,
                CreadoPor = a.CreadoPor,
                FechaModificacion = a.FechaModificacion,
                ModificadoPor = a.ModificadoPor
            });

            return Ok(response);
        }

        // ============================
        // 📌 GET: api/administrador/{id}
        // ============================
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var admin = await _administradorService.GetByIdAsync(id);

            var response = new AdministradorResponseDto
            {
                Id = admin.Id,
                Nombres = admin.Nombres,
                Apellidos = admin.Apellidos,
                Correo = admin.Correo,
                Estado = admin.Estado,
                FechaCreacion = admin.FechaCreacion,
                CreadoPor = admin.CreadoPor,
                FechaModificacion = admin.FechaModificacion,
                ModificadoPor = admin.ModificadoPor
            };

            return Ok(response);
        }

        // ============================
        // 📌 POST: api/administrador
        // ============================
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] AdministradorCreateDto dto)
        {
            var administrador = new Administrador
            {
                Nombres = dto.Nombres,
                Apellidos = dto.Apellidos,
                Correo = dto.Correo,

                // OJO: aquí va la contraseña PLANA
                PasswordHash = dto.Password
            };

            await _administradorService.CreateAsync(
                administrador,
                dto.RolesIds,
                dto.CreadoPor ?? 0
            );

            return CreatedAtAction(
                nameof(GetById),
                new { id = administrador.Id },
                null
            );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();

            var admin = await _administradorService
                .LoginAsync(dto.Correo, dto.Password, ip!);

            // 🔐 Generar JWT (INFRAESTRUCTURA, NO BL)
            var token = _jwtTokenGenerator.GenerateToken(admin);

            var response = new LoginResponseDto
            {
                Id = admin.Id,
                Nombres = admin.Nombres,
                Apellidos = admin.Apellidos,
                Correo = admin.Correo,
                UltimoLogin = admin.UltimoLogin!.Value,
                Roles = admin.Roles.Select(r => r.Rol.Nombre),
                Token = token
            };

            return Ok(response);
        }

    }
}
