using Domain.Models;
using Domain.Services.Authentication.Dto;

namespace Domain.Services.Authentication.Interface
{
    public interface IAuthservice
    {
        Task<SystemuserDto?> RegisterAsync(
            SystemuserDto userDto);

        Task<bool> CreatePasswordAsync(
            Guid userId,
            string password);

        Task<AuthUser?> LoginAsync(
            string email,
            string password);
    }
}