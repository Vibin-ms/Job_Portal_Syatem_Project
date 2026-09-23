using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services.JobApplication.Interface
{
    public interface IJobApplicationRepo
    {
        Task<Models.JobProvider?> GetProviderBySystemUserIdAsync(Guid systemUserId);
        Task<Models.JobApplication?> GetByIdAsync(Guid applicationId);
        Task<IEnumerable<Models.JobApplication>> GetApplicationsByJobPostIdAsync(Guid jobPostId);
        Task<IEnumerable<Models.JobApplication>> GetApplicationsByProviderIdAsync(Guid providerId);
        Task UpdateApplicationAsync(Models.JobApplication application);
    }
}
