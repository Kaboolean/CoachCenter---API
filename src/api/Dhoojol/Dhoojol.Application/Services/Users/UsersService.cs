using Dhoojol.Application.Models.Auth;
using Dhoojol.Application.Models.Users;
using Dhoojol.Application.Services.Auth;
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
        public UsersService(IUserRepository userRepo)
        {
            _userRepository = userRepo;
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
    }

}
