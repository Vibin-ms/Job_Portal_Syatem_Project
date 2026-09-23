using Domain.Data;
using Domain.Models;
using Domain.Services.InterviewSchedule.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Services.InterviewSchedule
{
    public class InterviewScheduleRepository : IInterviewScheduleRepo
    {
        private readonly AppDbContext _context;

        public InterviewScheduleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Models.JobProvider?> GetProviderBySystemUserIdAsync(Guid systemUserId)
        {
            return await _context.JobProviders.FirstOrDefaultAsync(jp => jp.SystemUserId == systemUserId);
        }

        public async Task<Models.JobApplication?> GetApplicationWithJobPostAsync(Guid applicationId)
        {
            return await _context.JobApplications
                .Include(ja => ja.JobPost)
                .FirstOrDefaultAsync(ja => ja.JobApplicationId == applicationId);
        }

        public async Task UpdateApplicationStatusAsync(Guid applicationId, int status)
        {
            var app = await _context.JobApplications.FindAsync(applicationId);
            if (app != null)
            {
                app.ApplicationStatus = (Domain.Enum.ApplicationStatus)status;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Models.InterviewSchedule?> GetByIdAsync(Guid interviewId)
        {
            return await _context.InterviewSchedules
                .Include(i => i.JobApplication)
                    .ThenInclude(ja => ja.JobPost)
                .Include(i => i.JobApplication)
                    .ThenInclude(ja => ja.AppliedJob)
                        .ThenInclude(aj => aj.JobSeekerProfile)
                            .ThenInclude(jsp => jsp.JobSeeker)
                                .ThenInclude(js => js.SystemUser)
                .Include(i => i.JobProvider)
                .FirstOrDefaultAsync(i => i.InterviewScheduleId == interviewId);
        }

        public async Task<IEnumerable<Models.InterviewSchedule>> GetByProviderIdAsync(Guid providerId)
        {
            return await _context.InterviewSchedules
                .Include(i => i.JobApplication)
                    .ThenInclude(ja => ja.JobPost)
                .Include(i => i.JobApplication)
                    .ThenInclude(ja => ja.AppliedJob)
                        .ThenInclude(aj => aj.JobSeekerProfile)
                            .ThenInclude(jsp => jsp.JobSeeker)
                                .ThenInclude(js => js.SystemUser)
                .Where(i => i.JobProviderId == providerId)
                .OrderByDescending(i => i.ScheduledStartTime)
                .ToListAsync();
        }

        public async Task<Models.InterviewSchedule> CreateInterviewAsync(Models.InterviewSchedule interview)
        {
            await _context.InterviewSchedules.AddAsync(interview);
            await _context.SaveChangesAsync();
            return interview;
        }

        public async Task UpdateInterviewAsync(Models.InterviewSchedule interview)
        {
            _context.InterviewSchedules.Update(interview);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteInterviewAsync(Guid interviewId)
        {
            var item = await _context.InterviewSchedules.FindAsync(interviewId);
            if (item == null) return false;

            _context.InterviewSchedules.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
