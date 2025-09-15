using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolFees.API.DTOs.TipoPago;
using SchoolFees.API.Models;
using SchoolFees.API.Services.TypePago;

namespace SchoolFees.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoPagoController : ControllerBase
    {
        private readonly ITipoPago _tipoPagoService;
        private readonly IMapper _mapper;

        public TipoPagoController(ITipoPago tipoPago, IMapper mapper)
        {
            _tipoPagoService = tipoPago;
            _mapper = mapper;
        }

        // GET: api/tipopago?institucionId={guid}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoPagoReadDto>>> GetAllTipoPago([FromQuery] Guid institucionId)
        {
            if (institucionId == Guid.Empty)
                return BadRequest("Se debe proporcionar un Id de institución válido.");

            var tiposPagos = await _tipoPagoService.GetAllTipoPagoAsinc(institucionId);
            var result = _mapper.Map<IEnumerable<TipoPagoReadDto>>(tiposPagos);
            return Ok(result);
        }


        // GET: api/tipopago/{id}?institucionId={guid}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipoPagoReadDto>> GetTipoPagoById(int id, [FromQuery] Guid institucionId)
        {
            if (institucionId == Guid.Empty)
                return BadRequest(new { message = "Se debe proporcionar un Id de institución válido." });

            var tipoPago = await _tipoPagoService.GetByIdTipoPagoAsync(id, institucionId);
            if (tipoPago == null)
                return NotFound(new { message = $"No se encontró el tipo de pago con Id {id} para esta institución." });

            var result = _mapper.Map<TipoPagoReadDto>(tipoPago);
            return Ok(result);
        }

        // POST: api/tipopago?institucionId={guid}
        [HttpPost]
        public async Task<ActionResult> CreateTipoPago([FromBody] TipoPagoCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tipoPagoEntity = _mapper.Map<TipoPago>(createDto);
            await _tipoPagoService.CreateTipoPagoAsync(tipoPagoEntity, createDto.IdInstitucion);

            var readDto = _mapper.Map<TipoPagoReadDto>(tipoPagoEntity);
            return CreatedAtAction(nameof(GetTipoPagoById), new { id = readDto.Id }, readDto);
        }

        // PUT: api/tipopago/{id}?institucionId={guid}
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateTipoPago(int id, [FromBody] TipoPagoUpdateDto updateDto, [FromQuery] Guid institucionId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (institucionId == Guid.Empty)
                return BadRequest(new { message = "Se debe proporcionar un Id de institución válido." });

            var tipoPagoExiste = await _tipoPagoService.GetByIdTipoPagoAsync(id, institucionId);
            if (tipoPagoExiste == null)
                return NotFound(new { message = $"No se encontró el tipo de pago con Id {id} para esta institución." });

            _mapper.Map(updateDto, tipoPagoExiste);
            await _tipoPagoService.UpdateTipoPagoAsync(tipoPagoExiste, institucionId);
            return NoContent();
        }

        // DELETE: api/tipopago/{id}?institucionId={guid}
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteTipoPago(int id, [FromQuery] Guid institucionId)
        {
            if (institucionId == Guid.Empty)
                return BadRequest(new { message = "Se debe proporcionar un Id de institución válido." });

            var tipoPagoExiste = await _tipoPagoService.GetByIdTipoPagoAsync(id, institucionId);
            if (tipoPagoExiste == null)
                return NotFound(new { message = $"No se encontró el tipo de pago con Id {id} para esta institución." });

            await _tipoPagoService.DeleteTipoPago(id, institucionId);
            return NoContent();
        }
    }
}
