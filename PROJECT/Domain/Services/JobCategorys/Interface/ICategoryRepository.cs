using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.JobCategorys.Interface
{
    public interface ICategoryRepository
    {
        Task<JobCategory> AddCategoryAsync(JobCategory jobCategory);
        Task<IEnumerable<JobCategory>> GetAllCategoryAsync();
        Task<JobCategory> UpdateCategoryAsync(Guid id, JobCategory jobCategory);
        Task<bool> deleteCategoryAsync(Guid id);

    }
}
