using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Location.Interface
{
    public interface ILocationRepository
    {
        Task<Domain.Models.Location> AddLocationAsync(Domain.Models.Location location);
        Task<IEnumerable<Domain.Models.Location>> GetAllLocationAsync();
        Task<Domain.Models.Location>UpdateLocationAsync(Guid id, Domain.Models.Location location);
        Task<bool>DeleteLocationAsync(Guid id);

    }
}
