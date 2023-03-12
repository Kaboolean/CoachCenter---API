using Dhoojol.Application.Models.Coaches;
using Dhoojol.Application.Models.Sessions;
using Dhoojol.Application.Models.Users;
using Dhoojol.Domain.Entities.Coaches;
using Dhoojol.Infrastructure.EfCore.Repositories.Sessions;
using Microsoft.EntityFrameworkCore;

namespace Dhoojol.Application.Services.Sessions
{
    internal class SessionsService : ISessionsService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly ISessionParticipantRepository _sessionParticipantRepository;

        public SessionsService(ISessionRepository sessionRepository, ISessionParticipantRepository sessionParticipantRepository)
        {
            _sessionRepository = sessionRepository;
            _sessionParticipantRepository = sessionParticipantRepository;
        }

        public async Task<List<ListSessionModel>> GetAllAsync()
        {
            var query = _sessionRepository.AsQueryable().Select(e => new ListSessionModel
            {
                Id = e.Id,
                Name = e.Name,
                Date = e.Date,
                Location = e.Location,
                Duration = e.Duration,
                Description = e.Description,
                Tags = e.Tags,
                ParticipantCount = e.Participants.Count(),
                CoachId = e.Coach.Id,
                CoachFirstName = e.Coach.User.FirstName,
                CoachLastName = e.Coach.User.LastName,

            });
            var sessions = await query.ToListAsync();
            return sessions;
        }
        public async Task<GetSessionModel> GetById(Guid id)
        {
            var query = _sessionRepository.AsQueryable().Select(e => new GetSessionModel
            {
                Id = e.Id,
                Name = e.Name,
                Date = e.Date,
                Location = e.Location,
                Duration = e.Duration,
                Description = e.Description,
                Tags = e.Tags,
                Coach = new GetCoachModel
                {
                    UserId = e.Coach.User.Id,
                    UserName = e.Coach.User.UserName,
                    Email = e.Coach.User.Email,
                    FirstName = e.Coach.User.FirstName,
                    LastName = e.Coach.User.LastName,
                    Grades = e.Coach.Grades,
                    Description = e.Coach.Description,
                    HourlyRate = e.Coach.HourlyRate
                }
            }) ;
            var session = await query.FirstOrDefaultAsync(e => e.Id == id);
            if (session == null)
            {
                throw new Exception("session not found");
            }
            return session;
        }

        public async Task<List<GetParticipantModel>> GetParticipants(Guid id)
        {
            var query = _sessionRepository.AsQueryable().Where(e => e.Id == id).SelectMany(e => e.Participants).Select(e => new GetParticipantModel
            {
                Age = e.Client.Age,
                UserId = e.Client.User.Id,
                UserName = e.Client.User.UserName,
                FirstName = e.Client.User.FirstName,
                LastName = e.Client.User.LastName
            });
            var listParticipant = await query.ToListAsync();
            return listParticipant;
        }
    }
}
