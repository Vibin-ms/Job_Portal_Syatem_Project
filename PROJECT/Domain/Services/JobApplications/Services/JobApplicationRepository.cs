using Domain.Data;
using Domain.Models;
using Domain.Services.JobApplications.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Services.JobApplications.Services
{
    public class JobApplicationRepository : IJobApplicationRepository, IApplicationRepository
    {
        private readonly AppDbContext _context;

        public JobApplicationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Models.JobProvider?> GetProviderBySystemUserIdAsync(Guid systemUserId)
        {
            return await _context.JobProviders.FirstOrDefaultAsync(jp => jp.SystemUserId == systemUserId);
        }

        public async Task<Models.JobApplication?> GetByIdAsync(Guid applicationId)
        {
            return await _context.JobApplications
                .Include(ja => ja.JobPost)
                    .ThenInclude(jp => jp.Company)
                .Include(ja => ja.AppliedJob)
                    .ThenInclude(aj => aj.JobSeekerProfile)
                        .ThenInclude(jsp => jsp.JobSeeker)
                            .ThenInclude(js => js.SystemUser)
                .FirstOrDefaultAsync(ja => ja.JobApplicationId == applicationId);
        }

        public async Task<IEnumerable<Models.JobApplication>> GetApplicationsByJobPostIdAsync(Guid jobPostId)
        {
            return await _context.JobApplications
                .Include(ja => ja.JobPost)
                    .ThenInclude(jp => jp.Company)
                .Include(ja => ja.AppliedJob)
                    .ThenInclude(aj => aj.JobSeekerProfile)
                        .ThenInclude(jsp => jsp.JobSeeker)
                            .ThenInclude(js => js.SystemUser)
                .Where(ja => ja.JobPostId == jobPostId)
                .OrderByDescending(ja => ja.ApplicationDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Models.JobApplication>> GetApplicationsByProviderIdAsync(Guid providerId)
        {
            return await _context.JobApplications
                .Include(ja => ja.JobPost)
                    .ThenInclude(jp => jp.Company)
                .Include(ja => ja.AppliedJob)
                    .ThenInclude(aj => aj.JobSeekerProfile)
                        .ThenInclude(jsp => jsp.JobSeeker)
                            .ThenInclude(js => js.SystemUser)
                .Where(ja => ja.JobPost.JobProviderId == providerId)
                .OrderByDescending(ja => ja.ApplicationDate)
                .ToListAsync();
        }

        public async Task UpdateApplicationAsync(Models.JobApplication application)
        {
            _context.JobApplications.Update(application);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Models.JobApplication>> GetApplicationsByJobPostAsync(Guid jobId)
        {
            return await _context.JobApplications
                .Include(x => x.AppliedJob.JobSeekerProfile.JobSeeker.SystemUser)
                .Include(x => x.AppliedJob.JobSeekerProfile.Skill)
                .Include(x => x.AppliedJob.JobSeekerProfile.Qualification)
                .Include(x => x.AppliedJob.JobSeekerProfile.Experience)
                .Include(x => x.AppliedJob.JobPost)
                .Include(x => x.AppliedJob.JobSeekerProfile)
                .Where(x => x.JobPostId == jobId)
                .ToListAsync();
        }
    }
}
