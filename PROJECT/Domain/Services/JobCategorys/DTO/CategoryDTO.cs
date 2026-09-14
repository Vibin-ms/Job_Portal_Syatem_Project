using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.JobCategorys.DTO
{
    public class CategoryDTO
    {
        public Guid JobCategoryId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

    }
}
