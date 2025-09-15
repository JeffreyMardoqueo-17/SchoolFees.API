using Microsoft.AspNetCore.Mvc;
using SchoolFees.API.Models;
using SchoolFees.API.Services.TypeInstitution;

namespace SchoolFees.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoInstitucionController : ControllerBase
    {
        private readonly ITipeInstitution _service;
        private readonly ILogger<TipoInstitucionController> _logger;

        public TipoInstitucionController(ITipeInstitution service, ILogger<TipoInstitucionController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Obtener todos los tipos de institución
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoInstitucion>>> GetAll()
        {
            var result = await _service.GetAllTipoInsititucion();
            return Ok(result);
        }

        /// <summary>
        /// Obtener un tipo de institución por Id
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipoInstitucion>> GetById(int id)
        {
            try
            {
                var result = await _service.GetByIdTipoInstitucion(id);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Id inválido");
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "No encontrado");
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
