using Dhoojol.Application.Models.Users;
using Dhoojol.Domain.Entities.Users;
using Dhoojol.Infrastructure.EfCore.Repositories;
using Dhoojol.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dhoojol.Api.Controllers;

[ApiController]
[Route("users")]
public class UsersController : Controller
{
    private readonly IRepository<User> _userRepository;

    public UsersController(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateUserModel model)
    {
        bool userNameExists = await _userRepository.AsQueryable()
            .AnyAsync(e => e.UserName.ToLower() == model.UserName.ToLower());

        if (userNameExists)
        {
            return BadRequest(new { Message = $"The username {model.UserName} already exists." });
        }

        if (model.Password.Length < 3)
        {
            return BadRequest(new { Message = $"The password must have 3 characters minimum." });
        }

        var user = new User
        {
            UserName = model.UserName,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            BirthDate = model.BirthDate,
            Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
        };

        await _userRepository.CreateAsync(user);

        return Ok(new { Id = user.Id });
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
