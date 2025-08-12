using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolFees.API.DTOs.Change;
using SchoolFees.API.Models;
using SchoolFees.API.Services.Change;

namespace SchoolFees.API.Controllers
{
    public class TurnoController : ControllerBase
    {
        private readonly ITurno _Service;
        private readonly Mapper _mapper;
        public TurnoController(ITurno service, Mapper mapper)
        {
            _Service = service;
            _mapper = mapper;
        }
        //get : turno
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TurnoReadDto>>> GetAllTurno()
        {
            var turnos = await _Service.GetAllTurnoAsync();
            var result = _mapper.Map<IEnumerable<TurnoReadDto>>(turnos);
            return Ok(result);
        }
        //GET : turno/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TurnoReadDto>> GetByIdTurno(int id)
        {
            var turno = await _Service.GetByIdTurnoAsync(id);
            if(turno == null)
                NotFound(new { message = $"No se encontro ningun tipo de documento con este id: {id}" });
            var result = _mapper.Map<TurnoReadDto>(turno);
            return Ok(result);
        }
    }
}
