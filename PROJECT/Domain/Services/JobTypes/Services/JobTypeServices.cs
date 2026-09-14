using AutoMapper;
using Domain.Models;
using Domain.Services.JobCategorys.DTO;
using Domain.Services.JobTypes.DTO;
using Domain.Services.JobTypes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.JobTypes.Services
{
    public class JobTypeServices:IJobTypeServices
    {
        private readonly IMapper mapper;
        private readonly IJobTypeRepository repository;
        public JobTypeServices(IMapper _mapper, IJobTypeRepository _repository)
        {
            mapper = _mapper;
            repository  = _repository;
        }
        public async Task<TypeDTO> AddJobTypeAsync(TypeDTO typeDTO)
        {
            var type = mapper.Map<JobType>(typeDTO);
            var typeAdded = await repository.AddJobTypeAsync(type);
            if (typeAdded == null)
            {
                return null;
            }
            return mapper.Map<TypeDTO>(typeAdded);
        }

        public async Task<IEnumerable<TypeDTO>> GetAllJobTypeAsync()
        {
            var response = await repository.GetAllJobTypeAsync();
            return mapper.Map<IEnumerable<TypeDTO>>(response);
        }

        public async Task<TypeDTO> UpdateJobTypeAsync(Guid id, TypeDTO typeDTO)
        {
            var type = mapper.Map<JobType>(typeDTO);
            var typeUpdated = await repository.UpdateJobTypeAsync(id, type);

            return mapper.Map<TypeDTO>(typeUpdated);
        }
        public async Task<bool> deleteJobTypeAsync(Guid id)
        {
            var Type = await repository.deleteJobTypeAsync(id);
            return Type;
        }


    }
}
