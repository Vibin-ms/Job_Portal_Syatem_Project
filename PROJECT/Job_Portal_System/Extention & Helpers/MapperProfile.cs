using AutoMapper;
using Domain.Models;
using Domain.Services.Authentication.Dto;
using Domain.Services.CompanyProfile.DTO;
using Domain.Services.Experiences.DTO;
using Domain.Services.Industrys.DTO;
using Domain.Services.InterviewSchedules.Dto;
using Domain.Services.JobApplications.DTO;
using Domain.Services.JobCategorys.DTO;
using Domain.Services.JobPost.DTO;
using Domain.Services.JobProvider.Dto;
using Domain.Services.JobProviderProfile.DTO;
using Domain.Services.Jobseeker.DTO;
using Domain.Services.Jobseekerprofile.Dto;
using Domain.Services.JobTypes.DTO;
using Domain.Services.Location.DTO;
using Domain.Services.Qualifications.DTO;
using Domain.Services.Skills.DTO;
using Job_Portal_System.API.AdminController;
using Job_Portal_System.API.AdminController.Request___Response_Body;
using Job_Portal_System.API.Authentication_Controller.Register;
using Job_Portal_System.API.JobProviderController.Company.Request___Response_Body;
using Job_Portal_System.API.JobProviderController.InterviewSchedule.Request___Response_Body;
using Job_Portal_System.API.JobProviderController.JobApplication.Request___Response_Body;
using Job_Portal_System.API.JobProviderController.JobPost.Request___Response_Body;
using Job_Portal_System.API.JobProviderController.Profile.Request___Response_Body;
using Job_Portal_System.API.JobSeekeerController.Request___Response_Body.Jobseekerprofile;
using Job_Portal_System.API.JobSeekeerController.RequestObject;
using Job_Portal_System.API.JobSeekeerController.ResponseObject;
using static Domain.Services.JobPost.Dto.JobPostDtos;
using static Domain.Services.Jobseekerprofile.Dto.Jobseekerprofiledto;

namespace Job_Portal_System.Extention___Helpers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //Authentication part//

            CreateMap<Registerrequest, SystemuserDto>();
            CreateMap<SystemuserDto, SystemUser>();
            CreateMap<SystemUser, SystemuserDto>();
            CreateMap<SystemuserDto, Registerresponse>().ForMember(dest => dest.Message, opt => opt.MapFrom(src => "User registered successfully"));


            //Admin part//
            CreateMap<SkillRequest, skillDTO>().ReverseMap();
            CreateMap<skillDTO, Skill>().ReverseMap();
            CreateMap<SkillResponse, skillDTO>().ReverseMap();

            CreateMap<LocationRequest, LocationDTO>().ReverseMap();
            CreateMap<LocationDTO, Domain.Models.Location>().ReverseMap();
            CreateMap<LocationResponse, LocationDTO>().ReverseMap();

            CreateMap<QualificationDTO, Qualification>().ReverseMap();
            CreateMap<QualificationRequest, QualificationDTO>();
            CreateMap<QualificationResponse, QualificationDTO>().ReverseMap();

            CreateMap<Experience_Request, ExperienceDTO>().ReverseMap();
            CreateMap<ExperienceDTO, Experience>().ReverseMap();
            CreateMap<ExperienceResponse, ExperienceDTO>().ReverseMap();

            CreateMap<IndustryRequest, IndustryDTO>().ReverseMap();
            CreateMap<IndustryDTO, Industry>().ReverseMap();
            CreateMap<IndustryResponse, IndustryDTO>().ReverseMap();

            CreateMap<CategoryRequest, CategoryDTO>().ReverseMap();
            CreateMap<CategoryDTO, JobCategory>().ReverseMap();
            CreateMap<CategoryResponse, CategoryDTO>().ReverseMap();

            CreateMap<JobTypeRequest, TypeDTO>().ReverseMap();
            CreateMap<TypeDTO, JobType>().ReverseMap();
            CreateMap<JobTypeResponse, TypeDTO>().ReverseMap();

            CreateMap<JobSeekerProfile, JobSeekerResponseDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.JobSeekerProfileId))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.JobSeeker != null && src.JobSeeker.SystemUser != null ? src.JobSeeker.SystemUser.FirstName : string.Empty))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.JobSeeker != null && src.JobSeeker.SystemUser != null ? src.JobSeeker.SystemUser.LastName : string.Empty))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.JobSeeker != null && src.JobSeeker.SystemUser != null ? src.JobSeeker.SystemUser.Email : string.Empty))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.JobSeeker != null && src.JobSeeker.SystemUser != null ? src.JobSeeker.SystemUser.Phone : string.Empty))
                .ForMember(dest => dest.SkillName, opt => opt.MapFrom(src => src.Skill != null ? src.Skill.SkillName : string.Empty))
                .ForMember(dest => dest.QualificationName, opt => opt.MapFrom(src => src.Qualification != null ? src.Qualification.Name : string.Empty))
                .ForMember(dest => dest.ExperienceName, opt => opt.MapFrom(src => src.Experience != null ? src.Experience.Name : string.Empty))
                .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location != null ? src.Location.Name : string.Empty)).ReverseMap();
            CreateMap<JobSeekerResponseDTO, JobSeekerProfileResponse>().ReverseMap();


            //JobSeeker part//

            CreateMap<Createrequest, Jobseekerprofiledto>();
            CreateMap<Updaterequest, Jobseekerprofiledto>();

            CreateMap<JobSeekerProfile, Jobseekerprofiledto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.JobSeeker != null && src.JobSeeker.SystemUser != null ? src.JobSeeker.SystemUser.FirstName : string.Empty))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.JobSeeker != null && src.JobSeeker.SystemUser != null ? src.JobSeeker.SystemUser.LastName : string.Empty))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.JobSeeker != null && src.JobSeeker.SystemUser != null ? src.JobSeeker.SystemUser.Email : string.Empty))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.JobSeeker != null && src.JobSeeker.SystemUser != null ? src.JobSeeker.SystemUser.Phone : string.Empty))
                .ForMember(dest => dest.SkillName, opt => opt.MapFrom(src => src.Skill != null ? src.Skill.SkillName : string.Empty))
                .ForMember(dest => dest.SkillDescription, opt => opt.MapFrom(src => src.Skill != null ? src.Skill.Description : string.Empty))
                .ForMember(dest => dest.QualificationName, opt => opt.MapFrom(src => src.Qualification != null ? src.Qualification.Name : string.Empty))
                .ForMember(dest => dest.QualificationDescription, opt => opt.MapFrom(src => src.Qualification != null ? src.Qualification.Description : string.Empty))
                .ForMember(dest => dest.ExperienceName, opt => opt.MapFrom(src => src.Experience != null ? src.Experience.Name : string.Empty))
                .ForMember(dest => dest.ExperienceDescription, opt => opt.MapFrom(src => src.Experience != null ? src.Experience.Description : string.Empty))
                .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location != null ? src.Location.Name : string.Empty))
                .ForMember(dest => dest.LocationDescription, opt => opt.MapFrom(src => src.Location != null ? src.Location.Description : string.Empty));
            CreateMap<Jobseekerprofiledto, Getprofileresponse>();

            // JobSeeker Jobs & Actions response mappings
            CreateMap<ApplyJobRequest, ApplyJobDTO>();
            CreateMap<SaveJobRequest, SaveJobDTO>();

            CreateMap<Domain.Models.JobPost, GetJobsResponse>()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company != null ? src.Company.ComapnyName : string.Empty))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Jobcategory != null ? src.Jobcategory.Name : string.Empty))
                .ForMember(dest => dest.JobType, opt => opt.MapFrom(src => src.JobType != null ? src.JobType.Name : string.Empty))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location != null ? src.Location.Name : string.Empty));

            CreateMap<Domain.Models.JobPost, SearchJobResponse>()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company != null ? src.Company.ComapnyName : string.Empty))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Jobcategory != null ? src.Jobcategory.Name : string.Empty))
                .ForMember(dest => dest.JobType, opt => opt.MapFrom(src => src.JobType != null ? src.JobType.Name : string.Empty))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location != null ? src.Location.Name : string.Empty));

            CreateMap<Domain.Models.JobApplication, ApplyJobResponse>()
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => "Job applied successfully"))
                .ForMember(dest => dest.JobSeekerProfileId, opt => opt.MapFrom(src => src.AppliedJob != null ? src.AppliedJob.JobSeekerProfileId : Guid.Empty))
                .ForMember(dest => dest.ApplicationStatus, opt => opt.MapFrom(src => (int)src.ApplicationStatus));

            CreateMap<Domain.Models.JobApplication, GetAppliedJobResponse>()
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.JobPost != null ? src.JobPost.Title : string.Empty))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.JobPost != null && src.JobPost.Company != null ? src.JobPost.Company.ComapnyName : string.Empty))
                .ForMember(dest => dest.ApplicationStatus, opt => opt.MapFrom(src => (int)src.ApplicationStatus))
                .ForMember(dest => dest.AppliedDate, opt => opt.MapFrom(src => src.AppliedJob != null ? src.AppliedJob.AppliedDate : src.ApplicationDate));

            CreateMap<SavedJob, SaveJobResponse>()
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => "Job saved successfully"));

            CreateMap<SavedJob, GetSavedJobResponse>()
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.JobPost != null ? src.JobPost.Title : string.Empty))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.JobPost != null && src.JobPost.Company != null ? src.JobPost.Company.ComapnyName : string.Empty));

            CreateMap<Domain.Models.InterviewSchedule, GetInterviewScheduleResponse>();


            // Job provider part
            CreateMap<ProviderProfileRequest, CreateJobProviderDto>().ReverseMap();
            CreateMap<ProviderProfileRequest, UpdateJobProviderDto>().ReverseMap();
            CreateMap<JobProvider, JobProviderDto>().ReverseMap();

            // Company mappings
            CreateMap<CreateCompanyRequest, CreateCompanyDto>().ReverseMap();
            CreateMap<UpdateCompanyRequest, UpdateCompanyDto>().ReverseMap();
            CreateMap<SelectCompanyRequest, SelectCompanyDto>().ReverseMap();
            CreateMap<Company, CompanyDto>().ReverseMap();
            CreateMap<CreateCompanyDto, Company>().ReverseMap();
            CreateMap<UpdateCompanyDto, Company>().ReverseMap();
            CreateMap<CompanyUser, CompanyUserDto>().ReverseMap();

            // Job post mappings
            CreateMap<CreateJobRequest, CreateJobPostDto>().ReverseMap();
            CreateMap<UpdateJobRequest, UpdateJobPostDto>().ReverseMap();
            CreateMap<CreateJobPostDto, JobPost>().ReverseMap();
            CreateMap<UpdateJobPostDto, JobPost>().ReverseMap();
            CreateMap<JobPost, JobPostResponseDto>().ReverseMap();
            CreateMap<JobPost, JobPostSummaryDto>().ReverseMap();

            // Job application mappings
            CreateMap<ApplicationStatusRequest, UpdateApplicationStatusDto>().ReverseMap();
            CreateMap<Domain.Models.JobApplication, JobApplicationDto>()
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.JobPost != null ? src.JobPost.Title : string.Empty))
                .ForMember(dest => dest.CandidateName, opt => opt.MapFrom(src => src.AppliedJob != null && src.AppliedJob.JobSeekerProfile != null && src.AppliedJob.JobSeekerProfile.JobSeeker != null && src.AppliedJob.JobSeekerProfile.JobSeeker.SystemUser != null ? src.AppliedJob.JobSeekerProfile.JobSeeker.SystemUser.FirstName + " " + src.AppliedJob.JobSeekerProfile.JobSeeker.SystemUser.LastName : string.Empty))
                .ForMember(dest => dest.CandidateEmail, opt => opt.MapFrom(src => src.AppliedJob != null && src.AppliedJob.JobSeekerProfile != null && src.AppliedJob.JobSeekerProfile.JobSeeker != null && src.AppliedJob.JobSeekerProfile.JobSeeker.SystemUser != null ? src.AppliedJob.JobSeekerProfile.JobSeeker.SystemUser.Email : string.Empty))
                .ForMember(dest => dest.CandidatePhone, opt => opt.MapFrom(src => src.AppliedJob != null && src.AppliedJob.JobSeekerProfile != null && src.AppliedJob.JobSeekerProfile.JobSeeker != null && src.AppliedJob.JobSeekerProfile.JobSeeker.SystemUser != null ? src.AppliedJob.JobSeekerProfile.JobSeeker.SystemUser.Phone : string.Empty))
                .ForMember(dest => dest.ResumeUrl, opt => opt.MapFrom(src => src.AppliedJob != null && src.AppliedJob.JobSeekerProfile != null ? src.AppliedJob.JobSeekerProfile.ResumeUrl : string.Empty))
                .ForMember(dest => dest.JobSeekerProfileId, opt => opt.MapFrom(src => src.AppliedJob != null ? src.AppliedJob.JobSeekerProfileId : Guid.Empty))
                .ForMember(dest => dest.ApplicationStatusName, opt => opt.MapFrom(src => src.ApplicationStatus.ToString()));

            CreateMap<Domain.Models.JobApplication, ApplicationDTO>()
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.JobPost != null ? src.JobPost.Title : string.Empty))
                .ForMember(dest => dest.JobDescription, opt => opt.MapFrom(src => src.JobPost != null ? src.JobPost.Description : string.Empty))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.AppliedJob != null && src.AppliedJob.JobSeekerProfile != null && src.AppliedJob.JobSeekerProfile.JobSeeker != null && src.AppliedJob.JobSeekerProfile.JobSeeker.SystemUser != null ? src.AppliedJob.JobSeekerProfile.JobSeeker.SystemUser.FirstName : string.Empty))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.AppliedJob != null && src.AppliedJob.JobSeekerProfile != null && src.AppliedJob.JobSeekerProfile.JobSeeker != null && src.AppliedJob.JobSeekerProfile.JobSeeker.SystemUser != null ? src.AppliedJob.JobSeekerProfile.JobSeeker.SystemUser.LastName : string.Empty))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppliedJob != null && src.AppliedJob.JobSeekerProfile != null && src.AppliedJob.JobSeekerProfile.JobSeeker != null && src.AppliedJob.JobSeekerProfile.JobSeeker.SystemUser != null ? src.AppliedJob.JobSeekerProfile.JobSeeker.SystemUser.Email : string.Empty))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.AppliedJob != null && src.AppliedJob.JobSeekerProfile != null && src.AppliedJob.JobSeekerProfile.JobSeeker != null && src.AppliedJob.JobSeekerProfile.JobSeeker.SystemUser != null ? src.AppliedJob.JobSeekerProfile.JobSeeker.SystemUser.Phone : string.Empty))
                .ForMember(dest => dest.ResumeUrl, opt => opt.MapFrom(src => src.AppliedJob != null && src.AppliedJob.JobSeekerProfile != null ? src.AppliedJob.JobSeekerProfile.ResumeUrl : string.Empty))
                .ForMember(dest => dest.SkillName, opt => opt.MapFrom(src => src.AppliedJob != null && src.AppliedJob.JobSeekerProfile != null && src.AppliedJob.JobSeekerProfile.Skill != null ? src.AppliedJob.JobSeekerProfile.Skill.SkillName : string.Empty))
                .ForMember(dest => dest.QualificationName, opt => opt.MapFrom(src => src.AppliedJob != null && src.AppliedJob.JobSeekerProfile != null && src.AppliedJob.JobSeekerProfile.Qualification != null ? src.AppliedJob.JobSeekerProfile.Qualification.Name : string.Empty))
                .ForMember(dest => dest.ExperienceName, opt => opt.MapFrom(src => src.AppliedJob != null && src.AppliedJob.JobSeekerProfile != null && src.AppliedJob.JobSeekerProfile.Experience != null ? src.AppliedJob.JobSeekerProfile.Experience.Name : string.Empty))
                .ForMember(dest => dest.ApplicationStatus, opt => opt.MapFrom(src => src.ApplicationStatus.ToString()));

            // Interview schedule mappings
            CreateMap<CreateInterviewRequest, CreateInterviewScheduleDto>().ReverseMap();
            CreateMap<UpdateInterviewRequest, UpdateInterviewScheduleDto>().ReverseMap();
            CreateMap<CreateInterviewScheduleDto, Domain.Models.InterviewSchedule>().ReverseMap();
            CreateMap<UpdateInterviewScheduleDto, Domain.Models.InterviewSchedule>().ReverseMap();
            CreateMap<Domain.Models.InterviewSchedule, InterviewScheduleDto>().ReverseMap();
        }
    }
}
