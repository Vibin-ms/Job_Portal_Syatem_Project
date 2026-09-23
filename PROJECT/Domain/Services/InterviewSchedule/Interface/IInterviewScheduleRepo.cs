using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services.InterviewSchedules.Interface
{
    public interface IInterviewScheduleRepo
    {
        Task<Models.JobProvider?> GetProviderBySystemUserIdAsync(Guid systemUserId);
        Task<Models.JobApplication?> GetApplicationWithJobPostAsync(Guid applicationId);
        Task UpdateApplicationStatusAsync(Guid applicationId, int status);
        Task<Models.InterviewSchedule?> GetByIdAsync(Guid interviewId);
        Task<IEnumerable<Models.InterviewSchedule>> GetByProviderIdAsync(Guid providerId);
        Task<Models.InterviewSchedule> CreateInterviewAsync(Models.InterviewSchedule interview);
        Task UpdateInterviewAsync(Models.InterviewSchedule interview);
        Task<bool> DeleteInterviewAsync(Guid interviewId);
    }
}
