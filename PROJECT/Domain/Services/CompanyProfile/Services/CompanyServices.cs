using AutoMapper;
using Domain.Services.CompanyProfile.DTO;
using Domain.Services.CompanyProfile.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.CompanyProfile.Services
{
    public class CompanyServices:ICompanySerices
    {
        private readonly ICompanyRepository companyRepository;
        private readonly IMapper mapper;
        public CompanyServices(ICompanyRepository _companyRepository, IMapper _mapper)
        {
            companyRepository = _companyRepository;
            mapper = _mapper;
        }
        public async Task<IEnumerable<CompanyProfileDTO>>GetAllCompanyAsync()
        {
            var company= await companyRepository.GetAllCompanyAsync();
            return mapper.Map<IEnumerable<CompanyProfileDTO>>(company);
        }
        public async Task<bool>DeleteCompanyAsync(Guid id)
        {
            var company=await companyRepository.DeleteCompanyAsync(id);
            return company;
        }
        public async Task<IEnumerable<CompanyProfileDTO>> GetAcceptedCompanyAsync()
        {
            var company = await companyRepository.GetAcceptedCompanyAsync();
            return mapper.Map<IEnumerable<CompanyProfileDTO>>(company);
        }
        public async Task<IEnumerable<CompanyProfileDTO>> GetRejectedCompanyAsync()
        {
            var company = await companyRepository.GetRejectedCompanyAsync();
            return mapper.Map<IEnumerable<CompanyProfileDTO>>(company);
        }
        public async Task<IEnumerable<CompanyProfileDTO>> GetPendingCompanyAsync()
        {
            var company = await companyRepository.GetPendingCompanyAsync();
            return mapper.Map<IEnumerable<CompanyProfileDTO>>(company);
        }
        public async Task<IEnumerable<CompanyProfileDTO>> GetCompaniesByProviderAsync(Guid providerId)
        {
            var company = await companyRepository.GetCompaniesByProviderAsync(providerId);
            return mapper.Map<IEnumerable<CompanyProfileDTO>>(company);
        }

    }
}
