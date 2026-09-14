using Domain.Data;
using Domain.Models;
using Domain.Services.Qualifications.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Qualifications.Services
{
    public class QualificationRepository:IQualificationRepository
    {
        private readonly AppDbContext appDbContext;
        public QualificationRepository(AppDbContext _appDbContext)
        {
            appDbContext = _appDbContext;
        }
        public async Task<Qualification>AddQualificationAsync(Qualification Qualification)
        {
            var existingQualification = await appDbContext.Qualifications
            .FirstOrDefaultAsync(x =>
                x.Name.ToLower() == Qualification.Name.ToLower());

            if (existingQualification != null)
            {
                return null;
            }

            await appDbContext.Qualifications.AddAsync(Qualification);
            await appDbContext.SaveChangesAsync();
            return Qualification;
        }
        public async Task<IEnumerable<Qualification>> GetAllQualificationsAsync()
        {
            var qualifications=await appDbContext.Qualifications.ToListAsync();
            return qualifications;
        }
        public async Task<Qualification>UpdateQualificationAsync(Guid id, Qualification Qualification)
        {
            var existing=await appDbContext.Qualifications.FirstOrDefaultAsync(x=>x.QualificationId==id);

            if(existing!=null)
            {
                existing.Name = Qualification.Name;
                existing.Description = Qualification.Description;
                await appDbContext.SaveChangesAsync();
                return existing;
            }
            return null;
        }
        public async Task<bool>deletequalificationAsync(Guid id)
        {
            var quali=await appDbContext.Qualifications.FirstOrDefaultAsync(x=>x.QualificationId== id);
            if(quali==null)
            {
                return false;
            }
            appDbContext.Qualifications.Remove(quali);
            await appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
