using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enum
{
    public enum ApplicationStatus
    {
        Applied = 1,
        UnderReview = 2,
        ShortListed = 3,
        InterviewScheduled = 4,
        Rejected = 5,
        Hired = 6
    }
}
