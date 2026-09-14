using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Experiences.DTO
{
    public class ExperienceDTO
    {
        public Guid ExperienceId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }


    }
}
