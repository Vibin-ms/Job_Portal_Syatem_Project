using Domain.Data;
using Domain.Helpers.EmailInterface;
using Domain.Helpers.EmailServicefolder;
using Domain.Services.AcceptORRejectCompany.Interface;
using Domain.Services.AcceptORRejectCompany.Services;
using Domain.Services.Authentication.Interface;
using Domain.Services.Authentication.JWT.Interface;
using Domain.Services.Authentication.JWT.Service;
using Domain.Services.Authentication.Repository;
using Domain.Services.Authentication.Service;
using Domain.Services.CompanyProfile.Interface;
using Domain.Services.CompanyProfile.Services;
using Domain.Services.Experiences.Interface;
using Domain.Services.Experiences.services;
using Domain.Services.Industrys.Interface;
using Domain.Services.Industrys.services;
using Domain.Services.InterviewSchedules.Interface;
using Domain.Services.InterviewSchedules.Services;
using Domain.Services.JobApplications.Interface;
using Domain.Services.JobApplications.Services;
using Domain.Services.JobCategorys.Interface;
using Domain.Services.JobCategorys.Services;
using Domain.Services.JobPost;
using Domain.Services.JobPost.Interface;
using Domain.Services.JobPost.Services;
using Domain.Services.JobProvider;
using Domain.Services.JobProvider.Interface;
using Domain.Services.JobProviderProfile.Interface;
using Domain.Services.JobProviderProfile.Services;
using Domain.Services.Jobseeker.Interface;
using Domain.Services.Jobseeker.Services;
using Domain.Services.Jobseekerprofile.Crudrepository;
using Domain.Services.Jobseekerprofile.Crudservice;
using Domain.Services.Jobseekerprofile.Interface;
using Domain.Services.JobTypes.Interfaces;
using Domain.Services.JobTypes.Services;
using Domain.Services.Location.Interface;
using Domain.Services.Location.Services;
using Domain.Services.Qualifications.Interface;
using Domain.Services.Qualifications.Services;
using Domain.Services.Skills.Interface;
using Domain.Services.Skills.Services;
using Microsoft.EntityFrameworkCore;

namespace Job_Portal_System.Extention___Helpers
{
    public static class AppExtentions
    {
        public static IServiceCollection AppServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddHttpContextAccessor();

            services.AddAutoMapper(cfg => { }, typeof(MapperProfile));

            //Authentication and Authorization
            services.AddScoped<IAuthrepository, AuthenticationRepository>();
            services.AddScoped<IAuthservice, Authenticationservice>();
            services.AddScoped<IJwtservice, Jwtservice>();
            services.AddScoped<IEmailservice, Emailserviceclass>();
            services.AddDbContext<AppDbContext>(cfg => cfg.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            //Admin Part
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<ISkillServices, SkillServices>();

            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<ILocationServices, LocationServices>();

            services.AddScoped<IQualificationRepository, QualificationRepository>();
            services.AddScoped<IQualificationServices, QualificationServices>();

            services.AddScoped<IExperienceRepository, ExperienceRepository>();
            services.AddScoped<IExperienceServices, Experienceservices>();

            services.AddScoped<IIndustryRepository, IndustryRepository>();
            services.AddScoped<IIndustryServices, IndustryServices>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryServices, CategoryServices>();

            services.AddScoped<IJobTypeRepository, JobTypeRepository>();
            services.AddScoped<IJobTypeServices, JobTypeServices>();

            services.AddScoped<IAcceptORRejectRepository, AcceptORRejectRepository>();
            services.AddScoped<IAccpetORRejectServices, AcceptORRejectServices>();

            services.AddScoped<IJobProviderRepository, Domain.Services.JobProviderProfile.Services.JobProviderRepository>();
            services.AddScoped<IJobProviderServices, Domain.Services.JobProviderProfile.Services.JobProviderServices>();

            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanySerices, CompanyServices>();

            services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();
            services.AddScoped<IApplicationRepository, JobApplicationRepository>();

            services.AddScoped<IJobApplicationService, JobApplicationService>();
            services.AddScoped<IApplicationServices, JobApplicationService>();

            services.AddScoped<IJobPostRepository, Domain.Services.JobPost.Services.JobPostRepository>();
            services.AddScoped<IJobPostServices, Domain.Services.JobPost.Services.JobPostServices>();

            //JobSeeker part
            services.AddScoped<ICrudrepository, Jobseekerprofilecrudrepository>();
            services.AddScoped<ICrudservice, Jobseekerprofilecrudservice>();

            services.AddScoped<IJobSeekerRepository, Domain.Services.Jobseeker.Services.JobSeekerRepository>();
            services.AddScoped<IJobSeekerServices, Domain.Services.Jobseeker.Services.JobSeekerServices>();

            //JobProvider Part
            services.AddScoped<IJobProviderRepo, Domain.Services.JobProvider.JobProviderRepository>();
            services.AddScoped<IJobProviderService, JobProviderService>();

            services.AddScoped<IJobPostRepo, Domain.Services.JobPost.JobPostRepository>();
            services.AddScoped<IJobPostService, JobPostService>();

            services.AddScoped<IInterviewScheduleRepo, InterviewScheduleRepository>();
            services.AddScoped<IInterviewScheduleService, InterviewScheduleService>();

            return services;
        }
    }
}
