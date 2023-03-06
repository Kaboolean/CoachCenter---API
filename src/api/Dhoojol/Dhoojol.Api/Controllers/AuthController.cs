using Dhoojol.Api.Helpers;
using Dhoojol.Application.Models.Auth;
using Dhoojol.Application.Services.Auth;
using Microsoft.AspNetCore.Mvc;

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
