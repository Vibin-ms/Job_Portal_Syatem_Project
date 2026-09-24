using Domain.Data;
using Domain.Models;
using Domain.Services.Jobseeker.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Jobseeker.Services
{
    public class JobSeekerRepository : IJobSeekerRepository 
    {
        private readonly AppDbContext dbContext;

        public JobSeekerRepository(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<List<Domain.Models.JobPost>> GetAllJobsAsync()
        {
            var jobs = await dbContext.JobPosts.Include(j => j.Company).Include(j => j.Jobcategory)
                .Include(j => j.JobType).Include(j => j.Location).Include(j => j.JobProvider).ToListAsync();

            return jobs;
        }

       public async Task<List<Domain.Models.JobPost>> SearchJobAsync(string name)
        {
            var jobs = await dbContext.JobPosts.Include(j => j.Company).Include(j => j.Jobcategory)
                .Include(j => j.JobType).Include(j => j.Location).Include(j => j.JobProvider)
                .Where(j => j.Title.Contains(name)).ToListAsync();

            return jobs;
        }


        public async Task<Guid?> GetJobSeekerProfileIdAsync(Guid systemUserId)
        {
            return await dbContext.JobSeekerProfiles
                .Where(profile => profile.JobSeeker.SystemUserId == systemUserId)
                .Select(profile => (Guid?)profile.JobSeekerProfileId)
                .FirstOrDefaultAsync();
        }


        public async Task<Domain.Models.JobApplication?> ApplyJobAsync(Guid jobPostId, Guid jobSeekerProfileId)
        {
            var jobExists = await dbContext.JobPosts.AnyAsync(job => job.JobPostId == jobPostId);

            if (!jobExists)
            {
                return null;
            }

            var alreadyApplied = await dbContext.AppliedJobs .AnyAsync(appliedJob => appliedJob.JobPostId == jobPostId &&
                appliedJob.JobSeekerProfileId == jobSeekerProfileId);

            if (alreadyApplied)
            {
                return null;
            }

            var appliedJob = new AppliedJob
            {
                AppliedJobId = Guid.NewGuid(),
                JobPostId = jobPostId,
                JobSeekerProfileId = jobSeekerProfileId,
                AppliedDate = DateTime.UtcNow
            };

            var jobApplication = new Domain.Models.JobApplication
            {
                JobApplicationId = Guid.NewGuid(),
                JobPostId = jobPostId,
                AppliedJobId = appliedJob.AppliedJobId,
                ApplicationStatus = 0,
                ApplicationDate = DateTime.UtcNow
            };

            dbContext.AppliedJobs.Add(appliedJob);
            dbContext.JobApplications.Add(jobApplication);

            await dbContext.SaveChangesAsync();

            return await dbContext.JobApplications
            .Include(application => application.AppliedJob)
            .FirstOrDefaultAsync(application => application.JobApplicationId == jobApplication.JobApplicationId);
        }



        public async Task<List<Domain.Models.JobApplication>> GetAppliedJobsAsync(Guid jobSeekerProfileId)
        {
            return await dbContext.JobApplications
            .Include(application => application.AppliedJob)
            .ThenInclude(appliedJob => appliedJob.JobPost)
            .ThenInclude(jobPost => jobPost.Company)
            .Where(application => application.AppliedJob.JobSeekerProfileId == jobSeekerProfileId)
            .OrderByDescending(application => application.ApplicationDate).ToListAsync();
        }

        public async Task<SavedJob?> SaveJobAsync(Guid jobPostId, Guid jobSeekerProfileId)
        {
            var jobExists = await dbContext.JobPosts.AnyAsync(job => job.JobPostId == jobPostId);

            if (!jobExists)
            {
                return null;
            }

            var alreadySaved = await dbContext.SavedJobs
            .AnyAsync(savedJob => savedJob.JobPostId == jobPostId && savedJob.JobSeekerProfileId == jobSeekerProfileId);

            if (alreadySaved)
            {
                return null;
            }

            var savedJob = new SavedJob
            {
                SavedJobId = Guid.NewGuid(),
                JobPostId = jobPostId,
                JobSeekerProfileId = jobSeekerProfileId,
                SavedDate = DateTime.UtcNow
            };

            dbContext.SavedJobs.Add(savedJob);

            await dbContext.SaveChangesAsync();

            return savedJob;
        }


        public async Task<List<SavedJob>> GetSavedJobsAsync(Guid jobSeekerProfileId)
        {
            return await dbContext.SavedJobs
                .Include(savedJob => savedJob.JobPost).ThenInclude(jobPost => jobPost.Company)
                .Where(savedJob => savedJob.JobSeekerProfileId == jobSeekerProfileId)
                .OrderByDescending(savedJob => savedJob.SavedDate).ToListAsync();
        }



        public async Task<bool> RemoveSavedJobAsync(Guid savedJobId,Guid jobSeekerProfileId)
        {
            var savedJob = await dbContext.SavedJobs
            .FirstOrDefaultAsync(savedJob =>
             savedJob.SavedJobId == savedJobId && savedJob.JobSeekerProfileId == jobSeekerProfileId);

            if (savedJob == null)
            {
                return false;
            }

            dbContext.SavedJobs.Remove(savedJob);

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<Domain.Models.InterviewSchedule>> GetInterviewSchedulesAsync(Guid jobSeekerProfileId)
        {
            return await dbContext.InterviewSchedules
            .Include(interview => interview.JobApplication)
            .ThenInclude(application => application.AppliedJob)
            .Where(interview => interview.JobApplication.AppliedJob.JobSeekerProfileId == jobSeekerProfileId)
            .OrderBy(interview => interview.ScheduledStartTime).ToListAsync();
        }


    }
}
