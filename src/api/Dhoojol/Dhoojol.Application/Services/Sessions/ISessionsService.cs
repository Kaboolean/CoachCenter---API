using Dhoojol.Application.Models.Sessions;
using Dhoojol.Application.Models.Users;

namespace Dhoojol.Application.Services.Sessions
{
    public interface ISessionsService
    {
        Task<List<ListSessionModel>> GetAllAsync();
        Task<GetSessionModel> GetById(Guid id);
        Task<List<GetParticipantModel>> GetParticipants(Guid id);
    }
}
