using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.JobApplications.Interface
{
    public interface IApplicationRepository
    {
        Task<IEnumerable<JobApplication>>GetApplicationsByJobPostAsync(Guid jobId);
    }
}
