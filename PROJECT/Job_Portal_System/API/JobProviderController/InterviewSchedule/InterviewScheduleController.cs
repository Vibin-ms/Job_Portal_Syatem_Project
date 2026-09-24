using AutoMapper;
using Domain.Services.InterviewSchedules.Dto;
using Domain.Services.InterviewSchedules.Interface;
using Job_Portal_System.API.JobProviderController.InterviewSchedule.Request___Response_Body;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Job_Portal_System.API.JobProviderController.InterviewSchedule
{
    [Route("api/JobProvider/[controller]")]
    [ApiController]
    [Authorize(Roles = "JobProvider")]
    [Tags("Job Provider - Interviews")]
    public class InterviewScheduleController : ControllerBase
    {
        private readonly IInterviewScheduleService _interviewScheduleService;
        private readonly IMapper _mapper;

        public InterviewScheduleController(IInterviewScheduleService interviewScheduleService, IMapper mapper)
        {
            _interviewScheduleService = interviewScheduleService;
            _mapper = mapper;
        }

        private Guid? GetCurrentUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(userId, out Guid systemUserId) ? systemUserId : null;
        }

        [HttpPost]
        [Route("ScheduleInterview")]
        public async Task<IActionResult> ScheduleInterview([FromBody] CreateInterviewRequest request)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null) return Unauthorized(new { message = "Invalid or missing user token" });

                var dto = _mapper.Map<CreateInterviewScheduleDto>(request);
                var result = await _interviewScheduleService.ScheduleInterviewAsync(userId.Value, dto);

                return Ok(new
                {
                    message = "Interview scheduled successfully",
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
        [Route("GetMyInterviews")]
        public async Task<IActionResult> GetMyInterviews()
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null) return Unauthorized(new { message = "Invalid or missing user token" });

                var result = await _interviewScheduleService.GetInterviewsForProviderAsync(userId.Value);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetInterview/{id}")]
        public async Task<IActionResult> GetInterview(Guid id)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null) return Unauthorized(new { message = "Invalid or missing user token" });

                var result = await _interviewScheduleService.GetInterviewByIdAsync(userId.Value, id);
                if (result == null)
                {
                    return NotFound(new { message = "Interview not found or unauthorized" });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("UpdateInterview/{id}")]
        public async Task<IActionResult> UpdateInterview(Guid id, [FromBody] UpdateInterviewRequest request)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null) return Unauthorized(new { message = "Invalid or missing user token" });

                var dto = _mapper.Map<UpdateInterviewScheduleDto>(request);
                var result = await _interviewScheduleService.UpdateInterviewAsync(userId.Value, id, dto);
                if (result == null)
                {
                    return NotFound(new { message = "Interview not found or unauthorized to update" });
                }

                return Ok(new
                {
                    message = "Interview updated successfully",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("CancelInterview/{id}")]
        public async Task<IActionResult> CancelInterview(Guid id)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null) return Unauthorized(new { message = "Invalid or missing user token" });

                var success = await _interviewScheduleService.CancelInterviewAsync(userId.Value, id);
                if (!success)
                {
                    return NotFound(new { message = "Interview not found or unauthorized to cancel" });
                }

                return Ok(new { message = "Interview cancelled successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
