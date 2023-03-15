using Dhoojol.Api.Helpers;
using Dhoojol.Application.Models.Auth;
using Dhoojol.Application.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dhoojol.Api.Controllers;

[ApiController]
//attribut d'auth basé sur les rôles [Authorize(Roles = "admin,client, coaches")]
[Authorize]
[Route("auth")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<ApiResult<TokenResult>>> LoginAsync([FromBody] LoginModel model)
    {
        try
        {
            var token = await _authService.LoginAsync(model);
            return Ok(ApiResult.Success(token));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResult.Failed<TokenResult>(ex.Message));
        }
    }
}
