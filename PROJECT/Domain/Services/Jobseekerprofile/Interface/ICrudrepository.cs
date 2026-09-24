using Domain.Models;
using Domain.Services.Jobseekerprofile.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Jobseekerprofile.Interface
{
    public interface ICrudrepository
    {

        Task<JobSeeker?> GetJobSeekerBySystemUserId(
            Guid systemUserId);


        Task<JobSeekerProfile?> CreateAsync(
            JobSeekerProfile profile);

        Task<JobSeekerProfile?> UpdateProfile(
            Guid id,
            JobSeekerProfile jobseekerprofile);

        Task<bool> DeleteProfile(
            Guid profileId);

        Task<JobSeekerProfile?> GetProfileBySystemUserId(
     Guid systemUserId);






        Task<List<Skill>> GetAllSkills();

        Task<List<Qualification>> GetAllQualifications();

        Task<List<Experience>> GetAllExperiences();

        Task<List<Domain.Models.Location>> GetAllLocations();


        Task<IEnumerable<JobSeekerProfile>> GetAllJobSeekersAsync();

        Task<bool> DeleteJobSeekerAccount(Guid systemUserId);

    }
}
