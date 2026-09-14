using Domain.Services.Experiences.DTO;
using Domain.Services.Industrys.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Industrys.Interface
{
    public interface IIndustryServices
    {
        Task<IndustryDTO> AddIndustryAsync(IndustryDTO industryDTO);
        Task<IEnumerable<IndustryDTO>> GetAllIndustryAsync();
        Task<IndustryDTO> UpdateIndustryAsync(Guid id, IndustryDTO industryDTO);
        Task<bool> deleteIndustryAsync(Guid id);

    }
}
