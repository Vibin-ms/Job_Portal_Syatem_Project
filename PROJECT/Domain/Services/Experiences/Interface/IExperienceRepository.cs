using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Experiences.Interface
{
    public interface IExperienceRepository
    {
        Task<Experience> AddExperienceAsync(Experience experience);
        Task<IEnumerable<Experience>> GetAllExperienceAsync();
        Task<Experience> UpdateExperienceAsync(Guid id, Experience experience);
        Task<bool> deleteExperienceAsync(Guid id);

    }
}
