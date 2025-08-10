using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolFees.API.DTOs.TipoPago;
using SchoolFees.API.Models;
using SchoolFees.API.Services.TypePago;

namespace SchoolFees.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoPagoController : Controller
    {
        //agregar el servicio al constructor
        private readonly ITipoPago? _tipoPagoService;
        private readonly IMapper _mapper;
        public TipoPagoController(ITipoPago tipoPago, IMapper mapper)
        {
            _tipoPagoService = tipoPago;
            _mapper = mapper;
        }

        //get: api/tipopago
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoPagoReadDto>>> GetAllTipoPago()
        {
            var tiposPagos = await _tipoPagoService.GetAllTipoPagoAsinc(); //metodo del service
                        //_mapper.Map<Destino>(origen)
            var result = _mapper.Map<IEnumerable<TipoPagoReadDto>>(tiposPagos); 
            return Ok(result);
        }

        // GET: api/tipopago/{id}
        [HttpGet("{id=int}")]
        public async Task<ActionResult<TipoPagoReadDto>> GetTipoPagoById(int id)
        {
            var tipoPago = await _tipoPagoService.GetByIdTipoPagoAsync(id);
            if (tipoPago == null)
                return NotFound();

            var result = _mapper.Map<TipoPagoReadDto>(tipoPago);
            return Ok(tipoPago);
        }
       
        // POST: api/tipopago
        [HttpPost]
        public async Task<ActionResult> CreateTipoPago([FromBody] TipoPagoCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tipoPagoEntity = _mapper.Map<TipoPago>(createDto);
            await _tipoPagoService.CreateTipoPagoAsync(tipoPagoEntity);

            // Retornamos Created con la ruta para obtener el nuevo recurso
            return CreatedAtAction(nameof(GetTipoPagoById), new { id = tipoPagoEntity.Id }, null);
        }

    }
}
