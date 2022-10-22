using Dhoojol.Application.Models.Auth;
using Dhoojol.Application.Models.Users;
using Dhoojol.Domain.Entities.Users;
using Dhoojol.Infrastructure.EfCore.Repositories.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dhoojol.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : Controller
{
    private readonly IRepository<User> _userRepository;

    public AuthController(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
    {
        var user = await _userRepository
            .AsQueryable()
            .FirstOrDefaultAsync(e => e.UserName.ToLower() == model.UserName.ToLower());

        if (user is null)
        {
            return BadRequest(new { Message = "The username or the password is invalid" });
        }

        if (!BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
        {
            return BadRequest(new { Message = "The username or the password is invalid" });
        }

        return Ok();
    }
}
