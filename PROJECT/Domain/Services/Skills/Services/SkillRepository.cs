using Domain.Data;
using Domain.Models;
using Domain.Services.Skills.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Skills.Services
{
    public class SkillRepository: ISkillRepository
    {
        private readonly AppDbContext appDbContext;
        public SkillRepository(AppDbContext _appDbContext)
        {
           appDbContext = _appDbContext;
        }

        public async Task<Skill> AddSkillAsync(Skill skill)
        {
            var existingSkill = await appDbContext.Skills
            .FirstOrDefaultAsync(x =>
             x.SkillName.ToLower() == skill.SkillName.ToLower());

            if (existingSkill != null)
            {
                return null;
            }
 
            await appDbContext.Skills.AddAsync(skill);
            await appDbContext.SaveChangesAsync();
            return skill;
        }

        public async Task<IEnumerable<Skill>> GetAllSkillsAsync()
        {
            var allskills=await appDbContext.Skills.ToListAsync();

            return allskills;
        }

        public async Task<Skill>UpdateSkillAsync(Guid id ,Skill skill)
        {
            var Existingskill=await appDbContext.Skills.FirstOrDefaultAsync(x=>x.SkillId==id);

            if (Existingskill == null)
            {
                return null;
            }
            Existingskill.SkillName = skill.SkillName;
            Existingskill.Description= skill.Description;

            await appDbContext.SaveChangesAsync();
            return Existingskill;

        }
        public async Task<bool>DeleteSkillAsync(Guid id)
        {
            var skill=await appDbContext.Skills.FirstOrDefaultAsync(x=>x.SkillId == id);

            if (skill != null)
            { 
                appDbContext.Remove(skill);
                await appDbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
                
        
        }
    }
}
