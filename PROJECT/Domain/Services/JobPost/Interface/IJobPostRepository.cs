using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.JobPost.Interface
{
    public interface IJobPostRepository
    {
        Task<IEnumerable<Domain.Models.JobPost>> GetAllJobAsync();
        Task<bool> DeleteJobsAsync(Guid id);
    }
}
