using Dhoojol.Api.Helpers;
using Dhoojol.Application.Models.Auth;
using Dhoojol.Application.Models.Users;
using Dhoojol.Application.Services.Auth;
using Dhoojol.Application.Services.Users;
using Dhoojol.Domain.Entities.Users;
using Dhoojol.Infrastructure.EfCore.Repositories.Base;
using Dhoojol.Infrastructure.EfCore.Repositories.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dhoojol.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<ServiceResponse<TokenResult>>> LoginAsync([FromBody] LoginModel model)
    {
        try
        {
            var token = await _authService.LoginAsync(model);
            return Ok(ServiceResponse.Success(token));
        }
        catch (Exception ex)
        {
            return BadRequest(ServiceResponse.Failed(ex.Message));
        }
    }
}
