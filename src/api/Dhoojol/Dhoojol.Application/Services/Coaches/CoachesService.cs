using Dhoojol.Application.Models.Coaches;
using Dhoojol.Domain.Entities.Coaches;
using Dhoojol.Domain.Entities.Users;
using Dhoojol.Infrastructure.EfCore.Repositories.Coaches;
using Microsoft.EntityFrameworkCore;

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
            var query = _coachRepository.AsQueryable().Select(e => new GetCoachModel
            {
                Id = e.Id,
                Grades = e.Grades,
                Description = e.Description,
                HourlyRate = e.HourlyRate,
                UserId = e.User.Id,
                UserName = e.User.UserName,
                Email = e.User.Email,
                FirstName = e.User.FirstName,
                LastName = e.User.LastName,
            });
            var coach = await query.FirstOrDefaultAsync(e => e.UserId == id);
            if(coach == null)
            {
                throw new Exception("Coach not found");
            }
            return coach;
        }

        public async Task CreateAsync(User user)
        {
            var coach = new Coach { User = user };
            await _coachRepository.CreateAsync(coach);
        }
        public async Task UpdateCoach(UpdateCoachModel model)
        {
            var coach = await _coachRepository.GetCoachByUserId(model.UserId);
            coach.Grades = model.Grades;
            coach.Description = model.Description;
            coach.HourlyRate = model.HourlyRate;
            await _coachRepository.UpdateAsync(coach);
        }
        public async Task DeleteCoachAsync(Guid id)
        {
            await _coachRepository.DeleteAsync(id);
            
        }

    }
}
