using Dhoojol.Application.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhoojol.Application.Services.Auth
{
    public interface IAuthService
    {
        Task<TokenResult> LoginAsync(LoginModel model);
    }
}
