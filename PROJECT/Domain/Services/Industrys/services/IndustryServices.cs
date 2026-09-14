using AutoMapper;
using Domain.Models;
using Domain.Services.Experiences.DTO;
using Domain.Services.Industrys.DTO;
using Domain.Services.Industrys.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Industrys.services
{
    public class IndustryServices:IIndustryServices
    {
        private readonly IMapper mapper;
        private readonly IIndustryRepository industryRepository;
        public IndustryServices(IMapper _mapper, IIndustryRepository _industryRepository)
        {
            mapper = _mapper;
            industryRepository = _industryRepository;
        }
        public async Task<IndustryDTO> AddIndustryAsync(IndustryDTO industryDTO)
        {
            var Ind = mapper.Map<Industry>(industryDTO);
            var IndAdded = await industryRepository.AddIndustryAsync(Ind);
            if (IndAdded == null)
            {
                return null;
            }
            return mapper.Map<IndustryDTO>(IndAdded);
        }

        public async Task<IEnumerable<IndustryDTO>> GetAllIndustryAsync()
        {
            var response = await industryRepository.GetAllIndustryAsync();
            return mapper.Map<IEnumerable<IndustryDTO>>(response);
        }

        public async Task<IndustryDTO> UpdateIndustryAsync(Guid id, IndustryDTO industryDTO)
        {
            var Ind = mapper.Map<Industry>(industryDTO);
            var IndUpdated = await industryRepository.UpdateIndustryAsync(id, Ind);
            return mapper.Map<IndustryDTO>(IndUpdated);
        }
        public async Task<bool> deleteIndustryAsync(Guid id)
        {
            var Ind = await industryRepository.deleteIndustryAsync(id);
            return Ind;
        }

    }
}
