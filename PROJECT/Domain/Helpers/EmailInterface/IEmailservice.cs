using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers.EmailInterface
{
 public  interface IEmailservice
    {
        Task SendRegistrationEmailAsync(
           string email,
           string firstName,
           string role);

        Task SendCompanyAcceptedEmailAsync(string email, string companyname);
        Task sendCompanyRejectedEmailAsync(string email, string companyname);
    }
}
