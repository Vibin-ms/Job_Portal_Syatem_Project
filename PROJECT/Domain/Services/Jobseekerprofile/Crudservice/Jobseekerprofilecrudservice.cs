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


        // =====================================================
        // CREATE PROFILE
        // =====================================================

        public async Task<Jobseekerprofiledto?> CreateProfile(
            Jobseekerprofiledto dto,
            Guid systemUserId)
        {
            var jobSeeker =
                await repository.GetJobSeekerBySystemUserId(systemUserId);

            if (jobSeeker == null)
                return null;


            var profile = new JobSeekerProfile
            {
                JobSeekerProfileId = Guid.NewGuid(),

                JobSeekerId = jobSeeker.JobSeekerId,

                About = dto.About,

                ResumeUrl = dto.ResumeUrl,

                // Existing Admin master IDs
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


        // =====================================================
        // UPDATE PROFILE
        // =====================================================

        public async Task<Jobseekerprofiledto?> UpdateProfile(
            Guid id,
            Jobseekerprofiledto dto)
        {
            var profile =
                await repository.GetProfileById(id);

            if (profile == null)
                return null;


            // =========================
            // Profile
            // =========================

            profile.About = dto.About;

            profile.ResumeUrl = dto.ResumeUrl;


            // =========================
            // SystemUser
            // =========================

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


            // =========================
            // Existing Admin Master IDs
            // =========================

            profile.SkillId = dto.SkillId;

            profile.QualificationId =
                dto.QualificationId;

            profile.ExperienceId =
                dto.ExperienceId;

            profile.LocationId =
                dto.LocationId;


            var updatedProfile =
                await repository.UpdateProfile(
                    id,
                    profile);

            if (updatedProfile == null)
                return null;

            return mapper.Map<Jobseekerprofiledto>(
                updatedProfile);
        }


        // =====================================================
        // DELETE PROFILE
        // =====================================================

        public async Task<bool> DeleteProfile(Guid id)
        {
            return await repository.DeleteProfile(id);
        }


        // =====================================================
        // GET PROFILE
        // =====================================================

        public async Task<Jobseekerprofiledto?> GetProfileById(
            Guid id)
        {
            var profile =
                await repository.GetProfileById(id);

            if (profile == null)
                return null;

            return mapper.Map<Jobseekerprofiledto>(
                profile);
        }


        // =====================================================
        // GET ALL SKILLS
        // =====================================================

        public async Task<List<Skill>> GetAllSkills()
        {
            return await repository.GetAllSkills();
        }


        // =====================================================
        // GET ALL QUALIFICATIONS
        // =====================================================

        public async Task<List<Qualification>> GetAllQualifications()
        {
            return await repository.GetAllQualifications();
        }


        // =====================================================
        // GET ALL EXPERIENCES
        // =====================================================

        public async Task<List<Experience>> GetAllExperiences()
        {
            return await repository.GetAllExperiences();
        }


        // =====================================================
        // GET ALL LOCATIONS
        // =====================================================

        public async Task<List<Location>> GetAllLocations()
        {
            return await repository.GetAllLocations();
        }
    }
}