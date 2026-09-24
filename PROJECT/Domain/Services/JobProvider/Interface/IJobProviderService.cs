using Domain.Services.JobProvider.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.JobProvider.Interface
{
    public interface IJobProviderService
    {
        Task<JobProviderDto?> GetProviderBySystemUserIdAsync(Guid systemUserId);
        Task<JobProviderDto> CreateProviderProfileAsync(Guid systemUserId, CreateJobProviderDto dto);
        Task<JobProviderDto?> UpdateProviderAsync(Guid systemUserId, UpdateJobProviderDto dto);
        Task<CompanyDto?> GetCompanyByIdAsync(Guid companyId);
        Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync();
        Task<CompanyDto> CreateCompanyAsync(CreateCompanyDto dto);
        Task<CompanyDto> CreateCompanyForProviderAsync(Guid systemUserId, CreateCompanyDto dto);
        Task<CompanyDto?> UpdateCompanyAsync(Guid companyId, UpdateCompanyDto dto);
        Task<CompanyUserDto> SelectCompanyAsync(Guid systemUserId, SelectCompanyDto dto);
        Task<CompanyUserDto?> GetCurrentCompanyUserAsync(Guid systemUserId);
        Task<IEnumerable<CompanyUserDto>> GetCompanyUsersAsync(Guid companyId);




    }
}
