using AutoMapper;
using Domain.Models;
using Domain.Services.Authentication.Interface;
using Domain.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Authentication.Service
{
  public  class Authenticationservice : IAuthservice
    {

        IAuthrepository authRepository;
        IMapper mapper;
        public Authenticationservice(IAuthrepository _authRepository, IMapper _mapper)
        {
            authRepository = _authRepository;
            mapper = _mapper;
        }
       public async Task<SystemuserDto?> RegisterAsync(SystemuserDto userdto)
        {
           var user = mapper.Map<SystemUser>(userdto);
            var result = await authRepository.RegisterAsync(user, userdto.Password);
            return mapper.Map<SystemuserDto>(result);
        }
        public async Task<AuthUser?> LoginAsync(string email, string password)
        {
            var authUser = await authRepository.LoginAsync(email, password);
            return authUser;
        }
    }
}
