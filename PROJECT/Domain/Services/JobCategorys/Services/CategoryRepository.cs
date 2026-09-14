using Domain.Data;
using Domain.Models;
using Domain.Services.JobCategorys.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.JobCategorys.Services
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly AppDbContext appDbContext;
        public CategoryRepository(AppDbContext _appDbContext)
        {
            appDbContext = _appDbContext;
        }
        public async Task<JobCategory> AddCategoryAsync(JobCategory jobCategory)
        {
            var existing = await appDbContext.JobCategories.FirstOrDefaultAsync(x =>
                x.Name.ToLower() == jobCategory.Name.ToLower());

            if (existing != null)
            {
                return null;
            }

            await appDbContext.JobCategories.AddAsync(jobCategory);
            await appDbContext.SaveChangesAsync();
            return jobCategory;
        }
        public async Task<IEnumerable<JobCategory>> GetAllCategoryAsync()
        {
            var Cate = await appDbContext.JobCategories.ToListAsync();
            return Cate;
        }
        public async Task<JobCategory> UpdateCategoryAsync(Guid id, JobCategory jobcategory)
        {
            var existing = await appDbContext.JobCategories.FirstOrDefaultAsync(x => x.JobCategoryId == id);

            if (existing != null)
            {
                existing.Name = jobcategory.Name;
                existing.Description = jobcategory.Description;
                await appDbContext.SaveChangesAsync();
                return existing;
            }
            return null;
        }
        public async Task<bool> deleteCategoryAsync(Guid id)
        {
            var cat = await appDbContext.JobCategories.FirstOrDefaultAsync(x => x.JobCategoryId == id);
            if (cat == null)
            {
                return false;
            }
            appDbContext.JobCategories.Remove(cat);
            await appDbContext.SaveChangesAsync();
            return true;
        }


    }
}
