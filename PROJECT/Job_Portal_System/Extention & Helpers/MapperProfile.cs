using AutoMapper;
using Domain.Models;
using Domain.Services.Authentication.Dto;
using Domain.Services.Jobseekerprofile.Dto;
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

            //JobSeeker part//

            CreateMap<Createrequest, Jobseekerprofiledto>();
            CreateMap<Updaterequest, Jobseekerprofiledto>();

            CreateMap<JobSeekerProfile, Jobseekerprofiledto>().ReverseMap();
              




            //Jobprovider part//


        }












    }
}
