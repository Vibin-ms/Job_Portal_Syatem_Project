using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Services.JobProvider.Dto
{
    public class JobProviderDto
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public Guid JobProviderId { get => Id; set => Id = value; }
        public Guid SystemUserId { get; set; }
        public string Designation { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Guid CompanyId { get; set; }
        public CompanyDto? Company { get; set; }
    }

    public class CompanyDto
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public Guid CompanyId { get => Id; set => Id = value; }
        public string CompanyName { get; set; } = string.Empty;
        [JsonIgnore]
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
        public string Designation { get; set; } = string.Empty;
    }

    public class UpdateJobProviderDto
    {
        public string Designation { get; set; } = string.Empty;
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
        public string? UserDesignation { get; set; }
        public string RoleInCompany { get; set; } = "Admin";
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
        public string Designation { get; set; } = string.Empty;
        public string RoleInCompany { get; set; } = "Recruiter";
        public bool IsActive { get; set; } = true;
        public DateTime JoinedAt { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get => JoinedAt; set => JoinedAt = value; }
        public CompanyDto? Company { get; set; }
    }

    public class SelectCompanyDto
    {
        public Guid CompanyId { get; set; }
        public string? Designation { get; set; }
        public string RoleInCompany { get; set; } = "Recruiter";
    }
}
