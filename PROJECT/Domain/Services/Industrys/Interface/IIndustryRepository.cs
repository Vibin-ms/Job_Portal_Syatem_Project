using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Industrys.Interface
{
    public interface IIndustryRepository
    {
        Task<Industry> AddIndustryAsync(Industry industry);
        Task<IEnumerable<Industry>> GetAllIndustryAsync();
        Task<Industry> UpdateIndustryAsync(Guid id, Industry industry);
        Task<bool> deleteIndustryAsync(Guid id);

    }
}
