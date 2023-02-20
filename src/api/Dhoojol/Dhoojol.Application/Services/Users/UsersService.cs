using Dhoojol.Application.Models.Users;
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

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                //FirstName = model.FirstName,
                //LastName = model.LastName,
                //BirthDate = model.BirthDate,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
            };

            await _userRepository.CreateAsync(user);

            return user.Id;
        }
    }

}
