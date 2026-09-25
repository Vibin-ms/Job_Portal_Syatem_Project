using AutoMapper;
using Domain.Enum;
using Domain.Services.JobPost.DTO;
using Domain.Services.JobPost.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.JobPost.Services
{
    public class JobPostServices:IJobPostServices
    {
        private readonly IJobPostRepository repository;
        private readonly IMapper mapper;
        public JobPostServices(IJobPostRepository _jobPostRepository, IMapper _mapper)
        {
            repository = _jobPostRepository;
            mapper = _mapper;
        }
        public async Task<IEnumerable<JobPostDTO>> GetAllJobAsync()
        {
            var jobs = await repository.GetAllJobAsync();
            return mapper.Map<IEnumerable<JobPostDTO>>(jobs);
        }

        public async Task<bool> DeleteJobAsync(Guid id)
        {
            return await repository.DeleteJobsAsync(id);
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

            var activeCompanyUser = provider.CompanyUsers.FirstOrDefault(cu => cu.Status == (int)CompanyUserStatus.Active);
            if (activeCompanyUser == null)
            {
                throw new InvalidOperationException("You must select or register a company before posting jobs.");
            }

            var activeCompany = activeCompanyUser.Company;

            Guid locationId = dto.LocationId.HasValue && dto.LocationId.Value != Guid.Empty
                ? dto.LocationId.Value
                : (activeCompany != null && activeCompany.LocationId != Guid.Empty ? activeCompany.LocationId : await repository.GetDefaultLocationIdAsync());

            Guid categoryId = dto.CategoryId.HasValue && dto.CategoryId.Value != Guid.Empty
                ? dto.CategoryId.Value
                : await repository.GetDefaultCategoryIdAsync(activeCompany?.IndustryId);

            Guid jobTypeId = dto.JobTypeId.HasValue && dto.JobTypeId.Value != Guid.Empty
                ? dto.JobTypeId.Value
                : await repository.GetDefaultJobTypeIdAsync();

            var job = mapper.Map<Models.JobPost>(dto);
            job.JobProviderId = provider.JobProviderId;
            job.CompanyId = activeCompany.CompanyId;
            job.LocationId = locationId;
            job.JobcategoryId = categoryId;
            job.JobTypeId = jobTypeId;
            job.CreatedDate = DateTime.UtcNow;
            job.UpdatedDate = DateTime.UtcNow;
            job.Status = (JobStatus)JobPostStatus.Accepted;

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
            var originalLocationId = job.LocationId;
            var originalCategoryId = job.JobcategoryId;
            var originalJobTypeId = job.JobTypeId;

            mapper.Map(dto, job);

            job.LocationId = (dto.LocationId.HasValue && dto.LocationId.Value != Guid.Empty) ? dto.LocationId.Value : originalLocationId;
            job.JobcategoryId = (dto.CategoryId.HasValue && dto.CategoryId.Value != Guid.Empty) ? dto.CategoryId.Value : originalCategoryId;
            job.JobTypeId = (dto.JobTypeId.HasValue && dto.JobTypeId.Value != Guid.Empty) ? dto.JobTypeId.Value : originalJobTypeId;

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

            job.Status = (JobStatus)status;
            job.UpdatedDate = DateTime.UtcNow;
            await repository.UpdateJobPostAsync(job);
            return true;
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
