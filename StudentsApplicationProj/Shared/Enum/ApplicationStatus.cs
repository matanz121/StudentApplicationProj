using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsApplicationProj.Shared.Enum
{
    public enum ApplicationStatus
    {
        Created = 1,
        ApprovedByInstructor = 2,
        DeclinedByInstructor = 3,
        ApprovedByDeptHead = 4,
        DeclinedByDeptHead = 5,
        AppealedByStudent = 6,
        ApprovedByAll = 7
    }
}
