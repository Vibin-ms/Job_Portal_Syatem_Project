using Domain.Data;
using Domain.Models;
using Domain.Services.JobTypes.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.JobTypes.Services
{
    public class JobTypeRepository:IJobTypeRepository
    {
        private readonly AppDbContext appDbContext;
        public JobTypeRepository(AppDbContext _appDbContext)
        {

            appDbContext = _appDbContext;
        }
        public async Task<JobType> AddJobTypeAsync(JobType jobType)
        {
            var existing = await appDbContext.JobTypes.FirstOrDefaultAsync(x =>
                x.Name.ToLower() == jobType.Name.ToLower());

            if (existing != null)
            {
                return null;
            }

            await appDbContext.JobTypes.AddAsync(jobType);
            await appDbContext.SaveChangesAsync();
            return jobType;
        }
        public async Task<IEnumerable<JobType>> GetAllJobTypeAsync()
        {
            var Type = await appDbContext.JobTypes.ToListAsync();
            return Type;
        }
        public async Task<JobType> UpdateJobTypeAsync(Guid id, JobType jobType)
        {
            var existing = await appDbContext.JobTypes.FirstOrDefaultAsync(x => x.JobTypeId == id);

            if (existing != null)
            {
                existing.Name = jobType.Name;
                existing.Description = jobType.Description;
                await appDbContext.SaveChangesAsync();
                return existing;
            }
            return null;
        }
        public async Task<bool> deleteJobTypeAsync(Guid id)
        {
            var Type = await appDbContext.JobTypes.FirstOrDefaultAsync(x => x.JobTypeId == id);
            if (Type == null)
            {
                return false;
            }
            appDbContext.JobTypes.Remove(Type);
            await appDbContext.SaveChangesAsync();
            return true;
        }

    }
}
