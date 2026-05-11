using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum TaskStatus
    {
        New = 1,
        InProgress = 2,
        Pending = 3,
        Completed = 4,
        Cancelled = 5
    }
}
