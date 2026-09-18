using Domain.Services.CompanyProfile.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.CompanyProfile.Interface
{
    public interface ICompanySerices
    {
        Task<IEnumerable<CompanyProfileDTO>> GetAllCompanyAsync();
        Task<bool> DeleteCompanyAsync(Guid id);
        Task<IEnumerable<CompanyProfileDTO>> GetAcceptedCompanyAsync();
        Task<IEnumerable<CompanyProfileDTO>> GetRejectedCompanyAsync();
        Task<IEnumerable<CompanyProfileDTO>> GetPendingCompanyAsync();
        Task<IEnumerable<CompanyProfileDTO>> GetCompaniesByProviderAsync(Guid providerId);


    }
}
