using Dhoojol.Application.Models.Clients;
using Dhoojol.Application.Models.Coaches;
using Dhoojol.Application.Models.Helpers;
using Dhoojol.Application.Models.Users;
using Dhoojol.Application.Services.Clients;
using Dhoojol.Application.Services.Coaches;
using Dhoojol.Domain.Entities.Users;
using Dhoojol.Domain.Enums;
using Dhoojol.Infrastructure.EfCore.Repositories.Users;
using Microsoft.EntityFrameworkCore;


namespace Dhoojol.Application.Services.Users
{
    internal class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;
        private readonly IClientsService _clientsService;
        private readonly ICoachesService _coachesService;
        public UsersService(IUserRepository userRepo, IClientsService clientsService, ICoachesService coachesService)
        {
            _userRepository = userRepo;
            _clientsService = clientsService;
            _coachesService = coachesService; 
        }

        public async Task<GetUserModel> GetUserById(Guid id)
        {

            var query = await _userRepository.GetAsync(id);
            var user = new GetUserModel
            {
                Id = query.Id,
                UserName = query.UserName,
                Email = query.Email,
                LastLoginDate = query.LastLoginDate,
                FirstName = query.FirstName,
                LastName = query.LastName,
                BirthDate = query.BirthDate,
                UserType = query.UserType,
            };
            return user;
        }

        public async Task<User> GetUserByUserName(string userName)
        {
                var user = await _userRepository.GetUserByUserName(userName);
                return user;
        }
        public async Task<GetClientModel> GetClientByUserId(Guid id)
        {
            var client = await _clientsService.GetClientByUserId(id);
            return client;
        }
        public async Task<GetCoachModel> GetCoachByUserId(Guid id)
        {
            var coach = await _coachesService.GetCoachByUserId(id);
            return coach;
        }

        public async Task<List<ListUserModel>> GetAllAsync(ListUserQueryParameters queryParameters)
        {
            var query = _userRepository.AsQueryable()
                .Select(e => new ListUserModel
                {
                    Id = e.Id,
                    UserName = e.UserName,
                    Email = e.Email,
                    LastLoginDate = e.LastLoginDate,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    BirthDate = e.BirthDate,
                    UserType = e.UserType,
                });
            if (!string.IsNullOrEmpty(queryParameters.UserName))
            {
                query = query.Where(e => e.UserName.StartsWith(queryParameters.UserName));
            }

            if (!string.IsNullOrEmpty(queryParameters.Search))
            {
                query = query.Where(e =>
                    e.UserName.Contains(queryParameters.Search) ||
                    e.FirstName.Contains(queryParameters.Search) ||
                    e.LastName.Contains(queryParameters.Search));
            }
            var users = await query.ToListAsync();

            return users;
        }
        public async Task<List<ListUserNeverLoggedModel>> GetNeverLoggedAsync()
        {
            var users = await _userRepository.GetNeverLoggedAsync();
            var results = users
            .Select(e => new ListUserNeverLoggedModel
            {
                Id = e.Id,
                UserName = e.UserName,
                Email = e.Email,
                LastLoginDate = e.LastLoginDate
            }).ToList();
            return results;
        }

        public async Task<Guid> CreateAsync(CreateUserModel model)
        {
            bool userNameExists = await _userRepository.AsQueryable()
                .AnyAsync(e => e.UserName.ToLower() == model.UserName.ToLower());

            if (userNameExists)
            {
                throw new Exception($"The username {model.UserName} already exists.");
            }

            if (model.Password.Length < 3)
            {
                throw new Exception($"The password must have 3 characters minimum.");
            }
            if (model.UserType.ToLower() == UserType.Coach || model.UserType.ToLower() == UserType.Client)
            {
                var user = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                    UserType = model.UserType,
                };

                await _userRepository.CreateAsync(user);
                if (model.UserType.ToLower() == UserType.Client)
                {
                    await _clientsService.CreateAsync(user);
                }
                if (model.UserType.ToLower() == UserType.Coach)
                {
                    await _coachesService.CreateAsync(user);
                }
                return user.Id;
            }
            else
            {
                throw new Exception($"Wrong user type.");
            }
        }
        public async Task UpdateUser(GetUserModel model)
        {
                var user = await _userRepository.GetAsync(model.Id);
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.LastLoginDate = model.LastLoginDate;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.BirthDate = model.BirthDate;
                user.UserType = model.UserType;
                await _userRepository.UpdateAsync(user);
        }
        public async Task DeleteAsync(Guid id)
        {
            var userType = await _userRepository.GetUserTypeById(id);
            if (userType == UserType.Client)
            {
                await _clientsService.DeleteClientAsync(id);
            }
            if (userType == UserType.Coach)
            {
                await _coachesService.DeleteCoachAsync(id);
            }
            await _userRepository.DeleteAsync(id);
        }
    }

}
