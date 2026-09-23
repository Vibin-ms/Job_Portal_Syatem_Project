using AutoMapper;
using Domain.Models;
using Domain.Services.JobProvider.Dto;
using Domain.Services.JobProvider.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.JobProvider
{
    public class JobProviderService : IJobProviderService
    {
        private readonly IJobProviderRepo repository;
        private readonly IMapper mapper;

        public JobProviderService(IJobProviderRepo _repository, IMapper _mapper)
        {
            repository = _repository;
            mapper = _mapper;
        }

        public async Task<JobProviderDto> CreateProviderProfileAsync(Guid systemUserId, CreateJobProviderDto dto)
        {
            var existing = await repository.GetBySystemUserIdAsync(systemUserId);
            if (existing != null)
            {
                existing.Designation = dto.Designation;
                await repository.UpdateJobProviderAsync(existing);
                return mapper.Map<JobProviderDto>(existing);
            }

            var provider = new Models.JobProvider
            {
                JobProviderId = Guid.NewGuid(),
                SystemUserId = systemUserId,
                Designation = dto.Designation
            };

            await repository.CreateJobProviderAsync(provider);
            var reloaded = await repository.GetBySystemUserIdAsync(systemUserId);
            return mapper.Map<JobProviderDto>(reloaded ?? provider);
        }

        public async Task<JobProviderDto?> GetProviderBySystemUserIdAsync(Guid systemUserId)
        {
            var provider = await repository.GetBySystemUserIdAsync(systemUserId);
            if (provider == null) return null;
            return mapper.Map<JobProviderDto>(provider);
        }

        public async Task<JobProviderDto?> UpdateProviderAsync(Guid systemUserId, UpdateJobProviderDto dto)
        {
            var provider = await repository.GetBySystemUserIdAsync(systemUserId);
            if (provider == null) return null;

            provider.Designation = dto.Designation;

            await repository.UpdateJobProviderAsync(provider);
            return mapper.Map<JobProviderDto>(provider);
        }

        public async Task<CompanyDto?> GetCompanyByIdAsync(Guid companyId)
        {
            var company = await repository.GetCompanyByIdAsync(companyId);
            if (company == null) return null;
            return mapper.Map<CompanyDto>(company);
        }

        public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync()
        {
            var companies = await repository.GetAllCompaniesAsync();
            return mapper.Map<IEnumerable<CompanyDto>>(companies);
        }

        public async Task<CompanyDto> CreateCompanyAsync(CreateCompanyDto dto)
        {
            var company = mapper.Map<Company>(dto);
            await repository.CreateCompanyAsync(company);
            var reloaded = await repository.GetCompanyByIdAsync(company.CompanyId);
            return mapper.Map<CompanyDto>(reloaded ?? company);
        }

        public async Task<CompanyDto> CreateCompanyForProviderAsync(Guid systemUserId, CreateCompanyDto dto)
        {
            var provider = await repository.GetBySystemUserIdAsync(systemUserId);
            if (provider == null)
            {
                throw new KeyNotFoundException("Job provider profile not found.");
            }

            await repository.DeactivateAllCompanyUsersForProviderAsync(provider.JobProviderId);

            var company = mapper.Map<Company>(dto);
            await repository.CreateCompanyAsync(company);

            var companyUser = new CompanyUser
            {
                JobProviderId = provider.JobProviderId,
                CompanyId = company.CompanyId,
                Designation = !string.IsNullOrWhiteSpace(dto.UserDesignation) ? dto.UserDesignation : provider.Designation,
                RoleInCompany = !string.IsNullOrWhiteSpace(dto.RoleInCompany) ? dto.RoleInCompany : "Admin",
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            await repository.CreateCompanyUserAsync(companyUser);

            var reloaded = await repository.GetCompanyByIdAsync(company.CompanyId);
            return mapper.Map<CompanyDto>(reloaded ?? company);
        }

        public async Task<CompanyDto?> UpdateCompanyAsync(Guid companyId, UpdateCompanyDto dto)
        {
            var company = await repository.GetCompanyByIdAsync(companyId);
            if (company == null) return null;

            mapper.Map(dto, company);

            await repository.UpdateCompanyAsync(company);
            var reloaded = await repository.GetCompanyByIdAsync(company.CompanyId);
            return mapper.Map<CompanyDto>(reloaded ?? company);
        }

        public async Task<CompanyUserDto> SelectCompanyAsync(Guid systemUserId, SelectCompanyDto dto)
        {
            var provider = await repository.GetBySystemUserIdAsync(systemUserId);
            if (provider == null)
            {
                throw new KeyNotFoundException("Job provider profile not found.");
            }

            var company = await repository.GetCompanyByIdAsync(dto.CompanyId);
            if (company == null)
            {
                throw new KeyNotFoundException("Specified company was not found.");
            }

            await repository.DeactivateAllCompanyUsersForProviderAsync(provider.JobProviderId);

            var existingUser = await repository.GetCompanyUserAsync(provider.JobProviderId, dto.CompanyId);
            CompanyUser companyUser;

            if (existingUser != null)
            {
                existingUser.IsActive = true;
                if (!string.IsNullOrWhiteSpace(dto.Designation)) existingUser.Designation = dto.Designation;
                if (!string.IsNullOrWhiteSpace(dto.RoleInCompany)) existingUser.RoleInCompany = dto.RoleInCompany;
                await repository.UpdateCompanyUserAsync(existingUser);
                companyUser = existingUser;
            }
            else
            {
                companyUser = new CompanyUser
                {
                    JobProviderId = provider.JobProviderId,
                    CompanyId = company.CompanyId,
                    Designation = !string.IsNullOrWhiteSpace(dto.Designation) ? dto.Designation : provider.Designation,
                    RoleInCompany = !string.IsNullOrWhiteSpace(dto.RoleInCompany) ? dto.RoleInCompany : "Recruiter",
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow
                };
                await repository.CreateCompanyUserAsync(companyUser);
            }

            var reloaded = await repository.GetCompanyUserAsync(provider.JobProviderId, company.CompanyId);
            return mapper.Map<CompanyUserDto>(reloaded ?? companyUser);
        }

        public async Task<CompanyUserDto?> GetCurrentCompanyUserAsync(Guid systemUserId)
        {
            var provider = await repository.GetBySystemUserIdAsync(systemUserId);
            if (provider == null) return null;

            var activeCu = await repository.GetActiveCompanyUserByJobProviderIdAsync(provider.JobProviderId);
            if (activeCu == null) return null;
            return mapper.Map<CompanyUserDto>(activeCu);
        }

        public async Task<IEnumerable<CompanyUserDto>> GetCompanyUsersAsync(Guid companyId)
        {
            var users = await repository.GetCompanyUsersByCompanyIdAsync(companyId);
            return mapper.Map<IEnumerable<CompanyUserDto>>(users);
        }
    }
}
