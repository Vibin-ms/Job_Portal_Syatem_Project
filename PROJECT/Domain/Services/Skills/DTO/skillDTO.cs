using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Skills.DTO
{
    public class skillDTO
    {
        public Guid SkillId { get; set; }
        public string SkillName { get; set; } = null!;

        public string? Description { get; set; }

    }
}
