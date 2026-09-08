using AutoMapper;
using Domain.Enum;
using Domain.Helpers.EmailInterface;
using Domain.Models;
using Domain.Services.Authentication.Dto;
using Domain.Services.Authentication.Interface;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace Domain.Services.Authentication.Service
{
    public class Authenticationservice : IAuthservice
    {
        private readonly IAuthrepository _authRepository;

        private readonly IMapper _mapper;

        private readonly IEmailservice _emailService;

        public Authenticationservice(
            IAuthrepository authRepository,
            IMapper mapper,
            IEmailservice emailService)
        {
            _authRepository = authRepository;

            _mapper = mapper;

            _emailService = emailService;
        }

        public async Task<SystemuserDto?> RegisterAsync(
            SystemuserDto userDto)
        {
            var user = _mapper.Map<SystemUser>(userDto);

            var result =
                await _authRepository.RegisterAsync(user);
            if (result == null)
            {
                return null;
            }

           
            await _emailService.SendRegistrationEmailAsync(
                result.Email,
                result.FirstName,
                result.Roles?.ToString() ?? ""
            );

           
            await _authRepository.UpdateStatusAsync(result.Id,Status.Verified);

            result.Status = Status.Verified;

            return _mapper.Map<SystemuserDto>(result);
        }
        public async Task<bool> CreatePasswordAsync( Guid userId, string password)
        {
            return await _authRepository.CreatePasswordAsync(userId, password);
        }

        public async Task<AuthUser?> LoginAsync(string email, string password)
        {
               return await _authRepository.LoginAsync(email, password);
        }

    }
}