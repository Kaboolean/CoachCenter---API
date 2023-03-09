﻿using Dhoojol.Application.Models.Auth;

namespace Dhoojol.Application.Services.Auth
{
    public interface IAuthService
    {
        Task<TokenResult> LoginAsync(LoginModel model);
    }
}
