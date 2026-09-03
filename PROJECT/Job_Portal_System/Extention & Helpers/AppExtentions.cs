using Domain.Services.Skills.Interface;
using Domain.Services.Skills.Services;
using Org.BouncyCastle.Asn1;

namespace Job_Portal_System.Extention___Helpers
{
    public static class AppExtentions
    {
        public static IServiceCollection AppServices(this IServiceCollection services , IConfiguration config)
        {
            
            services.AddAutoMapper(cfg => { }, typeof(MapperProfile));
            //Admin Part//
            services.AddScoped<ISkillRepository , SkillRepository>();
            services.AddScoped<ISkillServices , SkillServices>();





            //JobSeeker part//






            //JobProvider Part//
            return services ;
        }
    }
}
