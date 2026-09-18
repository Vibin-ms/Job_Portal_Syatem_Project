using Domain.Models;
using Domain.Services.Authentication.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Authentication.Interface
{
   public  interface IAuthrepository
    {
        Task<SystemUser?> RegisterAsync(SystemUser user);

        Task<bool> UpdateStatusAsync(
            Guid userId,
            Domain.Enum.Status status);

        Task<bool> CreatePasswordAsync(
            Guid userId,
            string password);

        Task<AuthUser?> LoginAsync(
            string email,
            string password);

        Task RevokeTokenAsync(string token);

        Task<bool> IsTokenRevokedAsync(string token);
    }
}
