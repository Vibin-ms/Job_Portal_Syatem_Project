using Domain.Services.JobCategorys.DTO;
using Domain.Services.JobTypes.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.JobTypes.Interfaces
{
    public interface IJobTypeServices
    {
        Task<TypeDTO> AddJobTypeAsync(TypeDTO typeDTO);
        Task<IEnumerable<TypeDTO>> GetAllJobTypeAsync();
        Task<TypeDTO> UpdateJobTypeAsync(Guid id, TypeDTO typeDTO);
        Task<bool> deleteJobTypeAsync(Guid id);

    }
}
