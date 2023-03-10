using Dhoojol.Application.Models.Auth;
using Dhoojol.Application.Services.Users;
using Dhoojol.Domain.Entities.Users;
using Dhoojol.Infrastructure.EfCore.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace Dhoojol.Application.Services.Auth
{
    internal class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUsersService _userService;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration, IUsersService userService)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _userService = userService;
        }

        public async Task<TokenResult> LoginAsync(LoginModel model)
        {
            
            var user = await _userRepository
            .AsQueryable()
            .FirstOrDefaultAsync(e => e.UserName.ToLower() == model.UserName.ToLower());

            if (user is null)
            {
                throw new Exception($"The username or the password is invalid");
            }

            if (!BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                throw new Exception($"The username or the password is invalid");

            }
            else
            {
                var User = await _userService.GetUserByUserName(model.UserName);
                var token = CreateToken(User);
                token.UserName = User.UserName;
                token.UserId = User.Id;
                token.UserType = User.UserType;

                _userRepository.UpdateLoginDate(User);
                return token;
            }
        }
        private TokenResult CreateToken(User user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName)
        };

            var appSettingsToken = _configuration.GetSection("AppSettings:Token").Value;
            if (appSettingsToken is null)
            {
                throw new Exception("AppSettings Token is null !");
            }
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettingsToken));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddSeconds(3600),
                SigningCredentials = creds,
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return new TokenResult()
            {
                Token = tokenHandler.WriteToken(token),
                ExpiresIn = 3600,
            };
        }
    }
}
