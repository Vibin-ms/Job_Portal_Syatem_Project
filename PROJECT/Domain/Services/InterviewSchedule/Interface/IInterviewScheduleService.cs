using Domain.Services.InterviewSchedules.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services.InterviewSchedules.Interface
{
    public interface IInterviewScheduleService
    {
        Task<InterviewScheduleDto> ScheduleInterviewAsync(Guid systemUserId, CreateInterviewScheduleDto dto);
        Task<IEnumerable<InterviewScheduleDto>> GetInterviewsForProviderAsync(Guid systemUserId);
        Task<InterviewScheduleDto?> GetInterviewByIdAsync(Guid systemUserId, Guid interviewId);
        Task<InterviewScheduleDto?> UpdateInterviewAsync(Guid systemUserId, Guid interviewId, UpdateInterviewScheduleDto dto);
        Task<bool> CancelInterviewAsync(Guid systemUserId, Guid interviewId);
    }
}
