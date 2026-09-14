using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.JobTypes.DTO
{
    public class TypeDTO
    {
        public Guid JobTypeId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

    }
}
