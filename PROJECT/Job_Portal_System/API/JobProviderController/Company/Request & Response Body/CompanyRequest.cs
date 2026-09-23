using System;

namespace Job_Portal_System.API.JobProviderController.Company.Request___Response_Body
{
    public class CreateCompanyRequest
    {
        public string CompanyName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Website { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public Guid IndustryId { get; set; }
        public Guid LocationId { get; set; }
        public string? UserDesignation { get; set; }
        public string RoleInCompany { get; set; } = "Admin";
    }

    public class UpdateCompanyRequest
    {
        public string CompanyName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Website { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public Guid IndustryId { get; set; }
        public Guid LocationId { get; set; }
    }

    public class SelectCompanyRequest
    {
        public Guid CompanyId { get; set; }
        public string? Designation { get; set; }
        public string RoleInCompany { get; set; } = "Recruiter";
    }
}
