using Domain.Data;
using Domain.Models;
using Domain.Services.Experiences.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Experiences.services
{
    public class ExperienceRepository:IExperienceRepository
    {
        private readonly AppDbContext appDbContext;
        public ExperienceRepository(AppDbContext _appDbContext)
        {
            appDbContext = _appDbContext;
        }
        public async Task<Experience> AddExperienceAsync(Experience experience)
        {
            var existingExp = await appDbContext.Experiences
            .FirstOrDefaultAsync(x =>
                x.Name.ToLower() == experience.Name.ToLower());

            if (existingExp != null)
            {
                return null;
            }

            await appDbContext.Experiences.AddAsync(experience);
            await appDbContext.SaveChangesAsync();
            return experience;
        }
        public async Task<IEnumerable<Experience>> GetAllExperienceAsync()
        {
            var Exp = await appDbContext.Experiences.ToListAsync();
            return Exp;
        }
        public async Task<Experience> UpdateExperienceAsync(Guid id, Experience experience)
        {
            var existing = await appDbContext.Experiences.FirstOrDefaultAsync(x => x.ExperienceId == id);

            if (existing != null)
            {
                existing.Name = experience.Name;
                existing.Description = experience.Description;
                await appDbContext.SaveChangesAsync();
                return existing;
            }
            return null;
        }
        public async Task<bool> deleteExperienceAsync(Guid id)
        {
            var exp = await appDbContext.Experiences.FirstOrDefaultAsync(x => x.ExperienceId == id);
            if (exp == null)
            {
                return false;
            }
            appDbContext.Experiences.Remove(exp);
            await appDbContext.SaveChangesAsync();
            return true;
        }


    }
}
