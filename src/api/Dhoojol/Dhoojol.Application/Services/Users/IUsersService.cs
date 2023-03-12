using Dhoojol.Application.Models.Clients;
using Dhoojol.Application.Models.Coaches;
using Dhoojol.Application.Models.Helpers;
using Dhoojol.Application.Models.Users;
using Dhoojol.Domain.Entities.Users;

namespace Dhoojol.Application.Services.Users
{
    public interface IUsersService
    {
        Task<GetUserModel> GetUserById(Guid id);
        Task<User> GetUserByUserName(string userName);
        Task<GetClientModel> GetClientByUserId(Guid id);
        Task<GetCoachModel> GetCoachByUserId(Guid id);
        Task<List<ListUserModel>> GetAllAsync(ListUserQueryParameters queryParameters);
        Task<List<ListUserNeverLoggedModel>> GetNeverLoggedAsync();
        Task<Guid> CreateAsync(CreateUserModel model);
        Task UpdateUser(GetUserModel model);
        Task DeleteAsync(Guid id);
    }
}
