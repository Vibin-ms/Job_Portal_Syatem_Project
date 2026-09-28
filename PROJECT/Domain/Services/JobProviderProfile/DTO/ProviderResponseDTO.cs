using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Services.JobProviderProfile.DTO
{
    public class ProviderResponseDTO
    {
        public Guid JobProviderId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Roles { get; set; } = string.Empty;
    }

    public class JobProviderDto
    {
        public Guid Id { get; set; }
        public Guid JobProviderId { get => Id; set => Id = value; }
        public Guid SystemUserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Guid CompanyId { get; set; }
        public CompanyDto? Company { get; set; }
    }

    public class CompanyDto
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get => Id; set => Id = value; }
        public string CompanyName { get; set; } = string.Empty;
        public string ComapnyName { get => CompanyName; set => CompanyName = value; }
        public string? Description { get; set; }
        public string? Website { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Guid IndustryId { get; set; }
        public string IndustryName { get; set; } = string.Empty;
        public Guid LocationId { get; set; }
        public string LocationName { get; set; } = string.Empty;
        public int TotalJobPosts { get; set; }
        public int TotalCompanyUsers { get; set; }
    }

    public class CreateJobProviderDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }

    public class UpdateJobProviderDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }

    public class CreateCompanyDto
    {
        public string CompanyName { get; set; } = string.Empty;
        [JsonIgnore]
        public string ComapnyName { get => CompanyName; set => CompanyName = value; }
        public string? Description { get; set; }
        public string? Website { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Guid IndustryId { get; set; }
        public Guid LocationId { get; set; }
        public int Roles { get; set; } = (int)CompanyUserRole.Admin;
        public int Status { get; set; } = (int)CompanyUserStatus.Active;
    }

    public class UpdateCompanyDto
    {
        public string CompanyName { get; set; } = string.Empty;
        [JsonIgnore]
        public string ComapnyName { get => CompanyName; set => CompanyName = value; }
        public string? Description { get; set; }
        public string? Website { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Guid IndustryId { get; set; }
        public Guid LocationId { get; set; }
    }

    public class CompanyUserDto
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public Guid CompanyUserId { get => Id; set => Id = value; }
        public Guid JobProviderId { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public int Roles { get; set; } = (int)CompanyUserRole.Recruiter;
        public int Status { get; set; } = (int)CompanyUserStatus.Active;
        public DateTime JoinedAt { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get => JoinedAt; set => JoinedAt = value; }
        public CompanyDto? Company { get; set; }
    }

    public class SelectCompanyDto
    {
        public Guid CompanyId { get; set; }
        public int Roles { get; set; } = (int)CompanyUserRole.Recruiter;
        public int Status { get; set; } = (int)CompanyUserStatus.Active;
    }
}
