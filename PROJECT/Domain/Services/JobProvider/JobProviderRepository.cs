using Domain.Data;
using Domain.Models;
using Domain.Services.JobProvider.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services.JobProvider
{
    public class JobProviderRepository : IJobProviderRepo
    {
        private readonly AppDbContext _context;

        public JobProviderRepository(AppDbContext context)
        {
             _context = context;
        }

        public async Task<Models.JobProvider?> GetBySystemUserIdAsync(Guid systemUserId)
        {
            return await _context.JobProviders
                .Include(u => u.SystemUser)
                .Include(u => u.CompanyUsers)
                 .ThenInclude(uv => uv.Company)
                  .ThenInclude(uc => uc.Industry)

                .FirstOrDefaultAsync(u => u.SystemUserId == systemUserId);
        }

        public async Task<Models.JobProvider?> GetByIdAsync(Guid jobProviderid) 
        {
            return await _context.JobProviders
                .Include(u => u.SystemUser)
                .Include(u => u.CompanyUsers)
                 .ThenInclude(uv => uv.Company)
                  .ThenInclude(uc => uc.Industry)

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
                .Include(c => c.JobPosts)
                .ToListAsync();
        }

        public async Task<Company?> GetCompanyByIdAsync(Guid companyId)
        {
            return await _context.Companies
                .Include(c => c.Industry)
                .Include(c => c.JobPosts)

                .FirstOrDefaultAsync(u => u.CompanyId == companyId);
        }

        public async Task<Company> CreateCompanyAsync(Company company)
        {
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
                .Include(ic => ic.Company)
                    .ThenInclude(icu => icu.Industry)
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
                .Where(cu => cu.JobProviderId == jobProviderId && cu.IsActive)
                .ToListAsync();

            foreach (var u in activeUsers)
            {
                u.IsActive = false;
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
                .Include(cu => cu.JobProvider)
                .FirstOrDefaultAsync(cu => cu.JobProviderId == jobProviderId && cu.IsActive);
        }

        public async Task<IEnumerable<CompanyUser>> GetCompanyUsersByCompanyIdAsync(Guid companyId)
        {
            return await _context.CompanyUsers
                .Include(cu => cu.JobProvider)
                    .ThenInclude(jp => jp.SystemUser)
                .Include(cu => cu.Company)
                .Where(cu => cu.CompanyId == companyId)
                .ToListAsync();
        }
    }
}
