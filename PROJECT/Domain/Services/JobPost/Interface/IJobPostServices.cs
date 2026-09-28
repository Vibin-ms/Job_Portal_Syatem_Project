using Domain.Enum;
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

        Task<JobPostResponseDto?> GetJobById(Guid id);
        Task<IEnumerable<JobPostSummaryDto>> GetAllPublicJobs();
        Task<IEnumerable<JobPostSummaryDto>> GetJobsByProviderUserIdAsync(Guid systemUserId);
        Task<JobPostResponseDto> CreateJobPostAsync(Guid systemUserId, CreateJobPostDto dto);
        Task<JobPostResponseDto?> UpdateJobPostAsync(Guid systemUserId, Guid jobId, UpdateJobPostDto dto);
        Task<bool> ChangeJobStatusAsync(Guid systemUserId, Guid jobId, JobPostStatus status);
        Task<bool> DeleteJobPostAsync(Guid systemUserId, Guid jobId);
    }
}
