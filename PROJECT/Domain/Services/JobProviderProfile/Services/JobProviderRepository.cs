using Domain.Data;
using Domain.Services.JobProviderProfile.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.JobProviderProfile.Services
{
    public class JobProviderRepository:IJobProviderRepository
    {
        private readonly AppDbContext appDbContext;
        public JobProviderRepository(AppDbContext _appDbContext)
        {
            appDbContext = _appDbContext;
        }
       public async Task<IEnumerable<Domain.Models.JobProvider>>GetAllProviderAsync()
        {
            var provider=await appDbContext.JobProviders.Include(x=>x.SystemUser).ToListAsync();
            return provider;
        }

        public async Task<bool>DeleteProviderAsync(Guid id)
        {
            var provider=await appDbContext.JobProviders.FindAsync(id);
            if(provider == null)
            {
                return false;
            }
            var systemUserId= provider.SystemUserId;
            
           var jobs=await appDbContext.JobPosts.Where(x=>x.JobProviderId==id).ToListAsync();
            appDbContext.JobPosts.RemoveRange(jobs);
            var company = await appDbContext.Companies.Where(x => x.JobProviderId == id).ToListAsync();
            appDbContext .Companies.RemoveRange(company);

            appDbContext.JobProviders.Remove(provider);

            var auathuser=await appDbContext.AuthUsers.FirstOrDefaultAsync(x=>x.SystemUserId==systemUserId);
            if(auathuser != null)
            {
                appDbContext.AuthUsers.Remove(auathuser);
            }
            
            var systemuser=await appDbContext.SystemUsers.FirstOrDefaultAsync(x=>x.Id==systemUserId);
            if(systemuser != null)
            {
                appDbContext.SystemUsers.Remove(systemuser);
            }
            await appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
