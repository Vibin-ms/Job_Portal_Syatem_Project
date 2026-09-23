using Domain.Models;
using Domain.Services.Jobseeker.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Jobseeker.Interface
{
    public interface IJobSeekerServices
    {
        Task<List<Domain.Models.JobPost>> GetAllJobsAsync();

        Task<List<Domain.Models.JobPost>> SearchJobAsync(string name);

        Task<JobApplication?> ApplyJobAsync(ApplyJobDTO dto);

        Task<List<JobApplication>> GetAppliedJobsAsync();

        Task<SavedJob?> SaveJobAsync(SaveJobDTO dto);
        Task<List<SavedJob>> GetSavedJobsAsync();

        Task<bool> RemoveSavedJobAsync(Guid savedJobId);

        Task<List<InterviewSchedule>> GetInterviewSchedulesAsync();
    }
}
