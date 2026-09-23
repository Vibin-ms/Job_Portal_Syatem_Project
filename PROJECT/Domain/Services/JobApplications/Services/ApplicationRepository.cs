using Domain.Data;
using Domain.Models;
using Domain.Services.JobApplications.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.JobApplications.Services
{
    public class ApplicationRepository:IApplicationRepository
    {
        private readonly AppDbContext dbContext;
        public ApplicationRepository(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<IEnumerable<Domain.Models.JobApplication>> GetApplicationsByJobPostAsync(Guid jobId)
        {
            var application=await dbContext.JobApplications.Include(x=>x.AppliedJob.JobSeekerProfile.JobSeeker.SystemUser).Include(x=>x.AppliedJob.JobSeekerProfile.Skill)
                .Include(x=>x.AppliedJob.JobSeekerProfile.Qualification).Include(x=>x.AppliedJob.JobSeekerProfile.Experience).Include(x=>x.AppliedJob.JobPost).Include(x=>x.AppliedJob.JobSeekerProfile)
                .Where(x=>x.JobPostId==jobId).ToListAsync();
            return application;
        }
    }
}
