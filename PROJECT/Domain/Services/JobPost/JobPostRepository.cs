using Domain.Data;
using Domain.Services.JobPost.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enum;

namespace Domain.Services.JobPost
{
    public class JobPostRepository : IJobPostRepo
    {
        private readonly AppDbContext _context;

        public JobPostRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Models.JobPost?> GetByIdAsync(Guid id)
        {
            return await _context.JobPosts
                .Include(j => j.JobProvider)
                .Include(j => j.Company)
                .Include(j => j.Jobcategory)
                .Include(j => j.Location)
                .Include(j => j.JobApplications)
                .FirstOrDefaultAsync(j => j.JobPostId == id);
        }

        public async Task<IEnumerable<Models.JobPost>> GetAllPublishedJobsAsync()
        {
            return await _context.JobPosts
                .Include(j => j.Company)
                .Include(j => j.Jobcategory)
                .Include(j => j.Location)
                .Where(j => j.Status == JobPostStatus.Accepted && j.DeadLine >= DateTime.UtcNow)
                .OrderByDescending(j => j.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Models.JobPost>> GetAllJobsAsync(JobPostStatus? status = null)
        {
            var query = _context.JobPosts
                .Include(j => j.Company)
                .Include(j => j.Jobcategory)
                .Include(j => j.Location)
                .Include(j => j.JobType)
                .AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(j => j.Status == status.Value);
            }

            return await query
                .OrderByDescending(j => j.CreatedDate)
                .ToListAsync();
        }

        public Task<Models.JobProvider?> GetBySystemUserIdAsync(Guid systemUserId)
        {
           return _context.JobProviders.FirstOrDefaultAsync(jp => jp.SystemUserId == systemUserId);
        }

        public Task<Models.JobProvider?> GetProviderBySystemUserIdWithCompanyAsync(Guid systemUserId)
        {
            return _context.JobProviders
                .Include(jp => jp.CompanyUsers)
                .FirstOrDefaultAsync(jp => jp.SystemUserId == systemUserId);
        }


        public async Task<IEnumerable<Models.JobPost>> GetJobsByProviderIdAsync(Guid providerId)
        {
            return await _context.JobPosts
                .Include(j => j.Company)
                .Include(j => j.Jobcategory)
                .Include(j => j.Location)
                .Where(j => j.JobProviderId == providerId)
                .OrderByDescending(j => j.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Models.JobPost>> GetJobsByCompanyIdAsync(Guid companyId)
        {
            return await _context.JobPosts
                .Include(j => j.Jobcategory)
                .Include(j => j.Location)
                .Where(j => j.CompanyId == companyId)
                .OrderByDescending(j => j.CreatedDate)
                .ToListAsync();
        }

        public async Task<Models.JobPost> CreateJobPostAsync(Models.JobPost jobPost)
        {
            await _context.JobPosts.AddAsync(jobPost);
            await _context.SaveChangesAsync();
            return jobPost;
        }

        public async Task UpdateJobPostAsync(Models.JobPost jobPost)
        {
            _context.JobPosts.Update(jobPost);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteJobPostAsync(Guid id)
        {
            var job = await _context.JobPosts.FindAsync(id);
            if (job == null) return false;

            _context.JobPosts.Remove(job);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
