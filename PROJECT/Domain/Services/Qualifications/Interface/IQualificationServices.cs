using Domain.Services.Qualifications.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Qualifications.Interface
{
    public interface IQualificationServices
    {
        Task<QualificationDTO> AddQualificationAsync(QualificationDTO qualificationDTO);
        Task<IEnumerable< QualificationDTO>> GetAllQualificationsAsync();
        Task<QualificationDTO>UpdateQualificationAsync(Guid id, QualificationDTO qualificationDTO);
        Task<bool> deletequalificationAsync(Guid id);
    }
}
