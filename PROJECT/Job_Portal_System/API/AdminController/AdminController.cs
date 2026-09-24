using AutoMapper;
using Azure;
using Domain.Enum;
using Domain.Services.AcceptORRejectCompany.Interface;
using Domain.Services.CompanyProfile.Interface;
using Domain.Services.Experiences.DTO;
using Domain.Services.Experiences.Interface;
using Domain.Services.Industrys.DTO;
using Domain.Services.Industrys.Interface;
using Domain.Services.JobApplications.Interface;
using Domain.Services.JobCategorys.DTO;
using Domain.Services.JobCategorys.Interface;
using Domain.Services.JobPost.Interface;
using Domain.Services.JobProviderProfile.Interface;
using Domain.Services.Jobseekerprofile.Interface;
using Domain.Services.JobTypes.DTO;
using Domain.Services.JobTypes.Interfaces;
using Domain.Services.Location.DTO;
using Domain.Services.Location.Interface;
using Domain.Services.Qualifications.DTO;
using Domain.Services.Qualifications.Interface;
using Domain.Services.Skills.DTO;
using Domain.Services.Skills.Interface;
using Job_Portal_System.API.AdminController.Request___Response_Body;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Job_Portal_System.API.AdminController
{
    [Authorize(Roles = "Admin")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ISkillServices skillservices;
        private readonly ILocationServices locationservices;
        private readonly IQualificationServices qualificationservices;
        private readonly IExperienceServices experienceservices;
        private readonly IIndustryServices industryServices;
        private readonly ICategoryServices categoryservices;
        private readonly IJobTypeServices jobtypeservices;
        private readonly ICrudservice crudservice;
        private readonly IJobProviderServices jobproviderservices;
        private readonly ICompanySerices companyserings;
        private readonly IJobPostServices jobPostServices;
        private readonly IJobPostService _jobPostService;
        private readonly IAccpetORRejectServices accpetORRejectServices;
        private readonly IApplicationServices applicationservices;
        private readonly IMapper mapper;
        public AdminController(ISkillServices _skillservices, IMapper _mapper, ILocationServices _locationservices,
            IQualificationServices _qualificationservices, IExperienceServices _experienceservices, IIndustryServices _industryServices,
            ICategoryServices _categoryservices, IJobTypeServices _jobtypeservices, ICrudservice _crudservice,
            IJobProviderServices _jobproviderservices, ICompanySerices _companyserings, IJobPostServices _jobPostServices,
            IAccpetORRejectServices _accpetORRejectServices, IApplicationServices _applicationservices,
            IJobPostService jobPostService)
        {
            skillservices = _skillservices;
            mapper = _mapper;
            locationservices = _locationservices;
            qualificationservices = _qualificationservices;
            experienceservices = _experienceservices;
            industryServices = _industryServices;
            categoryservices = _categoryservices;
            jobtypeservices = _jobtypeservices;
            crudservice = _crudservice;
            jobproviderservices = _jobproviderservices;
            companyserings = _companyserings;
            jobPostServices = _jobPostServices;
            accpetORRejectServices = _accpetORRejectServices;
            applicationservices = _applicationservices;
            _jobPostService = jobPostService;
        }
        [HttpPost]
        [Route("AddSkill")]
        public async Task<IActionResult>AddSkill(SkillRequest skillRequest)
        {
            try
            {
                
                    var newskill = mapper.Map<skillDTO>(skillRequest);
               var skilladded= await skillservices.AddSkillAsync(newskill);
                if (skilladded == null)
                {
                    return Conflict(new
                    {
                        message = "Skill already exists"
                    });
                }
                return Ok(new
                {
                    Message = "Skill Added Succeffuly",  
                        Data = skilladded
                }
                    );
               

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message );
            }    

        }
        [HttpGet]
        [Route("AllSkills")]
        public async Task<IActionResult>GetAllSkills()
        {
            try
            {
                var skills = await skillservices.GetSkillAsync();
                var respons = mapper.Map<IEnumerable<SkillResponse>>(skills);
                if(respons==null || !respons.Any())
                {
                    return NotFound("No data found");
                }

                return Ok(respons);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [HttpPut]
        [Route("EditSkill/{id:guid}")]
        public async Task<IActionResult>EditSkill(Guid id,SkillRequest skillRequest)
        {
            try
            {
                var skill = mapper.Map<skillDTO>(skillRequest);
                var updateskill = await skillservices.UpdateSkillAsync(id,skill);
                if (updateskill == null)
                {
                    return NotFound(new
                    {
                        message = "Invalid Skill ID"
                    });
                }

                return Ok(new
                {
                    message = "Skill Updated",
                    data = updateskill

                });
            }
            catch(Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
        [HttpDelete]
        [Route("DeleteSkill/{id:Guid}")]
        public async Task<IActionResult>DeleteSkill(Guid id)
        {
            try
            {
                var skill = await skillservices.DeleteSkillAsync(id);
                if (!skill)
                    return NotFound("Skill Not Found");

                return Ok("skill Delete Successfull");
            }
            catch (Exception ex) 
                {
                    return BadRequest(ex.Message);
                }
        }





        //Location//
        [HttpPost]
        [Route("AddLocation")]
        public async Task<IActionResult>AddLocation(LocationRequest locationRequest)
        {
            try
            {
                var Location = mapper.Map<LocationDTO>(locationRequest);
                var AddedLocation = await locationservices.AddLocationAsync(Location);
                if (AddedLocation == null)
                {
                    return Conflict(new
                    {
                        message = "Location already exists"
                    });
                }
                return Ok(
                    new
                    {
                        message = "Location Added Successfully",
                        Data= AddedLocation
                    }
                    );
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        [HttpGet]
        [Route("GetAllLocation")]
        public async Task<IActionResult>GetAllLocation()
        {
            try
            {
                var allLoc = await locationservices.GetAllLocationAsync();
                var Loc = mapper.Map<IEnumerable<LocationResponse>>(allLoc);
                if(Loc==null||!Loc.Any())
                {
                    return NotFound("There is no data to show");
                }
                return Ok(Loc);

            }
            catch(Exception ex)
            { return BadRequest(ex.Message); }
        }
        [HttpPut]
        [Route("UpdateLocation/{id:guid}")]
        public async Task<IActionResult> UpdateLocation(Guid id, LocationRequest locationRequest)
        {
            try
            {
                var upd = mapper.Map<LocationDTO>(locationRequest);
                var UpdateLoc = await locationservices.UpdateLocationAsync(id, upd);
                if (UpdateLoc == null)
                {
                    return NotFound(new
                    {
                        message = "Invalid Location ID"
                    });
                }

                return Ok(new
                {
                    message = "Location Updated",
                    data = UpdateLoc

                });
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }
        }
        [HttpDelete]
        [Route("DeleteLocation/{id:guid}")]
        public async Task<IActionResult> DeleteLocation(Guid id)
        {
            try
            {
                var loc = await locationservices.DeleteLocationAsync(id);
                if (!loc)
                    return NotFound("Skill Not Found");
                return Ok("Location is deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        //Qualification//

        [HttpPost]
        [Route("AddQualification")]
        public async Task<IActionResult> AddQualification(QualificationRequest qualificationRequest)
        {
            try
            {
                var qualification = mapper.Map<QualificationDTO>(qualificationRequest);
                var addQualification = await qualificationservices.AddQualificationAsync(qualification);
                if (addQualification == null)
                {
                    return Conflict(new
                    {
                        message = "Qualification already exists"
                    });
                }

                return Ok(
                    new
                    {
                        message = "Qualification Added Successfull",
                        data = addQualification
                    });

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetAllQualification")]
        public async Task<IActionResult>GetAllQualification()
        {
            try
            {
                var quali = await qualificationservices.GetAllQualificationsAsync();
                var response = mapper.Map<IEnumerable<QualificationResponse>>(quali);
                if(response==null || !response.Any())
                {
                    return NotFound("There is no data added");
                }
                return Ok(response);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [HttpPut]
        [Route("UpdateQualification/{id:guid}")]
        public async Task<IActionResult>UpdateQualification(Guid id,QualificationRequest qualificationRequest)
        {
            try
            {
                var update = mapper.Map<QualificationDTO>(qualificationRequest);
                var updated = await qualificationservices.UpdateQualificationAsync(id, update);
                if (updated == null)
                {
                    return NotFound(new
                    {
                        message = "Invalid Qualification ID"
                    });
                }

                return Ok(new
                {
                    message = "Qualification Updated",
                    data = updated

                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        [HttpDelete]
        [Route("deleteQualification/{id:guid}")]
        public async Task<IActionResult>deletequalification(Guid id)
        {
            var response=await qualificationservices.deletequalificationAsync(id);
            if(!response)
            {
                return NotFound("Invalid Id ");
            }
            return Ok("Qualification deleted success");
        }

        //Experience//

        [HttpPost]
        [Route("AddExperience")]
        public async Task<IActionResult> AddExperience(Experience_Request experience_Request)
        {
            try
            {
                var exp = mapper.Map<ExperienceDTO>(experience_Request);
                var addExp = await experienceservices.AddExperienceAsync(exp);
                if (addExp == null)
                {
                    return Conflict(new
                    {
                        message = "Experience already exists"
                    });
                }

                return Ok(
                    new
                    {
                        message = "Experience Added Successfull",
                        data = addExp
                    });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetAllExperience")]
        public async Task<IActionResult> GetAllExperience()
        {
            try
            {
                var Exp = await experienceservices.GetAllExperienceAsync();
                var response = mapper.Map<IEnumerable<ExperienceResponse>>(Exp);
                if (response == null || !response.Any())
                {
                    return NotFound("There is no data added");
                }
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut]
        [Route("UpdateExperience/{id:guid}")]
        public async Task<IActionResult> UpdateExperience(Guid id, Experience_Request experience_Request)
        {
            try
            {
                var update = mapper.Map<ExperienceDTO>(experience_Request);
                var updated = await experienceservices.UpdateExperienceAsync(id, update);
                if (updated == null)
                {
                    return NotFound(new
                    {
                        message = "Invalid Experience ID"
                    });
                }

                return Ok(new
                {
                    message = "Experience Updated",
                    data = updated

                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        [HttpDelete]
        [Route("deleteExperience/{id:guid}")]
        public async Task<IActionResult> deleteExperience(Guid id)
        {
            var response = await experienceservices.deleteExperienceAsync(id);
            if (!response)
            {
                return NotFound("Invalid Id ");
            }
            return Ok("Experience deleted success");
        }


        //Industry/
        [HttpPost]
        [Route("AddIndustry")]
        public async Task<IActionResult> AddIndustry(IndustryRequest industryRequest)
        {
            try
            {
                var Ind = mapper.Map<IndustryDTO>(industryRequest);
                var addInd = await industryServices.AddIndustryAsync(Ind);
                if (addInd == null)
                {
                    return Conflict(new
                    {
                        message = "Industry already exists"
                    });
                }

                return Ok(
                    new
                    {
                        message = "Industry Added Successfull",
                        data = addInd
                    });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetAllIndustry")]
        public async Task<IActionResult> GetAllIndustry()
        {
            try
            {
                var Ind = await industryServices.GetAllIndustryAsync();
                var response = mapper.Map<IEnumerable<IndustryResponse>>(Ind);
                if (response == null || !response.Any())
                {
                    return NotFound("There is no data added");
                }
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut]
        [Route("UpdateIndustry/{id:guid}")]
        public async Task<IActionResult> UpdateIndustry(Guid id, IndustryRequest industryRequest)
        {
            try
            {
                var update = mapper.Map<IndustryDTO>(industryRequest);
                var updated = await industryServices.UpdateIndustryAsync(id, update);
                if (updated == null)
                {
                    return NotFound(new
                    {
                        message = "Invalid Industry ID"
                    });
                }

                return Ok(new
                {
                    message = "Industry Updated",
                    data = updated

                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        [HttpDelete]
        [Route("deleteIndustry/{id:guid}")]
        public async Task<IActionResult> deleteIndustry(Guid id)
        {
            var response = await industryServices.deleteIndustryAsync(id);
            if (!response)
            {
                return NotFound("Invalid Id ");
            }
            return Ok("Industry deleted success");
        }



        //JobCategory//
        [HttpPost]
        [Route("AddCategory")]
        public async Task<IActionResult> AddCategory(CategoryRequest categoryrequest)
        {
            try
            {
                var cat = mapper.Map<CategoryDTO>(categoryrequest);
                var addcat = await categoryservices.AddCategoryAsync(cat);
                if (addcat == null)
                {
                    return Conflict(new
                    {
                        message = "Category already exists"
                    });
                }

                return Ok(
                    new
                    {
                        message = "category Added Successfull",
                        data = addcat
                    });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetAllCategory")]
        public async Task<IActionResult> GetAllCategory()
        {
            try
            {
                var cat = await categoryservices.GetAllCategoryAsync();
                var response = mapper.Map<IEnumerable<CategoryResponse>>(cat);
                if (response == null||!response.Any())
                {
                    return NotFound("There is no data added");
                }
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut]
        [Route("UpdateCategory/{id:guid}")]
        public async Task<IActionResult> Updatecategory(Guid id, CategoryRequest categoryRequest)
        {
            try
            {
                var update = mapper.Map<CategoryDTO>(categoryRequest);
                var updated = await categoryservices.UpdateCategoryAsync(id, update);
                if (updated == null)
                {
                    return NotFound(new
                    {
                        message = "Invalid category ID"
                    });
                }

                return Ok(new
                {
                    message = "category Updated",
                    data = updated

                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        [HttpDelete]
        [Route("deleteCategory/{id:guid}")]
        public async Task<IActionResult> deleteCategory(Guid id)
        {
            var response = await categoryservices.deleteCategoryAsync(id);
            if (!response)
            {
                return NotFound("Invalid Id ");
            }
            return Ok("category deleted success");
        }


        //JobType//
        [HttpPost]
        [Route("AddJobType")]
        public async Task<IActionResult> AddJobType(JobTypeRequest jobTypeRequest)
        {
            try
            {
                var type = mapper.Map<TypeDTO>(jobTypeRequest);
                var addtype = await jobtypeservices.AddJobTypeAsync(type);
                if (addtype == null)
                {
                    return Conflict(new
                    {
                        message = "JobType already exists"
                    });
                }

                return Ok(
                    new
                    {
                        message = "JobType Added Successfull",
                        data = addtype
                    });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetAllJobType")]
        public async Task<IActionResult> GetAllJobType()
        {
            try
            {
                var Type = await jobtypeservices.GetAllJobTypeAsync();
                var response = mapper.Map<IEnumerable<JobTypeResponse>>(Type);
                if (response == null||!response.Any())
                {
                    return NotFound("There is no data added");
                }
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut]
        [Route("UpdateJobType/{id:guid}")]
        public async Task<IActionResult> UpdateJobType(Guid id, JobTypeRequest jobTypeRequest)
        {
            try
            {
                var update = mapper.Map<TypeDTO>(jobTypeRequest);
                var updated = await jobtypeservices.UpdateJobTypeAsync(id, update);
                if (updated == null)
                {
                    return NotFound(new
                    {
                        message = "Invalid JobType ID"
                    });
                }

                return Ok(new
                {
                    message = "JobType Updated",
                    data = updated

                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        [HttpDelete]
        [Route("deleteJobType/{id:guid}")]
        public async Task<IActionResult> deleteJobType(Guid id)
        {
            var response = await jobtypeservices.deleteJobTypeAsync(id);
            if (!response)
            {
                return NotFound("Invalid Id ");
            }
            return Ok("JobType deleted successfully");
        }


        //JobSeeker Profile//

        [HttpGet]
        [Route("GetAllJobSeekers")]
        public async Task<IActionResult>GetAllJobSeekers()
        {
            try
            {
                var jobseekers = await crudservice.GetAllJobSeekerAsync();
                var seekers = mapper.Map<IEnumerable<JobSeekerProfileResponse>>(jobseekers);
                if (seekers == null||!seekers.Any())
                {

                     return NotFound("There is NO Data Found");
                }
                return Ok(seekers);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetAllProviders")]
        public async Task<IActionResult>GetAllProviders()
        {
            try
            {
                var provider = await jobproviderservices.GetAllProviderAsync();
                var allProvier = mapper.Map<IEnumerable<JobProviderResponse>>(provider);
                if(allProvier == null||!allProvier.Any())
                {
                    return NotFound("There Is No Data Found");
                }
                return Ok(allProvier);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteProvider/{id:guid}")]
        public async Task<IActionResult>DeleteProvider(Guid id)
        {
            var provider=await jobproviderservices.DeleteProviderAsync(id);
            if(!provider)
            {
                return NotFound("Invalid Id");
            }
            return Ok("This provider Is Deleted Successfully");
        }
        [HttpGet]
        [Route("GetAllJobs")]
        public async Task<IActionResult> GetAllJobs()
        {
            try
            {
                var job = await jobPostServices.GetAllJobAsync();
                var AllJob = mapper.Map<IEnumerable<JobPostResponse>>(job);
                if (AllJob == null || !AllJob.Any())
                {
                    return NotFound("There is no Data");
                }
                return Ok(AllJob);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteJob/{id:guid}")]
        public async Task<IActionResult> DeleteJob(Guid id)
        {
            var job = await jobPostServices.DeleteJobAsync(id);
            if (!job)
            {
                return NotFound("Invalid Id");
            }
            return Ok("This company Is Deleted Successfully");
        }
        [HttpGet]
        [Route("GetAllCompanies")]
        public async Task<IActionResult> GetAllCompanies()
        {
            try
            {
                var company = await companyserings.GetAllCompanyAsync();
                var comp = mapper.Map<IEnumerable<CompanyResponse>>(company);
                if (comp == null || !comp.Any())
                {
                    return NotFound("There is no Data");
                }
                return Ok(comp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetPendingCompanies")]
        public async Task<IActionResult> GetPendingCompanies()
        {
            try
            {
                var company = await companyserings.GetPendingCompanyAsync();
                var comp = mapper.Map<IEnumerable<CompanyResponse>>(company);
                if (comp == null || !comp.Any())
                {
                    return NotFound("There is no Data");
                }
                return Ok(comp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Route("AcceptCompany/{companyid:guid}")]
        public async Task<IActionResult>AcceptCompany(Guid companyid)
        {
            try
            {
                var accepted = await accpetORRejectServices
                    .AcceptCompanyAsync(companyid);

                if (!accepted)
                {
                    return NotFound("There is no company with this ID");
                }

                return Ok("Company accepted, mail sent successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("RejectCompany/{companyid:guid}")]
        public async Task<IActionResult> RejectCompany(Guid companyid)
        {
            try
            {
                var Rejected = await accpetORRejectServices
                    .RejectCompanyAsync(companyid);

                if (!Rejected)
                {
                    return NotFound("There is no company with this ID");
                }

                return Ok("Company Rejected, mail sent successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        [Route("GetAcceptedCompanies")]
        public async Task<IActionResult> GetAcceptedCompanies()
        {
            try
            {
                var company = await companyserings.GetAcceptedCompanyAsync();
                var comp = mapper.Map<IEnumerable<CompanyResponse>>(company);
                if (comp == null || !comp.Any())
                {
                    return NotFound("There is no Data");
                }
                return Ok(comp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetRejectedCompanies")]
        public async Task<IActionResult> GetRejectedCompanies()
        {
            try
            {
                var company = await companyserings.GetRejectedCompanyAsync();
                var comp = mapper.Map<IEnumerable<CompanyResponse>>(company);
                if (comp == null || !comp.Any())
                {
                    return NotFound("There is no Data");
                }
                return Ok(comp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetCompaniesByProvider/{providerId:guid}")]
        public async Task<IActionResult> GetCompaniesByProvider(Guid providerId)
        {
            try
            {
                var company = await companyserings.GetCompaniesByProviderAsync(providerId);
                var comp = mapper.Map<IEnumerable<CompanyResponse>>(company);
                if (comp == null || !comp.Any())
                {
                    return NotFound("There is no Data");
                }
                return Ok(comp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteCompany/{id:guid}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            var company = await companyserings.DeleteCompanyAsync(id);
            if (!company)
            {
                return NotFound("Invalid Id");
            }
            return Ok("This company Is Deleted Successfully");
        }
        [HttpGet]
        [Route("ApplicationsByJobPost/{jobpostId:guid}")]
        public async Task<IActionResult>GetApplicationsByJobPost(Guid jobpostId)
        {
            try
            {
                var applications = await applicationservices.GetApplicationsByJobPostAsync(jobpostId);
                var jobapplications = mapper.Map<IEnumerable<ApplicationResponse>>(applications);
                if(jobapplications == null || !jobapplications.Any())
                {
                    return NotFound("There is no Applications yet");
                }
                return Ok(jobapplications);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("AcceptJobPost/{jobpostId:guid}")]
        public async Task<IActionResult> AcceptJobPost(Guid jobpostId)
        {
            try
            {
                var accepted = await _jobPostService.ChangeJobStatusByAdminAsync(jobpostId, JobPostStatus.Accepted);
                if (!accepted)
                {
                    return NotFound(new { message = "There is no Job Post with this ID" });
                }

                return Ok(new { message = "Job Post approved and published successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("RejectJobPost/{jobpostId:guid}")]
        public async Task<IActionResult> RejectJobPost(Guid jobpostId)
        {
            try
            {
                var rejected = await _jobPostService.ChangeJobStatusByAdminAsync(jobpostId, JobPostStatus.Rejected);
                if (!rejected)
                {
                    return NotFound(new { message = "There is no Job Post with this ID" });
                }

                return Ok(new { message = "Job Post rejected successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetPendingJobs")]
        public async Task<IActionResult> GetPendingJobs()
        {
            try
            {
                var jobs = await _jobPostService.GetAllJobsForAdminAsync(JobPostStatus.Pending);
                if (jobs == null || !jobs.Any())
                {
                    return NotFound(new { message = "There are no pending jobs" });
                }

                return Ok(jobs);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetAcceptedJobs")]
        public async Task<IActionResult> GetAcceptedJobs()
        {
            try
            {
                var jobs = await _jobPostService.GetAllJobsForAdminAsync(JobPostStatus.Accepted);
                if (jobs == null || !jobs.Any())
                {
                    return NotFound(new { message = "There are no accepted jobs" });
                }

                return Ok(jobs);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetRejectedJobs")]
        public async Task<IActionResult> GetRejectedJobs()
        {
            try
            {
                var jobs = await _jobPostService.GetAllJobsForAdminAsync(JobPostStatus.Rejected);
                if (jobs == null || !jobs.Any())
                {
                    return NotFound(new { message = "There are no rejected jobs" });
                }

                return Ok(jobs);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}