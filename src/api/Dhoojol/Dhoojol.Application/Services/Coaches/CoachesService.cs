using Dhoojol.Domain.Entities.Coaches;
using Dhoojol.Domain.Entities.Users;
using Dhoojol.Infrastructure.EfCore.Repositories.Coaches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhoojol.Application.Services.Coaches
{
    internal class CoachesService : ICoachesService
    {
        private readonly ICoachRepository _coachRepository;
        public CoachesService(ICoachRepository coachRepository)
        {
            _coachRepository = coachRepository;
        }

        public async Task CreateAsync(User user)
        {
            var coach = new Coach { User = user };
            await _coachRepository.CreateAsync(coach);
        }
        public async Task DeleteCoachAsync(Guid userId)
        {
            var coachId = await _coachRepository.GetCoachIdByUserId(userId);
            if (coachId != Guid.Empty)
            {
                await _coachRepository.DeleteAsync(coachId);
            }
            
        }
    }
}
