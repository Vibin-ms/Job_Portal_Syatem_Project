using AutoMapper;
using Domain.Models;
using Domain.Services.Jobseeker.DTO;
using Domain.Services.Jobseeker.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Jobseeker.Services
{
    public class JobSeekerServices : IJobSeekerServices
    {
        private readonly IJobSeekerRepository jobSeekerRepository;
        
        private readonly IHttpContextAccessor httpContextAccessor;


        public JobSeekerServices(IJobSeekerRepository _jobSeekerRepository, IHttpContextAccessor _httpContextAccessor)
        {
            jobSeekerRepository = _jobSeekerRepository;
            
            httpContextAccessor = _httpContextAccessor;
        }


        public async Task<List<Domain.Models.JobPost>> GetAllJobsAsync()
        {
            var jobs = await jobSeekerRepository.GetAllJobsAsync();
            return jobs;


        }

        public async Task<List<Domain.Models.JobPost>> SearchJobAsync(string name)
        {
            var jobs = await jobSeekerRepository.SearchJobAsync(name);

            return jobs;
        }





        private Guid GetSystemUserId()
        {
            var claimValue = httpContextAccessor.HttpContext?
                .User?
                .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?
                .Value;

            if (Guid.TryParse(claimValue, out var systemUserId))
            {
                return systemUserId;
            }

            return Guid.Empty;
        }



        private async Task<Guid?> GetLoggedInJobSeekerProfileIdAsync()
        {
            var systemUserId = GetSystemUserId();

            if (systemUserId == Guid.Empty)
            {
                return null;
            }

            return await jobSeekerRepository.GetJobSeekerProfileIdAsync(systemUserId);
        }

        
        public async Task<Domain.Models.JobApplication?> ApplyJobAsync(ApplyJobDTO dto)
        {
            var jobSeekerProfileId =
                await GetLoggedInJobSeekerProfileIdAsync();

            if (!jobSeekerProfileId.HasValue)
            {
                return null;
            }

            return await jobSeekerRepository.ApplyJobAsync(dto.JobPostId,jobSeekerProfileId.Value);
        }


        public async Task<List<Domain.Models.JobApplication>> GetAppliedJobsAsync()
        {
            var jobSeekerProfileId =
                await GetLoggedInJobSeekerProfileIdAsync();

            if (!jobSeekerProfileId.HasValue)
            {
                return new List<Domain.Models.JobApplication>();
            }

            return await jobSeekerRepository.GetAppliedJobsAsync(jobSeekerProfileId.Value);
        }


        public async Task<SavedJob?> SaveJobAsync(SaveJobDTO dto)
        {
            var jobSeekerProfileId = await GetLoggedInJobSeekerProfileIdAsync();

            if (!jobSeekerProfileId.HasValue)
            {
                return null;
            }

            return await jobSeekerRepository.SaveJobAsync( dto.JobPostId, jobSeekerProfileId.Value);
        }




        public async Task<List<SavedJob>> GetSavedJobsAsync()
        {
            var jobSeekerProfileId = await GetLoggedInJobSeekerProfileIdAsync();

            if (!jobSeekerProfileId.HasValue)
            {
                return new List<SavedJob>();
            }

            return await jobSeekerRepository.GetSavedJobsAsync(jobSeekerProfileId.Value);
        }


        public async Task<bool> RemoveSavedJobAsync(Guid savedJobId)
        {
            var jobSeekerProfileId = await GetLoggedInJobSeekerProfileIdAsync();

            if (!jobSeekerProfileId.HasValue)
            {
                return false;
            }

            return await jobSeekerRepository.RemoveSavedJobAsync(savedJobId,jobSeekerProfileId.Value);
        }


        public async Task<List<Domain.Models.InterviewSchedule>> GetInterviewSchedulesAsync()
        {
            var jobSeekerProfileId = await GetLoggedInJobSeekerProfileIdAsync();

            if (!jobSeekerProfileId.HasValue)
            {
                return new List<Domain.Models.InterviewSchedule>();
            }

            return await jobSeekerRepository.GetInterviewSchedulesAsync( jobSeekerProfileId.Value);
        }
    }
}
