using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolFees.API.DTOs.Institucion;
using SchoolFees.API.Models;
using SchoolFees.API.Services.Institution;
using SchoolFees.API.Helpers;
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
            var result = await _institucionService.GetAllInstitucionesAsync();

            if (!result.Success)
                return NotFound(new { message = result.Message });

            var dtos = _mapper.Map<IEnumerable<InstitucionReadDto>>(result.Data);
            return Ok(dtos);
        }

        // GET: api/institucion/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<InstitucionReadDto>> GetInstitucionById(Guid id)
        {
            var result = await _institucionService.GetInstitucionByIdAsync(id);

            if (!result.Success)
                return NotFound(new { message = result.Message });

            var dto = _mapper.Map<InstitucionReadDto>(result.Data);
            return Ok(dto);
        }

        // POST: api/institucion
        [HttpPost]
        public async Task<ActionResult<InstitucionReadDto>> CreateInstitucion([FromBody] InstitucionCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var institucionEntity = _mapper.Map<Institucion>(createDto);
            var result = await _institucionService.CreateInstitucionAsync(institucionEntity);

            if (!result.Success)
                return BadRequest(new { message = result.Message });

            var readDto = _mapper.Map<InstitucionReadDto>(result.Data);
            return CreatedAtAction(nameof(GetInstitucionById), new { id = readDto.Id }, readDto);
        }

        // PUT: api/institucion/{id}
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<InstitucionReadDto>> UpdateInstitucion(Guid id, [FromBody] InstitucionUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != updateDto.Id)
                return BadRequest(new { message = "El Id de la URL no coincide con el Id del body" });

            var institucion = _mapper.Map<Institucion>(updateDto);
            var result = await _institucionService.UpdateInstitucionAsync(institucion);

            if (!result.Success)
                return NotFound(new { message = result.Message });

            var readDto = _mapper.Map<InstitucionReadDto>(result.Data);
            return Ok(readDto);
        }

        // DELETE: api/institucion/{id}
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteInstitucion(Guid id)
        {
            var result = await _institucionService.DeleteInstitucionAsync(id);

            if (!result.Success)
                return NotFound(new { message = result.Message });

            return NoContent();
        }
    }
}
