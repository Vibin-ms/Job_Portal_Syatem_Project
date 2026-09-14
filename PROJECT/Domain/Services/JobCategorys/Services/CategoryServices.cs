using AutoMapper;
using Domain.Models;
using Domain.Services.Industrys.DTO;
using Domain.Services.Industrys.Interface;
using Domain.Services.Industrys.services;
using Domain.Services.JobCategorys.DTO;
using Domain.Services.JobCategorys.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.JobCategorys.Services
{
    public class CategoryServices:ICategoryServices
    {
        private readonly IMapper mapper;
        private readonly ICategoryRepository repository;
        public CategoryServices(IMapper _mapper, ICategoryRepository _repository)
        {
            mapper = _mapper;
            repository = _repository;
        }
        public async Task<CategoryDTO> AddCategoryAsync(CategoryDTO categoryDTO)
        {
            var cat = mapper.Map<JobCategory>(categoryDTO);
            var catAdded = await repository.AddCategoryAsync(cat);
            if (catAdded == null)
            {
                return null;
            }
            return mapper.Map<CategoryDTO>(catAdded);
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoryAsync()
        {
            var response = await repository.GetAllCategoryAsync();
            return mapper.Map<IEnumerable<CategoryDTO>>(response);
        }

        public async Task<CategoryDTO> UpdateCategoryAsync(Guid id, CategoryDTO categoryDTO)
        {
            var cat = mapper.Map<JobCategory>(categoryDTO);
            var catUpdated = await repository.UpdateCategoryAsync(id, cat);
            return mapper.Map<CategoryDTO>(catUpdated);
        }
        public async Task<bool> deleteCategoryAsync(Guid id)
        {
            var cat = await repository.deleteCategoryAsync(id);
            return cat;
        }


    }
}
