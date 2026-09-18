using Domain.Services.JobPost.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.JobPost.Interface
{
    public interface IJobPostServices
    {
        Task<IEnumerable<JobPostDTO>> GetAllJobAsync();
        Task<bool> DeleteJobAsync(Guid Id);
    }
}
