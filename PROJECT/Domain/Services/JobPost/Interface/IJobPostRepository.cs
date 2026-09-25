using Domain.Enum;
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
        Task<IEnumerable<Models.JobPost>> GetAllJobAsync();
        Task<bool> DeleteJobsAsync(Guid id);

        Task<Models.JobPost?> GetByIdAsync(Guid id);
        Task<IEnumerable<Models.JobPost>> GetAllPublishedJobsAsync();
        Task<IEnumerable<Models.JobPost>> GetAllJobsAsync(JobPostStatus? status = null);
        Task<Models.JobProvider?> GetBySystemUserIdAsync(Guid systemUserId);
        Task<Models.JobProvider?> GetProviderBySystemUserIdWithCompanyAsync(Guid systemUserId);
        Task<IEnumerable<Models.JobPost>> GetJobsByProviderIdAsync(Guid providerId);
        Task<IEnumerable<Models.JobPost>> GetJobsByCompanyIdAsync(Guid companyId);
        Task<Models.JobPost> CreateJobPostAsync(Models.JobPost jobPost);
        Task UpdateJobPostAsync(Models.JobPost jobPost);
        Task<bool> DeleteJobPostAsync(Guid id);
        Task<Guid> GetDefaultLocationIdAsync();
        Task<Guid> GetDefaultCategoryIdAsync(Guid? industryId = null);
        Task<Guid> GetDefaultJobTypeIdAsync();
    }
}
