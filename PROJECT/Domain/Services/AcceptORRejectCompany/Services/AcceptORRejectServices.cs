using Domain.Helpers.EmailInterface;
using Domain.Services.AcceptORRejectCompany.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.AcceptORRejectCompany.Services
{
    public class AcceptORRejectServices:IAccpetORRejectServices
    {
        private readonly IAcceptORRejectRepository acceptORRejectRepository;
        private readonly IEmailservice emailservice;

        public AcceptORRejectServices(IAcceptORRejectRepository _acceptORRejectRepository, IEmailservice _emailservice)
        {
            acceptORRejectRepository= _acceptORRejectRepository;
            emailservice= _emailservice;
        }

        public async Task<bool> AcceptCompanyAsync(Guid companyId)
        {
            var compay=await acceptORRejectRepository.AcceptCompanyAsync(companyId);
            if(compay == null)
            {
                return false;
            }

            await emailservice.SendCompanyAcceptedEmailAsync(compay.Email, compay.ComapnyName);
            return true;
        }

        public async Task<bool>RejectCompanyAsync(Guid companyId)
        {
            var company=await acceptORRejectRepository.RejectCompanyAsync(companyId);
            if(company == null)
            {
                return false;
            }
            await emailservice.sendCompanyRejectedEmailAsync(company.Email, company.ComapnyName);
            return true;
        }

    }
}
