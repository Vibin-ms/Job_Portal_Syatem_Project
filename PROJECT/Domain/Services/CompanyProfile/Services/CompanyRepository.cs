using Domain.Data;
using Domain.Enum;
using Domain.Models;
using Domain.Services.CompanyProfile.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.CompanyProfile.Services
{
    public class CompanyRepository:ICompanyRepository
    {
        private readonly AppDbContext appDbContext;
        public CompanyRepository(AppDbContext _appDbContext)
        {

            appDbContext = _appDbContext;
        }
       public async Task<IEnumerable<Company>>GetAllCompanyAsync()
        {
            var company=await appDbContext.Companies.Include(x=>x.Industry).Include(x=>x.Location).ToListAsync();
            return company;
        }
        public async Task<bool>DeleteCompanyAsync(Guid id)
        {
            var comapny=await appDbContext.Companies.FirstOrDefaultAsync(x=>x.CompanyId==id);
            if(comapny==null)
            {
                return false;
            }
            appDbContext.Companies.Remove(comapny);
            await appDbContext.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<Company>> GetAcceptedCompanyAsync()
        {
            var company = await appDbContext.Companies.Include(x => x.Industry).Include(x => x.Location)
                .Where(x=>x.Status==CompanyStatus.Accepted).ToListAsync();
            return company;
        }
        public async Task<IEnumerable<Company>> GetRejectedCompanyAsync()
        {
            var company = await appDbContext.Companies.Include(x => x.Industry).Include(x => x.Location)
                .Where(x => x.Status == CompanyStatus.Rejected).ToListAsync();
            return company;
        }
        public async Task<IEnumerable<Company>> GetPendingCompanyAsync()
        {
            var company = await appDbContext.Companies.Include(x => x.Industry).Include(x => x.Location)
                .Where(x => x.Status == CompanyStatus.Pending).ToListAsync();
            return company;
        }
        public async Task<IEnumerable<Company>> GetCompaniesByProviderAsync(Guid providerId)
        {
            var company = await appDbContext.Companies.Include(x => x.Industry).Include(x => x.Location)
                .Where(x => x.JobProviderId==providerId).ToListAsync();
            return company;
        }


    }
}
