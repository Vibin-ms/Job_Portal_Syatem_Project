using Domain.Data;
using Domain.Models;
using Domain.Services.Jobseekerprofile.Interface;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services.Jobseekerprofile.Crudrepository
{
    public class Jobseekerprofilecrudrepository : ICrudrepository
    {
        private readonly AppDbContext context;

        public Jobseekerprofilecrudrepository(AppDbContext _context)
        {
            context = _context;
        }

        // =====================================================
        // CREATE PROFILE
        // =====================================================

        public async Task<JobSeekerProfile?> CreateAsync(
            JobSeekerProfile profile)
        {
            await using var transaction =
                await context.Database.BeginTransactionAsync();

            try
            {
                // Generate Profile ID
                profile.JobSeekerProfileId = Guid.NewGuid();

                // Skill, Qualification, Experience and Location
                // are already created by Admin.
                // Only their existing IDs are saved in JobSeekerProfile.

                await context.JobSeekerProfiles.AddAsync(profile);

                await context.SaveChangesAsync();

                await transaction.CommitAsync();

                return profile;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        // =====================================================
        // UPDATE PROFILE
        // =====================================================

        public async Task<JobSeekerProfile?> UpdateProfile(
            Guid id,
            JobSeekerProfile jobseekerprofile)
        {
            await using var transaction =
                await context.Database.BeginTransactionAsync();

            try
            {
                var existingProfile =
                    await context.JobSeekerProfiles
                        .Include(x => x.JobSeeker)
                            .ThenInclude(x => x.SystemUser)
                        .FirstOrDefaultAsync(x =>
                            x.JobSeekerProfileId == id);

                if (existingProfile == null)
                    return null;


                // =================================================
                // PROFILE DETAILS
                // =================================================

                existingProfile.About =
                    jobseekerprofile.About;

                existingProfile.ResumeUrl =
                    jobseekerprofile.ResumeUrl;


                // =================================================
                // SYSTEM USER DETAILS
                // =================================================

                if (existingProfile.JobSeeker?.SystemUser != null &&
                    jobseekerprofile.JobSeeker?.SystemUser != null)
                {
                    existingProfile.JobSeeker.SystemUser.FirstName =
                        jobseekerprofile.JobSeeker.SystemUser.FirstName;

                    existingProfile.JobSeeker.SystemUser.LastName =
                        jobseekerprofile.JobSeeker.SystemUser.LastName;

                    existingProfile.JobSeeker.SystemUser.Email =
                        jobseekerprofile.JobSeeker.SystemUser.Email;

                    existingProfile.JobSeeker.SystemUser.Phone =
                        jobseekerprofile.JobSeeker.SystemUser.Phone;
                }


                // =================================================
                // ADMIN MASTER DATA
                // =================================================
                // We are NOT updating Skill, Qualification,
                // Experience or Location tables.
                //
                // We only change the selected IDs.
                // =================================================

                existingProfile.SkillId =
                    jobseekerprofile.SkillId;

                existingProfile.QualificationId =
                    jobseekerprofile.QualificationId;

                existingProfile.ExperienceId =
                    jobseekerprofile.ExperienceId;

                existingProfile.LocationId =
                    jobseekerprofile.LocationId;


                await context.SaveChangesAsync();

                await transaction.CommitAsync();

                return existingProfile;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        // =====================================================
        // DELETE PROFILE
        // =====================================================

        public async Task<bool> DeleteProfile(Guid profileId)
        {
            await using var transaction =
                await context.Database.BeginTransactionAsync();

            try
            {
                var profile =
                    await context.JobSeekerProfiles
                        .FirstOrDefaultAsync(x =>
                            x.JobSeekerProfileId == profileId);

                if (profile == null)
                    return false;


                // ONLY DELETE JOB SEEKER PROFILE
                //
                // DO NOT DELETE:
                // Skill
                // Qualification
                // Experience
                // Location
                // JobSeeker
                // SystemUser
                // AuthUser

                context.JobSeekerProfiles.Remove(profile);

                await context.SaveChangesAsync();

                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        // =====================================================
        // GET PROFILE BY PROFILE ID
        // =====================================================

        public async Task<JobSeekerProfile?> GetProfileById(Guid id)
        {
            return await context.JobSeekerProfiles

                .Include(x => x.JobSeeker)
                    .ThenInclude(x => x.SystemUser)

                .Include(x => x.Skill)

                .Include(x => x.Qualification)

                .Include(x => x.Experience)

                .Include(x => x.Location)

                .FirstOrDefaultAsync(x =>
                    x.JobSeekerProfileId == id);
        }


        // =====================================================
        // GET JOB SEEKER BY SYSTEM USER ID
        // =====================================================

        public async Task<JobSeeker?> GetJobSeekerBySystemUserId(
            Guid systemUserId)
        {
            return await context.JobSeekers
                .Include(x => x.SystemUser)
                .FirstOrDefaultAsync(x =>
                    x.SystemUserId == systemUserId);
        }


        // =====================================================
        // GET ALL SKILLS
        // =====================================================

        public async Task<List<Skill>> GetAllSkills()
        {
            return await context.Skills
                .AsNoTracking()
                .ToListAsync();
        }


        // =====================================================
        // GET ALL QUALIFICATIONS
        // =====================================================

        public async Task<List<Qualification>> GetAllQualifications()
        {
            return await context.Qualifications
                .AsNoTracking()
                .ToListAsync();
        }


        // =====================================================
        // GET ALL EXPERIENCES
        // =====================================================

        public async Task<List<Experience>> GetAllExperiences()
        {
            return await context.Experiences
                .AsNoTracking()
                .ToListAsync();
        }


        // =====================================================
        // GET ALL LOCATIONS
        // =====================================================

        public async Task<List<Domain.Models.Location>> GetAllLocations()
        {
            return await context.Locations
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IEnumerable<JobSeekerProfile>> GetAllJobSeekersAsync()
        {
            var jobseekers = await context.JobSeekerProfiles.
                Include(x => x.JobSeeker).ThenInclude(x => x.SystemUser).Include(x=>x.Skill).
                Include(x=>x.Qualification).Include(x=>x.Experience).Include(x=>x.Location).ToListAsync();
            return jobseekers;
        }
    }
}