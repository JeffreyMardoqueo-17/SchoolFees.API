using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolFees.Application.users.DTOs;

namespace SchoolFees.Application.users.Repositories
{
    public interface IUserLoginRepository
    {
        public Task<LoginRequest> UserLoginAsyn(LoginRequest loginRequest);
        public Task<bool> UserLogoutAsync(string userId);
        
    }
}