using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Enums
{
    public enum RemoveResult
    {
        Success = 0,
        NotFoundForRemove = -1,
        DeleteIsNotValid = -2
    }
}
