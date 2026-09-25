using AutoMapper;
using Domain.Services.JobApplications.DTO;
using Domain.Services.JobApplications.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.JobApplications.Services
{
    public class ApplicationServices:IApplicationServices
    {
        private readonly IApplicationRepository repository;
        private readonly IMapper mapper;
        public ApplicationServices(IApplicationRepository _applicationRepository, IMapper _mapper)
        {
            repository = _applicationRepository;
            mapper = _mapper;
        }

        public async Task<IEnumerable<JobApplicationDto>> GetApplicationsForJobAsync(Guid systemUserId, Guid jobPostId)
        {
            var provider = await repository.GetProviderBySystemUserIdAsync(systemUserId);
            if (provider == null) return Enumerable.Empty<JobApplicationDto>();

            var apps = await repository.GetApplicationsByJobPostIdAsync(jobPostId);
            var providerApps = apps.Where(a => a.JobPost?.JobProviderId == provider.JobProviderId);
            return mapper.Map<IEnumerable<JobApplicationDto>>(providerApps);
        }

        public async Task<IEnumerable<JobApplicationDto>> GetApplicationsForProviderAsync(Guid systemUserId)
        {
            var provider = await repository.GetProviderBySystemUserIdAsync(systemUserId);
            if (provider == null) return Enumerable.Empty<JobApplicationDto>();

            var apps = await repository.GetApplicationsByProviderIdAsync(provider.JobProviderId);
            return mapper.Map<IEnumerable<JobApplicationDto>>(apps);
        }

        public async Task<JobApplicationDto?> GetApplicationByIdAsync(Guid systemUserId, Guid applicationId)
        {
            var provider = await repository.GetProviderBySystemUserIdAsync(systemUserId);
            if (provider == null) return null;

            var app = await repository.GetByIdAsync(applicationId);
            if (app == null || app.JobPost?.JobProviderId != provider.JobProviderId) return null;

            return mapper.Map<JobApplicationDto>(app);
        }

        public async Task<JobApplicationDto?> UpdateApplicationStatusAsync(Guid systemUserId, Guid applicationId, UpdateApplicationStatusDto dto)
        {
            var provider = await repository.GetProviderBySystemUserIdAsync(systemUserId);
            if (provider == null) return null;

            var app = await repository.GetByIdAsync(applicationId);
            if (app == null || app.JobPost?.JobProviderId != provider.JobProviderId) return null;

            app.ApplicationStatus = (Domain.Enum.ApplicationStatus)dto.Status;
            await repository.UpdateApplicationAsync(app);

            return mapper.Map<JobApplicationDto>(app);
        }

        public async Task<IEnumerable<ApplicationDTO>> GetApplicationsByJobPostAsync(Guid jobId)
        {
            var applications = await repository.GetApplicationsByJobP0stAsync(jobId);
            return mapper.Map<IEnumerable<ApplicationDTO>>(applications);
        }
    }
}
