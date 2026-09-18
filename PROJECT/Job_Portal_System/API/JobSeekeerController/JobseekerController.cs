using AutoMapper;
using Domain.Services.Jobseekerprofile.Dto;
using Domain.Services.Jobseekerprofile.Interface;
using Job_Portal_System.API.JobSeekeerController.Request___Response_Body.Jobseekerprofile;
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
        public JobseekerController(ICrudservice _crudservice, IMapper _mapper)
        {
            crudservice = _crudservice;
            mapper = _mapper;
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
    }
}
