using Domain.Data;
using Domain.Enum;
using Domain.Models;
using Domain.Services.AcceptORRejectCompany.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.AcceptORRejectCompany.Services
{
    public class AcceptORRejectRepository:IAcceptORRejectRepository
    {
        private readonly AppDbContext appDbContext;
        public AcceptORRejectRepository(AppDbContext _appDbContext)
        {
            appDbContext = _appDbContext;
        }
        public async Task<Company?> AcceptCompanyAsync(Guid companyId)
        {
            var company=await appDbContext.Companies.FirstOrDefaultAsync(x=>x.CompanyId== companyId);
            if(company==null)
            {
                return null;
            }
            company.Status=CompanyStatus.Accepted;
            await appDbContext.SaveChangesAsync();
            return company;
        }
        public async Task<Company?>RejectCompanyAsync(Guid companyId)
        {
            var company=await appDbContext.Companies.FirstOrDefaultAsync(x=>x.CompanyId== companyId);
            if( company==null)
            {
                return null;
            }
            company.Status =CompanyStatus.Rejected;
            await appDbContext.SaveChangesAsync();
            return company;
        }
    }
}
