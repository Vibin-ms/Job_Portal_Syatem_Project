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
        private readonly IApplicationRepository applicationRepository;
        private readonly IMapper mapper;
        public ApplicationServices(IApplicationRepository _applicationRepository, IMapper _mapper)
        {
            applicationRepository = _applicationRepository;
            mapper = _mapper;
        }
        public async Task<IEnumerable<ApplicationDTO>> GetApplicationsByJobPostAsync(Guid jobId)
        {
            var applications = await applicationRepository.GetApplicationsByJobPostAsync(jobId);
            return mapper.Map<IEnumerable<ApplicationDTO>>(applications);
        }
    }
}
