using AutoMapper;
using Domain.Enum;
using Domain.Services.JobPost.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Domain.Services.JobPost.Dto.JobPostDtos;

namespace Domain.Services.JobPost
{
    public class JobPostService : IJobPostService
    {
        private readonly IJobPostRepo repository;
        private readonly IMapper mapper;

        public JobPostService(IJobPostRepo _repository, IMapper _mapper)
        {
            repository = _repository;
            mapper = _mapper;
        }

        public async Task<JobPostResponseDto?> GetJobById(Guid id)
        {
            var job = await repository.GetByIdAsync(id);
            if (job == null) return null;
            return mapper.Map<JobPostResponseDto>(job);
        }

        public async Task<IEnumerable<JobPostSummaryDto>> GetAllPublicJobs()
        {
            var jobs = await repository.GetAllPublishedJobsAsync();
            return mapper.Map<IEnumerable<JobPostSummaryDto>>(jobs);
        }

        public async Task<IEnumerable<JobPostSummaryDto>> GetJobsByProviderUserIdAsync(Guid systemUserId)
        {
            var provider = await repository.GetBySystemUserIdAsync(systemUserId);
            if (provider == null) return Enumerable.Empty<JobPostSummaryDto>();

            var jobs = await repository.GetJobsByProviderIdAsync(provider.JobProviderId);
            return mapper.Map<IEnumerable<JobPostSummaryDto>>(jobs);
        }

        public async Task<JobPostResponseDto> CreateJobPostAsync(Guid systemUserId, CreateJobPostDto dto)
        {
            var provider = await repository.GetProviderBySystemUserIdWithCompanyAsync(systemUserId);
            if (provider == null)
            {
                throw new KeyNotFoundException("Job provider profile not found.");
            }

            var activeCompanyId = provider.CompanyUsers.FirstOrDefault(cu => cu.IsActive)?.CompanyId;
            if (!activeCompanyId.HasValue)
            {
                throw new InvalidOperationException("You must select or register a company before posting jobs.");
            }

            var job = mapper.Map<Models.JobPost>(dto);
            job.JobProviderId = provider.JobProviderId;
            job.CompanyId = activeCompanyId.Value;
            job.CreatedDate = DateTime.UtcNow;
            job.UpdatedDate = DateTime.UtcNow;
            job.Status = JobPostStatus.Pending;

            await repository.CreateJobPostAsync(job);
            var reloaded = await repository.GetByIdAsync(job.JobPostId);
            return mapper.Map<JobPostResponseDto>(reloaded ?? job);
        }

        public async Task<JobPostResponseDto?> UpdateJobPostAsync(Guid systemUserId, Guid jobId, UpdateJobPostDto dto)
        {
            var provider = await repository.GetBySystemUserIdAsync(systemUserId);
            if (provider == null) return null;

            var job = await repository.GetByIdAsync(jobId);
            if (job == null || job.JobProviderId != provider.JobProviderId) return null;

            var originalStatus = job.Status;
            mapper.Map(dto, job);
            job.Status = originalStatus;
            job.UpdatedDate = DateTime.UtcNow;

            await repository.UpdateJobPostAsync(job);
            var reloaded = await repository.GetByIdAsync(job.JobPostId);
            return mapper.Map<JobPostResponseDto>(reloaded ?? job);
        }

        public async Task<bool> ChangeJobStatusAsync(Guid systemUserId, Guid jobId, JobPostStatus status)
        {
            var provider = await repository.GetBySystemUserIdAsync(systemUserId);
            if (provider == null) return false;

            var job = await repository.GetByIdAsync(jobId);
            if (job == null || job.JobProviderId != provider.JobProviderId) return false;

            job.Status = status;
            job.UpdatedDate = DateTime.UtcNow;
            await repository.UpdateJobPostAsync(job);
            return true;
        }

        public async Task<bool> ChangeJobStatusByAdminAsync(Guid jobId, JobPostStatus status)
        {
            if (status != JobPostStatus.Accepted && status != JobPostStatus.Rejected)
            {
                throw new ArgumentException("Admin can only update job status to Published or Closed.");
            }

            var job = await repository.GetByIdAsync(jobId);
            if (job == null) return false;

            job.Status = status;
            job.UpdatedDate = DateTime.UtcNow;
            await repository.UpdateJobPostAsync(job);
            return true;
        }

        public async Task<IEnumerable<JobPostSummaryDto>> GetAllJobsForAdminAsync(JobPostStatus? status = null)
        {
            var jobs = await repository.GetAllJobsAsync(status);
            return mapper.Map<IEnumerable<JobPostSummaryDto>>(jobs);
        }

        public async Task<bool> DeleteJobPostAsync(Guid systemUserId, Guid jobId)
        {
            var provider = await repository.GetBySystemUserIdAsync(systemUserId);
            if (provider == null) return false;

            var job = await repository.GetByIdAsync(jobId);
            if (job == null || job.JobProviderId != provider.JobProviderId) return false;

            return await repository.DeleteJobPostAsync(jobId);
        }
    }
}
