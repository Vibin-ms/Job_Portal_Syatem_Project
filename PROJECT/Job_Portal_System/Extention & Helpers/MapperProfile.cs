using AutoMapper;
using Domain.Models;
using Domain.Services.Authentication.Dto;
using Domain.Services.CompanyProfile.DTO;
using Domain.Services.Experiences.DTO;
using Domain.Services.Industrys.DTO;
using Domain.Services.JobApplications.DTO;
using Domain.Services.JobCategorys.DTO;
using Domain.Services.JobPost.DTO;
using Domain.Services.JobProviderProfile.DTO;
using Domain.Services.Jobseekerprofile.Dto;
using Domain.Services.JobTypes.DTO;
using Domain.Services.Location.DTO;
using Domain.Services.Qualifications.DTO;
using Domain.Services.Skills.DTO;
using Job_Portal_System.API.AdminController;
using Job_Portal_System.API.AdminController.Request___Response_Body;
using Job_Portal_System.API.Authentication_Controller.Register;
using Job_Portal_System.API.JobSeekeerController.Request___Response_Body.Jobseekerprofile;
using static Domain.Services.Jobseekerprofile.Dto.Jobseekerprofiledto;

namespace Job_Portal_System.Extention___Helpers
{
    public class MapperProfile:Profile
    {
       

        public MapperProfile()
        {
            //Authentication part//

            CreateMap<Registerrequest, SystemuserDto>();
            CreateMap<SystemuserDto ,SystemUser>();
            CreateMap<SystemUser, SystemuserDto>(); 
             CreateMap<SystemuserDto, Registerresponse>() .ForMember( dest => dest.Message, opt => opt.MapFrom( src => "User registered successfully" ) );


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
    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.JobSeeker.SystemUser.FirstName))
    .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.JobSeeker.SystemUser.LastName))
    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.JobSeeker.SystemUser.Email))
    .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.JobSeeker.SystemUser.Phone))
    .ForMember(dest => dest.SkillName, opt => opt.MapFrom(src => src.Skill.SkillName))
    .ForMember(dest => dest.QualificationName, opt => opt.MapFrom(src => src.Qualification.Name))
    .ForMember(dest => dest.ExperienceName, opt => opt.MapFrom(src => src.Experience.Name))
    .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location.Name)).ReverseMap();
            CreateMap<JobSeekerResponseDTO, JobSeekerProfileResponse>().ReverseMap();


            CreateMap<JobProvider, ProviderResponseDTO>()
                .ForMember(dest => dest.JobProviderId, opt => opt.MapFrom(src => src.JobProviderId))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.SystemUser.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.SystemUser.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.SystemUser.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.SystemUser.Phone))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.SystemUser.Roles.ToString())).
                ReverseMap();
            CreateMap<ProviderResponseDTO, JobProviderResponse>().ReverseMap();


            CreateMap<Company, CompanyProfileDTO>().ForMember(dest => dest.Industry, opt => opt.MapFrom(src => src.Industry.Name))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location.Name)).ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString())).ReverseMap();
            CreateMap<CompanyProfileDTO, CompanyResponse>().ReverseMap();

            CreateMap<JobPost, JobPostDTO>().ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company.ComapnyName))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location.Name)).ForMember(dest => dest.Jobcategory, opt => opt.MapFrom(src => src.Jobcategory.Name))
                .ForMember(dest => dest.JobType, opt => opt.MapFrom(src => src.JobType.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString())).ReverseMap();
            CreateMap<JobPostDTO, JobPostResponse>().ReverseMap();


            CreateMap<JobApplication, ApplicationDTO>().ForMember(dest => dest.JobApplicationId, opt => opt.MapFrom(src => src.JobApplicationId)).
                ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.JobPost.Title)).ForMember(dest => dest.JobDescription, opt => opt.MapFrom(src => src.JobPost.Description)).
                ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.AppliedJob.JobSeekerProfile.JobSeeker.SystemUser.FirstName)).ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.AppliedJob.JobSeekerProfile.JobSeeker.SystemUser.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppliedJob.JobSeekerProfile.JobSeeker.SystemUser.Email)).ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.AppliedJob.JobSeekerProfile.JobSeeker.SystemUser.Phone))
                .ForMember(dest => dest.ResumeUrl, opt => opt.MapFrom(src => src.AppliedJob.JobSeekerProfile.ResumeUrl)).ForMember(dest => dest.SkillName, opt => opt.MapFrom(src => src.AppliedJob.JobSeekerProfile.Skill.SkillName))
                .ForMember(dest => dest.QualificationName, opt => opt.MapFrom(src => src.AppliedJob.JobSeekerProfile.Qualification.Name)).ForMember(dest => dest.ExperienceName, opt => opt.MapFrom(src => src.AppliedJob.JobSeekerProfile.Experience.Name))
                .ForMember(dest => dest.ApplicationStatus, opt => opt.MapFrom(src => src.ApplicationStatus.ToString())).ForMember(dest => dest.ApplicationDate, opt => opt.MapFrom(src => src.ApplicationDate)).ReverseMap();
            CreateMap<ApplicationDTO, ApplicationResponse>().ReverseMap();





            //JobSeeker part//


            CreateMap<Createrequest, Jobseekerprofiledto>();

            
            CreateMap<Updaterequest, Jobseekerprofiledto>();


         
            CreateMap<JobSeekerProfile, Jobseekerprofiledto>()

               

                .ForMember(
                    dest => dest.FirstName,
                    opt => opt.MapFrom(
                        src => src.JobSeeker.SystemUser.FirstName))

                .ForMember(
                    dest => dest.LastName,
                    opt => opt.MapFrom(
                        src => src.JobSeeker.SystemUser.LastName))

                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(
                        src => src.JobSeeker.SystemUser.Email))

                .ForMember(
                    dest => dest.Phone,
                    opt => opt.MapFrom(
                        src => src.JobSeeker.SystemUser.Phone))



                .ForMember(
                    dest => dest.SkillName,
                    opt => opt.MapFrom(
                        src => src.Skill.SkillName))

                .ForMember(
                    dest => dest.SkillDescription,
                    opt => opt.MapFrom(
                        src => src.Skill.Description))


            

                .ForMember(
                    dest => dest.QualificationName,
                    opt => opt.MapFrom(
                        src => src.Qualification.Name))

                .ForMember(
                    dest => dest.QualificationDescription,
                    opt => opt.MapFrom(
                        src => src.Qualification.Description))


                .ForMember(
                    dest => dest.ExperienceName,
                    opt => opt.MapFrom(
                        src => src.Experience.Name))

                .ForMember(
                    dest => dest.ExperienceDescription,
                    opt => opt.MapFrom(
                        src => src.Experience.Description))


              
                .ForMember(
                    dest => dest.LocationName,
                    opt => opt.MapFrom(
                        src => src.Location.Name))

                .ForMember(
                    dest => dest.LocationDescription,
                    opt => opt.MapFrom(
                        src => src.Location.Description));
            CreateMap<Jobseekerprofiledto, Getprofileresponse>();
        }
     
        //Jobprovider part//


    












    }
}
