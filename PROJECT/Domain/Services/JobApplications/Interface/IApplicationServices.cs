using Domain.Services.JobApplications.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.JobApplications.Interface
{
    public interface IApplicationServices
    {
        Task<IEnumerable<ApplicationDTO>> GetApplicationsByJobPostAsync(Guid jobId);
    }
}
