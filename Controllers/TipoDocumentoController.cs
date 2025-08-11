using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolFees.API.DTOs.TipoDocumento;
using SchoolFees.API.DTOs.TipoPago;
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

    }
}
