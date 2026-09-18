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

    }
}
