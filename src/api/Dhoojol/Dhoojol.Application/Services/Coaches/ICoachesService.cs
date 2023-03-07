using Dhoojol.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhoojol.Application.Services.Coaches
{
    public interface ICoachesService
    {
        Task CreateAsync(User user);
    }
}
