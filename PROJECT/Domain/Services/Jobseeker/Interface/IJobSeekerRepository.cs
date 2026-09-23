using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Jobseeker.Interface
{
    public interface IJobSeekerRepository
    {
        Task<List<Domain.Models.JobPost>> GetAllJobsAsync();

        Task<List<Domain.Models.JobPost>> SearchJobAsync(string name);


        Task<Guid?> GetJobSeekerProfileIdAsync(Guid systemUserId);

        Task<Domain.Models.JobApplication?> ApplyJobAsync(Guid jobPostId,Guid jobSeekerProfileId);

        Task<List<Domain.Models.JobApplication>> GetAppliedJobsAsync(Guid jobSeekerProfileId);

        Task<SavedJob?> SaveJobAsync(Guid jobPostId,Guid jobSeekerProfileId);

        Task<List<SavedJob>> GetSavedJobsAsync(Guid jobSeekerProfileId);

        Task<bool> RemoveSavedJobAsync(  Guid savedJobId,Guid jobSeekerProfileId);

        Task<List<Domain.Models.InterviewSchedule>> GetInterviewSchedulesAsync(Guid jobSeekerProfileId);

    }
}
