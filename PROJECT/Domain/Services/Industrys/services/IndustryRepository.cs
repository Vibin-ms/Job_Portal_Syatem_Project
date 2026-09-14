using Domain.Data;
using Domain.Models;
using Domain.Services.Industrys.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Industrys.services
{
    public class IndustryRepository:IIndustryRepository
    {
        private readonly AppDbContext appDbContext;
        public IndustryRepository(AppDbContext _appDbContext)
        {
            appDbContext = _appDbContext;
        }
        public async Task<Industry> AddIndustryAsync(Industry industry)
        {
            var existingInd= await appDbContext.Industries.FirstOrDefaultAsync(x =>
                x.Name.ToLower() == industry.Name.ToLower());

            if (existingInd != null)
            {
                return null;
            }

            await appDbContext.Industries.AddAsync(industry);
            await appDbContext.SaveChangesAsync();
            return industry;
        }
        public async Task<IEnumerable<Industry>> GetAllIndustryAsync()
        {
            var Ind = await appDbContext.Industries.ToListAsync();
            return Ind;
        }
        public async Task<Industry> UpdateIndustryAsync(Guid id, Industry industry)
        {
            var existing = await appDbContext.Industries.FirstOrDefaultAsync(x => x.IndustryId == id);

            if (existing != null)
            {
                existing.Name = industry.Name;
                existing.Description = industry.Description;
                await appDbContext.SaveChangesAsync();
                return existing;
            }
            return null;
        }
        public async Task<bool> deleteIndustryAsync(Guid id)
        {
            var Ind = await appDbContext.Industries.FirstOrDefaultAsync(x => x.IndustryId == id);
            if (Ind == null)
            {
                return false;
            }
            appDbContext.Industries.Remove(Ind);
            await appDbContext.SaveChangesAsync();
            return true;
        }


    }
}
