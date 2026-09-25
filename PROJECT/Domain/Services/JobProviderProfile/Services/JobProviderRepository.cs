using Domain.Data;
using Domain.Models;
using Domain.Services.JobProviderProfile.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using Domain.Enum;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.JobProviderProfile.Services
{
    public class JobProviderRepository:IJobProviderRepository
    {
        private readonly AppDbContext _context;
        public JobProviderRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }



        public async Task<IEnumerable<JobProvider>> GetAllProviderAsync()
        {
            return await _context.JobProviders
                .Include(x => x.SystemUser)
                .Include(x => x.CompanyUsers)
                    .ThenInclude(cu => cu.Company)
                        .ThenInclude(c => c.Industry)
                .Include(x => x.CompanyUsers)
                    .ThenInclude(cu => cu.Company)
                        .ThenInclude(c => c.Location)
                .ToListAsync();
        }

        public async Task<bool> DeleteProviderAsync(Guid id)
        {
            var provider = await _context.JobProviders.FindAsync(id);
            if (provider == null)
            {
                return false;
            }
            var systemUserId = provider.SystemUserId;

            var jobs = await _context.JobPosts.Where(x => x.JobProviderId == id).ToListAsync();
            _context.JobPosts.RemoveRange(jobs);

            _context.JobProviders.Remove(provider);

            var auathuser = await _context.AuthUsers.FirstOrDefaultAsync(x => x.SystemUserId == systemUserId);
            if (auathuser != null)
            {
                _context.AuthUsers.Remove(auathuser);
            }

            var systemuser = await _context.SystemUsers.FirstOrDefaultAsync(x => x.Id == systemUserId);
            if (systemuser != null)
            {
                _context.SystemUsers.Remove(systemuser);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<JobProvider?> GetBySystemUserIdAsync(Guid systemUserId)
        {
            return await _context.JobProviders
                .Include(u => u.SystemUser)
                .Include(u => u.CompanyUsers)
                    .ThenInclude(uv => uv.Company)
                        .ThenInclude(uc => uc.Industry)
                .Include(u => u.CompanyUsers)
                    .ThenInclude(uv => uv.Company)
                        .ThenInclude(uc => uc.Location)
                .FirstOrDefaultAsync(u => u.SystemUserId == systemUserId);
        }

        public async Task<JobProvider?> GetByIdAsync(Guid jobProviderid)
        {
            return await _context.JobProviders
                .Include(u => u.SystemUser)
                .Include(u => u.CompanyUsers)
                    .ThenInclude(uv => uv.Company)
                        .ThenInclude(uc => uc.Industry)
                .Include(u => u.CompanyUsers)
                    .ThenInclude(uv => uv.Company)
                        .ThenInclude(uc => uc.Location)
                .FirstOrDefaultAsync(u => u.JobProviderId == jobProviderid);
        }

        public async Task<Models.JobProvider> CreateJobProviderAsync(Models.JobProvider provider)
        {
            await _context.JobProviders.AddAsync(provider);
            await _context.SaveChangesAsync();
            return provider;
        }

        public async Task UpdateJobProviderAsync(Models.JobProvider provider)
        {
            _context.JobProviders.Update(provider);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
        {
            return await _context.Companies
                .Include(c => c.Industry)
                .Include(c => c.Location)
                .Include(c => c.JobPosts)
                .Include(c => c.CompanyUsers)
                .ToListAsync();
        }

        public async Task<Company?> GetCompanyByIdAsync(Guid companyId)
        {
            return await _context.Companies
                .Include(c => c.Industry)
                .Include(c => c.Location)
                .Include(c => c.JobPosts)
                .Include(c => c.CompanyUsers)
                .FirstOrDefaultAsync(u => u.CompanyId == companyId);
        }

        public async Task<Company> CreateCompanyAsync(Company company)
        {
            if (company.IndustryId == Guid.Empty)
            {
                var industry = await _context.Industries.FirstOrDefaultAsync();
                if (industry == null)
                {
                    industry = new Industry { IndustryId = Guid.NewGuid(), Name = "Information Technology", Description = "Default Industry" };
                    await _context.Industries.AddAsync(industry);
                    await _context.SaveChangesAsync();
                }
                company.IndustryId = industry.IndustryId;
            }

            if (company.LocationId == Guid.Empty)
            {
                var location = await _context.Locations.FirstOrDefaultAsync();
                if (location == null)
                {
                    location = new Models.Location { LocationId = Guid.NewGuid(), Name = "Headquarters / Remote", Description = "Default Location" };
                    await _context.Locations.AddAsync(location);
                    await _context.SaveChangesAsync();
                }
                company.LocationId = location.LocationId;
            }

            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
            return company;
        }

        public async Task UpdateCompanyAsync(Company company)
        {
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
        }

        public async Task<CompanyUser?> GetCompanyUserAsync(Guid jobProviderId, Guid companyId)
        {
            return await _context.CompanyUsers
                .Include(ic => ic.JobProvider)
                    .ThenInclude(jp => jp.SystemUser)
                .Include(ic => ic.Company)
                    .ThenInclude(icu => icu.Industry)
                .Include(ic => ic.Company)
                    .ThenInclude(icu => icu.Location)
                .FirstOrDefaultAsync(u => u.JobProviderId == jobProviderId && u.CompanyId == companyId);
        }

        public async Task<CompanyUser?> CreateCompanyUserAsync(CompanyUser companyUser)
        {
            await _context.CompanyUsers.AddAsync(companyUser);
            await _context.SaveChangesAsync();
            return companyUser;
        }

        public async Task UpdateCompanyUserAsync(CompanyUser companyUser)
        {
            _context.CompanyUsers.Update(companyUser);
            await _context.SaveChangesAsync();
        }

        public async Task DeactivateAllCompanyUsersForProviderAsync(Guid jobProviderId)
        {
            var activeUsers = await _context.CompanyUsers
                .Where(cu => cu.JobProviderId == jobProviderId && cu.Status == (int)CompanyUserStatus.Active)
                .ToListAsync();

            foreach (var u in activeUsers)
            {
                u.Status = (int)CompanyUserStatus.Inactive;
            }

            if (activeUsers.Count > 0)
            {
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CompanyUser?> GetActiveCompanyUserByJobProviderIdAsync(Guid jobProviderId)
        {
            return await _context.CompanyUsers
                .Include(cu => cu.Company)
                    .ThenInclude(c => c.Industry)
                .Include(cu => cu.Company)
                    .ThenInclude(c => c.Location)
                .Include(cu => cu.JobProvider)
                    .ThenInclude(jp => jp.SystemUser)
                .FirstOrDefaultAsync(cu => cu.JobProviderId == jobProviderId && cu.Status == (int)CompanyUserStatus.Active);
        }

        public async Task<IEnumerable<CompanyUser>> GetCompanyUsersByCompanyIdAsync(Guid companyId)
        {
            return await _context.CompanyUsers
                .Include(cu => cu.JobProvider)
                    .ThenInclude(jp => jp.SystemUser)
                .Include(cu => cu.Company)
                    .ThenInclude(c => c.Industry)
                .Include(cu => cu.Company)
                    .ThenInclude(c => c.Location)
                .Where(cu => cu.CompanyId == companyId)
                .ToListAsync();
        }




    }
}
