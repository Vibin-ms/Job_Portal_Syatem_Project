using AutoMapper;
using Domain.Services.Jobseeker.DTO;
using Domain.Services.Jobseeker.Interface;
using Domain.Services.Jobseekerprofile.Dto;
using Domain.Services.Jobseekerprofile.Interface;
using Job_Portal_System.API.JobSeekeerController.Request___Response_Body.Jobseekerprofile;
using Job_Portal_System.API.JobSeekeerController.RequestObject;
using Job_Portal_System.API.JobSeekeerController.ResponseObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Job_Portal_System.API.JobSeekeerController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "JobSeeker")]
    public class JobseekerController : ControllerBase
    {
        ICrudservice crudservice;
        IMapper mapper;
        IJobSeekerServices jobSeekerServices;
        public JobseekerController(ICrudservice _crudservice, IMapper _mapper, IJobSeekerServices _jobSeekerServices)
        {
            crudservice = _crudservice;
            mapper = _mapper;
            jobSeekerServices = _jobSeekerServices;
        }
        [HttpPost]
        [Route("CreateProfile")]
        public async Task<IActionResult> CreateProfile(
    [FromBody] Createrequest request)
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized(new
                {
                    message = "User ID not found in token"
                });
            }

            if (!Guid.TryParse(userId, out Guid systemUserId))
            {
                return Unauthorized(new
                {
                    message = "Invalid User ID"
                });
            }

            var jobseekerprofile =
                mapper.Map<Jobseekerprofiledto>(request);

            var result = await crudservice.CreateProfile(
                jobseekerprofile,
                systemUserId);

            if (result == null)
            {
                return BadRequest(new
                {
                    message = "JobSeeker not found"
                });
            }

            return Ok(new
            {
                message = "Profile created successfully"
            });
        }

        [HttpGet]
        [Route("GetProfile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized(new
                {
                    message = "User ID not found in token"
                });
            }

            if (!Guid.TryParse(userId, out Guid systemUserId))
            {
                return Unauthorized(new
                {
                    message = "Invalid User ID"
                });
            }

            var result =
                await crudservice.GetProfileBySystemUserId(
                    systemUserId);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "Profile not found"
                });
            }

            var response =
                mapper.Map<Getprofileresponse>(result);

            return Ok(response);
        }
        [HttpPut]
        [Route("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile(
     [FromBody] Updaterequest request)
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized(new
                {
                    message = "User ID not found in token"
                });
            }

            if (!Guid.TryParse(userId, out Guid systemUserId))
            {
                return Unauthorized(new
                {
                    message = "Invalid User ID"
                });
            }

            var jobseekerprofile =
                mapper.Map<Jobseekerprofiledto>(request);

            var result =
                await crudservice.UpdateProfile(
                    systemUserId,
                    jobseekerprofile);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "Profile not found"
                });
            }

            return Ok(new
            {
                message = "Profile updated successfully"
            });
        }
        [HttpDelete]
        [Route("DeleteProfile/{id}")]
        public async Task<IActionResult> DeleteProfile(Guid id)
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var result = await crudservice.DeleteProfile(id);

            if (result)
            {
                return Ok(new
                {
                    message = "Profile deleted successfully"
                });
            }

            return NotFound(new
            {
                message = "Profile not found"
            });
        }
        [HttpGet]
        [Route("GetSkills")]
        public async Task<IActionResult> GetSkills()
        {
            var result = await crudservice.GetAllSkills();

            return Ok(result);
        }


        [HttpGet]
        [Route("GetQualifications")]
        public async Task<IActionResult> GetQualifications()
        {
            var result = await crudservice.GetAllQualifications();

            return Ok(result);
        }


        [HttpGet]
        [Route("GetExperiences")]
        public async Task<IActionResult> GetExperiences()
        {
            var result = await crudservice.GetAllExperiences();

            return Ok(result);
        }


        [HttpGet]
        [Route("GetLocations")]
        public async Task<IActionResult> GetLocations()
        {
            var result = await crudservice.GetAllLocations();

            return Ok(result);
        }
        [HttpDelete]
        [Route("DeleteAccount")]
        public async Task<IActionResult> DeleteAccount()
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized(new
                {
                    message = "User ID not found in token"
                });
            }

            if (!Guid.TryParse(userId, out Guid systemUserId))
            {
                return Unauthorized(new
                {
                    message = "Invalid User ID"
                });
            }

            var result =
                await crudservice.DeleteJobSeekerAccount(systemUserId);

            if (!result)
            {
                return NotFound(new
                {
                    message = "JobSeeker account not found"
                });
            }

            return Ok(new
            {
                message = "JobSeeker account deleted successfully"
            });
        }







        [HttpGet("GetJobs")]
        [AllowAnonymous]
        public async Task<IActionResult> GetJobs()
        {
            var jobs = await jobSeekerServices.GetAllJobsAsync();

            var response = mapper.Map<List<GetJobsResponse>>(jobs);

            return Ok(response);
        }


        [HttpPost("SearchJob")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchJob(
            [FromBody] SearchJobRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest(new
                {
                    Message = "Job name is required"
                });
            }

            var jobs = await jobSeekerServices.SearchJobAsync(request.Name);

            var response = mapper.Map<List<SearchJobResponse>>(jobs);

            return Ok(response);
        }



        [HttpPost("ApplyJob")]
        public async Task<IActionResult> ApplyJob([FromBody] ApplyJobRequest request)
        {
            if (request == null || request.JobPostId == Guid.Empty)
            {
                return BadRequest(new
                {
                    Message = "Valid JobPostId is required"
                });
            }

            var dto = mapper.Map<ApplyJobDTO>(request);

            var result = await jobSeekerServices.ApplyJobAsync(dto);

            if (result == null)
            {
                return BadRequest(new
                {
                    Message = "Job does not exist, profile not found, or already applied"
                });
            }

            var response = mapper.Map<ApplyJobResponse>(result);

            return Ok(response);
        }



        [HttpGet("GetAppliedJobs")]
        public async Task<IActionResult> GetAppliedJobs()
        {
            var appliedJobs =
                await jobSeekerServices.GetAppliedJobsAsync();

            var response =
                mapper.Map<List<GetAppliedJobResponse>>(appliedJobs);

            return Ok(response);
        }




        [HttpPost("SaveJob")]
        public async Task<IActionResult> SaveJob([FromBody] SaveJobRequest request)
        {
            if (request == null || request.JobPostId == Guid.Empty)
            {
                return BadRequest(new
                {
                    Message = "Valid JobPostId is required"
                });
            }

            var dto = mapper.Map<SaveJobDTO>(request);

            var result = await jobSeekerServices.SaveJobAsync(dto);

            if (result == null)
            {
                return BadRequest(new
                {
                    Message = "Job does not exist or already saved"
                });
            }

            var response = mapper.Map<SaveJobResponse>(result);

            response.Message = "Job saved successfully";

            return Ok(response);
        }





        [HttpGet("GetSavedJobs")]
        public async Task<IActionResult> GetSavedJobs()
        {
            var savedJobs = await jobSeekerServices.GetSavedJobsAsync();

            var response = mapper.Map<List<GetSavedJobResponse>>(savedJobs);

            return Ok(response);
        }


        [HttpDelete("RemoveSavedJob/{savedJobId:guid}")]
        public async Task<IActionResult> RemoveSavedJob(Guid savedJobId)
        {
            if (savedJobId == Guid.Empty)
            {
                return BadRequest(new
                {
                    Message = "Valid SavedJobId is required"
                });
            }

            var result = await jobSeekerServices.RemoveSavedJobAsync(savedJobId);

            if (!result)
            {
                return NotFound(new
                {
                    Message = "Saved job not found or does not belong to this JobSeeker"
                });
            }

            return Ok(new RemoveSavedJobResponse
            {
                Message = "Saved job removed successfully"
            });
        }



        [HttpGet("GetInterviewSchedules")]
        public async Task<IActionResult> GetInterviewSchedules()
        {
            var interviews = await jobSeekerServices.GetInterviewSchedulesAsync();

            var response = mapper.Map<List<GetInterviewScheduleResponse>>(interviews);

            return Ok(response);
        }


    }
}
