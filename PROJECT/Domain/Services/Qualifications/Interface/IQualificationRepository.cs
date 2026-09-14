using Domain.Models;
using Domain.Services.Qualifications.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Qualifications.Interface
{
    public interface IQualificationRepository
    {
        Task<Qualification> AddQualificationAsync(Qualification qualification);
        Task<IEnumerable< Qualification>> GetAllQualificationsAsync();
        Task<Qualification> UpdateQualificationAsync(Guid id, Qualification qualification);
        Task<bool> deletequalificationAsync(Guid id);

    }
}
