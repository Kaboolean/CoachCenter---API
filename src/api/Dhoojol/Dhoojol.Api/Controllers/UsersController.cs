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
    public async Task<ActionResult<ApiResult<GetUserModel>>> GetUserById([FromRoute] Guid id)
    {
        try
        {
            GetUserModel user = await _userService.GetUserById(id);
            return Ok(user);
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

    [HttpGet("never-logged")]
    public async Task<IActionResult> GetNeverLoggedUsersAsync()
    {
        var users = await _userService.GetNeverLoggedAsync();

        return Ok(users);
    }
    [HttpPost]
    public async Task<ActionResult<ApiResult<Guid>>> CreateAsync([FromBody] CreateUserModel model)
    {
        try
        {
            Guid id = await _userService.CreateAsync(model);
            return Ok(ApiResult.Success(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResult.Failed<Guid>(ex.Message));
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(GetUserModel model)
    {
        try
        {
            await _userService.UpdateUser(model);
            return Ok();
        }catch(Exception ex)
        {
            return BadRequest(ex.Message);
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
