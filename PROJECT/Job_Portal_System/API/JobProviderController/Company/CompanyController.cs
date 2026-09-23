using AutoMapper;
using Domain.Services.JobProvider.Dto;
using Domain.Services.JobProvider.Interface;
using Job_Portal_System.API.JobProviderController.Company.Request___Response_Body;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Job_Portal_System.API.JobProviderController.Company
{
    [Route("api/JobProvider/[controller]")]
    [ApiController]
    [Authorize(Roles = "JobProvider")]
    [Tags("Job Provider - Company")]
    public class CompanyController : ControllerBase
    {
        private readonly IJobProviderService _jobProviderService;
        private readonly IMapper _mapper;

        public CompanyController(IJobProviderService jobProviderService, IMapper mapper)
        {
            _jobProviderService = jobProviderService;
            _mapper = mapper;
        }

        private Guid? GetCurrentUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(userId, out Guid systemUserId) ? systemUserId : null;
        }

        [HttpPost]
        [Route("CreateCompany")]
        public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyRequest request)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null) return Unauthorized(new { message = "Invalid or missing user token" });

                var dto = _mapper.Map<CreateCompanyDto>(request);
                var result = await _jobProviderService.CreateCompanyForProviderAsync(userId.Value, dto);

                return Ok(new
                {
                    message = "Company created and linked to provider successfully",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetAllCompanies")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCompanies()
        {
            try
            {
                var result = await _jobProviderService.GetAllCompaniesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetCompany/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCompany(Guid id)
        {
            try
            {
                var result = await _jobProviderService.GetCompanyByIdAsync(id);
                if (result == null)
                {
                    return NotFound(new { message = "Company not found" });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("UpdateCompany/{id}")]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] UpdateCompanyRequest request)
        {
            try
            {
                var dto = _mapper.Map<UpdateCompanyDto>(request);
                var result = await _jobProviderService.UpdateCompanyAsync(id, dto);
                if (result == null)
                {
                    return NotFound(new { message = "Company not found" });
                }

                return Ok(new
                {
                    message = "Company updated successfully",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("SelectCompany")]
        public async Task<IActionResult> SelectCompany([FromBody] SelectCompanyRequest request)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null) return Unauthorized(new { message = "Invalid or missing user token" });

                var dto = _mapper.Map<SelectCompanyDto>(request);
                var result = await _jobProviderService.SelectCompanyAsync(userId.Value, dto);

                return Ok(new
                {
                    message = "Company selected and active association saved",
                    data = result
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetActiveCompany")]
        public async Task<IActionResult> GetActiveCompany()
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null) return Unauthorized(new { message = "Invalid or missing user token" });

                var result = await _jobProviderService.GetCurrentCompanyUserAsync(userId.Value);
                if (result == null)
                {
                    return NotFound(new { message = "No active company association found for current provider" });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetCompanyUsers/{companyId}")]
        public async Task<IActionResult> GetCompanyUsers(Guid companyId)
        {
            try
            {
                var result = await _jobProviderService.GetCompanyUsersAsync(companyId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
