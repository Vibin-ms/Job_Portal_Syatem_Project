using AutoMapper;
using Domain.Models;
using Domain.Services.Qualifications.DTO;
using Domain.Services.Qualifications.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Qualifications.Services
{
    public class QualificationServices:IQualificationServices
    {
        private readonly IQualificationRepository qualificationRepository;
        private readonly IMapper mapper;
        public QualificationServices(IQualificationRepository _qualificationRepository, IMapper _mapper)
        {
            qualificationRepository = _qualificationRepository;
            mapper = _mapper;
        }
        public async Task<QualificationDTO>AddQualificationAsync(QualificationDTO QualificationDTO)
        {
            var qualification=mapper.Map<Qualification>(QualificationDTO);
            var qualificationadded=await qualificationRepository.AddQualificationAsync(qualification);
            if (qualificationadded == null)
            {
                return null;
            }
            return mapper.Map<QualificationDTO>(qualificationadded);
        }
        public async Task<IEnumerable<QualificationDTO>> GetAllQualificationsAsync()
        {
            var response=await qualificationRepository.GetAllQualificationsAsync();
            return mapper.Map<IEnumerable<QualificationDTO>>(response);
        }

        public async Task<QualificationDTO>UpdateQualificationAsync(Guid id,QualificationDTO QualificationDTO)
        {
            var qualification = mapper.Map<Qualification>(QualificationDTO);
            var qualificationUpdated = await qualificationRepository.UpdateQualificationAsync(id,qualification);
            return mapper.Map<QualificationDTO>(qualificationUpdated);
        }
        public async Task<bool>deletequalificationAsync(Guid id)
        {
            var quali = await qualificationRepository.deletequalificationAsync(id);
            return quali;
        }
    }
}
