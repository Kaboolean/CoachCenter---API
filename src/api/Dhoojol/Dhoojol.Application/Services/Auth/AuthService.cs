using Dhoojol.Application.Consts;
using Dhoojol.Application.Models.Auth;
using Dhoojol.Application.Services.Coaches;
using Dhoojol.Application.Services.Users;
using Dhoojol.Domain.Entities.Users;
using Dhoojol.Domain.Enums;
using Dhoojol.Infrastructure.EfCore.Repositories.Users;
using Microsoft.AspNetCore.Http;
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
        private readonly IConfiguration _configuration;
        //accèder au context de l'http et récupérer le token
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IUserRepository userRepository, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public Guid GetUserId()
        {
            var claimValue = GetClaimValue(ClaimTypes.NameIdentifier);
            return Guid.Parse(claimValue);
        }
        public Guid GetCoachId()
        {
            var claimValue = GetClaimValue(UserClaimsTypes.CoachId);
            return Guid.Parse(claimValue);
        }
        public Guid GetClientId()
        {

            var claimValue = GetClaimValue(UserClaimsTypes.ClientId);
            return Guid.Parse(claimValue);
        }
        public string GetUserType()
        {
            var claimValue = GetClaimValue(ClaimTypes.Role);
            return claimValue;
        }
        private string GetClaimValue(string claimType)
        {
            HttpContext context = _httpContextAccessor.HttpContext;
            if (context?.User == null)
            {
                throw new InvalidOperationException("User is not logged");
            }

            var claim = context.User.Claims.FirstOrDefault(c => c.Type == claimType);
            return claim.Value;
        }

        public bool IsAuthenticated()
        {
            HttpContext context = _httpContextAccessor.HttpContext;
            if (context?.User?.Identities?.First().IsAuthenticated == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<TokenResult> LoginAsync(LoginModel model)
        {

            var user = await _userRepository.GetUserByUserName(model.UserName);
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

                List<Claim> additionnalClaims = new List<Claim>();
                if (user.UserType == UserType.Client)
                {
                    additionnalClaims.Add(new Claim(UserClaimsTypes.ClientId, user.Client!.Id.ToString()));
                }
                if (user.UserType == UserType.Coach)
                {
                    additionnalClaims.Add(new Claim(UserClaimsTypes.CoachId, user.Coach!.Id.ToString()));
                }
                var token = CreateToken(user, additionnalClaims);
                token.UserName = user.UserName;
                token.UserId = user.Id;
                token.UserType = user.UserType;

                _userRepository.UpdateLoginDate(user);
                return token;
            }
        }
        private TokenResult CreateToken(User user, List<Claim> additionnalClaims)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, user.UserType)
            
        };
            //additional claims
            claims.AddRange(additionnalClaims);
            var appSettingsToken = _configuration.GetSection("Auth:Token").Value;
            if (appSettingsToken is null)
            {
                throw new Exception("AppSettings Token is null !");
            }
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettingsToken));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddSeconds(10800),
                SigningCredentials = creds,
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return new TokenResult()
            {
                Token = tokenHandler.WriteToken(token),
                ExpiresIn = 10800,
            };
        }
    }
}
