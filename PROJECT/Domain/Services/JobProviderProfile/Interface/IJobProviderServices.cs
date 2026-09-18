using Domain.Services.JobProviderProfile.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.JobProviderProfile.Interface
{
    public interface IJobProviderServices
    {
        Task<IEnumerable<ProviderResponseDTO>> GetAllProviderAsync();
        Task<bool> DeleteProviderAsync(Guid id);
    }
}
