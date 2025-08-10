using Microsoft.EntityFrameworkCore;
using SchoolFees.API.DataBase;
using SchoolFees.API.Models;

namespace SchoolFees.API.Services.TypeDocumento
{
    public class TipoDocumentoService : ITipoDocumento
    {
        //inyecto el context
        private readonly AplicationDBContext _context;
        private readonly ILogger<TipoDocumentoService> _logger;
        public TipoDocumentoService(AplicationDBContext aplicationDBContext, ILogger<TipoDocumentoService> logger)
        {
            _logger = logger;
            _context = aplicationDBContext;
        }
        //obtener todos
        public async Task<IEnumerable<TipoDocumento>> GetAllTipoDocumentoAsinc()
        {
            return await _context.TipoDocumento.ToListAsync();
        }
        //obtener por id
        public async Task<TipoDocumento> GetByIdTipoDocumento(int id)
        {
            //VALIDO QUE EL ID SEA VALIDO
            if(id <= 0) 
                throw new ArgumentException("El id debe de ser mayor a 0", nameof(id));
            //SI ES VALIDO HAGO LA CONSULTA A LA BD
            var tipoDocumento = await _context.TipoDocumento.FindAsync(id);
            //ESPERO QUE LA RESPUESTA NO SEA NULA
            if (tipoDocumento == null)
                throw new KeyNotFoundException($"No se encontro ningun tipo de documenot con este ID: {id}");
            return tipoDocumento;
        }
        //crear un tipoDocumento
        public async Task CreateTipoDocumentoAsync(TipoDocumento tipoDocumento)
        {
            try
            {
                if (tipoDocumento == null)
                    throw new ArgumentNullException(nameof(tipoDocumento), "No se puede crear un objeto nulo.");

                if (string.IsNullOrWhiteSpace(tipoDocumento.Name))
                    throw new ArgumentException("El nombre del tipo de documento es obligatorio.", nameof(tipoDocumento.Name));

                await _context.TipoDocumento.AddAsync(tipoDocumento);
                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException ex)
            {
                _logger?.LogWarning(ex, "Objeto nulo recibido en CreateTipoDocumentoAsync.");
                throw;
            }
            catch (ArgumentException ex)
            {
                _logger?.LogWarning(ex, "Argumento inválido en CreateTipoDocumentoAsync: {Mensaje}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error inesperado creando TipoDocumento.");
                throw new ApplicationException("Error inesperado creando TipoDocumento, contacte al administrador.", ex);
            }
        }

        //actualizar tipo documento
        public async Task UpdateTipoDocumentoAsync(TipoDocumento tipoDocumento)
        {
            try
            {

                if (tipoDocumento == null)
                    throw new ArgumentNullException(nameof(tipoDocumento), "No se puede actualizar un objeto nulo.");

                if (tipoDocumento.Id <= 0)
                    throw new ArgumentException("El Id es invalido. Debe ser mayor que 0.", nameof(tipoDocumento.Id));

                if (string.IsNullOrWhiteSpace(tipoDocumento.Name))
                    throw new ArgumentException("El nombre del tipo de documento es obligatorio.", nameof(tipoDocumento.Name));

                var tipoDocumentoExiste = await GetByIdTipoDocumento(tipoDocumento.Id);
                // Si GetByIdTipoDocumento ya lanza KeyNotFoundException, no es necesario validar null aqui

                tipoDocumentoExiste.Name = tipoDocumento.Name;

                _context.TipoDocumento.Update(tipoDocumentoExiste);
                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException ex)
            {
                _logger?.LogWarning(ex, "Objeto nulo recivido en UpdateTipoDocumentoAsync");
                throw new ArgumentException(nameof(tipoDocumento), "El objeto tipo documento no puede ser nulo");
            }
            catch (ArgumentException ex)
            {
                _logger?.LogWarning(ex, "Argumento inválido en UpdateTipoDocumentoAsync: {Mensaje}", ex.Message);
                throw new ArgumentException(ex.Message, ex.ParamName);
            }
            catch (KeyNotFoundException ex)
            {
                _logger?.LogWarning(ex, "Tipo Documento no encontrado en UpdateTipoDocumentoAsync: {Mensaje}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error inesperado actualizando TipoDocumento.");
                throw new ApplicationException("Error inesperado actualizando TipoPago, contacte al administrador pum", ex);
            }
        }

        //eliminar tipo documenot 
        public async Task DeleteTipoDocumentoAsync(int id)
        {
            try
            {
                var tipoDocumentoExiste = await GetByIdTipoDocumento(id);
                if (tipoDocumentoExiste == null)
                    throw new KeyNotFoundException("El tipo de documento no existe, no hay que eliminar");
                _context.TipoDocumento.Remove(tipoDocumentoExiste);
                await _context.SaveChangesAsync();

            }
            catch (KeyNotFoundException ex)
            {
                _logger?.LogWarning(ex, "Tipo Documento no encontrado en UpdateTipoDocumentoAsync: {Mensaje}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error inesperado actualizando TipoDocumento.");
                throw new ApplicationException("Error inesperado actualizando TipoPago, contacte al administrador pum", ex);
            }
        }
    }
}


 
          