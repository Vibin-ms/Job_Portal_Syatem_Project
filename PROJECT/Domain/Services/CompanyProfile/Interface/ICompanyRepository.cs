using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.CompanyProfile.Interface
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>>GetAllCompanyAsync();
        Task<bool> DeleteCompanyAsync(Guid id);
        Task<IEnumerable<Company>> GetAcceptedCompanyAsync();
        Task<IEnumerable<Company>> GetRejectedCompanyAsync();
        Task<IEnumerable<Company>> GetPendingCompanyAsync();
        Task<IEnumerable<Company>> GetCompaniesByProviderAsync(Guid providerId);
    }
}
