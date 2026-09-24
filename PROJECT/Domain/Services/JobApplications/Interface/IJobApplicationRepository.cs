using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services.JobApplications.Interface
{
    public interface IJobApplicationRepository
    {
        Task<Models.JobProvider?> GetProviderBySystemUserIdAsync(Guid systemUserId);
        Task<Models.JobApplication?> GetByIdAsync(Guid applicationId);
        Task<IEnumerable<Models.JobApplication>> GetApplicationsByJobPostIdAsync(Guid jobPostId);
        Task<IEnumerable<Models.JobApplication>> GetApplicationsByProviderIdAsync(Guid providerId);
        Task UpdateApplicationAsync(Models.JobApplication application);
        Task<IEnumerable<Models.JobApplication>> GetApplicationsByJobPostAsync(Guid jobId);
    }

    public interface IApplicationRepository
    {
        Task<IEnumerable<Models.JobApplication>> GetApplicationsByJobPostAsync(Guid jobId);
    }
}
