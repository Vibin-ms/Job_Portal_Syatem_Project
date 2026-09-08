using Domain.Data;
using Domain.Helpers.EmailInterface;

using Domain.Helpers.EmailServicefolder;
using Domain.Services.Authentication.Interface;
using Domain.Services.Authentication.JWT.Interface;
using Domain.Services.Authentication.JWT.Service;
using Domain.Services.Authentication.Repository;
using Domain.Services.Authentication.Service;
using Domain.Services.Jobseekerprofile.Crudrepository;
using Domain.Services.Jobseekerprofile.Crudservice;
using Domain.Services.Jobseekerprofile.Interface;
using Domain.Services.Skills.Interface;
using Domain.Services.Skills.Services;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1;

namespace Job_Portal_System.Extention___Helpers
{
    public static class AppExtentions
    {
        public static IServiceCollection AppServices(this IServiceCollection services, IConfiguration config)
        {

            services.AddAutoMapper(cfg => { }, typeof(MapperProfile));
            //Authentication and Authorization//
            services.AddScoped<IAuthrepository,AuthenticationRepository>();
            services.AddScoped<IAuthservice, Authenticationservice>();
            services.AddScoped<IJwtservice, Jwtservice>();
            services.AddScoped<IEmailservice, Emailserviceclass>();
            services.AddDbContext<AppDbContext>(cfg =>cfg.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            //Admin Part//
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<ISkillServices, SkillServices>();





            //JobSeeker part//

            services.AddScoped<ICrudrepository, Jobseekerprofilecrudrepository>();
            services.AddScoped<ICrudservice, Jobseekerprofilecrudservice>();




            //JobProvider Part//
            return services;
        }
    }
}
