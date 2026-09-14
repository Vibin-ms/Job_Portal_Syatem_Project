using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Qualifications.DTO
{
    public class QualificationDTO
    {
        public Guid QualificationId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }


    }
}
