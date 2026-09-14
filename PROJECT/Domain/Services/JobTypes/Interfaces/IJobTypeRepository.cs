using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.JobTypes.Interfaces
{
    public interface IJobTypeRepository
    {
        Task<JobType> AddJobTypeAsync(JobType jobType);
        Task<IEnumerable<JobType>> GetAllJobTypeAsync();
        Task<JobType> UpdateJobTypeAsync(Guid id, JobType jobType);
        Task<bool> deleteJobTypeAsync(Guid id);

    }
}
