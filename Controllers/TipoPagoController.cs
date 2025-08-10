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
        //agregar el servicio al constructor
        private readonly ITipoPago _tipoPagoService;
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
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipoPagoReadDto>> GetTipoPagoById(int id)
        {
            var tipoPago = await _tipoPagoService.GetByIdTipoPagoAsync(id);
            if (tipoPago == null)
                return NotFound(new { message = $"No se encontro el tipo pago con el Id {id}" });

            var result = _mapper.Map<TipoPagoReadDto>(tipoPago);
            return Ok(result);
        }
       
        // POST: api/tipopago
        [HttpPost]
        public async Task<ActionResult> CreateTipoPago([FromBody] TipoPagoCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tipoPagoEntity = _mapper.Map<TipoPago>(createDto);
            await _tipoPagoService.CreateTipoPagoAsync(tipoPagoEntity);

            var readDto = _mapper.Map<TipoPagoReadDto>(tipoPagoEntity); //esto es para que despues de creado, ahora devuelvo el dto de lectura
            return CreatedAtAction(nameof(GetTipoPagoById), new { id = readDto.Id }, readDto);

        }
        [HttpPut("{id:int}")]
        //PUT : api/tipopago/{id}
        public async Task<ActionResult> UpdateTipoPago(int id, [FromBody] TipoPagoUpdateDto updateDto)
        {
            //validar que se mande lo necesario
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //validar que exista el que quiero actualizar
            var tipoPagoExiste = await _tipoPagoService.GetByIdTipoPagoAsync(id);
            if (tipoPagoExiste == null)
                return NotFound(new { message = $"No se encontro el tipo de pago con el Id : {id}" });

            // Mapear datos del DTO a la entidad existente para actualizarla
            _mapper.Map(updateDto, tipoPagoExiste);
            await _tipoPagoService.UpdateTipoPagoAsync(tipoPagoExiste);
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        //DELETE : api/tipopago/{id}
        public async Task<ActionResult> DeleteTipoPago(int id)
        {
            //verificar si existe
            var tipoPagoExiste = await _tipoPagoService.GetByIdTipoPagoAsync(id);
            //validar que no sea nullo
            if (tipoPagoExiste == null)
                return NotFound(new { message = $"No se encontró el TipoPago con id {id}" });

            //si existe y no es nullo, lo elimno
            await _tipoPagoService.DeleteTipoPago(id);
            //devuelve 204, que la operacion es exitosa pero no hay nada que devolver
            return NoContent();
        }

    }
}
