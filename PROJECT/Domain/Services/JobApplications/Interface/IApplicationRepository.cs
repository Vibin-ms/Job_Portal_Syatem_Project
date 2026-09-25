using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.JobApplications.Interface
{
    public interface IApplicationRepository
    {
        Task<Models.JobProvider?> GetProviderBySystemUserIdAsync(Guid systemUserId);
        Task<Models.JobApplication?> GetByIdAsync(Guid applicationId);
        Task<IEnumerable<Models.JobApplication>> GetApplicationsByJobPostIdAsync(Guid jobPostId);
        Task<IEnumerable<Models.JobApplication>> GetApplicationsByProviderIdAsync(Guid providerId);
        Task UpdateApplicationAsync(Models.JobApplication application);
        Task<IEnumerable<Models.JobApplication>> GetApplicationsByJobP0stAsync(Guid jobId);
    }
}
