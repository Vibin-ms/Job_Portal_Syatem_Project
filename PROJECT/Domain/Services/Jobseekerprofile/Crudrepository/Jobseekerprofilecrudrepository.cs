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


        public async Task<JobSeekerProfile?> CreateAsync(
            JobSeekerProfile profile)
        {
            await using var transaction =
                await context.Database.BeginTransactionAsync();

            try
            {
                
                profile.JobSeekerProfileId = Guid.NewGuid();

                

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


                

                existingProfile.About =
                    jobseekerprofile.About;

                existingProfile.ResumeUrl =
                    jobseekerprofile.ResumeUrl;


               

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


        

        public async Task<JobSeeker?> GetJobSeekerBySystemUserId(
            Guid systemUserId)
        {
            return await context.JobSeekers
                .Include(x => x.SystemUser)
                .FirstOrDefaultAsync(x =>
                    x.SystemUserId == systemUserId);
        }


       

        public async Task<List<Skill>> GetAllSkills()
        {
            return await context.Skills
                .AsNoTracking()
                .ToListAsync();
        }


      
        public async Task<List<Qualification>> GetAllQualifications()
        {
            return await context.Qualifications
                .AsNoTracking()
                .ToListAsync();
        }


       
        public async Task<List<Experience>> GetAllExperiences()
        {
            return await context.Experiences
                .AsNoTracking()
                .ToListAsync();
        }


<<<<<<<<< Temporary merge branch 1
        // =====================================================
        // GET ALL LOCATIONS
        // =====================================================

        public async Task<List<Domain.Models.Location>> GetAllLocations()
        {
            return await context.Locations
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<JobSeekerProfile?> GetProfileBySystemUserId(
    Guid systemUserId)
        {
            return await context.JobSeekerProfiles
                .Include(x => x.JobSeeker)
                    .ThenInclude(x => x.SystemUser)

                .Include(x => x.Skill)

                .Include(x => x.Qualification)

                .Include(x => x.Experience)

                .Include(x => x.Location)

                .FirstOrDefaultAsync(x =>
                    x.JobSeeker.SystemUserId == systemUserId);
        }
        public async Task<bool> DeleteJobSeekerAccount(Guid systemUserId)
        {
            await using var transaction =
                await context.Database.BeginTransactionAsync();

            try
            {
                
                var jobSeeker = await context.JobSeekers
                    .FirstOrDefaultAsync(x => x.SystemUserId == systemUserId);

                if (jobSeeker == null)
                {
                    return false;
                }

                var profile = await context.JobSeekerProfiles
                    .FirstOrDefaultAsync(x => x.JobSeekerId == jobSeeker.JobSeekerId);

                if (profile != null)
                {
                    
                    var savedJobs = await context.SavedJobs
                        .Where(x => x.JobSeekerProfileId == profile.JobSeekerProfileId)
                        .ToListAsync();

                    if (savedJobs.Any())
                    {
                        context.SavedJobs.RemoveRange(savedJobs);
                    }

                 
                    var appliedJobs = await context.AppliedJobs
                        .Where(x => x.JobSeekerProfileId == profile.JobSeekerProfileId)
                        .ToListAsync();

                    if (appliedJobs.Any())
                    {
                        context.AppliedJobs.RemoveRange(appliedJobs);
                    }

                   
                    context.JobSeekerProfiles.Remove(profile);
                }

               
                var authUser = await context.AuthUsers
                    .FirstOrDefaultAsync(x => x.SystemUserId == systemUserId);

                if (authUser != null)
                {
                    context.AuthUsers.Remove(authUser);
                }

               
                context.JobSeekers.Remove(jobSeeker);

              
                var systemUser = await context.SystemUsers
                    .FirstOrDefaultAsync(x => x.Id == systemUserId);

                if (systemUser != null)
                {
                    context.SystemUsers.Remove(systemUser);
                }

                
                await context.SaveChangesAsync();

              
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
>>>>>>>>> Temporary merge branch 2
        }
    }
}