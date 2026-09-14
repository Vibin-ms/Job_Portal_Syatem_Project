using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Skills.Interface
{
    public interface ISkillRepository
    {
        Task<Skill> AddSkillAsync(Skill skill);
        Task<IEnumerable<Skill>> GetAllSkillsAsync();
        Task<Skill>UpdateSkillAsync(Guid id,Skill skill);
        Task<bool>DeleteSkillAsync(Guid id);
    }
}
