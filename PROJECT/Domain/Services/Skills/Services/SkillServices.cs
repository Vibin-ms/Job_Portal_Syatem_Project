using AutoMapper;
using Domain.Models;
using Domain.Services.Skills.DTO;
using Domain.Services.Skills.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Skills.Services
{
    public class SkillServices:ISkillServices
    {
        private readonly ISkillRepository skillRepository;
        private readonly IMapper mapper;
        public SkillServices(ISkillRepository _skillRepository, IMapper _mapper)
        {
            skillRepository = _skillRepository;
            mapper = _mapper;
        }

        public async Task<skillDTO>AddSkillAsync(skillDTO skillDTO)
        {
            var newskill = mapper.Map<Skill>(skillDTO);
            var skilladded= await skillRepository.AddSkillAsync(newskill);
            if (skilladded == null)
            {
                return null;
            }
            return mapper.Map<skillDTO>(skilladded);
        }

        public async Task<IEnumerable<skillDTO>>GetSkillAsync()
        {
            var allSkills=await skillRepository.GetAllSkillsAsync();
            return mapper.Map<IEnumerable<skillDTO>>(allSkills);
        }

        public async Task<skillDTO>UpdateSkillAsync(Guid id ,skillDTO skillDTO)
        {
            var update=mapper.Map<Skill>(skillDTO);
            var udatedskill=await skillRepository.UpdateSkillAsync(id ,update);
            return mapper.Map<skillDTO> (udatedskill);
        }
        public async Task<bool> DeleteSkillAsync(Guid id)
        {
            var deleted=await skillRepository.DeleteSkillAsync(id);
            return deleted;
        }
    }
}
