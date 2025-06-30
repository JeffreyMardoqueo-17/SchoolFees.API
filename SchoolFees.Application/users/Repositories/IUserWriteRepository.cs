using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolFees.Application.users.DTOs;

namespace SchoolFees.Application.users.Repositories
{
    public interface IUserWriteRepository
    {
        public Task<CreateUserRequest> CreateUserAsync(CreateUserRequest userRequest);
        //metdo para eliminar un usuario
        public Task<bool> DeleteUserAsync(Guid id);
        
    }
}