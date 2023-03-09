using Dhoojol.Api.Helpers;
using Dhoojol.Application.Models.Auth;
using Dhoojol.Application.Models.Clients;
using Dhoojol.Application.Models.Coaches;
using Dhoojol.Application.Models.Helpers;
using Dhoojol.Application.Models.Users;
using Dhoojol.Application.Services.Users;
using Dhoojol.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Dhoojol.Api.Controllers;

[ApiController]
[Route("users")]
public class UsersController : Controller
{
    private readonly IUsersService _userService;

    public UsersController( IUsersService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ListUserModel>>> GetAllAsync([FromQuery] ListUserQueryParameters queryParameters)
    {
        List<ListUserModel> mylist = await _userService.GetAllAsync(queryParameters);
        return Ok(mylist);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<GetUserModel>>> GetUserById([FromRoute] Guid id)
    {
        try
        {
            var user = await _userService.GetUserById(id);

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

    [HttpGet("details/{id}")]
    public async Task<ActionResult> GetUserDetails([FromRoute] Guid id)
    {
        try
        {
            var user = await _userService.GetUserById(id);
            if (user.UserType == "client")
            {
                var client = await _userService.GetClientDetails(id, user);
                var userDetails = new ServiceResponse<WrapperUserDetails<GetClientDetails>>
                {
                    Data = client
                };
                return Ok(userDetails);
            }
            if (user.UserType == "coach")
            {
                var coach = await _userService.GetCoachDetails(id, user);
                var coachDetails = new ServiceResponse<WrapperUserDetails<GetCoachDetails>>
                {
                    Data = coach
                };
                return Ok(coachDetails);
            }
            else { return BadRequest(); }
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

        [HttpGet("never-logged")]
    public async Task<IActionResult> GetNeverLoggedUsersAsync()
    {
        var users = await _userService.GetNeverLoggedAsync();

        return Ok(users);
    }
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<TokenResult>>> CreateAsync([FromBody] CreateUserModel model)
    {
        try
        {
            var token = await _userService.CreateAsync(model);
            return Ok(ServiceResponse.Success(token));
        }
        catch (Exception ex)
        {
            return BadRequest(ServiceResponse.Failed(ex.Message));
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        try
        {
            await _userService.DeleteAsync(id);
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
