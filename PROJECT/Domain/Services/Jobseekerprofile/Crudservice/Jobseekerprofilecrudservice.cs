
using AutoMapper;
using Domain.Models;
using Domain.Services.Jobseekerprofile.Dto;
using Domain.Services.Jobseekerprofile.Interface;

namespace Domain.Services.Jobseekerprofile.Crudservice
{
    public class Jobseekerprofilecrudservice : ICrudservice
    {
        private readonly ICrudrepository repository;
        private readonly IMapper mapper;

        public Jobseekerprofilecrudservice(
            ICrudrepository _repository,
            IMapper _mapper)
        {
            repository = _repository;
            mapper = _mapper;
        }


       
      

        public async Task<Jobseekerprofiledto?> CreateProfile(
            Jobseekerprofiledto dto,
            Guid systemUserId)
        {
            var jobSeeker =
                await repository.GetJobSeekerBySystemUserId(systemUserId);

            if (jobSeeker == null)
                return null;

            var existingProfile =
                await repository.GetProfileBySystemUserId(systemUserId);
            if (existingProfile != null)
            {
                if (existingProfile.JobSeeker?.SystemUser != null)
                {
                    dto.FirstName = existingProfile.JobSeeker.SystemUser.FirstName;
                    dto.LastName = existingProfile.JobSeeker.SystemUser.LastName;
                    dto.Email = existingProfile.JobSeeker.SystemUser.Email;
                    dto.Phone = existingProfile.JobSeeker.SystemUser.Phone;
                }
                return await UpdateProfile(systemUserId, dto);
            }


            var profile = new JobSeekerProfile
            {
                JobSeekerProfileId = Guid.NewGuid(),

                JobSeekerId = jobSeeker.JobSeekerId,

                About = dto.About,

                ResumeUrl = dto.ResumeUrl,

              
                SkillId = dto.SkillId,

                QualificationId = dto.QualificationId,

                ExperienceId = dto.ExperienceId,

                LocationId = dto.LocationId
            };


            var createdProfile =
                await repository.CreateAsync(profile);

            if (createdProfile == null)
                return null;

            return mapper.Map<Jobseekerprofiledto>(
                createdProfile);
        }




        public async Task<Jobseekerprofiledto?> UpdateProfile(
     Guid systemUserId,
     Jobseekerprofiledto dto)
        {
            var profile =
                await repository.GetProfileBySystemUserId(systemUserId);

            if (profile == null)
                return null;


            profile.About = dto.About;

            profile.ResumeUrl = dto.ResumeUrl;


          

            if (profile.JobSeeker?.SystemUser != null)
            {
                profile.JobSeeker.SystemUser.FirstName =
                    dto.FirstName;

                profile.JobSeeker.SystemUser.LastName =
                    dto.LastName;

                profile.JobSeeker.SystemUser.Email =
                    dto.Email;

                profile.JobSeeker.SystemUser.Phone =
                    dto.Phone;
            }


           

            profile.SkillId = dto.SkillId;

            profile.QualificationId =
                dto.QualificationId;

            profile.ExperienceId =
                dto.ExperienceId;

            profile.LocationId =
                dto.LocationId;


            var updatedProfile =
                await repository.UpdateProfile(
                    profile.JobSeekerProfileId,
                    profile);

            if (updatedProfile == null)
                return null;

            return mapper.Map<Jobseekerprofiledto>(
                updatedProfile);
        }



        public async Task<bool> DeleteProfile(Guid id)
        {
            return await repository.DeleteProfile(id);
        }


      
        public async Task<Jobseekerprofiledto?> GetProfileBySystemUserId(
     Guid systemUserId)
        {
            var profile =
                await repository.GetProfileBySystemUserId(systemUserId);

            if (profile == null)
                return null;

            return mapper.Map<Jobseekerprofiledto>(profile);
        }


       

        public async Task<List<Skill>> GetAllSkills()
        {
            return await repository.GetAllSkills();
        }


      

        public async Task<List<Qualification>> GetAllQualifications()
        {
            return await repository.GetAllQualifications();
        }


       
       

        public async Task<List<Experience>> GetAllExperiences()
        {
            return await repository.GetAllExperiences();
        }


       
        
        // =====================================================
        // GET ALL LOCATIONS
        // =====================================================

        public async Task<List<Domain.Models.Location>> GetAllLocations()
        {
            return await repository.GetAllLocations();
        }
        public async Task<bool> DeleteJobSeekerAccount(Guid systemUserId)
        {
            return await repository.DeleteJobSeekerAccount(systemUserId);
        }

        public async Task<IEnumerable<JobSeekerResponseDTO>> GetAllJobSeekerAsync()
        {
            var jobseeker = await repository.GetAllJobSeekersAsync();
            return mapper.Map<IEnumerable<JobSeekerResponseDTO>>(jobseeker);
        }
        public async Task<bool> DeleteJobSeekerAccount(Guid systemUserId)
        {
            return await repository.DeleteJobSeekerAccount(systemUserId);
        }
    }
}