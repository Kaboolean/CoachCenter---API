using Dhoojol.Application.Models.Auth;
using Dhoojol.Application.Models.Users;
using Dhoojol.Application.Services.Auth;
using Dhoojol.Application.Services.Clients;
using Dhoojol.Application.Services.Coaches;
using Dhoojol.Domain.Entities.Users;
using Dhoojol.Infrastructure.EfCore.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<Guid> CreateAsync( CreateUserModel model)
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
            if (model.UserType.ToLower() == "coach" || model.UserType.ToLower() == "client")
            {
                var user = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                    UserType = model.UserType,
                };

                await _userRepository.CreateAsync(user);
                if(model.UserType.ToLower() == "client")
                {
                    await _clientsService.CreateAsync(user);
                }
                if (model.UserType.ToLower() == "coach")
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
        public async Task DeleteAsync(Guid id)
        {
            var userType = await _userRepository.GetUserTypeById(id);
            if(userType == "client")
            {
                await _clientsService.DeleteClientAsync(id);
            }
            if (userType == "coach")
            {
                await _coachesService.DeleteCoachAsync(id);
            }
                await _userRepository.DeleteAsync(id);
        }
    }

}
