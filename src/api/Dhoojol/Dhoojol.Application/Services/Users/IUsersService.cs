using Dhoojol.Application.Models.Clients;
using Dhoojol.Application.Models.Coaches;
using Dhoojol.Application.Models.Helpers;
using Dhoojol.Application.Models.Users;

namespace Dhoojol.Application.Services.Users
{
    public interface IUsersService
    {
        Task<GetUserModel> GetUserById(Guid id);
        Task<GetClientModel> GetClientByUserId(Guid id);
        Task<GetCoachModel> GetCoachByUserId(Guid id);
        Task<WrapperUserDetails<GetClientDetails>> GetClientDetails(Guid id, GetUserModel user);
        Task<WrapperUserDetails<GetCoachDetails>> GetCoachDetails(Guid id, GetUserModel user);
        Task<List<ListUserModel>> GetAllAsync(ListUserQueryParameters queryParameters);
        Task<List<ListUserNeverLoggedModel>> GetNeverLoggedAsync();
        Task<Guid> CreateAsync(CreateUserModel model);
        Task DeleteAsync(Guid id);
    }
}
