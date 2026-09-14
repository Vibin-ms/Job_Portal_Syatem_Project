using Domain.Services.Location.DTO;
using Domain.Services.Location.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Location.Interface
{
    public interface ILocationServices
    {
        Task<LocationDTO>AddLocationAsync(LocationDTO locationDTO);
        Task<IEnumerable<LocationDTO>> GetAllLocationAsync();
        Task<LocationDTO> UpdateLocationAsync(Guid id,LocationDTO locationDTO);
        Task<bool> DeleteLocationAsync(Guid id);

    }
}
