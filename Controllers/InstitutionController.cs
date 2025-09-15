using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolFees.API.DTOs.Institucion;
using SchoolFees.API.Models;
using SchoolFees.API.Services.Institution;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolFees.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstitutionController : ControllerBase
    {
        private readonly IInstitucion _institucionService;
        private readonly IMapper _mapper;

        public InstitutionController(IInstitucion institucionService, IMapper mapper)
        {
            _institucionService = institucionService;
            _mapper = mapper;
        }

        // GET: api/institucion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstitucionReadDto>>> GetAllInstituciones()
        {
            var instituciones = await _institucionService.GetAllInstitucionesAsync();
            var result = _mapper.Map<IEnumerable<InstitucionReadDto>>(instituciones);
            return Ok(result);
        }

        // GET: api/institucion/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<InstitucionReadDto>> GetInstitucionById(Guid id)
        {
            try
            {
                var institucion = await _institucionService.GetInstitucionByIdAsync(id);
                var result = _mapper.Map<InstitucionReadDto>(institucion);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"No se encontró la institución con Id {id}" });
            }
        }

        // POST: api/institucion
        [HttpPost]
        public async Task<ActionResult> CreateInstitucion([FromBody] InstitucionCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var institucionEntity = _mapper.Map<Institucion>(createDto);
            await _institucionService.CreateInstitucionAsync(institucionEntity);

            var readDto = _mapper.Map<InstitucionReadDto>(institucionEntity);
            return CreatedAtAction(nameof(GetInstitucionById), new { id = readDto.Id }, readDto);
        }

        // PUT: api/institucion/{id}
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateInstitucion(Guid id, [FromBody] InstitucionUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var institucion = await _institucionService.GetInstitucionByIdAsync(id);
                _mapper.Map(updateDto, institucion);
                await _institucionService.UpdateInstitucionAsync(institucion);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"No se encontró la institución con Id {id}" });
            }
        }

        // DELETE: api/institucion/{id}
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteInstitucion(Guid id)
        {
            try
            {
                await _institucionService.DeleteInstitucionAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"No se encontró la institución con Id {id}" });
            }
        }
    }
}
