using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Authentication.JWT.Interface
{
    public  interface IJwtservice
    {
        string GenerateToken(
            Guid userId,
            string email,
            string role);
    }
}
