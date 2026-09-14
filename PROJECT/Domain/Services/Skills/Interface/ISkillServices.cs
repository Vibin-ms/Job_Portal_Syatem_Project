using Domain.Models;
using Domain.Services.Skills.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Skills.Interface
{
    public interface ISkillServices
    {
        Task<skillDTO>AddSkillAsync(skillDTO skillDTO);
        Task<IEnumerable<skillDTO>> GetSkillAsync();
        Task<skillDTO>UpdateSkillAsync(Guid id,skillDTO skillDTO);
        Task<bool>DeleteSkillAsync(Guid id);
    }
}
