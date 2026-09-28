using Domain.Data;
using Domain.Enum;
using Domain.Services.JobPost.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.JobPost.Services
{
    public class JobPostRepository:IJobPostRepository
    {
        private readonly AppDbContext _context;
        public JobPostRepository(AppDbContext _appDbContext)
        {
            _context = _appDbContext;
        }

        public async Task<IEnumerable<Models.JobPost>> GetAllJobAsync()
        {
            var jobs = await _context.JobPosts.Include(x => x.Company).Include(x => x.Jobcategory)
                .Include(x => x.Location).Include(x => x.JobType).ToListAsync();
            return jobs;
        }

        public async Task<bool> DeleteJobsAsync(Guid id)
        {
            var jobs = await _context.JobPosts.FirstOrDefaultAsync(x => x.JobPostId == id);
            if (jobs == null)
            {
                return false;
            }
            _context.JobPosts.Remove(jobs);
            await _context.SaveChangesAsync();
            return true;
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
                .Where(j => j.Status == (JobStatus)JobPostStatus.Accepted && j.DeadLine >= DateTime.UtcNow)
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
                var targetStatus = (JobStatus)status.Value;
                query = query.Where(j => j.Status == targetStatus);
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
                    .ThenInclude(cu => cu.Company)
                        .ThenInclude(c => c.Industry)
                .Include(jp => jp.CompanyUsers)
                    .ThenInclude(cu => cu.Company)
                        .ThenInclude(c => c.Location)
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

            var interviews = _context.InterviewSchedules.Where(i => i.JobApplication != null && i.JobApplication.JobPostId == id);
            _context.InterviewSchedules.RemoveRange(interviews);

            var apps = _context.JobApplications.Where(a => a.JobPostId == id);
            _context.JobApplications.RemoveRange(apps);

            var applied = _context.AppliedJobs.Where(a => a.JobPostId == id);
            _context.AppliedJobs.RemoveRange(applied);

            _context.JobPosts.Remove(job);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Guid> GetDefaultLocationIdAsync()
        {
            var loc = await _context.Locations.FirstOrDefaultAsync();
            if (loc == null)
            {
                loc = new Models.Location { LocationId = Guid.NewGuid(), Name = "Headquarters / Remote", Description = "Default Location" };
                await _context.Locations.AddAsync(loc);
                await _context.SaveChangesAsync();
            }
            return loc.LocationId;
        }

        public async Task<Guid> GetDefaultCategoryIdAsync(Guid? industryId = null)
        {
            if (industryId.HasValue && industryId.Value != Guid.Empty)
            {
                var industry = await _context.Industries.FindAsync(industryId.Value);
                if (industry != null)
                {
                    var matchedCategory = await _context.JobCategories
                        .FirstOrDefaultAsync(c => c.Name.Contains(industry.Name) || industry.Name.Contains(c.Name));
                    if (matchedCategory != null)
                    {
                        return matchedCategory.JobCategoryId;
                    }
                }
            }
            var category = await _context.JobCategories.FirstOrDefaultAsync();
            if (category == null)
            {
                category = new Models.JobCategory { JobCategoryId = Guid.NewGuid(), Name = "Software Engineering", Description = "Default Category" };
                await _context.JobCategories.AddAsync(category);
                await _context.SaveChangesAsync();
            }
            return category.JobCategoryId;
        }

        public async Task<Guid> GetDefaultJobTypeIdAsync()
        {
            var type = await _context.JobTypes.FirstOrDefaultAsync();
            if (type == null)
            {
                type = new Models.JobType { JobTypeId = Guid.NewGuid(), Name = "Full Time", Description = "Default Job Type" };
                await _context.JobTypes.AddAsync(type);
                await _context.SaveChangesAsync();
            }
            return type.JobTypeId;
        }

    }
}
