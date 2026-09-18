using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.AcceptORRejectCompany.Interface
{
    public interface IAccpetORRejectServices
    {
        Task<bool> AcceptCompanyAsync(Guid companyId);
        Task<bool> RejectCompanyAsync(Guid companyId);
    }
}
