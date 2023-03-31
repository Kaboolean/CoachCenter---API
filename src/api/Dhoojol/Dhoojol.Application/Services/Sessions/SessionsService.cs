using Dhoojol.Application.Models.Coaches;
using Dhoojol.Application.Models.Sessions;
using Dhoojol.Application.Models.Users;
using Dhoojol.Application.Services.Auth;
using Dhoojol.Application.Services.Clients;
using Dhoojol.Domain.Entities.Clients;
using Dhoojol.Domain.Entities.Coaches;
using Dhoojol.Domain.Entities.Sessions;
using Dhoojol.Infrastructure.EfCore.Repositories.Clients;
using Dhoojol.Infrastructure.EfCore.Repositories.Coaches;
using Dhoojol.Infrastructure.EfCore.Repositories.Sessions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Dhoojol.Application.Services.Sessions
{
    internal class SessionsService : ISessionsService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly ISessionParticipantRepository _sessionParticipantRepository;
        private readonly ICoachRepository _coachRepository;
        private readonly IClientsService _clientsService;
        private readonly IClientRepository _clientRepository;
        private readonly IAuthService _authService;

        public SessionsService(ISessionRepository sessionRepository, ISessionParticipantRepository sessionParticipantRepository, ICoachRepository coachRepository, IAuthService authService, IClientsService clientsService, IClientRepository clientRepository)
        {
            _sessionRepository = sessionRepository;
            _sessionParticipantRepository = sessionParticipantRepository;
            _coachRepository = coachRepository;
            _authService = authService;
            _clientsService = clientsService;
            _clientRepository = clientRepository;
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

        public async Task<List<ListSessionModel>> GetClientSessions()
        {
            Guid clientId = _authService.GetClientId();
            var query = _sessionParticipantRepository.AsQueryable().Where(e => e.ClientId == clientId).Select(e => e.SessionId);

            var sessionsIdList = await query.ToListAsync();

            List<ListSessionModel> sessions = new List<ListSessionModel>();
            foreach(var sessionId in sessionsIdList)
            {
                var querySession = _sessionRepository.AsQueryable().Where(e => e.Id == sessionId).Select(e => new ListSessionModel
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

                }).FirstOrDefault();
                if(querySession is null)
                {
                    break;
                }
                sessions.Add(querySession);  
            }
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
            });
            var session = await query.FirstOrDefaultAsync(e => e.Id == id);
            if (session == null)
            {
                throw new Exception("Session not found");
            }
            return session;
        }
        public async Task<List<ListSessionModel>> GetByCoachUserId(Guid coachUserId)
        {
            var query = _sessionRepository.AsQueryable().Where(e => e.Coach.User.Id == coachUserId).Select(e => new ListSessionModel
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
            var coachSessions = await query.ToListAsync();
            return coachSessions;
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
            //controle si query = null
            var listParticipant = await query.ToListAsync();
            return listParticipant;
        }

        public async Task JoinSession(Guid sessionId)
        {
            var userId = _authService.GetUserId();
            var clientId = _authService.GetClientId();

            bool alreadyJoined = _sessionParticipantRepository.AsQueryable().Where(e => e.SessionId == sessionId).Any(e => e.ClientId == clientId);
            if (alreadyJoined)
            {
                return;
            }
            Client client = await _clientRepository.GetAsync(clientId);
            Session session = await _sessionRepository.GetAsync(sessionId);
            SessionParticipant sessionParticipant = new SessionParticipant { Client = client, Session = session, ClientId = client.Id, SessionId = session.Id };
            await _sessionParticipantRepository.CreateAsync(sessionParticipant);
        }

        public async Task<Guid> CreateSession(CreateSessionModel model)
        {
            var userId = _authService.GetUserId();
            Coach coach = await _coachRepository.GetCoachByUserId(userId);

            var session = new Session
            {
                Name = model.Name,
                Date = model.Date,
                Location = model.Location,
                Duration = model.Duration,
                Description = model.Description,
                Tags = model.Tags,
                Coach = coach,
            };

            await _sessionRepository.CreateAsync(session);

            return session.Id;

        }

        public async Task UpdateSession(UpdateSessionModel model)
        {
            var userId = _authService.GetUserId();
            var session = await _sessionRepository.AsQueryable().Include(e => e.Coach.User).FirstOrDefaultAsync(e => e.Id == model.Id);
            if (session is null)
            {
                throw new Exception("Session not found");
            }
            if (session.Coach.User.Id != userId)
            {
                throw new Exception("Not authorized");
            }
            session.Name = model.Name;
            session.Date = model.Date;
            session.Location = model.Location;
            session.Duration = model.Duration;
            session.Description = model.Description;
            session.Tags = model.Tags;

            await _sessionRepository.UpdateAsync(session);
        }

        public async Task DeleteSession(Guid id)
        {
            await DeleteSessionParticipant(id);
            await _sessionRepository.DeleteAsync(id);
        }

        public async Task DeleteClientSessionParticipant(Guid clientId)
        {
            var queryList = await _sessionParticipantRepository.AsQueryable().Where(e => e.ClientId == clientId).ToListAsync();
            foreach (var elem in queryList)
            {
                await _sessionParticipantRepository.DeleteAsync(elem.Id);
            }
        }

        public async Task DeleteSessionParticipant(Guid sessionId)
        {
            var sessionParticipantList = await _sessionParticipantRepository.AsQueryable().Where(e => e.SessionId == sessionId).ToListAsync();
            foreach (var sessionParticipant in sessionParticipantList)
            {
                await _sessionParticipantRepository.DeleteAsync(sessionParticipant.Id);
            }
        }
    }
}
