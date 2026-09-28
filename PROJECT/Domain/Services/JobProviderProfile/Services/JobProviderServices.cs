using AutoMapper;
using Domain.Enum;
using Domain.Models;
using Domain.Services.JobProviderProfile.DTO;
using Domain.Services.JobProviderProfile.Interface;
using Org.BouncyCastle.Crypto.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.JobProviderProfile.Services
{
    public class JobProviderServices:IJobProviderServices
    {
        private readonly IJobProviderRepository repository;
        private readonly IMapper mapper;
        public JobProviderServices(IJobProviderRepository _jobProviderRepository, IMapper _mapper)
        {
            mapper = _mapper;
            repository = _jobProviderRepository;

        }


        public async Task<IEnumerable<ProviderResponseDTO>> GetAllProviderAsync()
        {
            var provider = await repository.GetAllProviderAsync();
            return mapper.Map<IEnumerable<ProviderResponseDTO>>(provider);
        }

        public async Task<bool> DeleteProviderAsync(Guid id)
        {
            var provider = await repository.DeleteProviderAsync(id);
            return provider;
        }

        private static void UpdateSystemUserFields(Models.SystemUser? user, string? firstName, string? lastName, string? email, string? phone)
        {
            if (user == null) return;
            if (!string.IsNullOrWhiteSpace(firstName)) user.FirstName = firstName;
            if (!string.IsNullOrWhiteSpace(lastName)) user.LastName = lastName;
            if (!string.IsNullOrWhiteSpace(email)) user.Email = email;
            if (!string.IsNullOrWhiteSpace(phone)) user.Phone = phone;
        }

        public async Task<JobProviderDto> CreateProviderProfileAsync(Guid systemUserId, CreateJobProviderDto dto)
        {
            var existing = await repository.GetBySystemUserIdAsync(systemUserId);
            if (existing != null)
            {
                UpdateSystemUserFields(existing.SystemUser, dto.FirstName, dto.LastName, dto.Email, dto.Phone);
                await repository.UpdateJobProviderAsync(existing);
                return mapper.Map<JobProviderDto>(existing);
            }

            var provider = new Models.JobProvider
            {
                JobProviderId = Guid.NewGuid(),
                SystemUserId = systemUserId
            };

            await repository.CreateJobProviderAsync(provider);
            var reloaded = await repository.GetBySystemUserIdAsync(systemUserId);
            if (reloaded?.SystemUser != null)
            {
                UpdateSystemUserFields(reloaded.SystemUser, dto.FirstName, dto.LastName, dto.Email, dto.Phone);
                await repository.UpdateJobProviderAsync(reloaded);
            }
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

            UpdateSystemUserFields(provider.SystemUser, dto.FirstName, dto.LastName, dto.Email, dto.Phone);

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
                Roles = dto.Roles != 0 ? dto.Roles : (int)CompanyUserRole.Admin,
                Status = dto.Status != 0 ? dto.Status : (int)CompanyUserStatus.Active,
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
                existingUser.Status = dto.Status != 0 ? dto.Status : (int)CompanyUserStatus.Active;
                if (dto.Roles != 0) existingUser.Roles = dto.Roles;
                await repository.UpdateCompanyUserAsync(existingUser);
                companyUser = existingUser;
            }
            else
            {
                companyUser = new CompanyUser
                {
                    JobProviderId = provider.JobProviderId,
                    CompanyId = company.CompanyId,
                    Roles = dto.Roles != 0 ? dto.Roles : (int)CompanyUserRole.Recruiter,
                    Status = dto.Status != 0 ? dto.Status : (int)CompanyUserStatus.Active,
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

