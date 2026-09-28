using Domain.Services.JobApplications.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.JobApplications.Interface
{
    public interface IApplicationServices
    {
        Task<IEnumerable<JobApplicationDto>> GetApplicationsForJobAsync(Guid systemUserId, Guid jobPostId);
        Task<IEnumerable<JobApplicationDto>> GetApplicationsForProviderAsync(Guid systemUserId);
        Task<JobApplicationDto?> GetApplicationByIdAsync(Guid systemUserId, Guid applicationId);
        Task<JobApplicationDto?> UpdateApplicationStatusAsync(Guid systemUserId, Guid applicationId, UpdateApplicationStatusDto dto);
        Task<IEnumerable<ApplicationDTO>> GetApplicationsByJobPostAsync(Guid jobId);
    }
}
