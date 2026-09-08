using AutoMapper;
using Domain.Services.Authentication.Dto;
using Domain.Services.Authentication.Interface;
using Domain.Services.Authentication.JWT.Interface;
using Job_Portal_System.API.Authentication_Controller.Login;
using Job_Portal_System.API.Authentication_Controller.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Job_Portal_System.API.Authentication_Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthservice _authService;
        private readonly IMapper _mapper;
        private readonly IJwtservice _jwtService;

        public AuthController(
            IAuthservice authService,
            IMapper mapper,
            IJwtservice jwtService)
        {
            _authService = authService;
            _mapper = mapper;
            _jwtService = jwtService;
        }



        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(
            Registerrequest request)
        {
            var dto = _mapper.Map<SystemuserDto>(request);

            var result =
                await _authService.RegisterAsync(dto);

            if (result == null)
            {
                return BadRequest(new
                {
                    Message = "Email already exists."
                });
            }

            var response =
                _mapper.Map<Registerresponse>(result);

            response.Message =
                "Registration successful. Verification email sent.";

            return Ok(response);
        }
        [HttpPost]
        [Route("CreatePassword")]
        public async Task<IActionResult> CreatePassword(
            Createpasswordrequestr request)
        {
            if (request.Password != request.ConfirmPassword)
            {
                return BadRequest(new
                {
                    Message = "Passwords do not match."
                });
            }

            var result =
                await _authService.CreatePasswordAsync(
                    request.UserId,
                    request.Password
                );

            if (!result)
            {
                return BadRequest(new
                {
                    Message =
                        "Password cannot be created. " +
                        "User may not be verified or password already exists."
                });
            }

            return Ok(new
            {
                Message =
                    "Password created successfully. Account completed."
            });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            Loginrequest request)
        {
            var authUser =
                await _authService.LoginAsync(
                    request.Email,
                    request.Password
                );

            if (authUser == null)
            {
                return Unauthorized(new
                {
                    Message =
                        "Invalid email/password or account is not completed."
                });
            }

            var systemUser = authUser.SystemUser;

            var role =
                systemUser.Roles?.ToString() ?? "";

            var token =
                _jwtService.GenerateToken(
                    systemUser.Id,
                    systemUser.Email,
                    role
                );

            var response = new Loginresponse
            {
                UserId = systemUser.Id,
                Email = systemUser.Email,
                Role = role,
                Token = token
            };

            return Ok(response);
        }




    }
        }