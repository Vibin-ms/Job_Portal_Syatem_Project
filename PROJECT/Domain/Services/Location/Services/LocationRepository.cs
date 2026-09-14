using Domain.Data;
using Domain.Models;
using Domain.Services.Location.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Location.Services
{
    public class LocationRepository:ILocationRepository
    {
        private readonly AppDbContext appDbContext;
        public LocationRepository(AppDbContext _appDbContext)
        {
            appDbContext = _appDbContext;
        }

        public async Task<Domain.Models.Location>AddLocationAsync(Domain.Models.Location location)
        {
            var existingLocation = await appDbContext.Locations
           .FirstOrDefaultAsync(x =>
               x.Name.ToLower() == location.Name.ToLower());

            if (existingLocation != null)
            {
                return null;
            }
            await appDbContext.Locations.AddAsync(location);
            await appDbContext.SaveChangesAsync();
            return location;
        }
        public async Task<IEnumerable<Domain.Models.Location>>GetAllLocationAsync()
        {
            var AllLoc = await appDbContext.Locations.ToListAsync();
            return AllLoc;
        }
        public async Task<Domain.Models.Location>UpdateLocationAsync(Guid id,Domain.Models.Location location)
        {
            var exixting_Loc=await appDbContext.Locations.FindAsync(id);

            if (exixting_Loc == null)
                return null;
            exixting_Loc.Name = location.Name;
            exixting_Loc.Description = location.Description;
            await appDbContext.SaveChangesAsync();
            return exixting_Loc;

        }
        public async Task<bool> DeleteLocationAsync(Guid id)
        {
            var loc = await appDbContext.Locations.FindAsync(id);
            if(loc == null)
                return false;
            appDbContext.Remove(loc);
            await appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
