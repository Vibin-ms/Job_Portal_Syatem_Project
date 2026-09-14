using AutoMapper;
using Domain.Services.Location.DTO;
using Domain.Services.Location.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Location.Services
{
    public class LocationServices:ILocationServices
    {
        private readonly ILocationRepository locationRepository;
        private readonly IMapper mapper;
        public LocationServices(ILocationRepository _locationRepository, IMapper _mapper)
        {
            locationRepository = _locationRepository;
            mapper = _mapper;
        }

        public async Task<LocationDTO>AddLocationAsync(LocationDTO locationDTO)
        {
            var loc = mapper.Map<Domain.Models.Location>(locationDTO);
            var NewLoc=await locationRepository.AddLocationAsync(loc);
            if (NewLoc == null)
            {
                return null;
            }
            return mapper.Map<LocationDTO>(NewLoc);
        }
        
        public async Task<IEnumerable<LocationDTO>>GetAllLocationAsync()
        {
            var loc=await locationRepository.GetAllLocationAsync();
            return mapper.Map<IEnumerable<LocationDTO>>(loc);
        }
        public async Task<LocationDTO> UpdateLocationAsync(Guid id,LocationDTO locationDTO)
        {
            var loc=mapper.Map<Domain.Models.Location>(locationDTO);
            var updated=await locationRepository.UpdateLocationAsync(id,loc);
            return mapper.Map<LocationDTO> (updated);

        }
        public async Task<bool> DeleteLocationAsync(Guid id)
        {
            var loc=await locationRepository.DeleteLocationAsync(id);
            return loc;
        }
    }
}
