using Dhoojol.Api.Helpers;
using Dhoojol.Application.Models.Users;
using Dhoojol.Application.Services.Users;
using Dhoojol.Domain.Entities.Users;
using Dhoojol.Infrastructure.EfCore.Repositories.Base;
using Dhoojol.Infrastructure.EfCore.Repositories.Users;
using Dhoojol.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dhoojol.Api.Controllers;

[ApiController]
[Route("users")]
public class UsersController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IUsersService _userService;

    public UsersController(IUserRepository userRepository, IUsersService userService)
    {
        _userRepository = userRepository;
        _userService = userService;
    }
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<Guid>>> CreateAsync([FromBody] CreateUserModel model)
    {
        try
        {
            var userId = await _userService.CreateAsync(model);
            return Ok(ServiceResponse.Success(userId));
        }
        catch (Exception ex)
        {
            return BadRequest(ServiceResponse.Failed(ex.Message));
        }
        
    }

    [HttpGet("never-logged")]
    public async Task<IActionResult> GetNeverLoggedUsersAsync()
    {
        var users = await _userRepository.GetNeverLoggedAsync();

        var results = users
            .Select(e => new ListUserNeverLoggedModel
            {
                Id = e.Id,
                UserName = e.UserName,
                Email = e.Email,
                LastLoginDate = e.LastLoginDate
            });

        return Ok(results);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] ListUserQueryParameters queryParameters)
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

        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        try
        {
            var user = await _userRepository.GetAsync(id);

            var model = new GetUserModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                LastLoginDate = user.LastLoginDate,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
            };

            return Ok(model);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        try
        {
            await _userRepository.DeleteAsync(id);

            return Ok();
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}
