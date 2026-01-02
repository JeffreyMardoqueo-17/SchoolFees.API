using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolFees.BL.Interfaces;
using SchoolFees.DAL.Interfaces;
using SchoolFees.EN.models;

namespace SchoolFees.BL.Services
{
    public class AdministradorService : IAdministradorService
    {
        private readonly IAdministradorRepository _administradorRepository;
        public AdministradorService(IAdministradorRepository administradorRepository)
        {
            _administradorRepository = administradorRepository;
        }
        public async Task<IEnumerable<Administrador>> GetAllAsync()
        {
            return await _administradorRepository.GetAllAsync();
        }
        public async Task<Administrador> GetByIdAsync( int id)
        {
            var administrador = await _administradorRepository.GetByIdAsync(id);
            if (administrador == null)
            {
                throw new KeyNotFoundException("Administrador no encontrado.");
            }
            return administrador;
        }

    }
}