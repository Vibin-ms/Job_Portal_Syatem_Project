using AutoMapper;
using Domain.Services.JobProviderProfile.DTO;
using Domain.Services.JobProviderProfile.Interface;
using Org.BouncyCastle.Crypto.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.JobProviderProfile.Services
{
    public class JobProviderServices:IJobProviderServices
    {
        private readonly IJobProviderRepository jobProviderRepository;
        private readonly IMapper mapper;
        public JobProviderServices(IJobProviderRepository _jobProviderRepository, IMapper _mapper)
        {
            mapper = _mapper;
            jobProviderRepository = _jobProviderRepository;

        }
        public async Task<IEnumerable<ProviderResponseDTO>>GetAllProviderAsync()
        {
            var provider=await jobProviderRepository.GetAllProviderAsync();
            return mapper.Map<IEnumerable<ProviderResponseDTO>>(provider);
        }
        public async Task<bool>DeleteProviderAsync(Guid id)
        {
            var provider=await jobProviderRepository.DeleteProviderAsync(id);
            return provider;
        }
    }
}
