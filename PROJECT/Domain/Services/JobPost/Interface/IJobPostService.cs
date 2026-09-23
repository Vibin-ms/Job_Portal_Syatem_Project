using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Services.JobPost.Dto.JobPostDtos;

namespace Domain.Services.JobPost.Interface
{
    public interface IJobPostService
    {
        Task<JobPostResponseDto?> GetJobById(Guid id);
        Task<IEnumerable<JobPostSummaryDto>> GetAllPublicJobs();
        Task<IEnumerable<JobPostSummaryDto>> GetJobsByProviderUserIdAsync(Guid systemUserId);
        Task<JobPostResponseDto> CreateJobPostAsync(Guid systemUserId, CreateJobPostDto dto);
        Task<JobPostResponseDto?> UpdateJobPostAsync(Guid systemUserId, Guid jobId, UpdateJobPostDto dto);
        Task<bool> ChangeJobStatusAsync(Guid systemUserId, Guid jobId, JobPostStatus status);
        Task<bool> ChangeJobStatusByAdminAsync(Guid jobId, JobPostStatus status);
        Task<IEnumerable<JobPostSummaryDto>> GetAllJobsForAdminAsync(JobPostStatus? status = null);
        Task<bool> DeleteJobPostAsync(Guid systemUserId, Guid jobId);
        
    }
}
