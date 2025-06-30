using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolFees.Application.users.DTOs;

namespace SchoolFees.Application.users.Repositories
{
    public interface IUserReadRepository
    {
        public Task<List<UserResponse>> GetUserAllAsync();
        public Task<UserResponse?> GetUserByIdAsync(Guid id);
        public Task<UserResponse?> GetUserByEmailAsync(string email);
        public Task<List<UserResponse>> GetUsersByInstitutionIdAsync(Guid institutionId);
        public Task<List<UserResponse>> GetUsersByRoleIdAsync(Guid roleId);
    }
}