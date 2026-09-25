using AutoMapper;
using Domain.Services.JobPost.Interface;
using Domain.Services.JobPost.DTO;
using Job_Portal_System.API.JobProviderController.JobPost.Request___Response_Body;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Job_Portal_System.API.JobProviderController.JobPost
{
    [Route("api/JobProvider/[controller]")]
    [ApiController]
    [Authorize(Roles = "JobProvider")]
    [Tags("Job Provider - Job Postings")]
    public class JobPostController : ControllerBase
    {
        private readonly IJobPostServices _jobPostService;
        private readonly IMapper _mapper;

        public JobPostController(IJobPostServices jobPostService, IMapper mapper)
        {
            _jobPostService = jobPostService;
            _mapper = mapper;
        }

        private Guid? GetCurrentUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(userId, out Guid systemUserId) ? systemUserId : null;
        }

        [HttpPost]
        [Route("CreateJob")]
        public async Task<IActionResult> CreateJob([FromBody] CreateJobRequest request)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null) return Unauthorized(new { message = "Invalid or missing user token" });

                var dto = _mapper.Map<CreateJobPostDto>(request);
                var result = await _jobPostService.CreateJobPostAsync(userId.Value, dto);

                return Ok(new
                {
                    message = "Job posted successfully",
                    data = result
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetMyJobs")]
        public async Task<IActionResult> GetMyJobs()
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null) return Unauthorized(new { message = "Invalid or missing user token" });

                var result = await _jobPostService.GetJobsByProviderUserIdAsync(userId.Value);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetJob/{id}")]
        public async Task<IActionResult> GetJob(Guid id)
        {
            try
            {
                var result = await _jobPostService.GetJobById(id);
                if (result == null)
                {
                    return NotFound(new { message = "Job not found" });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("UpdateJob/{id}")]
        public async Task<IActionResult> UpdateJob(Guid id, [FromBody] UpdateJobRequest request)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null) return Unauthorized(new { message = "Invalid or missing user token" });

                var dto = _mapper.Map<UpdateJobPostDto>(request);
                var result = await _jobPostService.UpdateJobPostAsync(userId.Value, id, dto);
                if (result == null)
                {
                    return NotFound(new { message = "Job not found or unauthorized to update" });
                }

                return Ok(new
                {
                    message = "Job updated successfully",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("DeleteJob/{id}")]
        public async Task<IActionResult> DeleteJob(Guid id)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null) return Unauthorized(new { message = "Invalid or missing user token" });

                var success = await _jobPostService.DeleteJobPostAsync(userId.Value, id);
                if (!success)
                {
                    return NotFound(new { message = "Job not found or unauthorized to delete" });
                }

                return Ok(new { message = "Job deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
