using Domain.Data;
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
        private readonly AppDbContext appDbContext;
        public JobPostRepository(AppDbContext _appDbContext)
        {
            appDbContext = _appDbContext;
        }
        public async Task<IEnumerable<Domain.Models.JobPost>>GetAllJobAsync()
        {
            var jobs=await appDbContext.JobPosts.Include(x=>x.Company).Include(x=>x.Jobcategory)
                .Include(x=>x.Location).Include(x=>x.JobType).ToListAsync();
            return jobs;
        }
        public async Task<bool> DeleteJobsAsync(Guid id)
        {
            var jobs = await appDbContext.JobPosts.FirstOrDefaultAsync(x => x.JobPostId == id);
            if (jobs == null)
            {
                return false;
            }
            appDbContext.JobPosts.Remove(jobs);
            await appDbContext.SaveChangesAsync();
            return true;
        }

    }
}
