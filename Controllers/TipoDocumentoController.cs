using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolFees.API.Models;
using SchoolFees.API.Services.Institution;
using SchoolFees.API.DTOs.TipoInsititution;

namespace SchoolFees.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstitucionController : ControllerBase
    {
        private readonly IInstitucion _institucionService;
        private readonly IMapper _mapper;

        public InstitucionController(IInstitucion institucionService, IMapper mapper)
        {
            _institucionService = institucionService;
            _mapper = mapper;
        }

        // GET: api/institucion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Institucion>>> GetAllInstituciones()
        {
            var instituciones = await _institucionService.GetAllInstitucionesAsync();
            var result = _mapper.Map<IEnumerable<TipoInstitucionReadDto>>(instituciones);
            return Ok(result);
        }

        // GET: api/institucion/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TipoInstitucionReadDto>> GetInstitucionById(Guid id)
        {
            var institucion = await _institucionService.GetInstitucionByIdAsync(id);
            if (institucion == null)
                return NotFound(new { message = $"No se encontró ninguna institución con el id {id}" });

            var result = _mapper.Map<TipoInstitucionReadDto>(institucion);
            return Ok(result);
        }

        // POST: api/institucion
        [HttpPost]
        public async Task<ActionResult> CreateInstitucion([FromBody] TipoInstitucionCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var institucionEntity = _mapper.Map<Institucion>(createDto);
            await _institucionService.CreateInstitucionAsync(institucionEntity);

            var readDto = _mapper.Map<TipoInstitucionReadDto>(institucionEntity);

            return CreatedAtAction(nameof(GetInstitucionById), new { id = readDto.Id }, readDto);
        }

        // PUT: api/institucion/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateInstitucion(Guid id, [FromBody] TipoInstitucionUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _institucionService.GetInstitucionByIdAsync(id);
            if (existing == null)
                return NotFound(new { message = $"No se encontró la institución con id {id}" });

            _mapper.Map(updateDto, existing);
            await _institucionService.UpdateInstitucionAsync(existing);

            return NoContent();
        }

        // DELETE: api/institucion/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteInstitucion(Guid id)
        {
            var existing = await _institucionService.GetInstitucionByIdAsync(id);
            if (existing == null)
                return NotFound(new { message = $"No se encontró la institución con id {id}" });

            await _institucionService.DeleteInstitucionAsync(id);
            return NoContent();
        }
    }
}
