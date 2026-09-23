using Domain.Services.JobApplication.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services.JobApplication.Interface
{
    public interface IJobApplicationService
    {
        Task<IEnumerable<JobApplicationDto>> GetApplicationsForJobAsync(Guid systemUserId, Guid jobPostId);
        Task<IEnumerable<JobApplicationDto>> GetApplicationsForProviderAsync(Guid systemUserId);
        Task<JobApplicationDto?> GetApplicationByIdAsync(Guid systemUserId, Guid applicationId);
        Task<JobApplicationDto?> UpdateApplicationStatusAsync(Guid systemUserId, Guid applicationId, UpdateApplicationStatusDto dto);
    }
}
