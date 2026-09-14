using Domain.Services.Experiences.DTO;
using Domain.Services.Qualifications.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Experiences.Interface
{
    public interface IExperienceServices
    {
        Task<ExperienceDTO> AddExperienceAsync(ExperienceDTO experienceDTO);
        Task<IEnumerable<ExperienceDTO>> GetAllExperienceAsync();
        Task<ExperienceDTO> UpdateExperienceAsync(Guid id, ExperienceDTO experienceDTO);
        Task<bool> deleteExperienceAsync(Guid id);

    }
}
