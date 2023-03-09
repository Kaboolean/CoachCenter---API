using Dhoojol.Application.Models.Coaches;
using Dhoojol.Domain.Entities.Users;

namespace Dhoojol.Application.Services.Coaches
{
    public interface ICoachesService
    {
        Task<GetCoachModel> GetCoachByUserId(Guid id);
        Task CreateAsync(User user);
        Task DeleteCoachAsync(Guid id);
    }
}
