using AutoMapper;
using Domain.Services.JobProviderProfile.DTO;
using Domain.Services.JobProviderProfile.Interface;
using Job_Portal_System.API.JobProviderController.Profile.Request___Response_Body;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Job_Portal_System.API.JobProviderController.Profile
{
    [Route("api/JobProvider/[controller]")]
    [ApiController]
    [Authorize(Roles = "JobProvider")]
    [Tags("Job Provider - Profile")]
    public class ProfileController : ControllerBase
    {
        private readonly IJobProviderServices _jobProviderService;
        private readonly IMapper _mapper;

        public ProfileController(IJobProviderServices jobProviderService, IMapper mapper)
        {
            _jobProviderService = jobProviderService;
            _mapper = mapper;
        }

        private Guid? GetCurrentUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(userId, out Guid systemUserId) ? systemUserId : null;
        }

        [HttpPost]
        [Route("CreateProfile")]
        public async Task<IActionResult> CreateProfile([FromBody] ProviderProfileRequest request)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null) return Unauthorized(new { message = "Invalid or missing user token" });

                var dto = _mapper.Map<CreateJobProviderDto>(request);
                var result = await _jobProviderService.CreateProviderProfileAsync(userId.Value, dto);

                return Ok(new
                {
                    message = "Provider profile created successfully",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetProfile")]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null) return Unauthorized(new { message = "Invalid or missing user token" });

                var result = await _jobProviderService.GetProviderBySystemUserIdAsync(userId.Value);
                if (result == null)
                {
                    return NotFound(new { message = "Provider profile not found" });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile([FromBody] ProviderProfileRequest request)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null) return Unauthorized(new { message = "Invalid or missing user token" });

                var dto = _mapper.Map<UpdateJobProviderDto>(request);
                var result = await _jobProviderService.UpdateProviderAsync(userId.Value, dto);
                if (result == null)
                {
                    return NotFound(new { message = "Provider profile not found" });
                }

                return Ok(new
                {
                    message = "Provider profile updated successfully",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
