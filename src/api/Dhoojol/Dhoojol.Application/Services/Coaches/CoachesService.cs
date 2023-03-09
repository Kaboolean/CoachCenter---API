using Dhoojol.Application.Models.Coaches;
using Dhoojol.Domain.Entities.Coaches;
using Dhoojol.Domain.Entities.Users;
using Dhoojol.Infrastructure.EfCore.Repositories.Coaches;

namespace Dhoojol.Application.Services.Coaches
{
    internal class CoachesService : ICoachesService
    {
        private readonly ICoachRepository _coachRepository;
        public CoachesService(ICoachRepository coachRepository)
        {
            _coachRepository = coachRepository;
        }

        public async Task<GetCoachModel> GetCoachByUserId(Guid id)
        {
            var query = await _coachRepository.GetCoachByUserId(id);
            var coach = new GetCoachModel
            {
                CoachId = query.Id,
                Grades =query.Grades,
                Description=query.Description,
                HourlyRate=query.HourlyRate
            };
            return coach;
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
