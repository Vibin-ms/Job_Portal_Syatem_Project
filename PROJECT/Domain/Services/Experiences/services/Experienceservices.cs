using AutoMapper;
using Domain.Models;
using Domain.Services.Experiences.DTO;
using Domain.Services.Experiences.Interface;
using Domain.Services.Qualifications.DTO;
using Domain.Services.Qualifications.Interface;
using Domain.Services.Qualifications.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Experiences.services
{
    public class Experienceservices:IExperienceServices
    {
        private readonly IExperienceRepository exprepository;
        private readonly IMapper mapper;
        public Experienceservices(IExperienceRepository _exprepository, IMapper _mapper)
        {
            exprepository = _exprepository;
            mapper = _mapper;
        }
        public async Task<ExperienceDTO>AddExperienceAsync(ExperienceDTO experienceDTO)
        {
            var exp = mapper.Map<Experience>(experienceDTO);
            var ExpAdded = await exprepository.AddExperienceAsync(exp);
            if (ExpAdded == null)
            {
                return null;
            }
            return mapper.Map<ExperienceDTO>(ExpAdded);
        }

        public async Task<IEnumerable<ExperienceDTO>> GetAllExperienceAsync()
        {
            var response = await exprepository.GetAllExperienceAsync();
            return mapper.Map<IEnumerable<ExperienceDTO>>(response);
        }

        public async Task<ExperienceDTO> UpdateExperienceAsync(Guid id, ExperienceDTO experienceDTO)
        {
            var exp = mapper.Map<Experience>(experienceDTO);
            var ExpUpdated = await exprepository.UpdateExperienceAsync(id, exp);
            return mapper.Map<ExperienceDTO>(ExpUpdated);
        }
        public async Task<bool> deleteExperienceAsync(Guid id)
        {
            var exp = await exprepository.deleteExperienceAsync(id);
            return exp;
        }

    }
}
