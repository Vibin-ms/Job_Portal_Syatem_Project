using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.JobProviderProfile.Interface
{
    public interface IJobProviderRepository
    {
        Task<IEnumerable<Domain.Models.JobProvider>> GetAllProviderAsync();
        Task<bool> DeleteProviderAsync(Guid id);


        Task<Models.JobProvider?> GetBySystemUserIdAsync(Guid systemUserId);
        Task<Models.JobProvider?> GetByIdAsync(Guid jobProviderid);
        Task<Models.JobProvider> CreateJobProviderAsync(Models.JobProvider provider);
        Task UpdateJobProviderAsync(Models.JobProvider provider);
        Task<IEnumerable<Company>> GetAllCompaniesAsync();
        Task<Company?> GetCompanyByIdAsync(Guid companyId);
        Task<Company> CreateCompanyAsync(Company company);
        Task UpdateCompanyAsync(Company company);
        Task<CompanyUser?> CreateCompanyUserAsync(CompanyUser companyUser);
        Task UpdateCompanyUserAsync(CompanyUser companyUser);
        Task<CompanyUser?> GetCompanyUserAsync(Guid jobProviderId, Guid companyId);
        Task<CompanyUser?> GetActiveCompanyUserByJobProviderIdAsync(Guid jobProviderId);
        Task<IEnumerable<CompanyUser>> GetCompanyUsersByCompanyIdAsync(Guid companyId);
        Task DeactivateAllCompanyUsersForProviderAsync(Guid jobProviderId);

    }
}
