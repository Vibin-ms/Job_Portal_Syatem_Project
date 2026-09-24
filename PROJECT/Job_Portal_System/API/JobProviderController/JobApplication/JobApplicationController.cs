using AutoMapper;
using Domain.Services.JobApplications.DTO;
using Domain.Services.JobApplications.Interface;
using Job_Portal_System.API.JobProviderController.JobApplication.Request___Response_Body;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Job_Portal_System.API.JobProviderController.JobApplication
{
    [Route("api/JobProvider/[controller]")]
    [ApiController]
    [Authorize(Roles = "JobProvider")]
    [Tags("Job Provider - Applications")]
    public class JobApplicationController : ControllerBase
    {
        private readonly IJobApplicationService _jobApplicationService;
        private readonly IMapper _mapper;

        public JobApplicationController(IJobApplicationService jobApplicationService, IMapper mapper)
        {
            _jobApplicationService = jobApplicationService;
            _mapper = mapper;
        }

        private Guid? GetCurrentUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(userId, out Guid systemUserId) ? systemUserId : null;
        }

        [HttpGet]
        [Route("GetApplicationsForJob/{jobId}")]
        public async Task<IActionResult> GetApplicationsForJob(Guid jobId)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null) return Unauthorized(new { message = "Invalid or missing user token" });

                var result = await _jobApplicationService.GetApplicationsForJobAsync(userId.Value, jobId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetAllApplications")]
        public async Task<IActionResult> GetAllApplications()
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null) return Unauthorized(new { message = "Invalid or missing user token" });

                var result = await _jobApplicationService.GetApplicationsForProviderAsync(userId.Value);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetApplication/{id}")]
        public async Task<IActionResult> GetApplication(Guid id)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null) return Unauthorized(new { message = "Invalid or missing user token" });

                var result = await _jobApplicationService.GetApplicationByIdAsync(userId.Value, id);
                if (result == null)
                {
                    return NotFound(new { message = "Application not found or unauthorized" });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("UpdateApplicationStatus/{id}")]
        public async Task<IActionResult> UpdateApplicationStatus(Guid id, [FromBody] ApplicationStatusRequest request)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null) return Unauthorized(new { message = "Invalid or missing user token" });

                var dto = _mapper.Map<UpdateApplicationStatusDto>(request);
                var result = await _jobApplicationService.UpdateApplicationStatusAsync(userId.Value, id, dto);
                if (result == null)
                {
                    return NotFound(new { message = "Application not found or unauthorized to update" });
                }

                return Ok(new
                {
                    message = "Application status updated successfully",
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
