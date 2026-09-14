using Domain.Services.Industrys.DTO;
using Domain.Services.JobCategorys.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.JobCategorys.Interface
{
    public interface ICategoryServices
    {
        Task<CategoryDTO> AddCategoryAsync(CategoryDTO categoryDTO);
        Task<IEnumerable<CategoryDTO>> GetAllCategoryAsync();
        Task<CategoryDTO> UpdateCategoryAsync(Guid id, CategoryDTO categoryDTO);
        Task<bool> deleteCategoryAsync(Guid id);

    }
}
