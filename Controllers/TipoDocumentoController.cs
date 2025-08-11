using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolFees.API.DTOs.TipoDocumento;
using SchoolFees.API.DTOs.TipoPago;
using SchoolFees.API.Models;
using SchoolFees.API.Services.TypeDocumento;

namespace SchoolFees.API.Controllers
{
    public class TipoDocumentoController : ControllerBase
    {
        private readonly ITipoDocumento _tipoDocumentoService;
        private readonly Mapper _mapper;
        public TipoDocumentoController(ITipoDocumento tipoDocumento, Mapper mapper)
        {
            _tipoDocumentoService = tipoDocumento;
            _mapper = mapper;
        }
        //GET: api/tipodocumento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoDocReadDto>>> GetAllTipoDocumento()
        {
            var tipoDocumentos = await _tipoDocumentoService.GetAllTipoDocumentoAsinc();
            var resul = _mapper.Map<IEnumerable<TipoDocReadDto>>(tipoDocumentos);
            return Ok(resul);
        }
        //get : api/tipodocuemnto/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipoDocReadDto>> GetByIdTipoDocumento(int id)
        {
            var tipoDocumento = await _tipoDocumentoService.GetByIdTipoDocumento(id);
            if (tipoDocumento == null)
                NotFound(new { message = $"No se encontro ningun tipo de documento con este id: {id}" });
            var result = _mapper.Map<TipoDocReadDto>(tipoDocumento);
            return Ok(result);
        }
        //post : api/tipodocumento
        [HttpPost]
        public async Task<ActionResult> CreateTipoDocumento([FromBody] TipoDocCreate createDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var tipoDocumentoEntity = _mapper.Map<TipoDocumento>(createDTO);
            await _tipoDocumentoService.CreateTipoDocumentoAsync(tipoDocumentoEntity);

            var readDto = _mapper.Map<TipoDocReadDto>(tipoDocumentoEntity);
            return CreatedAtAction(nameof(GetByIdTipoDocumento), new {id = readDto.Id} );
        }

        //put : api/tipodocumento/{id}
        [HttpPut]
        public async Task<IActionResult> UpdateTipoDocumento(int id,[FromBody] TipoDocUpdateDto updateDto)
        {

        }

    }
}
