using AutoMapper;
using Domain.Enum;
using Domain.Services.InterviewSchedule.Dto;
using Domain.Services.InterviewSchedule.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Services.InterviewSchedule
{
    public class InterviewScheduleService : IInterviewScheduleService
    {
        private readonly IInterviewScheduleRepo repository;
        private readonly IMapper mapper;

        public InterviewScheduleService(IInterviewScheduleRepo _repository, IMapper _mapper)
        {
            repository = _repository;
            mapper = _mapper;
        }

        public async Task<InterviewScheduleDto> ScheduleInterviewAsync(Guid systemUserId, CreateInterviewScheduleDto dto)
        {
            var provider = await repository.GetProviderBySystemUserIdAsync(systemUserId);
            if (provider == null)
            {
                throw new KeyNotFoundException("Job provider profile not found.");
            }

            var application = await repository.GetApplicationWithJobPostAsync(dto.JobApplicationId);
            if (application == null || application.JobPost.JobProviderId != provider.JobProviderId)
            {
                throw new InvalidOperationException("Invalid application or application does not belong to your job post.");
            }

            var interview = mapper.Map<Models.InterviewSchedule>(dto);
            interview.InterviewScheduleId = Guid.NewGuid();
            interview.JobProviderId = provider.JobProviderId;
            interview.Status = 1; // Scheduled
            interview.CreatedDate = DateTime.UtcNow;

            await repository.CreateInterviewAsync(interview);
            await repository.UpdateApplicationStatusAsync(dto.JobApplicationId, (int)ApplicationStatus.InterviewScheduled);

            var reloaded = await repository.GetByIdAsync(interview.InterviewScheduleId);
            return mapper.Map<InterviewScheduleDto>(reloaded ?? interview);
        }

        public async Task<IEnumerable<InterviewScheduleDto>> GetInterviewsForProviderAsync(Guid systemUserId)
        {
            var provider = await repository.GetProviderBySystemUserIdAsync(systemUserId);
            if (provider == null) return Enumerable.Empty<InterviewScheduleDto>();

            var list = await repository.GetByProviderIdAsync(provider.JobProviderId);
            return mapper.Map<IEnumerable<InterviewScheduleDto>>(list);
        }

        public async Task<InterviewScheduleDto?> GetInterviewByIdAsync(Guid systemUserId, Guid interviewId)
        {
            var provider = await repository.GetProviderBySystemUserIdAsync(systemUserId);
            if (provider == null) return null;

            var interview = await repository.GetByIdAsync(interviewId);
            if (interview == null || interview.JobProviderId != provider.JobProviderId) return null;

            return mapper.Map<InterviewScheduleDto>(interview);
        }

        public async Task<InterviewScheduleDto?> UpdateInterviewAsync(Guid systemUserId, Guid interviewId, UpdateInterviewScheduleDto dto)
        {
            var provider = await repository.GetProviderBySystemUserIdAsync(systemUserId);
            if (provider == null) return null;

            var interview = await repository.GetByIdAsync(interviewId);
            if (interview == null || interview.JobProviderId != provider.JobProviderId) return null;

            mapper.Map(dto, interview);
            await repository.UpdateInterviewAsync(interview);

            var reloaded = await repository.GetByIdAsync(interviewId);
            return mapper.Map<InterviewScheduleDto>(reloaded ?? interview);
        }

        public async Task<bool> CancelInterviewAsync(Guid systemUserId, Guid interviewId)
        {
            var provider = await repository.GetProviderBySystemUserIdAsync(systemUserId);
            if (provider == null) return false;

            var interview = await repository.GetByIdAsync(interviewId);
            if (interview == null || interview.JobProviderId != provider.JobProviderId) return false;

            interview.Status = 3; // Cancelled
            await repository.UpdateInterviewAsync(interview);
            return true;
        }
    }
}
