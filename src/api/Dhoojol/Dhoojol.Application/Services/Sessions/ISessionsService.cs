using Dhoojol.Application.Models.Sessions;
using Dhoojol.Application.Models.Users;

namespace Dhoojol.Application.Services.Sessions
{
    public interface ISessionsService
    {
        Task<List<ListSessionModel>> GetAllAsync();
        Task<GetSessionModel> GetById(Guid id);
        Task<List<ListSessionModel>> GetByCoachUserId(Guid coachUserId);
        Task<List<GetParticipantModel>> GetParticipants(Guid id);
        Task JoinSession(Guid sessionId);
        Task<Guid> CreateSession(CreateSessionModel model);
        Task UpdateSession(UpdateSessionModel model);

        Task DeleteSession(Guid id);

        Task DeleteClientSessionParticipant(Guid id);
        Task DeleteSessionParticipant(Guid sessionId);
    }
}
