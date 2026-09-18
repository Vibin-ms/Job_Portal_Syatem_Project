using AutoMapper;
using Domain.Services.JobPost.DTO;
using Domain.Services.JobPost.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.JobPost.Services
{
    public class JobPostServices:IJobPostServices
    {
        private readonly IJobPostRepository jobPostRepository;
        private readonly IMapper mapper;
        public JobPostServices(IJobPostRepository _jobPostRepository, IMapper _mapper)
        {
            jobPostRepository = _jobPostRepository;
            mapper = _mapper;
        }
        public async Task<IEnumerable<JobPostDTO>>GetAllJobAsync()
        {
            var jobs=await jobPostRepository.GetAllJobAsync();
            return mapper.Map<IEnumerable<JobPostDTO>>(jobs);
        }
        public async Task<bool>DeleteJobAsync(Guid id)
        {
            var jobs= await jobPostRepository.DeleteJobsAsync(id);
            return jobs;
        }
    }
}
