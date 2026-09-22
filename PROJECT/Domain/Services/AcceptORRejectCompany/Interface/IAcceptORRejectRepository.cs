using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.AcceptORRejectCompany.Interface
{
    public interface IAcceptORRejectRepository
    {
        Task<Company?> AcceptCompanyAsync(Guid companyId);
        Task<Company?> RejectCompanyAsync(Guid companyId);
    }
}
