using Domain.Models;
using Domain.Services.Jobseekerprofile.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Jobseekerprofile.Interface
{
  public  interface ICrudservice
    {
        Task<Jobseekerprofiledto?> CreateProfile(
          Jobseekerprofiledto dto,
          Guid systemUserId);

        Task<Jobseekerprofiledto?> UpdateProfile(
            Guid id,
            Jobseekerprofiledto dto);

        Task<bool> DeleteProfile(Guid id);

        Task<Jobseekerprofiledto?> GetProfileById(Guid id);

        Task<List<Skill>> GetAllSkills();

        Task<List<Qualification>> GetAllQualifications();

        Task<List<Experience>> GetAllExperiences();

        Task<List<Location>> GetAllLocations();

    }
}
