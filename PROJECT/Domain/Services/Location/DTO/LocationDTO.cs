using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Location.DTO
{
    public class LocationDTO
    {
        public Guid LocationId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}
